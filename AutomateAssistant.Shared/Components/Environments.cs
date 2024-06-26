using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateAssistant.Shared.Components
{
    //存储动态常量
    public class Environments
    {
        public static string AppVersion = "1.0.0";
        public static string AppDescription = "A tool for automating repetitive tasks.";

        private static string _appDataPath;
        public static string AppDataPath
        {
            get
            {
                if (string.IsNullOrEmpty(Fields.AppDataPath))
                {
                    _appDataPath = Environment.ExpandEnvironmentVariables(Fields.AppDataPath);
                }
                if (!Directory.Exists(_appDataPath))
                {
                    Directory.CreateDirectory(_appDataPath);
                }
                return _appDataPath;
            }
        }

        private static string _logFilePath;
        public static string LogFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_logFilePath))
                {
                    _logFilePath = Path.Combine(AppDataPath, Fields.LogFileName);
                }
                return _logFilePath;
            }
        }
    }
}
