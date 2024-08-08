using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoctorERP
{
    public partial class FlyoutStaticContent : UserControl
    {
        public FlyoutStaticContent()
        {
            InitializeComponent();

            this.radPictureBox1.ShowBorder = false;
            this.radPictureBox2.ShowBorder = false;
            this.radWaitingBar1.StartWaiting();
        }
    }
}
