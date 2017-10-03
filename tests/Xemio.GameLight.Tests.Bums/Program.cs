using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xemio.GameLight.Tests.Bums
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.Run(new EmbeddedTestForm());

            XGL.CreateWinFormsWindow(f =>
            {
                f.StartScene = new TestScene();
                f.Title = "Test-Game";
            });
        }
    }
}
