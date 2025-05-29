using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using frontend.Models;
using Microsoft.Win32;
using BCrypt.Net;
using System.IO;
using frontend.Services;

namespace frontend.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User _currentUser;
        private readonly ApiService _apiService;

        //全局字体设置
        private double baseFontSize = 14.0;
        public double BaseFontSize => baseFontSize;

        public int AddFontSize
        {
            get => SkinManager.Current.AddFontSize;
            set
            {
                if (SkinManager.Current.AddFontSize != value)
                {
                    SkinManager.Current.AddFontSize = value;
                    OnPropertyChanged(nameof(AddFontSize));
                    OnPropertyChanged(nameof(EffectiveFontSize)); // 通知更新
                }
            }
        }

        public double EffectiveFontSize => BaseFontSize + AddFontSize;

        //


        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        private string _newPassword;
        private string _confirmPassword;

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand ChangeAvatarCommand { get; }

        public UserViewModel()
        {
            // 初始化示例数据
            //CurrentUser = new User
            //{
            //    Id = 1,
            //    Username = "JohnDoe",
            //    Email = "john.doe@example.com",
            //    CreatedAt = DateTime.Now.AddMonths(-3),
            //    AvatarUrl = "pack://application:,,,/Assets/DefaultIcon.png"
            //};
            _apiService = new ApiService();

            CurrentUser = new User(); // 初始化空对象
            LoadUserInfoAsync(); // 异步加载数据

            SaveCommand = new UserRelayCommand(ExecuteSave);
            ChangeAvatarCommand = new UserRelayCommand(ExecuteChangeAvatar);
        }

        private async void LoadUserInfoAsync()
        {
            try
            {
                var result = await _apiService.getUserInfo();
                CurrentUser = result.Data;

                if(CurrentUser.AvatarUrl == null)
                {
                    CurrentUser.AvatarUrl = "pack://application:,,,/Assets/DefaultIcon.png";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载用户信息失败: {ex.Message}");
            }
        }

        public async Task<User> getUserInfo()
        {
            var result = await _apiService.getUserInfo();

            return result.Data;
        }

        private async void ExecuteSave(object parameter)
        {
            if(CurrentUser.Nickname == "" || CurrentUser.Email=="" || CurrentUser.PasswordHash == "")
            {
                MessageBox.Show("请输入全部字段", "Failed",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var result = await _apiService.updateUser(CurrentUser);

            if (result.Success)
            {
                MessageBox.Show("更新信息成功", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                MessageBox.Show("更新信息失败", "Failed",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //if (parameter is PasswordBox passwordBox && !string.IsNullOrEmpty(passwordBox.Password))
            //{
            //    CurrentUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordBox.Password);
            //    MessageBox.Show("Password updated successfully!", "Success",
            //        MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("User information updated!", "Success",
            //        MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }

        private void ExecuteChangeAvatar(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Select a profile picture"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // 读取文件为字节数组
                    byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);

                    // 获取文件扩展名
                    string extension = System.IO.Path.GetExtension(openFileDialog.FileName).ToLower();
                    string mimeType = extension switch
                    {
                        ".jpg" => "image/jpeg",
                        ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        _ => "image/jpeg"
                    };

                    // 转换为Base64字符串
                    string base64String = $"data:{mimeType};base64,{Convert.ToBase64String(imageBytes)}";

                    // 更新到模型
                    CurrentUser.AvatarUrl = base64String;

                    // 这里可以调用API将base64String保存到后端
                    // await SaveAvatarToBackend(base64String);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class UserRelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public UserRelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}