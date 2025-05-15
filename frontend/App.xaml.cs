using frontend.Controls;
using frontend.ViewModels;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Threading;
using frontend.Controls.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Hosting; // 提供 UseSerilog()
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using frontend.Views.Pages;
using frontend.Views;
using frontend.Models;

namespace frontend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost host { get; private set; }

        public App()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            BuildSerilogConfig(builder);
            Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Build())
              .Enrich.FromLogContext()
              .CreateLogger();

            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(b => { b.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!); })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context, services);
                })
                .UseSerilog()
            .Build();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Configuration App Setting
            services.Configure<AppSettings>(context.Configuration);

            // App Host
            services.AddHostedService<ApplicationHostService>();

            // Page resolver service
            services.AddSingleton<IPageService, PageService>();

            // Main window with navigation
            services.AddSingleton<INavigationWindow, MainWindow>();
            services.AddSingleton<MainWindowViewModel>();


            // Views and ViewModels
            services.AddSingleton<DashboardPage>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<CommonPage>();
            services.AddSingleton<CommonViewModel>();
            services.AddSingleton<MemoPage>();
            services.AddSingleton<CalculatorPage>();
            services.AddSingleton<SettingPage>();
            services.AddSingleton<SettingViewModel>();
            services.AddSingleton<QuickLaunchPage>();
            services.AddSingleton<QuickLaunchViewModel>();

            // Business Services
            //services.AddTransient<IWorkService, ClientWorkService>();
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e) {
            // 启动主机
            //await host.StartAsync();

            // 获取主窗口并隐藏
            //var mainWindow = Application.Current.MainWindow;
            //if (mainWindow != null)
            //{
            //    mainWindow.Hide();
            //}

            // 创建并显示登录窗口
            var loginWindow = new LoginWindow(host);
            loginWindow.Show();
        }
        //protected void Application_Startup(object sender, StartupEventArgs e)
        //{
        //    var loginWindow = new LoginWindow();

        //    loginWindow.Show();
        //}

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            Log.Error("程序退出了");
            await Log.CloseAndFlushAsync();
            await host.StopAsync();
            host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
            Log.Fatal(e.Exception, "未处理异常");
        }

        private void BuildSerilogConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.serilog.json", optional: true, reloadOnChange: true);
        }
    }
}
