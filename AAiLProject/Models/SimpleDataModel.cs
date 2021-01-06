using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAiLProject.Service.Logger.IServiceLogger;

namespace AAiLProject.Models
{
    internal class SimpleDataModel
    {
        public SimpleDataModel()
        {

        }

        public List<int> HorizontalCells = new List<int>();

        public List<int> VerticalCells = new List<int>();

        public List<int> HorizontalLeftCells = new List<int>();

        public List<int> HorizontalRightCells = new List<int>();

        public List<int> VerticalUpCells = new List<int>();

        public List<int> VerticalDownCell = new List<int>();

        public List<double> Means = new List<double>();

        public List<double> StandardDeviations = new List<double>();

        public List<double> UpperConfidenceInterval = new List<double>();

        public List<double> LowerConfidenceInterval = new List<double>();
    }
}
