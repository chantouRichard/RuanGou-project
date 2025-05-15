using System.Windows;
using System.Windows.Controls;
using frontend.ViewModels;

namespace frontend.Views.Pages
{
    public partial class OneClickPage : Page
    {
        public OneClickPage()
        {
            InitializeComponent();
            DataContext = new OneClickViewModel();
        }
    }
}