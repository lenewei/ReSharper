using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daemon;
using JetBrains.Annotations;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.Application.Settings;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon.CSharp.ContextHighlighters;
using JetBrains.ReSharper.Feature.Services.CSharp.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.Navigation.CustomHighlighting;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Impl.CodeStyle;
using JetBrains.ReSharper.Psi.Tree;

[assembly: RegisterConfigurableSeverity(
   UsingHighlighing.SeverityId,
   null,
   HighlightingGroupIds.BestPractice,
   "Consider remove unnessary Quantum references",
   "Description here.",
   Severity.SUGGESTION)]


namespace Daemon
{
   [ZoneMarker]
   public class ZoneMarker : IRequire<ILanguageCSharpZone>, IRequire<DaemonEngineZone>
   {
   }

   [DaemonStage]
   public class CSharpDaemon : CSharpDaemonStageBase, IDaemonStageProcess
   {
      public IDaemonProcess DaemonProcess { get; set; }
      internal ICSharpFile CurrentCSharpFile { get; set; }
      internal IContextBoundSettingsStore SettingsStore { get; set; }

      protected override IDaemonStageProcess CreateProcess([NotNull] IDaemonProcess process, [NotNull] IContextBoundSettingsStore settings, DaemonProcessKind processKind, [NotNull] ICSharpFile file)
      {
         DaemonProcess = process;
         SettingsStore = settings;
         CurrentCSharpFile = file;
         return this;
      }
      public void Execute([NotNull] Action<DaemonStageResult> committer)
      {
         var elementProcessor = new ElementProcessor(this);
         CurrentCSharpFile.ProcessDescendantsForResolve(elementProcessor);
         committer(new DaemonStageResult(elementProcessor.Highlightings));
      }

   }

   public class ElementProcessor : IRecursiveElementProcessor
   {
      public IList<HighlightingInfo> Highlightings => Consumer.Highlightings;

      public ElementProcessor([NotNull] CSharpDaemon myDaemon)
      {
         Consumer = new FilteringHighlightingConsumer(myDaemon, myDaemon.SettingsStore, myDaemon.CurrentCSharpFile);
      }

      private FilteringHighlightingConsumer Consumer { get; }

      public bool InteriorShouldBeProcessed(ITreeNode element)
      {
         return false;
      }

      public void ProcessBeforeInterior(ITreeNode element)
      {
         var usingList = element as IUsingList;
         if (usingList == null) return;

         if (usingList.Imports.Any(_ => _.ImportedSymbolName.QualifiedName.Contains("Quantum")))
         {
            foreach (var imports in usingList.Imports.ToListWhere(
               _ => _.ImportedSymbolName.QualifiedName.Contains("Quantum")))
            {
               Consumer.AddHighlighting(new UsingHighlighing(imports));
            }
         }
      }

      public void ProcessAfterInterior(ITreeNode element)
      {
         //nothing
      }

      public bool ProcessingIsFinished { get; }
   }

   //[StaticSeverityHighlighting(Severity.ERROR, "My Daemon")
   [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
   public class UsingHighlighing : IHighlighting
   {
      public const string SeverityId = "ContainsQuantumReference";
      public IUsingDirective UsingDirective { get; set; }

      public UsingHighlighing(IUsingDirective usingUsingDirective)
      {
         UsingDirective = usingUsingDirective;
      }

      /// <summary>
      /// Returns true if data (PSI, text ranges) associated with highlighting is valid
      /// </summary>
      public bool IsValid() => UsingDirective == null || UsingDirective.IsValid();

      public DocumentRange CalculateRange() => UsingDirective.GetHighlightingRange();

      /// <summary>
      /// Message for this highlighting to show in tooltip and in status bar (if <see cref="P:JetBrains.ReSharper.Daemon.HighlightingAttributeBase.ShowToolTipInStatusBar"/> is <c>true</c>)
      ///             To override the default mechanism of tooltip, mark the implementation class with 
      ///             <see cref="T:JetBrains.ReSharper.Daemon.DaemonTooltipProviderAttribute"/> attribute, and then this property will not be called
      /// </summary>
      public string ToolTip => UsingDirective.GetText() + "Contains Quantum reference.";

      /// <summary>
      /// Message for this highlighting to show in tooltip and in status bar (if <see cref="P:JetBrains.ReSharper.Daemon.HighlightingAttributeBase.ShowToolTipInStatusBar"/> is <c>true</c>)
      /// </summary>
      public string ErrorStripeToolTip => ToolTip;

      /// <summary>
      /// Specifies the offset from the Range.StartOffset to set the cursor to when navigating 
      ///             to this highlighting. Usually returns <c>0</c>
      /// </summary>
      public int NavigationOffsetPatch => 0;
   }
}
