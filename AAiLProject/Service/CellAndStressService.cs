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
    internal class CellAndStressService : ICellAndStressService
    {
        private readonly ILogger logger;

        public CellAndStressService(ILogger logger)
        {
            this.logger = logger;
        }

        public IList<DataPoint> GetCellAndStressPoints(IList<DataPoint> hydrodynamicStressPlot, IList<DataPoint> meanPlot)
        {
            IList<DataPoint> cellAndStressPoints = this.GetCellAndStressPointsInternal(hydrodynamicStressPlot, meanPlot);
            return cellAndStressPoints;
        }

        public int GetIndexOfHalfValue(IList<DataPoint> meanPoints)
        {
            int index = this.GetIndexOfHalfValueInternal(meanPoints);
            return index;
        }

        private IList<DataPoint> GetCellAndStressPointsInternal(IList<DataPoint> hydrodynamicStressPlot, IList<DataPoint> meanPlot)
        {
            IList<DataPoint> cellAndStressPoints = new List<DataPoint>();
            try
            {
                if (meanPlot.Count == hydrodynamicStressPlot.Count)
                {
                    for (int i = 0; i < meanPlot.Count; i++)
                    {
                        cellAndStressPoints.Add(new DataPoint(hydrodynamicStressPlot[i].Y, meanPlot[i].Y));

                        this.logger.LogEvent("Dodano punkty o współrzędnych: " + cellAndStressPoints[i].ToString());
                    }
                }

                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        cellAndStressPoints.Add(new DataPoint(i, 0));
                    }

                    this.logger.LogEvent("Wprowadzone dane nie są jednakowej długości.");
                }
            }
            catch (ArgumentNullException ex)
            {
                this.logger.LogError(ex, "Podane wartości nie mogą przyjmować wartości NULL");
            }
            
            return cellAndStressPoints;
        }

        private int GetIndexOfHalfValueInternal(IList<DataPoint> meanPoints)
        {
            int index = 0;
            double sum = 0;
            double halfSum = 0;

            foreach (var value in meanPoints)
            {
                sum += value.Y;
            }

            double referenceValue = sum / 2;
            for (int i = 0; i < meanPoints.Count; i++)
            {
                halfSum += meanPoints[i].Y;
                if (halfSum >= referenceValue)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}
