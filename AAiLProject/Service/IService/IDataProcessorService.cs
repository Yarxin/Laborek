using AAiLProject.Models;
using OxyPlot;
using System.Collections.Generic;

namespace AAiLProject.Service.IService
{
    internal interface IDataProcessorService
    {
        SimpleDataModel GetMeansAndStd(SimpleDataModel dataModel, bool isDiagonal = false);

        IList<DataPoint> GetMeanDataPoints(SimpleDataModel dataModel);

        IList<DataPoint> GetUpperConfidenceIntervalPlot(SimpleDataModel dataModel);

        IList<DataPoint> GetLowerConfidenceIntervalPlot(SimpleDataModel dataModel);
    }
}