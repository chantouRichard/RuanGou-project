using frontend.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace frontend.Views.Pages
{
    public partial class FilePage : Page
    {
        public FilePage()
        {
            InitializeComponent();
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
