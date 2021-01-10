using AAiLProject.Service;
using AAiLProject.Service.IService;
using AAiLProject.Service.Logger;
using AAiLProject.Service.Logger.IServiceLogger;
using AAiLProject.ViewModels;
using AAiLProject.ViewModels.IViewModel;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAiLProject.Startup
{
    internal static class ConfigureIoCContainer
    {
        public static void Configure(SimpleContainer container)
        {
            container.Singleton<IWindowManager, WindowManager>();

            container.PerRequest<ILogger, Logger>();
            container.PerRequest<IMainViewModel, MainViewModel>();
            container.PerRequest<ILoadFiledService, LoadFileService>();
            container.PerRequest<IDataProcessorService, DataProcessorService>();
            container.PerRequest<IHydroDynamicStressService, HydroDynamicStressService>();
            container.PerRequest<ICellAndStressService, CellAndStressService>();
        }
    }
}
