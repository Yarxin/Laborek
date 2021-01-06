using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAiLProject.Models
{
    internal class DiagonalDataModel : SimpleDataModel
    {
        public DiagonalDataModel()
        {
        }

        public List<int> DiagonalCellsLeft = new List<int>();

        public List<int> DiagonalCellsRight = new List<int>();

        public List<int> DiagonalLeftUpCells = new List<int>();

        public List<int> DiagonalLeftDownCells = new List<int>();

        public List<int> DiagonalRightUpCells = new List<int>();

        public List<int> DiagonalRightDownCells = new List<int>();
    }
}
