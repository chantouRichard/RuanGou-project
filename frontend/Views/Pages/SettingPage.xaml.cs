using frontend.Controls;
using frontend.ViewModels;

namespace frontend.Views.Pages;

/// <summary>
/// SettingPage.xaml 的交互逻辑
/// </summary>
public partial class SettingPage : INavigableView<SettingViewModel>
{
    public SettingViewModel ViewModel { get; }

    public SettingPage(SettingViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
