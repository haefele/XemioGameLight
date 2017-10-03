using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xemio.GameLight.Tests.Bums;
using Color = System.Drawing.Color;

namespace Xemio.GameLight.Tests.WpfEmbedded
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            this.WinFormHost.Child = new System.Windows.Forms.Panel();

            XGL.EmbedInControl(f =>
            {
                f.Control = this.WinFormHost.Child;
                f.StartScene = new TestScene();
            });
        }
    }
}
