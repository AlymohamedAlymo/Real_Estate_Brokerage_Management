using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.RadToastNotificationManager;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls;
using System.ComponentModel;
using System.IO;
using DoctorERP.Properties;

namespace ToastNotificationManagerExamples.FirstLook
{
    public partial class Form1 : RadForm
    {
        RadToastNotificationManager manager;
        public Form1()
        {
            InitializeComponent();
            
            Disposed += Form1_Disposed;

            manager = new RadToastNotificationManager(components);
            
            RadToastNotificationManager.RadToastOnActivated += RadToastNotificationManager_RadToastOnActivated;

            ((ISupportInitialize)this).BeginInit();

            int tabIndedx = 1;
            int stepSize = 40;
            int buttonLocationX = Width / 2 - 95;
            int buttonLocationY = 80;
            int buttonWidth = 190;
            int buttonHeight = 26;
            int imageSize = 32;
            int smallFix = 0;

            IEnumerable<RadToastTemplateType> templateTypes = Enum.GetValues(typeof(RadToastTemplateType)).Cast<RadToastTemplateType>();
            foreach (RadToastTemplateType type in templateTypes)
            {
                if (type == RadToastTemplateType.ToastGeneric || type == RadToastTemplateType.ToastLegacy)
                {
                    continue;
                }

                RadButton button = new RadButton
                {
                    Location = new System.Drawing.Point(buttonLocationX, buttonLocationY),
                    Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                    TabIndex = tabIndedx++,
                    Text = "Show " + type.ToString().Replace("Toast", "") + " Toast",
                    Tag = type,
                    Anchor = AnchorStyles.None
                };
                button.Click += Button_Click;
                Controls.Add(button);

                RadSvgImage svgImage = null;
                try
                {
                    byte[] svgBytes = (byte[])Resources.ResourceManager.GetObject(type.ToString());
                    svgImage = RadSvgImage.FromByteArray(svgBytes);
                }
                catch { }

                switch (type)
                {
                    case RadToastTemplateType.ToastNews:
                    case RadToastTemplateType.ToastAlarm:
                    case RadToastTemplateType.ToastCall:
                        smallFix = 1;
                        break;
                    default:
                        smallFix = 0;
                        break;
                }

                if (svgImage != null)
                {
                    RadPictureBox pictureBox = new RadPictureBox
                    {
                        SvgImage = svgImage,
                        Location = new System.Drawing.Point(buttonLocationX - imageSize - 25 - smallFix, buttonLocationY - 3),
                        Size = new System.Drawing.Size(imageSize, imageSize),
                        Anchor = AnchorStyles.None,
                        ContextMenuEnabled = false,
                        ShowBorder = false,
                        ShowScrollBars = false                        
                    };
                    pictureBox.ZoomProperties.AllowZoom = false;
                    pictureBox.RootElement.EnableElementShadow = false;
                    Controls.Add(pictureBox);
                }

                buttonLocationY += stepSize + buttonHeight;
            }            

            ((ISupportInitialize)this).EndInit();

            #region Load resources in the background
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
            #endregion Load resources in the background

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\Telerik\Toast\";
            // copy the TA_ files in the temp folder, so they can be used by the toast templates
            Directory.CreateDirectory(tempDir);

            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            string[] resourceNames = myAssembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                if (!resourceName.Contains("TA_")) continue;

                string fileName = resourceName.Substring(resourceName.IndexOf("TA_"));
                Stream rStream = myAssembly.GetManifestResourceStream(resourceName);
                FileStream fs = new FileStream(tempDir + fileName, FileMode.Create);
                rStream.CopyTo(fs);
                fs.Flush();
                fs.Close();
                rStream.Close();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            RadToastTemplateType type = (RadToastTemplateType)(sender as RadButton).Tag;
            manager.ShowNotification(new RadToastNotification(type, type.ToString()));
        }

        private void Form1_Disposed(object sender, EventArgs e)
        {
            try
            {
                RadToastNotificationManager.RadToastOnActivated -= RadToastNotificationManager_RadToastOnActivated;
                manager.Unregister();
            }
            catch { }
        }

        private void RadToastNotificationManager_RadToastOnActivated(RadToastOnActivatedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("arguments: " + (string.IsNullOrEmpty(e.Arguments) ? "{null}" : e.Arguments));
            if (e.UserInput != null && e.UserInput.Count > 0)
            {
                foreach(var pair in e.UserInput)
                {
                    sb.AppendLine();
                    sb.Append("key: {");
                    sb.Append(pair.Key);
                    sb.Append("} value: {");
                    sb.Append(pair.Value);
                    sb.Append("}");
                }
            }

            RadMessageBox.SetThemeName(ThemeName);

            if (InvokeRequired)
            {
                Action a = () =>
                {
                    RadMessageBox.Show(this, sb.ToString(), "RadToastActivated", MessageBoxButtons.OK, RadMessageIcon.Info);
                };
                Invoke(a);
            }
            else
            {
                RadMessageBox.Show(this, sb.ToString(), "RadToastActivated", MessageBoxButtons.OK, RadMessageIcon.Info);
            }
        }
    }
}
