using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using frontend.ViewModels;

namespace frontend.Views.Pages
{
    public partial class OneClickPage : Page
    {
        public OneClickPage()
        {
            InitializeComponent();
            DataContext = new OneClickViewModel();
        }
        private class ProcessInfo
        {
            public string DisplayName => $"{ProcessName} (PID: {ProcessId}) - {MemoryMB} MB";
            public string ProcessName { get; set; }
            public int ProcessId { get; set; }
            public long MemoryMB { get; set; }
            public Process Process { get; set; }
        }

        private static readonly string[] ProtectedProcesses = new[]
        {
            "explorer", "System", "Idle", "svchost", "csrss", "wininit", "winlogon", "services"
        };

        private void ShowProcessList_Click(object sender, RoutedEventArgs e)
        {
            LoadTopMemoryProcesses();
            ProcessExpander.IsExpanded = true;
        }

        private void LoadTopMemoryProcesses()
        {
            try
            {
                var processes = Process.GetProcesses()
                    .Where(p =>
                    {
                        try
                        {
                            return !ProtectedProcesses.Contains(p.ProcessName)
                                   && p.Responding;
                        }
                        catch
                        {
                            return false;
                        }
                    });

                var topList = processes
                    .OrderByDescending(p => p.WorkingSet64)
                    .Take(10)
                    .Select(p =>
                    {
                        long memoryMB = p.WorkingSet64 / (1024 * 1024);
                        return new ProcessInfo
                        {
                            ProcessName = p.ProcessName,
                            ProcessId = p.Id,
                            MemoryMB = memoryMB,
                            Process = p
                        };
                    })
                    .ToList();

                ProcessListBox.ItemsSource = topList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载进程信息失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void KillSelectedProcess_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessListBox.SelectedItem is ProcessInfo info)
            {
                try
                {
                    info.Process.Kill();
                    MessageBox.Show($"✅ 已关闭进程：{info.ProcessName} (PID: {info.ProcessId})", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTopMemoryProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"⚠️ 关闭进程失败：{ex.Message}", "失败", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个进程。", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void AutoKillHighMemoryProcesses_Click(object sender, RoutedEventArgs e)
        {
            const long memoryThresholdMB = 1000; // 内存阈值，单位 MB
            int killedCount = 0;
            var failedList = new List<string>();

            int currentProcessId = Process.GetCurrentProcess().Id;
            string currentProcessName = Process.GetCurrentProcess().ProcessName;
            bool isDebugging = System.Diagnostics.Debugger.IsAttached;

            // 如果调试中，排除 Visual Studio 和相关调试器进程
            var debugProtectedProcesses = new[]
            {
        "devenv",       // Visual Studio 主进程
        "conhost",      // 控制台宿主，可能附加调试时使用
        "vstest",       // 单元测试运行器
        "dotnet",       // .NET Core 应用程序主进程
        "frontend",     // 自己（保险）
    };

            try
            {
                var processes = Process.GetProcesses()
                    .Where(p =>
                    {
                        try
                        {
                            var memMB = p.WorkingSet64 / (1024 * 1024);
                            return !ProtectedProcesses.Contains(p.ProcessName)
                                   && p.Id != currentProcessId
                                   && p.Responding
                                   && memMB > memoryThresholdMB
                                   && (!isDebugging || !debugProtectedProcesses.Contains(p.ProcessName));
                        }
                        catch
                        {
                            return false;
                        }
                    });

                foreach (var process in processes)
                {
                    try
                    {
                        process.Kill();
                        killedCount++;
                    }
                    catch
                    {
                        failedList.Add($"{process.ProcessName} (PID: {process.Id})");
                    }
                }

                string message = $"✅ 已自动关闭 {killedCount} 个高内存进程。";
                if (failedList.Count > 0)
                {
                    message += $"\n⚠️ 以下进程关闭失败：\n" + string.Join("\n", failedList);
                }

                MessageBox.Show(message, "自动清理结果", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadTopMemoryProcesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"自动清理失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}