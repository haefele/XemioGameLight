using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xemio.GameLight.Tests.Bums
{
    public partial class EmbeddedTestForm : Form
    {
        public EmbeddedTestForm()
        {
            this.InitializeComponent();

            XGL.EmbedInControl(f =>
            {
                f.Control = this.panel1;
                f.StartScene = new InputScene();
            });
        }
    }
}
