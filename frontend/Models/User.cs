using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace frontend.Models
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set
            {
                _nickname = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string PasswordHash { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get => _avatarUrl;
            set
            {
                _avatarUrl = value;
                UpdateImageSource();
                OnPropertyChanged();
            }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            private set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateImageSource()
        {
            if (string.IsNullOrEmpty(_avatarUrl))
            {
                ImageSource = null;
                return;
            }

            try
            {
                // 判断是否是Base64字符串
                if (_avatarUrl.StartsWith("data:image"))
                {
                    ImageSource = Base64ToImageSource(_avatarUrl);
                }
                else // 普通文件路径
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(_avatarUrl, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    ImageSource = bitmap;
                }
            }
            catch
            {
                ImageSource = null;
            }
        }

        private ImageSource Base64ToImageSource(string base64String)
        {
            // 移除Base64前缀（如果有）
            var base64Data = base64String.Contains(",")
                ? base64String.Split(',')[1]
                : base64String;

            byte[] imageBytes = Convert.FromBase64String(base64Data);
            using (var ms = new MemoryStream(imageBytes))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }
    }
}