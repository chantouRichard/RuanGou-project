using frontend.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;

namespace frontend.Views.Pages
{
    public partial class FilePage : Page
    {
        private static string EverythingPath;

        public FilePage()
        {
            InitializeComponent();
            LoadEverythingPath();
            EnsureEverythingRunning();
        }
        private void AddEverythingPath_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SettingsManager.EverythingPath) || !File.Exists(SettingsManager.EverythingPath))
            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Everything 可执行文件|Everything.exe",
                    Title = "请选择 Everything.exe"
                };

                if (dialog.ShowDialog() == true)
                {
                    SettingsManager.EverythingPath = dialog.FileName;
                    SettingsManager.Save();
                    MessageBox.Show("Everything 路径已保存！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = SettingsManager.EverythingPath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show($"启动失败：{ex.Message}\n是否重新选择 Everything.exe？", "启动失败", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        SettingsManager.EverythingPath = null;
                        SettingsManager.Save();
                        AddEverythingPath_Click(sender, e); // 递归重新选择
                    }
                }
            }
        }

        private void EnsureEverythingRunning()
        {
            if (!IsEverythingRunning())
            {
                if (!string.IsNullOrEmpty(EverythingPath) && File.Exists(EverythingPath))
                {
                    try
                    {
                        Process.Start(EverythingPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"启动 Everything 失败: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("未找到 Everything 程序，请先添加路径。");
                }
            }
        }

        private bool IsEverythingRunning()
        {
            return Process.GetProcessesByName("Everything").Any();
        }

        private void SaveEverythingPath()
        {
            Properties.Settings.Default.EverythingPath = EverythingPath;
            Properties.Settings.Default.Save();
        }

        private void LoadEverythingPath()
        {
            EverythingPath = Properties.Settings.Default.EverythingPath;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchBox.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("请输入搜索关键字");
                return;
            }

            string extensionFilter = "*";
            if (ExtensionComboBox.SelectedItem is ComboBoxItem item)
            {
                extensionFilter = item.Tag.ToString();
            }


            // 拼接搜索字符串
            // 并加上扩展名过滤条件，比如 *.txt
            string searchQuery;
            searchQuery = keyword;
            

            if (extensionFilter != "*")
            {
                searchQuery += $" {extensionFilter}";
            }

            ResultsList.ItemsSource = null;

            try
            {
                List<string> results = await EverythingWrapper.SearchAsync(searchQuery);

                if (results.Count == 0)
                {
                    MessageBox.Show("未找到匹配的文件");
                }
                else
                {
                    ResultsList.ItemsSource = results;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜索时发生错误：" + ex.Message);
            }
        }

        // 移除 ResultsList_SelectionChanged 事件中的打开文件逻辑
            private void ResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                // 单击时只选中，不做任何操作
            }

            // 新增 ResultsList_MouseDoubleClick 事件处理方法
            private void ResultsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
                if (ResultsList.SelectedItem is string path)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("无法打开文件：" + ex.Message);
                    }
                }
            }

        private void SearchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                SearchButton_Click(sender, new RoutedEventArgs());
            }
        }

        private void Button_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                SearchButton_Click(sender, new RoutedEventArgs());
            }
        }

        private void ExtensionComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            switch (e.Key)
            {
                case Key.Up:
                    if (comboBox.SelectedIndex > 0)
                    {
                        comboBox.SelectedIndex--;
                    }
                    e.Handled = true;
                    break;

                case Key.Down:
                    if (comboBox.SelectedIndex < comboBox.Items.Count - 1)
                    {
                        comboBox.SelectedIndex++;
                    }
                    e.Handled = true;
                    break;

                case Key.Enter:
                    SearchButton_Click(sender, new RoutedEventArgs());
                    break;
            }
        }


        private string _rightClickedPath = null;

        private void ResultsList_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var originalElement = e.OriginalSource as DependencyObject;

            while (originalElement != null && !(originalElement is ListBoxItem))
            {
                originalElement = VisualTreeHelper.GetParent(originalElement);
            }

            if (originalElement is ListBoxItem item)
            {
                item.IsSelected = true; // 右键时也选中项
                _rightClickedPath = item.DataContext as string;
            }
        }

        private void CopyFilePath_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_rightClickedPath))
            {
                Clipboard.SetText(_rightClickedPath);
                MessageBox.Show("已复制路径：\n" + _rightClickedPath, "复制成功");
            }
            else
            {
                MessageBox.Show("未选中文件项");
            }
        }

        private void OpenFileLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_rightClickedPath))
            {
                try
                {
                    string folder = System.IO.Path.GetDirectoryName(_rightClickedPath);
                    if (!string.IsNullOrEmpty(folder))
                    {
                        Process.Start(new ProcessStartInfo("explorer.exe", $"/select,\"{_rightClickedPath}\"") { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开文件夹失败：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("未选中文件项");
            }
        }




    }
}
