using AAiLProject.Models;
using AAiLProject.Service.IService;
using AAiLProject.Service.Logger.IServiceLogger;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAiLProject.Service
{
    internal class DataProcessorService : IDataProcessorService
    {
        private readonly ILogger logger;
        private readonly ILoadFiledService loadFiledService;
        public DataProcessorService(ILogger logger, ILoadFiledService loadFiledService)
        {
            this.logger = logger;
            this.loadFiledService = loadFiledService;
        }

        public SimpleDataModel GetMeansAndStd(SimpleDataModel dataModel, bool isDiagonal = false)
        {
            SimpleDataModel expandedDataModel = this.GetMeansAndStdInternal(dataModel, isDiagonal);
            return expandedDataModel;
        }

        public IList<DataPoint> GetMeanDataPoints(SimpleDataModel dataModel)
        {
            IList<DataPoint> dataPoints = this.GetMeanDataPointsInternal(dataModel);
            return dataPoints;
        }

        public IList<DataPoint> GetUpperConfidenceIntervalPlot(SimpleDataModel dataModel)
        {
            IList<DataPoint> dataPoints = this.GetLowerConfidenceIntervalPlotInternal(dataModel);
            return dataPoints;
        }

        public IList<DataPoint> GetLowerConfidenceIntervalPlot(SimpleDataModel dataModel)
        {
            IList<DataPoint> dataPoints = this.GetUpperConfidenceIntervalPlotInternal(dataModel);
            return dataPoints;
        }

        private IList<DataPoint> GetMeanDataPointsInternal(SimpleDataModel dataModel)
        {
            IList<DataPoint> dataPoints = new List<DataPoint>();
            List<double> means = dataModel.Means;
            means.Reverse();
            for (int i = 0; i < means.Count; i++)
            {
                dataPoints.Add(new DataPoint(i, means[i]));
            }

            return dataPoints;
        }

        private IList<DataPoint> GetUpperConfidenceIntervalPlotInternal(SimpleDataModel dataModel)
        {
            IList<DataPoint> dataPoints = new List<DataPoint>();
            List<double> upperConfidenceInterval = dataModel.UpperConfidenceInterval;
            upperConfidenceInterval.Reverse();
            for (int i = 0; i < upperConfidenceInterval.Count; i++)
            {
                dataPoints.Add(new DataPoint(i, upperConfidenceInterval[i]));
            }

            return dataPoints;
        }

        private IList<DataPoint> GetLowerConfidenceIntervalPlotInternal(SimpleDataModel dataModel)
        {
            IList<DataPoint> dataPoints = new List<DataPoint>();
            List<double> lowerConfidenceInterval = dataModel.LowerConfidenceInterval;
            lowerConfidenceInterval.Reverse();
            for (int i = 0; i < lowerConfidenceInterval.Count; i++)
            {
                dataPoints.Add(new DataPoint(i, lowerConfidenceInterval[i]));
            }

            return dataPoints;
        }

        private SimpleDataModel SplitDataInternal(SimpleDataModel dataModel)
        {
            this.SplitDataHorizontal(dataModel);
            this.SplitDataVertical(dataModel);

            return dataModel;
        }

        private SimpleDataModel SplitDataHorizontal(SimpleDataModel dataModel)
        {
            List<int> workList = new List<int>();
            int halfOfCollection = dataModel.HorizontalCells.Count / 2;

            for (int i = 0; i < dataModel.HorizontalCells.Count; i++)
            {
                if (i < halfOfCollection)
                {
                    dataModel.HorizontalLeftCells.Add(dataModel.HorizontalCells[i]);
                }

                else
                {
                    workList.Add(dataModel.HorizontalCells[i]);
                }
            }
            workList.Reverse();
            dataModel.HorizontalRightCells = workList;

            return dataModel;
        }

        private SimpleDataModel SplitDataVertical(SimpleDataModel dataModel)
        {
            List<int> workList = new List<int>();
            int halfOfCollection = dataModel.VerticalCells.Count / 2;

            for (int i = 0; i < dataModel.VerticalCells.Count; i++)
            {
                if (i < halfOfCollection)
                {
                    dataModel.VerticalUpCells.Add(dataModel.VerticalCells[i]);
                }

                else
                {
                    workList.Add(dataModel.VerticalCells[i]);
                }
            }
            workList.Reverse();
            dataModel.VerticalDownCell = workList;

            return dataModel;
        }

        private SimpleDataModel GetMeansAndStdInternal(SimpleDataModel dataModel, bool isDiagonal = false)
        {
            this.SplitDataInternal(dataModel);

            for (int i = 0; i < dataModel.HorizontalLeftCells.Count; i++)
            {
                double sumHorizontal = dataModel.HorizontalLeftCells[i] + dataModel.HorizontalRightCells[i];
                double sumVertical = dataModel.VerticalUpCells[i] + dataModel.VerticalDownCell[i];
                double sum = sumHorizontal + sumVertical;

                double mean = sum / 4;

                double a = dataModel.HorizontalLeftCells[i] - mean;
                double b = dataModel.HorizontalRightCells[i] - mean;
                double c = dataModel.VerticalUpCells[i] - mean;
                double d = dataModel.VerticalDownCell[i] - mean;

                double aPow2 = Math.Pow(a, 2);
                double bPow2 = Math.Pow(b, 2);
                double cPow2 = Math.Pow(c, 2);
                double dPow2 = Math.Pow(d, 2);

                double sumPow2 = aPow2 + bPow2 + cPow2 + dPow2;
                double variance = sumPow2 / 4;

                double std = Math.Sqrt(variance);

                double upperConfidenceInterval = mean + std;
                double lowerConfidenceInterval = mean - std;

                dataModel.Means.Add(mean);
                dataModel.StandardDeviations.Add(std);
                dataModel.UpperConfidenceInterval.Add(upperConfidenceInterval);
                dataModel.LowerConfidenceInterval.Add(lowerConfidenceInterval);
            }

            return dataModel;
        }
    }
}
