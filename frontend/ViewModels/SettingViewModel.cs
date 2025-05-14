using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using frontend.Controls;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace frontend.ViewModels;

public partial class SettingViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private readonly ILogger _logger;

    [ObservableProperty]
    private string _selectedTheme = "浅色主题";

    [ObservableProperty]
    private string _selectedLanguage = "中文";

    public List<string> ThemeOptions { get; } = new() { "浅色主题", "深色主题" };
    public List<string> LanguageOptions { get; } = new() { "中文", "English" };

    public SettingViewModel(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SettingViewModel>();
    }

    private void InitializeData()
    {
        _logger.LogInformation("初始化设置页面数据");
        _isInitialized = true;
    }

    [RelayCommand]
    private void SaveSettings()
    {
        _logger.LogInformation($"保存设置 - 主题: {SelectedTheme}, 语言: {SelectedLanguage}");
        // 这里可以添加实际保存设置的代码
        // 例如: Properties.Settings.Default.Theme = SelectedTheme;
        // Properties.Settings.Default.Save();
    }

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeData();
    }

    public void OnNavigatedFrom()
    {
    }
}