using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;

namespace FiveM_Server_Restart
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PnlTopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void BtnMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        System.Windows.Threading.DispatcherTimer RestartTimer = new System.Windows.Threading.DispatcherTimer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RestartTimer.Tick += new EventHandler(RestartTimer_Tick);
            RestartTimer.Interval = new TimeSpan(0, 0, 1);
            RestartTimer.Start();
        }

        private void RestartTimer_Tick(object sender, EventArgs e)
        {

            DateTime localDate = DateTime.Now;
            string cultureNames = "pt-BR";
            var culture = new CultureInfo(cultureNames);
            var cultura = localDate.ToString("HH:mm:ss");

            var horaFinal = "Don't Panic";
            LblHoras.Text = horaFinal.ToString();

            if (cultura == "06:00:00")
            {
                Restart();
            }
        }

        private void BtnRestart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Restart();
        }

        private void BtnStart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Start();
        }

        private void BtnStop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            KillServer();
        }

        private void Restart()
        {
            KillServer();
            Start();
        }

        private void KillServer()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "FXServer")
                {
                    p.Kill();
                }

                if (p.ProcessName == "cmd")
                {
                    p.Kill();
                }
            }
        }

        private void Start()
        {
            string args;
            args = @"cd C:\basesp\server-data && start ..\run.cmd +exec server.cfg";

            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;

            proc1.WorkingDirectory = @"C:\Windows\System32";

            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            //proc1.Verb = "runas";
            proc1.Arguments = "/c " + args;
            proc1.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc1);
        }
    }
}
