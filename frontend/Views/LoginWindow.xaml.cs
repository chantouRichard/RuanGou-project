using frontend.Services;
using System;
using System.Windows;
//using System.Windows.Forms;
using frontend.Views;
using frontend.ViewModels;
using Microsoft.Extensions.Hosting;

namespace frontend.Views
{
    public partial class LoginWindow : Window
    {
        private readonly ApiService _apiService = new ApiService();

        private readonly IHost host;

        public LoginWindow(IHost host)
        {
            this.host = host;
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = LoginUsername.Text;
            var password = LoginPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LoginMessage.Text = "请输入用户名和密码";
                return;
            }

            //var result = await _apiService.Login(username, password);

            //if (result.Success)
            //{
            //    // Navigate to home window
            //    var mainWindow = new MainWindow();
            //    mainWindow.Show();
            //    this.Close();
            //}
            //else
            //{
            //    LoginMessage.Text = result.Message;
            //}
            //var mainWindow = Application.Current.MainWindow;
            //mainWindow.Show();

            await host.StartAsync();
            this.Close();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = RegisterUsername.Text;
            var email = RegisterEmail.Text;
            var password = RegisterPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                RegisterMessage.Text = "请填好所有字段";
                return;
            }

            var result = await _apiService.Register(username, email, password);

            if (result.Success)
            {
                // Navigate to home window
                var mainWindow = Application.Current.MainWindow;
                mainWindow.Show();
                this.Close();
            }
            else
            {
                RegisterMessage.Text = result.Message;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // 获取当前窗口
            Window currentWindow = Window.GetWindow(this);

            // 关闭当前窗口
            currentWindow.Close();

            // 如果是主窗口，退出应用程序
            if (currentWindow == Application.Current.MainWindow)
            {
                Application.Current.Shutdown();
            }
        }
    }
}