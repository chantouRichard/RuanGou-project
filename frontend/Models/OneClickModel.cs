using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace frontend.Models
{
    public class OneClickModel
    {
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

        public void ExecuteSleep()
        {
            SetSuspendState(false, false, false);
        }
    }
}
