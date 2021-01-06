using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAiLProject.Service.Logger.IServiceLogger;

namespace AAiLProject.Service.Logger
{
    internal class Logger : ILogger
    {
        private StreamWriter sWriter;
        private readonly string path = @"Log.txt";
        public Logger()
        {
            if (!File.Exists(path))
            {
                this.sWriter = new StreamWriter("Log.txt");
                this.sWriter.WriteLine("Początek Loga programu Laborek");
                this.sWriter.Flush();
                this.sWriter.Close();
            }
        }

        public void LogEvent(string eventCode)
        {
            this.LogEventInternal(eventCode);
        }

        public void LogError(Exception exception, string errorCode)
        {
            this.LogErrorInternal(exception, errorCode);
        }

        private void LogEventInternal(string eventCode)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(eventCode);
            }
        }

        private void LogErrorInternal(Exception exception, string errorCode)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(exception);
                sw.WriteLine(errorCode);
            }
        }
    }
}
