using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace frontend.Views
{
    /// <summary>
    /// FloatingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingWindow : Window
    {
        public FloatingWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (s, e) => DragMove();
        }

        private void TogglePin(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
            btnPin.Content = this.Topmost ? "📌" : "📍";
        }
    }
}
