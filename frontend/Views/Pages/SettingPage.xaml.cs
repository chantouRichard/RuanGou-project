using frontend.Controls;
using frontend.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;

namespace frontend.Views.Pages;

/// <summary>
/// SettingPage.xaml 的交互逻辑
/// </summary>
public partial class SettingPage : INavigableView<SettingViewModel>
{
    public SettingViewModel ViewModel { get; }

    public SettingPage(SettingViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    private void OnUploadImageClick(object sender, RoutedEventArgs e)
    {
        // 1. 打开文件选择对话框
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
            Title = "Select an image"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            // 2. 读取文件并转换为 ImageSource
            string filePath = openFileDialog.FileName;
            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // 立即加载，避免文件锁定
            bitmap.EndInit();

            // 3. 更新 SkinManager 的 BackgroundPic
            SkinManager.Current.BackgroundPic = bitmap;
        }
    }
}
