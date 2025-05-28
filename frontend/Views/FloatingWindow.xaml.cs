using frontend.Models;
using frontend.Services;
using frontend.ViewModels;
using Microsoft.Win32;
using ModernWpf.Controls;

// 为 NHotkey 添加这个 using 语句
using NHotkey;
using NHotkey.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Forms = System.Windows.Forms;
using Win = System.Windows;
using WinControls = System.Windows.Controls;

namespace frontend.Views.Windows
{
    public partial class FloatingWindow : Window
    {
        private FloatingViewModel viewModel = new FloatingViewModel();
        private ApiService _apiService = new ApiService();
        public class ShortcutItem
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }

        private ObservableCollection<ShortcutItem> shortcuts = new ObservableCollection<ShortcutItem>();
        private readonly string shortcutDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shortcuts.json");
        private const int MaxShortcuts = 4;

        public FloatingWindow()
        {
            InitializeComponent();
            ShortcutsPanel.ItemsSource = shortcuts;

            LoadShortcuts(); // 启动时加载快捷方式

            // 使用 NHotkey 注册热键
            RegisterGlobalHotkey();

            this.DataContext = viewModel; //设置数据上下文为 ViewModel
            this.Loaded += Floating_Loaded; 

        }

        private async void Floating_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadTodos(); // 加载待办事项
        }

        private void RegisterGlobalHotkey()
        {
            try
            {
                // 注册 Ctrl + K 热键
                // "ShowHideFloatingWindow" 是热键的唯一标识符
                HotkeyManager.Current.AddOrReplace("ShowHideFloatingWindow", Key.K, ModifierKeys.Control, OnShowHideFloatingWindowHotkey);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
                // 如果热键已被其他应用程序注册，可能会发生此异常
                Win.MessageBox.Show("Ctrl + K 热键已被其他应用程序占用。请关闭冲突的应用程序或选择其他热键。", "热键冲突", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                Win.MessageBox.Show($"注册热键失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnShowHideFloatingWindowHotkey(object sender, HotkeyEventArgs e)
        {
            e.Handled = true; // 将事件标记为已处理，以防止进一步处理

            if (this.Visibility == Visibility.Visible)
            {
                this.Hide(); // 如果窗口可见，则隐藏
            }
            else
            {
                this.Show(); // 如果窗口隐藏，则显示
                this.Activate(); // 激活窗口使其获得焦点
                this.Topmost = true; // 置于最前
                this.Topmost = false; // 然后允许其他窗口覆盖它
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            // 窗口关闭时注销热键
            HotkeyManager.Current.Remove("ShowHideFloatingWindow");
            base.OnClosed(e);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            var result = Win.MessageBox.Show("确定要关闭计算机吗？", "确认关机", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Process.Start("shutdown", "/s /t 0");
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            var result = Win.MessageBox.Show("确定要重新启动计算机吗？", "确认重启", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Process.Start("shutdown", "/r /t 0");
            }
        }

        private void Sleep_Click(object sender, RoutedEventArgs e)
        {
            var result = Win.MessageBox.Show("确定要使计算机进入睡眠状态吗？", "确认睡眠", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Forms.Application.SetSuspendState(Forms.PowerState.Suspend, true, true);
            }
        }

        private void AddShortcut_Click(object sender, RoutedEventArgs e)
        {
            if (shortcuts.Count >= MaxShortcuts)
            {
                Win.MessageBox.Show("最多只能添加 4 个快捷方式。", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dlg = new OpenFileDialog
            {
                Title = "选择一个快捷方式目标",
                Filter = "可执行文件 (*.exe)|*.exe|所有文件|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                string path = dlg.FileName;
                string name = Path.GetFileNameWithoutExtension(path);

                if (!shortcuts.Any(s => s.Path == path))
                {
                    shortcuts.Add(new ShortcutItem { Name = name, Path = path });
                    SaveShortcuts(); // 添加后保存
                }
            }
        }

        private void ShortcutButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is WinControls.Button button && button.Tag is ShortcutItem item)
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = item.Path,
                        UseShellExecute = true // 启用 shell 执行，支持快捷方式等
                    };
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    Win.MessageBox.Show($"启动失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RemoveShortcut_Click(object sender, RoutedEventArgs e)
        {
            if (sender is WinControls.Button button && button.Tag is ShortcutItem item)
            {
                shortcuts.Remove(item);
                SaveShortcuts(); // 移除后保存
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    this.DragMove();
                }
                catch (InvalidOperationException)
                {
                    // 忽略拖动过程中出现的异常，比如在窗口关闭时调用
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"拖动失败: {ex.Message}");
                }
            }
        }


        private void SaveShortcuts()
        {
            try
            {
                var json = JsonSerializer.Serialize(shortcuts);
                File.WriteAllText(shortcutDataPath, json);
            }
            catch (Exception ex)
            {
                Win.MessageBox.Show($"保存快捷方式失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadShortcuts()
        {
            try
            {
                if (File.Exists(shortcutDataPath))
                {
                    var json = File.ReadAllText(shortcutDataPath);
                    var loaded = JsonSerializer.Deserialize<ObservableCollection<ShortcutItem>>(json);
                    if (loaded != null)
                    {
                        shortcuts.Clear();
                        foreach (var item in loaded)
                        {
                            if (File.Exists(item.Path)) // 避免加载无效路径
                                shortcuts.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Win.MessageBox.Show($"加载快捷方式失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async void LoadTodos_Click(object sender, RoutedEventArgs e)
        {
            await  viewModel.LoadTodos();
        }

        private async void TaskCompleted_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TodoItem task)
            {
                try
                {
                    task.IsCompleted = checkBox.IsChecked ?? false;
                    var result = await _apiService.UpdateTodo(task);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result.Message);
                        // 恢复原状态
                        checkBox.IsChecked = !task.IsCompleted;
                        task.IsCompleted = !task.IsCompleted;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"更新任务状态失败: {ex.Message}");
                    checkBox.IsChecked = !task.IsCompleted;
                    task.IsCompleted = !task.IsCompleted;
                }
            }
        }

        

        private void ShowErrorMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                var dialog = new ContentDialog
                {
                    Title = "错误",
                    Content = message,
                    CloseButtonText = "确定",
                    DefaultButton = ContentDialogButton.Close
                };
                dialog.ShowAsync();
            });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.Refresh(); // 刷新待办事项列表
        }
    }
}