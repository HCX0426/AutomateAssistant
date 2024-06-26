using AutomateAssistant.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateAssistant.UI.Contracts
{
    public interface IThemeManager
    {
         public static abstract void  UpdateTheme(AppTheme theme);
    }
}
