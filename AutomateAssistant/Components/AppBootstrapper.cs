using AutomateAssistant.Attributes;
using AutomateAssistant.Shared.Components;
using AutomateAssistant.Shared.Contracts;
using AutomateAssistant.UI.Components;
using AutomateAssistant.ViewModels;
using Caliburn.Micro;
using System.Reflection;
using System.Windows;

namespace AutomateAssistant.Components
{
    public class AppBootstrapper : BootstrapperBase
    {
        //容器可以更换，只是个IOC容器用来注册服务
        private readonly SimpleContainer _container = new SimpleContainer();

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            //实例化容器
            _container.Instance(_container);
            //注册服务
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<ILogger, Logger>();
            //_container.Singleton<IThemeManager, ThemeManager>();

            foreach (var assembly in SelectAssemblies())
            {
                foreach (var type in assembly.GetTypes()
                                            .Where(type => type.IsClass)
                                            .Where(type => type.Name.EndsWith("ViewModel") && !type.IsAbstract))
                {
                    //特性注册,管理View生命周期
                    var functionalityAttribute = type.GetCustomAttribute<VMRegAttribute>();
                    if (functionalityAttribute != null)
                    {
                        // 根据分类来处理注册逻辑  
                        switch (functionalityAttribute.LifecycleMode)
                        {
                            case LifecycleMode.Singleton:
                                _container.RegisterSingleton(type, type.ToString(), type);
                                break;
                             case LifecycleMode.PerRequest:
                                _container.RegisterPerRequest(type, type.ToString(), type);
                                break;
                            default:
                                throw new InvalidOperationException($"Unsupported functionality: {functionalityAttribute.LifecycleMode}");
                        }
                    }
                    else
                    {
                        _container.RegisterPerRequest(type, type.ToString(), type);
                    }
                }
            }
        }

        //配置开始界面
        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync(IoC.Get<ShellViewModel>().GetType());
        }

        //解析IOC容器里的某个实例，通过依赖注入容器来创建一个新实例（如果它还不存在）或返回已经存在的实例（如果它已经被解析过并被容器管理）
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        //获取IOC容器里的所有实例，从某个集合或存储中检索已存在的实例。在DI容器的上下文中，它可能意味着从容器中检索一个已经存在的实例
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        //ViewModel找对应视图
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
