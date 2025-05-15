using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using frontend.Controls;
using frontend.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace frontend.ViewModels
{
    public partial class QuickLaunchViewModel : ObservableObject, INavigationAware
    {
        private readonly ILogger _logger;
        private bool _isInitialized = false;
        private readonly string _dataFilePath;
        private readonly string _iconsDirectory;

        [ObservableProperty]
        private ObservableCollection<QuickLaunchModel> _shortcuts;

        [ObservableProperty]
        private QuickLaunchModel _selectedShortcut;

        [ObservableProperty]
        private string _newShortcutName;

        [ObservableProperty]
        private string _newShortcutPath;

        public QuickLaunchViewModel(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QuickLaunchViewModel>();
            Shortcuts = new ObservableCollection<QuickLaunchModel>();
            
            // 设置数据文件路径
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "RuanGou",
                "QuickLaunch"
            );
            
            // 确保目录存在
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            
            _dataFilePath = Path.Combine(appDataPath, "shortcuts.json");
            _iconsDirectory = Path.Combine(appDataPath, "Icons");
            
            // 确保图标目录存在
            if (!Directory.Exists(_iconsDirectory))
            {
                Directory.CreateDirectory(_iconsDirectory);
            }
        }

        private void InitializeData()
        {
            try
            {
                // 尝试从文件加载数据
                if (File.Exists(_dataFilePath))
                {
                    string jsonData = File.ReadAllText(_dataFilePath);
                    var loadedShortcuts = JsonSerializer.Deserialize<QuickLaunchModel[]>(jsonData);
                    
                    if (loadedShortcuts != null && loadedShortcuts.Length > 0)
                    {
                        foreach (var shortcut in loadedShortcuts)
                        {
                            // 确保图标路径有效
                            if (string.IsNullOrEmpty(shortcut.IconPath) || !File.Exists(shortcut.IconPath))
                            {
                                shortcut.IconPath = "/frontend;component/Assets/DefaultIcon.png";
                            }
                            
                            Shortcuts.Add(shortcut);
                        }
                        _logger.LogInformation($"从文件加载了 {loadedShortcuts.Length} 个快捷方式");
                    }
                }
                else
                {
                    // 如果文件不存在，添加一些默认快捷方式
                    Shortcuts.Add(new QuickLaunchModel("计算器", "calc.exe"));
                    Shortcuts.Add(new QuickLaunchModel("记事本", "notepad.exe"));
                    Shortcuts.Add(new QuickLaunchModel("画图", "mspaint.exe"));
                    _logger.LogInformation("添加了默认快捷方式");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载快捷方式数据时出错");
                MessageBox.Show($"加载快捷方式数据失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                
                // 添加默认快捷方式作为备用
                Shortcuts.Add(new QuickLaunchModel("计算器", "calc.exe"));
                Shortcuts.Add(new QuickLaunchModel("记事本", "notepad.exe"));
                Shortcuts.Add(new QuickLaunchModel("画图", "mspaint.exe"));
            }

            _isInitialized = true;
        }

        [RelayCommand]
        private void AddShortcut()
        {
            if (string.IsNullOrWhiteSpace(NewShortcutName) || string.IsNullOrWhiteSpace(NewShortcutPath))
            {
                MessageBox.Show("请输入快捷方式名称和路径！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(NewShortcutPath) && !Directory.Exists(NewShortcutPath))
            {
                MessageBox.Show("指定的路径不存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string iconPath = ExtractIconFromFile(NewShortcutPath);
            
            var newShortcut = new QuickLaunchModel(NewShortcutName, NewShortcutPath, iconPath);
            Shortcuts.Add(newShortcut);

            // 清空输入框
            NewShortcutName = string.Empty;
            NewShortcutPath = string.Empty;

            _logger.LogInformation($"添加了新的快捷方式: {newShortcut.Name}");
            
            // 保存数据
            SaveData();
        }

        private string ExtractIconFromFile(string filePath)
        {
            try
            {
                // 如果是目录，使用默认图标
                if (Directory.Exists(filePath))
                {
                    return "/frontend;component/Assets/DefaultIcon.png";
                }
                
                // 生成唯一的图标文件名
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string iconFileName = $"{fileName}_{DateTime.Now.Ticks}.png";
                string iconFilePath = Path.Combine(_iconsDirectory, iconFileName);
                
                // 如果图标文件已存在，直接返回
                if (File.Exists(iconFilePath))
                {
                    return iconFilePath;
                }
                
                // 提取图标
                using (Icon icon = Icon.ExtractAssociatedIcon(filePath))
                {
                    if (icon != null)
                    {
                        using (Bitmap bitmap = icon.ToBitmap())
                        {
                            bitmap.Save(iconFilePath, System.Drawing.Imaging.ImageFormat.Png);
                            return iconFilePath;
                        }
                    }
                }
                
                // 如果提取失败，使用默认图标
                return "/frontend;component/Assets/DefaultIcon.png";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"提取图标失败: {filePath}");
                return "/frontend;component/Assets/DefaultIcon.png";
            }
        }

        [RelayCommand]
        private void RemoveShortcut()
        {
            try
            {
                if (SelectedShortcut == null)
                {
                    MessageBox.Show("请先选择要删除的快捷方式！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string shortcutName = SelectedShortcut.Name;
                
                var result = MessageBox.Show($"确定要删除快捷方式 '{shortcutName}' 吗？", "确认删除", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var shortcutToRemove = SelectedShortcut;
                    SelectedShortcut = null; // 先将引用设为null，避免后续操作引用已删除对象
                    Shortcuts.Remove(shortcutToRemove);
                    _logger.LogInformation($"删除了快捷方式: {shortcutName}");
                    
                    // 保存数据
                    SaveData();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除快捷方式时出错");
                MessageBox.Show($"删除失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void LaunchShortcut(QuickLaunchModel shortcut)
        {
            if (shortcut == null) return;

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = shortcut.Path,
                    UseShellExecute = true
                });

                shortcut.UseCount++;
                _logger.LogInformation($"启动了快捷方式: {shortcut.Name}");
                
                // 保存数据以更新使用次数
                SaveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError(ex, $"启动快捷方式失败: {shortcut.Name}");
            }
        }

        [RelayCommand]
        private void BrowseFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "可执行文件 (*.exe)|*.exe|所有文件 (*.*)|*.*",
                Title = "选择程序"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                NewShortcutPath = openFileDialog.FileName;
                if (string.IsNullOrWhiteSpace(NewShortcutName))
                {
                    NewShortcutName = Path.GetFileNameWithoutExtension(NewShortcutPath);
                }
            }
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeData();
        }

        public void OnNavigatedFrom()
        {
            // 保存数据
            SaveData();
        }
        
        private void SaveData()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                
                string jsonData = JsonSerializer.Serialize(Shortcuts, options);
                File.WriteAllText(_dataFilePath, jsonData);
                _logger.LogInformation("快捷方式数据已保存");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存快捷方式数据时出错");
                MessageBox.Show($"保存快捷方式数据失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 