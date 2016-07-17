using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //try
            //{
            //登录检验
            GuardLogin guardlogin1 = GuardLogin.GetGuardLogin();
            guardlogin1.ShowDialog();

            switch (guardlogin1.acheck)
            {
                case 1:
                    mainpanel mainpanel1 = mainpanel.Getmainpanel();
                    Application.Run(mainpanel1);
                    break;

            }
        }
   
    }
    

}
