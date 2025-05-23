using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace frontend
{
    public sealed class SkinManager : INotifyPropertyChanged
    {
        // Singleton 实例
        public static SkinManager Current { get; } = new SkinManager();

        // 皮肤属性（支持动态更新）
        private double Out_opacity = 0.6;
        private double In_opacity = 0.4;
        private ImageSource Background_pic = new BitmapImage(new Uri("pack://application:,,,/frontend;component/Assets/test.jpg"));

        public double OutOpacity
        {
            get => Out_opacity;
            set
            {
                Out_opacity = value;
                OnPropertyChanged();
            }
        }

        public double InOpacity
        {
            get => In_opacity;
            set
            {
                In_opacity = value;
                OnPropertyChanged();
            }
        }

        public ImageSource BackgroundPic
        {
            get => Background_pic;
            set
            {
                Background_pic = value;
                OnPropertyChanged();
            }
        }

        // 实现 INotifyPropertyChanged（用于动态绑定）
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
