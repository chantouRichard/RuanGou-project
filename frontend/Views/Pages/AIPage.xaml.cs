using frontend.Models;
using frontend.Services;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

// 使用别名解决命名冲突
using WpfBrushes = System.Windows.Media.Brushes;
using WpfColor = System.Windows.Media.Color;
using WpfPoint = System.Windows.Point;
using DrawingColor = System.Drawing.Color;
using DrawingPoint = System.Drawing.Point;
using WpfRectangle = System.Windows.Shapes.Rectangle;
using DrawingRectangle = System.Drawing.Rectangle;

namespace frontend.Views.Pages
{
    public partial class AIPage : Page
    {
        private readonly ITranslationService _translationService;
        private WpfPoint _startPoint;
        private WpfRectangle _selectionRect;

        public AIPage()
        {
            InitializeComponent();

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5166")
            };

            _translationService = new TranslationService(httpClient);
        }

        private async void TranslateText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SourceText.Text))
                {
                    MessageBox.Show("请输入要翻译的文本");
                    return;
                }

                var fromLang = (SourceLanguageComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "auto";
                var toLang = (TargetLanguageComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "en";

                TranslatedText.Text = "翻译中...";

                Debug.WriteLine($"Starting translation: {SourceText.Text} from {fromLang} to {toLang}");

                var request = new TextTranslationRequest
                {
                    Text = SourceText.Text,
                    From = fromLang,
                    To = toLang
                };

                var result = await _translationService.TranslateTextAsync(request);

                if (!string.IsNullOrEmpty(result.ErrorCode))
                {
                    MessageBox.Show($"翻译失败: {result.ErrorMsg}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    TranslatedText.Text = $"错误: {result.ErrorCode}";
                    return;
                }

                if (result?.Result?.TransResult?.Count > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var item in result.Result.TransResult)
                    {
                        sb.AppendLine(item.Dst);
                    }
                    TranslatedText.Text = sb.ToString();
                    Debug.WriteLine($"Translation successful: {TranslatedText.Text}");
                }
                else
                {
                    TranslatedText.Text = "无翻译结果";
                    Debug.WriteLine("Translation returned empty result");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Translation error: {ex}");
                MessageBox.Show($"翻译失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                TranslatedText.Text = "翻译失败";
            }
        }

        private void CaptureScreenshot_Click(object sender, RoutedEventArgs e)
        {
            var screenshotWindow = new Window
            {
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                Background = WpfBrushes.Transparent,
                AllowsTransparency = true,
                Topmost = true
            };

            var overlay = new Grid();
            overlay.MouseLeftButtonDown += Overlay_MouseLeftButtonDown;
            overlay.MouseMove += Overlay_MouseMove;
            overlay.MouseLeftButtonUp += Overlay_MouseLeftButtonUp;
            overlay.KeyDown += Overlay_KeyDown;
            overlay.Background = new SolidColorBrush(WpfColor.FromArgb(128, 0, 0, 0));
            overlay.Cursor = Cursors.Cross;

            screenshotWindow.Content = overlay;
            screenshotWindow.ShowDialog();
        }

        private void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition((IInputElement)sender);
            _selectionRect = new WpfRectangle
            {
                Stroke = WpfBrushes.Red,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(WpfColor.FromArgb(64, 255, 255, 255))
            };
            Panel.SetZIndex(_selectionRect, 1);
            Canvas.SetLeft(_selectionRect, _startPoint.X);
            Canvas.SetTop(_selectionRect, _startPoint.Y);

            var canvas = new Canvas();
            canvas.Children.Add(_selectionRect);
            ((Grid)sender).Children.Add(canvas);
        }

        private void Overlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectionRect == null) return;

            var currentPoint = e.GetPosition((IInputElement)sender);
            var width = currentPoint.X - _startPoint.X;
            var height = currentPoint.Y - _startPoint.Y;

            _selectionRect.Width = Math.Abs(width);
            _selectionRect.Height = Math.Abs(height);
            Canvas.SetLeft(_selectionRect, width > 0 ? _startPoint.X : currentPoint.X);
            Canvas.SetTop(_selectionRect, height > 0 ? _startPoint.Y : currentPoint.Y);
        }

        private void Overlay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_selectionRect == null) return;
            // 新增：获取DPI缩放比例
            var dpi = VisualTreeHelper.GetDpi(this);
            var dpiScaleX = dpi.DpiScaleX;
            var dpiScaleY = dpi.DpiScaleY;
            var currentPoint = e.GetPosition((IInputElement)sender);
            var x = (int)(Math.Min(_startPoint.X, currentPoint.X) * dpiScaleX);
            var y = (int)(Math.Min(_startPoint.Y, currentPoint.Y) * dpiScaleY);
            var width = (int)(Math.Abs(currentPoint.X - _startPoint.X) * dpiScaleX);
            var height = (int)(Math.Abs(currentPoint.Y - _startPoint.Y) * dpiScaleY);

            if (width > 0 && height > 0)
            {
                var bitmap = new Bitmap(width, height);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point(x, y),
                    new System.Drawing.Point(0, 0),
                    new System.Drawing.Size(width, height));
                }
                DisplayScreenshot(bitmap);
            }

            ((Grid)sender).Children.Clear();
            _selectionRect = null;
            ((Window)((Grid)sender).Parent).Close();
        }

        private void Overlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ((Grid)sender).Children.Clear();
                _selectionRect = null;
                ((Window)((Grid)sender).Parent).Close();
            }
        }

        private void DisplayScreenshot(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                ScreenshotImage.Source = bitmapImage;
            }
        }

        private void ClearScreenshot_Click(object sender, RoutedEventArgs e)
        {
            ScreenshotImage.Source = null;
            ImageTranslationResult.Text = string.Empty;
        }

        private async void TranslateImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ScreenshotImage.Source == null)
                {
                    MessageBox.Show("请先截图");
                    return;
                }

                var fromLang = (ImageSourceLang.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "auto";
                var toLang = (ImageTargetLang.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "en";

                ImageTranslationResult.Text = "翻译中...";

                Debug.WriteLine($"Starting image translation from {fromLang} to {toLang}");

                var bitmapSource = ScreenshotImage.Source as BitmapSource;
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    var bytes = stream.ToArray();

                    var request = new ImageTranslationRequest
                    {
                        ImageBytes = bytes,
                        From = fromLang,
                        To = toLang,
                        V = 3
                    };

                    var result = await _translationService.TranslateImageAsync(request);

                    if (!string.IsNullOrEmpty(result.ErrorCode))
                    {
                        MessageBox.Show($"翻译失败: {result.ErrorMsg}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        ImageTranslationResult.Text = $"错误: {result.ErrorCode}";
                        return;
                    }

                    if (result?.Data?.Content?.Count > 0)
                    {
                        var sb = new StringBuilder();
                        foreach (var content in result.Data.Content)
                        {
                            sb.AppendLine(content.Dst);
                        }

                        if (!string.IsNullOrEmpty(result.Data.SumDst))
                        {
                            sb.AppendLine();
                            sb.AppendLine("汇总翻译:");
                            sb.AppendLine(result.Data.SumDst);
                        }

                        ImageTranslationResult.Text = sb.ToString();
                        Debug.WriteLine($"Image translation successful: {ImageTranslationResult.Text}");
                    }
                    else
                    {
                        ImageTranslationResult.Text = "未识别到文本";
                        Debug.WriteLine("Image translation returned empty result");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Image translation error: {ex}");
                MessageBox.Show($"图片翻译失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                ImageTranslationResult.Text = "翻译失败";
            }
        }

        private void SourceLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 语言选择变化处理逻辑
        }

        private void TargetLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 语言选择变化处理逻辑
        }

        private void ImageSourceLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 图片源语言选择变化处理逻辑
        }

        private void ImageTargetLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 图片目标语言选择变化处理逻辑
        }
    }
}
