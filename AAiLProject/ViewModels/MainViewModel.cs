using AAiLProject.Models;
using AAiLProject.Service.IService;
using AAiLProject.Service.Logger.IServiceLogger;
using AAiLProject.ViewModels.IViewModel;
using Caliburn.Micro;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAiLProject.ViewModels
{
    internal class MainViewModel : Screen, IMainViewModel
    {
        private readonly ILoadFiledService loadFileService;
        private readonly IDataProcessorService dataProcessorService;
        private readonly IHydroDynamicStressService hydroDynamicStressService;

        private SimpleDataModel dataModel;
        private IList<DataPoint> meanPlot;
        private IList<DataPoint> upperConfidenceIntervalPlot;
        private IList<DataPoint> lowerConfidenceIntervalPlot;
        private IList<DataPoint> hydrodynamicStressPlot;

        private bool isDiagonal;

        private string title;
        private string xaxis;
        private string yaxis;

        private double cellNumber;
        private double flowSpeed;
        private double flushTime;
        private double dinamicViscosity;
        private double distance;
        private double radious;

        public MainViewModel(ILoadFiledService loadFileService,
                             IDataProcessorService dataProcessorService,
                             IHydroDynamicStressService hydroDynamicStressService)
        {
            this.loadFileService = loadFileService;
            this.dataProcessorService = dataProcessorService;
            this.hydroDynamicStressService = hydroDynamicStressService;

            this.cellNumber = 10.75;
            this.flowSpeed = 0.001111;
            this.flushTime = 2.5;
            this.dinamicViscosity = 0.001;
            this.distance = 0.25;
            this.radious = 45;
        }

        public bool IsDiagonal
        {
            get
            {
                return this.isDiagonal;
            }

            set
            {
                this.isDiagonal = value;
                this.NotifyOfPropertyChange(() => this.IsDiagonal);
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.NotifyOfPropertyChange(() => this.Title);
            }
        }

        public string Xaxis
        {
            get
            {
                return this.xaxis;
            }

            set
            {
                this.xaxis = value;
                this.NotifyOfPropertyChange(() => this.Xaxis);
            }
        }

        public string Yaxis
        {
            get
            {
                return this.yaxis;
            }

            set
            {
                this.yaxis = value;
                this.NotifyOfPropertyChange(() => this.Yaxis);
            }
        }

        public IList<DataPoint> MeanPlot
        {
            get
            {
                return this.meanPlot;
            }

            set
            {
                this.meanPlot = value;
                this.NotifyOfPropertyChange(() => this.MeanPlot);
            }
        }

        public IList<DataPoint> UpperConfidenceIntervalPlot
        {
            get
            {
                return this.upperConfidenceIntervalPlot;
            }

            set
            {
                this.upperConfidenceIntervalPlot = value;
                this.NotifyOfPropertyChange(() => this.UpperConfidenceIntervalPlot);
            }
        }

        public IList<DataPoint> LowerConfidenceIntervalPlot
        {
            get
            {
                return this.lowerConfidenceIntervalPlot;
            }

            set
            {
                this.lowerConfidenceIntervalPlot = value;
                this.NotifyOfPropertyChange(() => this.LowerConfidenceIntervalPlot);
            }
        }

        public IList<DataPoint> HydrodynamicStressPlot
        {
            get
            {
                return this.hydrodynamicStressPlot;
            }

            set
            {
                this.hydrodynamicStressPlot = value;
                this.NotifyOfPropertyChange(() => this.HydrodynamicStressPlot);
            }
        }

        public double CellNumber
        {
            get
            {
                return this.cellNumber;
            }

            set
            {
                this.cellNumber = value;
                this.NotifyOfPropertyChange(() => this.CellNumber);
            }
        }

        public double FlowSpeed
        {
            get
            {
                return this.flowSpeed;
            }

            set
            {
                this.flowSpeed = value;
                this.NotifyOfPropertyChange(() => this.FlowSpeed);
            }
        }

        public double FlushTime
        {
            get
            {
                return this.flushTime;
            }

            set
            {
                this.flushTime = value;
                this.NotifyOfPropertyChange(() => this.FlushTime);
            }
        }

        public double DinamicViscosity
        {
            get
            {
                return this.dinamicViscosity;
            }

            set
            {
                this.dinamicViscosity = value;
                this.NotifyOfPropertyChange(() => this.DinamicViscosity);
            }
        }

        public double Distance
        {
            get
            {
                return this.distance;
            }

            set
            {
                this.distance = value;
                this.NotifyOfPropertyChange(() => this.Distance);
            }
        }

        public void Close()
        {
            this.TryClose();
        }

        public void LoadFile()
        {
            SimpleDataModel simpledataModel = this.loadFileService.LoadFile();
            this.dataModel = this.dataProcessorService.GetMeansAndStd(simpledataModel);
            this.MeanStdPlot();
            this.GetHydrodynamicStressPlot();
        }

        public void MeanStdPlot()
        {
            this.MeanPlot = this.dataProcessorService.GetMeanDataPoints(this.dataModel);
            this.UpperConfidenceIntervalPlot = this.dataProcessorService.GetUpperConfidenceIntervalPlot(this.dataModel);
            this.LowerConfidenceIntervalPlot = this.dataProcessorService.GetLowerConfidenceIntervalPlot(this.dataModel);
        }

        public void GetHydrodynamicStressPlot()
        {
            this.HydrodynamicStressPlot = this.hydroDynamicStressService.GetHydroDynamicStressPoints(this.FlowSpeed,
                                                                                                     this.DinamicViscosity,
                                                                                                     this.radious,
                                                                                                     this.Distance);
        }
    }
}
