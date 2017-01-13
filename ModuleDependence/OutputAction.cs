using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.UI.ActionsRevised;
using JetBrains.UI.ActionsRevised.Handlers;
using JetBrains.Util;

namespace ModuleDependence
{
    public class ModuleDependence_OutputAction : IExecutableAction
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            // return true or false to enable/disable this action
            return true;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            MessageBox.ShowInfo("Module Dependence", "About...");
        }
    }
}
