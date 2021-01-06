using AAiLProject.Service.Logger.IServiceLogger;
using AAiLProject.ViewModels.IViewModel;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AAiLProject.Startup
{
    class Bootstrapper : BootstrapperBase
    {
        private ILogger logBootstrapper = null;
        private static readonly SimpleContainer Container = new SimpleContainer();
        public Bootstrapper()
        {
            this.Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.logBootstrapper = Container.GetInstance<ILogger>();
            this.logBootstrapper.LogEvent("Program start!");
            DisplayRootViewFor<IMainViewModel>();
        }

        protected override void Configure()
        {
            ViewLocator.AddSubNamespaceMapping("AAiLProject.ViewModels", "AAiLProject.Views", "AAiLProject.Models");
            ConfigureIoCContainer.Configure(Container);
        }

        protected override object GetInstance(Type service, string key)
        {
            return Container.GetInstance(service, key);
        }
    }
}
