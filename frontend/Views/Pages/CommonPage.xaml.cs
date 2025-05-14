using frontend.Controls;
using frontend.ViewModels;

namespace frontend.Views.Pages;

/// <summary>
/// CommonPage.xaml 的交互逻辑
/// </summary>
public partial class CommonPage : INavigableView<CommonViewModel>
{
    public CommonViewModel ViewModel { get; }

    public CommonPage(CommonViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
