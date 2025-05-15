using System.Windows;
using System.Windows.Interop;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using frontend.Models;

namespace frontend.ViewModels
{
    public partial class OneClickViewModel : ObservableObject
    {
        private readonly OneClickModel _model = new();

        public IRelayCommand ShutdownCommand { get; }
        public IRelayCommand RestartCommand { get; }
        public IRelayCommand SleepCommand { get; }

        public OneClickViewModel()
        {
            ShutdownCommand = new RelayCommand(ExecuteShutdown);
            RestartCommand = new RelayCommand(ExecuteRestart);
            SleepCommand = new RelayCommand(ExecuteSleep);
        }

        private Window GetActiveWindow()
        {
            // 优先获取当前活动窗口，否则使用主窗口
            return Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
                ?? Application.Current.MainWindow;
        }

        private void ExecuteShutdown()
        {
            var activeWindow = GetActiveWindow();
            if (MessageBox.Show(activeWindow,
                    "确定要立即关闭计算机吗？",
                    "关机确认",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _model.ExecuteShutdown();
            }
        }

        private void ExecuteRestart()
        {
            var activeWindow = GetActiveWindow();
            if (MessageBox.Show(activeWindow,
                    "确定要立即重启计算机吗？",
                    "重启确认",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _model.ExecuteRestart();
            }
        }

        private void ExecuteSleep()
        {
            var activeWindow = GetActiveWindow();
            if (MessageBox.Show(activeWindow,
                    "确定要使计算机进入待机模式吗？",
                    "待机确认",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _model.ExecuteSleep(new WindowInteropHelper(activeWindow).Handle);
            }
        }
    }
}