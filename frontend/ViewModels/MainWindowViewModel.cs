using CommunityToolkit.Mvvm.ComponentModel;
using frontend.Controls;
using frontend.Models;
using frontend.Views.Pages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;

namespace frontend.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private bool _isInitialized = false;
    private readonly ILogger _logger;
    private AppSettings _appSettings;

    [ObservableProperty] string title;
    [ObservableProperty] ObservableCollection<object> navigationItems = new();
    [ObservableProperty] ObservableCollection<object> navigationFooter = new();


    public MainWindowViewModel(ILoggerFactory loggerFactory, IOptions<AppSettings> options)
    {
        _logger = loggerFactory.CreateLogger<MainWindowViewModel>();
        _appSettings = options.Value;
        if (!_isInitialized)
            InitializeData();
    }

    private void InitializeData()
    {
        NavigationItems = new ObservableCollection<object>
        {
            new NavigationViewItem()
            {
                Content = "主页",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "常用工具",
                Icon = new SymbolIcon { Symbol = SymbolRegular.WindowDevTools24 },
                TargetPageType = typeof(CommonPage),
                MenuItems = new NavigationViewItem[]
                {
                    new NavigationViewItem
                    {
                        Content = "智能待办备忘录",
                        Icon = new SymbolIcon { Symbol = SymbolRegular.NoteAdd20 },
                        TargetPageType = typeof(MemoPage)
                    },
                     new NavigationViewItem
                    {
                        Content = "快捷计算器",
                        Icon = new SymbolIcon { Symbol = SymbolRegular.Calculator20 },
                        TargetPageType = typeof(CalculatorPage)
                    }
                }

            },
            new NavigationViewItem()
            {
                Content = "系统工具",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DesktopArrowDown24},
                TargetPageType = typeof(SystemToolPage),
                MenuItems = new NavigationViewItem[]
                {
                    new NavigationViewItem
                    {
                        Content = "一键",
                        Icon = new SymbolIcon {Symbol = SymbolRegular.Power20},
                        TargetPageType = typeof(OneClickPage)
                    },
                    new NavigationViewItem
                    {
                        Content = "快捷启动",
                        Icon = new SymbolIcon {Symbol = SymbolRegular.Play20},
                        TargetPageType = typeof(QuickLaunchPage)
                    },
                    new NavigationViewItem
                    {
                        Content = "文件服务",
                        Icon = new SymbolIcon {Symbol = SymbolRegular.FolderProhibited24},
                        TargetPageType = typeof(FilePage)
                    }
                }
            },
            new NavigationViewItem()
            {
                Content = "AI服务",
                Icon = new SymbolIcon { Symbol = SymbolRegular.LightbulbFilament24 },
                TargetPageType = typeof(AIPage)
            },
            new NavigationViewItem()
            {
                Content = "信息服务",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Info24},
                TargetPageType = typeof(SystemToolPage),
                MenuItems = new NavigationViewItem[]
                {
                    new NavigationViewItem
                    {
                        Content = "天气/节假日",
                        Icon = new SymbolIcon {Symbol = SymbolRegular.CalendarLtr24},
                        TargetPageType = typeof(OneClickPage)
                    },
                    new NavigationViewItem
                    {
                        Content = "云端提醒功能",
                        Icon = new SymbolIcon {Symbol = SymbolRegular.ClockAlarm20},
                        TargetPageType = typeof(QuickLaunchPage)
                    }
                }
            }
         };

        NavigationFooter = new ObservableCollection<object>
        {
            new NavigationViewItem()
            {
                Content = "设置",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(SettingPage)
            }
        };

        Title = _appSettings.AppName;

        _isInitialized = true;
    }

}
