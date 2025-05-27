using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32; // 用于操作 Windows 注册表
using System.Reflection; // 用于获取当前应用程序的执行路径

namespace frontend.Views.Pages
{
    /// <summary>
    /// QuickLaunchPage.xaml 的交互逻辑
    /// </summary>
    public partial class QuickLaunchPage : Page
    {
        public QuickLaunchPage()
        {
            InitializeComponent();
            DataContext = App.host.Services.GetService<QuickLaunchViewModel>();
            this.Loaded += SettingsPage_Loaded; // 为页面加载事件添加处理程序
        }
        // **注册表操作的常量**
        // 这是 Windows 存储用户应用程序开机自启动信息的标准注册表路径。
        private const string RUN_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        // **非常重要**：请将 "MyDesktopAssistantApp" 替换为您的应用程序的唯一名称。
        // 这个名称会显示在 Windows 任务管理器的“启动”选项卡中。
        private const string APP_NAME = "MyDesktopAssistantApp";
        /// <summary>
        /// 当设置页面加载完成后触发的事件处理程序。
        /// 它用于初始化开机启动复选框的显示状态。
        /// </summary>
        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            // 根据应用程序当前的开机启动状态设置复选框的选中状态。
            StartupCheckBox.IsChecked = IsAppSetToRunOnStartup();
        }

        /// <summary>
        /// 检查应用程序是否已设置为开机自启动。
        /// </summary>
        /// <returns>如果应用程序已配置为开机启动，则返回 true；否则返回 false。</returns>
        public bool IsAppSetToRunOnStartup()
        {
            // 打开当前用户（HKEY_CURRENT_USER）的 Run 注册表项
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RUN_KEY))
            {
                if (key != null)
                {
                    // 尝试获取以 APP_NAME 为名称的注册表值
                    object value = key.GetValue(APP_NAME);
                    // 获取当前应用程序的完整执行路径
                    string currentAppPath = Assembly.GetExecutingAssembly().Location;

                    // 如果值存在且与当前应用程序的路径匹配（不区分大小写），则表示已设置开机启动。
                    if (value != null && value.ToString().Equals(currentAppPath, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 将应用程序设置为开机自启动。
        /// </summary>
        public void SetAppToRunOnStartup()
        {
            try
            {
                string appPath = Assembly.GetExecutingAssembly().Location;

                // 打开 Run 注册表项，并允许写入 (第二个参数设置为 true)
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true))
                {
                    if (key != null)
                    {
                        // 设置注册表值：键名为 APP_NAME，值为应用程序路径
                        key.SetValue(APP_NAME, appPath);
                        MessageBox.Show("应用程序已成功设置为开机自启。", "设置成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("无法访问注册表键。请确保您的应用程序有足够的权限。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // 捕获并显示可能发生的任何异常（例如权限问题）。
                MessageBox.Show($"设置开机自启失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 取消应用程序的开机自启动设置。
        /// </summary>
        public void DisableAppRunOnStartup()
        {
            try
            {
                // 打开 Run 注册表项，并允许写入 (true)
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true))
                {
                    if (key != null)
                    {
                        // 删除对应的注册表值。第二个参数 'false' 表示即使值不存在也不会抛出异常。
                        key.DeleteValue(APP_NAME, false);
                        MessageBox.Show("应用程序已成功取消开机自启。", "设置成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"取消开机自启失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 当开机启动复选框被选中时触发的事件处理程序。
        /// </summary>
        private void StartupCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetAppToRunOnStartup();
        }

        /// <summary>
        /// 当开机启动复选框被取消选中时触发的事件处理程序。
        /// </summary>
        private void StartupCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DisableAppRunOnStartup();
        }

    }
}
