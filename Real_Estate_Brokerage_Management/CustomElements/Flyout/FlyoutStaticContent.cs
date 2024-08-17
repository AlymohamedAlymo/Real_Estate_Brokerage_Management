using System.Windows.Forms;

namespace DoctorERP.CustomElements
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
