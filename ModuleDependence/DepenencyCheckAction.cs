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
    [ContextAction(Name = "DepenencyCheck", Description = "Depenency Check", Group = "C#")]
    public class DepenencyCheckAction : ContextActionBase
    {

        private readonly ICSharpContextActionDataProvider _provider;

        public DepenencyCheckAction(ICSharpContextActionDataProvider provider)
        {
            _provider = provider;
        }

        public override string Text
        {
            get { return "DepenencyCheck"; }
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        protected override Action<ITextControl> ExecutePsiTransaction([NotNull] ISolution solution, [NotNull] IProgressIndicator progress)
        {
            return null;
        }
    }
}
