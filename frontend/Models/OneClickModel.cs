using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace frontend.Models
{
    public class OneClickModel
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_SYSCOMMAND = 0x0112;
        private const uint SC_MONITORPOWER = 0xF170;
        private const uint SC_SCREENSAVE = 0xF140;

        public void ExecuteShutdown()
        {
            Process.Start(new ProcessStartInfo("shutdown", "/s /t 0"));
        }

        public void ExecuteRestart()
        {
            Process.Start(new ProcessStartInfo("shutdown", "/r /t 0"));
        }

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        public void ExecuteSleep(IntPtr windowHandle)
        {
            // 使用系统休眠API替代消息发送
            SetSuspendState(false, false, false);
        }
    }
}
