using frontend.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

            bool exactMatch = ExactMatchCheckBox.IsChecked == true;

            // 拼接搜索字符串
            // 精确匹配则加引号包裹关键字
            // 并加上扩展名过滤条件，比如 *.txt
            string searchQuery;
            if (exactMatch)
            {
                searchQuery = $"\"{keyword}\"";
            }
            else
            {
                searchQuery = keyword;
            }

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

        private void ResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
}
