using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;


namespace ModuleDependence
{

    class SingleProjectProcessor : IRecursiveElementProcessor
    {

        #region Implementation of IRecursiveElementProcessor

        private readonly CSharpElementFactory _factory;

        public SingleProjectProcessor(CSharpElementFactory factory)
        {
            _factory = factory;
        }

        public bool InteriorShouldBeProcessed(ITreeNode element)
        {
            return true;
        }

        public void ProcessBeforeInterior(ITreeNode element)
        {
            var namespaceBody = element as INamespaceBody;
            if (namespaceBody != null)
            {
            }

            var classBody = element as IClassBody;
            if (classBody != null)
            {
            }
            else
            {
                var enumBody = element as IEnumBody;
                if (enumBody != null)
                {
                }
            }
        }

        public void ProcessAfterInterior(ITreeNode element)
        {
        }

        public bool ProcessingIsFinished { get; private set; }

        #endregion

    }

}
