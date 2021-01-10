using OxyPlot;
using System.Collections.Generic;

namespace AAiLProject.Service.IService
{
    internal interface ICellAndStressService
    {
        IList<DataPoint> GetCellAndStressPoints(IList<DataPoint> meanPlot, IList<DataPoint> hydrodynamicStressPlot);

        int GetIndexOfHalfValue(IList<DataPoint> meanPoints);
    }
}