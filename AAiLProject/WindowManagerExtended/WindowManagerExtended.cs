using AAiLProject.ViewModels.IViewModel;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAiLProject.WindowManagerExtended
{
    internal class WindowManagerExtended : WindowManager
    {
        private readonly SimpleContainer ioc_Container;

        public WindowManagerExtended(SimpleContainer ioc_Container)
        {
            this.ioc_Container = ioc_Container;
        }

        public bool? ShowDialog<T>(object context = null, IDictionary<string, object> settings = null) where T : IViewModel
        {
            var viewModel = this.ioc_Container.GetInstance<T>();
            return this.ShowDialog(viewModel, context, settings);
        }
    }
}
