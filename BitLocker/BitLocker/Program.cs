using BitLocker.Server;
using BitLocker.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitLocker
{
    static class Program
    {
        public static String currentPath = null;
        public static ServerControl servercontrol = null;
        public static ViewControl viewcontrol = null;
        public static MainWin mainWind = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            currentPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //监听退出进程
            var CurrentPro = Process.GetCurrentProcess();
            CurrentPro.EnableRaisingEvents = true;
            CurrentPro.Exited += new EventHandler(Pro_Exit);

            mainWind = new MainWin();

            //启动必要的线程
            servercontrol = new ServerControl();
            viewcontrol = new ViewControl(mainWind);

            servercontrol.StartListenThread();
            viewcontrol.StartListenServerThread();

            mainWind.FormClosed += new FormClosedEventHandler(FormExit);

            Application.Run(mainWind);
        }

        //正常退出
        static void FormExit(object sender, FormClosedEventArgs e)
        {
            ExitProgram();
        }

        static void Pro_Exit(object sender, EventArgs e)
        {
            ExitProgram();
        }

        //退出程序
        private static void ExitProgram()
        {
            servercontrol.StopListenThread();
            viewcontrol.StopListenServerThread();

            System.Environment.Exit(0);
        }
        




    }
}
