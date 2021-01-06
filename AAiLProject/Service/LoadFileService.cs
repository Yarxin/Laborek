using AAiLProject.Service.IService;
using ClosedXML.Excel;
using Microsoft.Win32;
using ExcelDataReader;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AAiLProject.Models;
using AAiLProject.Service.Logger.IServiceLogger;

namespace AAiLProject.Service
{
    internal class LoadFileService : ILoadFiledService
    {
        private readonly ILogger logger;
        public LoadFileService(ILogger logger)
        {
            this.logger = logger;
        }

        public SimpleDataModel LoadFile()
        {
            SimpleDataModel dataModel =  this.LoadFileInternal();
            return dataModel;
        }

        private SimpleDataModel LoadFileInternal()
        {
            this.logger.LogEvent("Przygotowuję dane");
            SimpleDataModel dataModel = new SimpleDataModel();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            openFileDialog.InitialDirectory = @"C:\Users\Msi\OneDrive\Pulpit\Projekty Uczelnia\AAiL\dane\temat 4";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    this.logger.LogEvent("Ładuję dane z: " + openFileDialog.FileName);
                    var file = new LinqToExcel.ExcelQueryFactory(openFileDialog.FileName);

                    this.logger.LogEvent("Ładuję plik Excel");
                    var query =
                        from row in file.Worksheet("Liczba komórek")
                        let item = new
                        {
                            sr_horizontal = row["Średnica Horizontal kom"].Cast<string>(),
                            sr_vertical = row["Średnica Vertical kom"].Cast<string>(),
                        }
                        select item;

                    this.logger.LogEvent("Ładowanie zakończone pomyślnie");
                    this.logger.LogEvent("Konwertuję dane");

                    int i = 1;
                    foreach (var item in query)
                    {        
                        dataModel.HorizontalCells.Add(int.Parse(item.sr_horizontal));
                        dataModel.VerticalCells.Add(int.Parse(item.sr_vertical));
                        this.logger.LogEvent("Przekonwertowano wiersz: " + i);
                        this.logger.LogEvent(" Horizontal: " + item.sr_horizontal);
                        this.logger.LogEvent(" Vertical: " + item.sr_vertical);
                        i += 1;
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "ERROR - ładowanie danych zakończone NIEPOWODZENIEM");
                }             
            }
            return dataModel;
        }
    }
}
