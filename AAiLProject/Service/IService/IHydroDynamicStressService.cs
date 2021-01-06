using OxyPlot;
using System.Collections.Generic;

namespace AAiLProject.Service.IService
{
    internal interface IHydroDynamicStressService
    {
        IList<DataPoint> GetHydroDynamicStressPoints(double flowSpeed, double dinamicViscosity, double radious, double distance);
    }
}