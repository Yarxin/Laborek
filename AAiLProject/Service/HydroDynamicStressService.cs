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
    internal class HydroDynamicStressService : IHydroDynamicStressService
    {
        private readonly ILogger logger;
        private int numberOfBins;

        public HydroDynamicStressService(ILogger logger)
        {
            this.logger = logger;
            this.numberOfBins = 80; // Value determined by microscope type
        }

        public IList<DataPoint> GetHydroDynamicStressPoints(double flowSpeed, double dinamicViscosity, double radious, double distance)
        {
            IList<DataPoint> dataPoints = this.GetHydroDynamicStressPointsInternal(flowSpeed, dinamicViscosity, radious, distance);
            return dataPoints;
        }

        private IList<DataPoint> GetHydroDynamicStressPointsInternal(double flowSpeed, double dinamicViscosity, double radious, double distance)
        {
            this.logger.LogEvent("Przygotowuję dane");
            this.logger.LogEvent("Szybkość przepływu: " + flowSpeed);
            this.logger.LogEvent("Lepkość dynamiczna: " + dinamicViscosity);
            this.logger.LogEvent("Promień w mm: " + radious);
            this.logger.LogEvent("Odległość: " + distance);
            IList<DataPoint> sigmaDataPoints = new List<DataPoint>();
            List<double> radiousList = this.GetRadiousListInternal(radious);
            double sigma_r = 0;
            double nominator = 3 * flowSpeed * dinamicViscosity;
            double denominator = 0;

            try
            {
                for (int i = 0; i < radiousList.Count; i++)
                {
                    denominator = Math.PI * radiousList[i] * Math.Pow(distance, 2);
                    sigma_r = nominator / denominator;
                    sigmaDataPoints.Add(new DataPoint(i, sigma_r));
                    this.logger.LogEvent("Numer próbka: " + i + "Naprężenie: " + sigma_r);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "ERROR - Błąd danych");
            }
            
            return sigmaDataPoints;
        }

        private List<double> GetRadiousListInternal(double radious)
        {
            List<double> radiousList = new List<double>();
            double elementarRadious = radious / this.numberOfBins;
            double nextRadious = 0;
            try
            {
                for (int i = 0; i < this.numberOfBins; i++)
                {
                    nextRadious += elementarRadious;
                    radiousList.Add(nextRadious);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "ERROR - Nieprawidłowy format wartości promienia");
            }
            
            return radiousList;
        }
    }
}
