using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace frontend.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        private double _outOpacity = 0.6;
        public double OutOpacity
        {
            get => _outOpacity;
            set
            {
                if (Math.Abs(_outOpacity - value) > 0.01)
                {
                    _outOpacity = value;
                    OnPropertyChanged();
                    SkinManager.Current.OutOpacity = _outOpacity;
                }
            }
        }

        private double _inOpacity = 0.4;
        public double InOpacity
        {
            get => _inOpacity;
            set
            {
                if (Math.Abs(_inOpacity - value) > 0.01)
                {
                    _inOpacity = value;
                    OnPropertyChanged();
                    SkinManager.Current.InOpacity = _inOpacity;
                }
            }
        }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                    ApplyThemeSettings(value);
                }
            }
        }

        private void ApplyThemeSettings(string theme)
        {
            switch (theme)
            {
                case "浅色主题":
                    OutOpacity = 0.6;
                    InOpacity = 0.4;
                    break;
                case "深色主题":
                    OutOpacity = 0.8;
                    InOpacity = 0.3;
                    break;
                case "高对比度":
                    OutOpacity = 0.9;
                    InOpacity = 0.2;
                    break;
                case "柔和模式":
                    OutOpacity = 0.5;
                    InOpacity = 0.5;
                    break;
                default:
                    break;
            }

            SkinManager.Current.InOpacity = InOpacity;
            SkinManager.Current.OutOpacity = OutOpacity;

            Console.WriteLine($"应用主题: {theme}，设置透明度为 Out={OutOpacity}，In={InOpacity}");
        }

        //字体设置
        private int baseFontSize = 16;
        private int addFontSize = 0;
        private string fontFamily = "Consolas";
        private string fontColor = "#000000";

        public int AddFontSize
        {
            get => addFontSize;
            set
            {
                if (addFontSize != value)
                {
                    addFontSize = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ActualFontSize));
                }
            }
        }

        public double ActualFontSize => baseFontSize + AddFontSize;

        public string FontFamily
        {
            get => fontFamily;
            set
            {
                if (fontFamily != value)
                {
                    if(value == "鸿雷行书简体")
                    {
                        fontFamily = "pack://application:,,,/Styles/Fonts/鸿雷行书简体";
                    }
                    fontFamily = value;
                    MessageBox.Show("当前字体已更改为" + fontFamily);
                    OnPropertyChanged(nameof(FontFamily));
                }
            }
        }

        private SolidColorBrush _fontBrush = new SolidColorBrush(Colors.Black);
        public SolidColorBrush FontBrush
        {
            get => _fontBrush;
            set
            {
                _fontBrush = value;
                FontColor = _fontBrush.Color.ToString(); // 自动设置 FontColor
                OnPropertyChanged(nameof(FontBrush));
            }
        }

        public string FontColor
        {
            get => fontColor;
            set
            {
                fontColor = value;
                OnPropertyChanged(nameof(FontColor));
            }
        }

        public ICommand SaveSettingsCommand { get; }

        public SettingViewModel()
        {
            SaveSettingsCommand = new RelayCommand(SaveSettings);
        }

        private void SaveSettings()
        {
            SkinManager.Current.FontColor = FontColor;
            SkinManager.Current.FontFamily = FontFamily;
            SkinManager.Current.AddFontSize = AddFontSize;

            MessageBox.Show("修改成功: " + FontColor + ", " + FontFamily + ", " + AddFontSize);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
