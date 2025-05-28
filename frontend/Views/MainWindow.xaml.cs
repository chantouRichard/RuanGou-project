using frontend.Controls;
using frontend.Controls.Contracts;
using frontend.ViewModels;
using frontend.Views;
using frontend.Views.Pages;
using frontend.Views.Windows;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Threading;

namespace frontend;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : INavigationWindow 
{
    public MainWindowViewModel ViewModel { get; }

    public FloatingWindow floatingWindow;

    public MainWindow(MainWindowViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        // 延迟创建和显示 FloatingWindow，避免阻塞 UI 初始化
        Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
        {
            floatingWindow = new FloatingWindow();
            floatingWindow.Show();

        }));

        // 注册关闭事件
        this.Closed += (s, e) =>
        {
            if (floatingWindow != null)
            {
                floatingWindow.Close();
            }
        };
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
    }

    #region INavigationWindow methods

    public INavigationView GetNavigation() => RootNavigation;

    public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

    public void SetPageService(IPageService pageService) =>
        RootNavigation.SetPageService(pageService);

    public void ShowWindow() => Show();

    //public void CloseWindow() => Close();
    public void CloseWindow()
    {
        Console.WriteLine("输出部分：：：");
        Console.WriteLine("floatingWindow:",floatingWindow==null?"不存在":"存在");
        floatingWindow.Close();
        floatingWindow = null;
        //Close();
    }

    #endregion INavigationWindow methods

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }
}
