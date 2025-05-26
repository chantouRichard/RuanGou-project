// SettingPage.xaml.cs
using frontend.Controls;
using frontend.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;

namespace frontend.Views.Pages
{
    public partial class SettingPage : INavigableView<SettingViewModel>
    {
        public SettingViewModel ViewModel { get; }

        public SettingPage(SettingViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void OnUploadImageClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Select an image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                SkinManager.Current.BackgroundPic = bitmap;
            }
        }
    }
}