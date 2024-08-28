using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace 软件系统服务端模版
{
    static class Program
    {
        /// <summary>
        /// main entrance
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process process = Process.GetCurrentProcess();
            // Iterate through the group of processes with the same name as the application
            foreach (Process p in Process.GetProcessesByName(process.ProcessName))
            {
                // If it's not the same process, close the newly created process
                if (p.Id != process.Id)
                {
                    // Showing the original window may take some time, otherwise it won't be displayed
                    ShowWindowAsync(p.MainWindowHandle, 9);
                    SetForegroundWindow(p.MainWindowHandle);
                    System.Threading.Thread.Sleep(10);
                    Application.Exit();// Close the current application
                    return;
                }
            }
            // Set the thread pool size for the application to prevent server-side deadlock; adjust according to memory and CPU
            System.Threading.ThreadPool.SetMaxThreads(1000, 256);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormServerWindow());
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
