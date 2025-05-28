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

        private int Add_fontSize = 20;
        private string Font_family = "consola";
        private string Font_color = "#000000";

        //新增属性
        private Stretch _backgroundStretch = Stretch.Uniform;
        private AlignmentX alignmentX = AlignmentX.Center;
        private AlignmentY alignmentY = AlignmentY.Center;

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

        public int AddFontSize
        {
            get => Add_fontSize;
            set
            {
                Add_fontSize = value;
                OnPropertyChanged();
            }
        }

        public string FontFamily
        {
            get => Font_family;
            set
            {
                Font_family = value;
                OnPropertyChanged();
            }
        }

        public string FontColor
        {
            get => Font_color;
            set
            {
                Font_color = value;
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

        public Stretch BackgroundStretch
        {
            get => _backgroundStretch;
            set
            {
                _backgroundStretch = value;
                OnPropertyChanged();
            }
        }   

        public AlignmentX BackgroundAlignmentX
        {
            get => alignmentX;
            set
            {
                alignmentX = value;
                OnPropertyChanged();
            }
        }

        public AlignmentY BackgroundAlignmentY
        {
            get => alignmentY;
            set
            {
                alignmentY = value;
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
