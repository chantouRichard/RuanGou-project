using frontend.Controls;
using frontend.ViewModels;

namespace frontend.Views.Pages;

/// <summary>
/// DashboardPage.xaml 的交互逻辑
/// </summary>
public partial class DashboardPage : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel { get; }

    public DashboardPage(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
