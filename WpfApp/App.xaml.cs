using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);

            //// 添加全局异常处理
            //AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            //{
            //    MessageBox.Show($"Unhandled Exception: {ex.ExceptionObject}");
            //};

            //DispatcherUnhandledException += (s, ex) =>
            //{
            //    MessageBox.Show($"Dispatcher Unhandled Exception: {ex.Exception.Message}");
            //    ex.Handled = true; // 防止程序崩溃
            //};

            //// 初始化主窗口
            //var mainWindow = new MainWindow();
            //mainWindow.Show();
        }
    }
}
