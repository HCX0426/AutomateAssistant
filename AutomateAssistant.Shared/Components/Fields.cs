using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateAssistant.Shared.Components
{
    //存储静态常量
    public static class Fields
    {
        public const string AppName = "AutomatedToolkit";
        
        public const string AppAuthor = "语衣";
        public const string AppCopyright = "Copyright © 2021 语衣";

        public const string ConfigPath = "Config";
        public const string AppDataPath = "%appdata%" + "\\" + AppName;

        public const string LogFileName = "log.txt";
    }
}
