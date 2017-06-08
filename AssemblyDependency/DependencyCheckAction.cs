using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using JetBrains.Application.Progress;
using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.TextControl;
using JetBrains.Util;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.ReSharper.Features.Navigation.Features.FindDependentCode;
using JetBrains.ReSharper.Feature.Services.Occurrences;

namespace AssemblyDependency
{

   [ContextAction(Name = "DependencyCheck", Description = "Check all the dependencies", Group = "C#")]
   public class DependencyCheckAction : ContextActionBase
   {

      public DependencyCheckAction(ICSharpContextActionDataProvider provider)
      {
         _provider = provider;
      }

      public override bool IsAvailable(IUserDataHolder cache)
      {
         return true;
      }

      private readonly ICSharpContextActionDataProvider _provider;
      private Thread _observerThread;
      private DependencyResult _form;
      private List<string> _output;

      [STAThread]
      public void ObserverThread()
      {
         using (_form = new DependencyResult(_output))
         {
            _form.Text = _provider.SourceFile.DisplayName;
            _form.ShowDialog();
         }
      }

      protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
      {
         // Main logic
         var sb = new StringBuilder();
         var sbOccur = new StringBuilder();
         _output = new List<string>();

         var projects = solution.GetAllProjects();

         foreach (var project in projects)
         {
            sb.AppendLine(project.Name);
            //var assemblyReferences = project.GetAssemblyReferences(TargetFrameworkId.Default).Where(_ => _.Name.Contains("SunGard.AvantGard.Quantum") && !_.Name.Contains("SunGard.AvantGard.Quantum.External"));
            var assemblyReferences = project.GetProjectReferences(TargetFrameworkId.Default).Where(_ => _.Name.Contains("ES.Shared")).ToList();
            foreach (var reference in assemblyReferences)
            {
               sb.AppendLine($"    - {reference.Name}");
            }

            var moduleReference = assemblyReferences.Cast<IProjectToModuleReference>().ToList();

            if (moduleReference.Count == 0) continue;

            sbOccur.AppendLine(project.Name);
            var searchRequest = new SearchCodeDependentOnReferenceRequest(moduleReference);
            var occurrences = searchRequest.Search();
            var referenceOccurrences = occurrences.Cast<ReferenceOccurrence>().ToList();
            foreach (var sameReferenceGroup in referenceOccurrences.GroupBy(_ => _.Target.ToString()).OrderBy(_ => _.Key))
            {
               sbOccur.AppendLine($"    - {sameReferenceGroup.Key}");

               //not output details for now
               //foreach (var item in sameReferenceGroup)
               //{
               //   sbOccur.AppendLine(string.Format("    - {0}", item.DumpToString()));
               //}
            }
         }
         _output.Add(sb.ToString());         
         _output.Add(sbOccur.ToString());

         // Pass all the environment variables to the form and gather the main logic there
         _observerThread = new Thread(ObserverThread);
         _observerThread.Start();

         return null;
      }

      public override string Text => "DependencyCheck";
   }
}
