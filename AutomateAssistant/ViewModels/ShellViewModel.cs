using AutomateAssistant.Attributes;
using AutomateAssistant.Shared.Enums;
using AutomateAssistant.UI.Components;
using AutomateAssistant.ViewModels.Shell;
using AutomatedToolkit.UI.Components;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AutomateAssistant.ViewModels
{
    [VMReg(LifecycleMode.Singleton)]
    public class ShellViewModel : Conductor<IScreen>
    {
        #region Properties
        public int ThemeIndex = 0;

        private ObservableCollection<IScreen> _menuItems = new ObservableCollection<IScreen>();

        public ObservableCollection<IScreen> MenuItems { get => _menuItems; set => Set(ref _menuItems, value); }

        private int _selectedMainMenuIndex = 0;

        public int SelectedMainMenuIndex
        {
            get => _selectedMainMenuIndex;
            set
            {
                if(Set(ref _selectedMainMenuIndex, value) && MenuItems != null && value >= 0 && value < MenuItems.Count)
                {
                    OnMainMenuSelectionChanged();
                }
            }
        }
        #endregion Properties

        #region Ctor
        public ShellViewModel(IEnumerable<IScreen> menuItems)
        {
            MenuItems.Add(IoC.Get<HomeViewModel>());
            MenuItems.Add(IoC.Get<TasksViewModel>());
            MenuItems.Add(IoC.Get<MessagesViewModel>());
            MenuItems.Add(IoC.Get<DatabaseViewModel>());
            MenuItems.Add(IoC.Get<SettingsViewModel>());
        }
        #endregion Ctor

        #region Methods
        public async void OnMainMenuSelectionChanged()
        {
            if(SelectedMainMenuIndex >= 0)
            {
                await ActivateItemAsync(MenuItems[SelectedMainMenuIndex]);
            }
        }

        public void BtnSwitchTheme_Click()
        {
            if(ThemeIndex == 0)
            {
                ThemeManager.UpdateTheme(AppTheme.Dark);
                ThemeIndex = 1;
            } else
            {
                ThemeManager.UpdateTheme(AppTheme.Light);
                ThemeIndex = 0;
            }
        }

        public static void MinimizeWindow_Click()
        { Application.Current.MainWindow.WindowState = WindowState.Minimized; }

        public void CloseWimdow_Click() { TryCloseAsync(); }
        #endregion Methods
    }
}