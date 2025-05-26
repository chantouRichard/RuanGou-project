using System.Windows;
using System.Windows.Controls;
using frontend.ViewModels;

namespace frontend.Views.Pages
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel();
        }
    }
}