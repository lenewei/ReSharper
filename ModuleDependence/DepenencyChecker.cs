using JetBrains.ReSharper.Feature.Services.ContextActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.TextControl;
using JetBrains.Util;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.Metadata.Reader.API;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace ModuleDependence
{
    [ContextAction(Description = "Depenency Check", Group = "C#", Name = "DepenencyCheck", Priority = 1)]
    public class DepenencyChecker : ContextActionBase
    {

        private readonly ICSharpContextActionDataProvider _provider;

        public DepenencyChecker(ICSharpContextActionDataProvider provider)
        {
            _provider = provider;
        }

        public override string Text
        {
            get { return "DepenencyChecker"; }
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        protected override Action<ITextControl> ExecutePsiTransaction([NotNull] ISolution solution, [NotNull] IProgressIndicator progress)
        {
            //var factory = CSharpElementFactory.GetInstance(_provider.PsiModule);
            //using (var output = new StreamWriter("ReferencedAssemblies.csv"))
            //{

            //    foreach (var reference in _provider.Project.GetModuleReferences(TargetFrameworkId.Default))
            //    {
            //        output.WriteLine(reference.Name + "," + reference.OwnerModule);
            //    }
            //}
            var factory = CSharpElementFactory.GetInstance(_provider.PsiModule);
            var oldComment = _provider.TokenAfterCaret as ICommentNode;
            var newComment = factory.CreateComment("/* hello, world */");
            ModificationUtil.ReplaceChild(oldComment, newComment);
            return null;
        }
    }
}
