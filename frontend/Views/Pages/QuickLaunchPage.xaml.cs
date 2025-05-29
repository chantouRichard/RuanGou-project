using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Microsoft.Extensions.DependencyInjection;
using frontend.ViewModels;

namespace frontend.Views.Pages
{
    /// <summary>
    /// 快速启动页面 - 提供应用程序开机自启动设置功能
    /// </summary>
    public partial class QuickLaunchPage : Page
    {
        #region 常量定义

        /// <summary>
        /// Windows 注册表中存储开机启动应用程序的标准路径
        /// </summary>
        private const string REGISTRY_RUN_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// 应用程序在注册表中的唯一标识名称
        /// 注意：请根据实际应用程序名称进行修改，该名称将显示在任务管理器的启动选项卡中
        /// </summary>
        private const string APPLICATION_NAME = "MyDesktopAssistantApp";

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化快速启动页面
        /// </summary>
        public QuickLaunchPage()
        {
            InitializeComponent();
            DataContext = App.host.Services.GetService<QuickLaunchViewModel>();
            Loaded += OnPageLoaded;
        }

        #endregion

        #region 事件处理方法

        /// <summary>
        /// 页面加载完成后的事件处理程序
        /// 用于初始化开机启动复选框的状态
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                StartupCheckBox.Checked -= StartupCheckBox_Checked;
                StartupCheckBox.Unchecked -= StartupCheckBox_Unchecked;

                StartupCheckBox.IsChecked = IsStartupEnabled();

                StartupCheckBox.Checked += StartupCheckBox_Checked;
                StartupCheckBox.Unchecked += StartupCheckBox_Unchecked;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("初始化开机启动状态时发生错误", ex.Message);
            }
        }


        /// <summary>
        /// 开机启动复选框选中事件处理程序
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void StartupCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EnableStartup();
        }

        /// <summary>
        /// 开机启动复选框取消选中事件处理程序
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void StartupCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DisableStartup();
        }

        #endregion

        #region 开机启动功能方法

        /// <summary>
        /// 检查应用程序是否已配置为开机自启动
        /// </summary>
        /// <returns>如果已设置开机启动则返回 true，否则返回 false</returns>
        public bool IsStartupEnabled()
        {
            try
            {
                using (var registryKey = Registry.CurrentUser.OpenSubKey(REGISTRY_RUN_KEY, false))
                {
                    if (registryKey == null)
                        return false;

                    var registryValue = registryKey.GetValue(APPLICATION_NAME);
                    if (registryValue == null)
                        return false;

                    var currentAppPath = GetCurrentApplicationPath();
                    return string.Equals(registryValue.ToString(), currentAppPath, StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("检查开机启动状态时发生错误", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 启用应用程序开机自启动
        /// </summary>
        public void EnableStartup()
        {
            try
            {
                var applicationPath = GetCurrentApplicationPath();

                using (var registryKey = Registry.CurrentUser.OpenSubKey(REGISTRY_RUN_KEY, true))
                {
                    if (registryKey == null)
                    {
                        ShowErrorMessage("操作失败", "无法访问注册表，请确保应用程序具有足够的权限");
                        return;
                    }

                    registryKey.SetValue(APPLICATION_NAME, applicationPath);
                    ShowSuccessMessage("开机自启动已成功启用");
                }
            }
            catch (UnauthorizedAccessException)
            {
                ShowErrorMessage("权限不足", "设置开机启动需要管理员权限，请以管理员身份运行程序");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("启用开机启动失败", ex.Message);
            }
        }

        /// <summary>
        /// 禁用应用程序开机自启动
        /// </summary>
        public void DisableStartup()
        {
            try
            {
                using (var registryKey = Registry.CurrentUser.OpenSubKey(REGISTRY_RUN_KEY, true))
                {
                    if (registryKey == null)
                    {
                        ShowErrorMessage("操作失败", "无法访问注册表，请确保应用程序具有足够的权限");
                        return;
                    }

                    // 删除注册表项，false 参数表示如果项不存在也不抛出异常
                    registryKey.DeleteValue(APPLICATION_NAME, false);
                    ShowSuccessMessage("开机自启动已成功禁用");
                }
            }
            catch (UnauthorizedAccessException)
            {
                ShowErrorMessage("权限不足", "修改开机启动设置需要管理员权限，请以管理员身份运行程序");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("禁用开机启动失败", ex.Message);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取当前应用程序的完整执行路径
        /// </summary>
        /// <returns>应用程序的完整路径</returns>
        private static string GetCurrentApplicationPath()
        {
            return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        }


        /// <summary>
        /// 显示成功消息对话框
        /// </summary>
        /// <param name="message">要显示的消息内容</param>
        private static void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "操作成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 显示错误消息对话框
        /// </summary>
        /// <param name="title">错误标题</param>
        /// <param name="message">错误详细信息</param>
        private static void ShowErrorMessage(string title, string message)
        {
            MessageBox.Show($"{title}：{message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}