using System;
using System.Linq;

namespace AutomateAssistant.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class VMRegAttribute : Attribute
    {
        public LifecycleMode LifecycleMode { get; set; }

        public VMRegAttribute(LifecycleMode lifecycleMode)
        {
            LifecycleMode = lifecycleMode;
        }
    }

    public enum LifecycleMode
    {
        Singleton,
        PerRequest
    }
}
