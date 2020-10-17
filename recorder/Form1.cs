using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace recorder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VideoFileWriter vw;
        Bitmap bp;
        Graphics gr;
        string path = @"one"+DateTime.Now.ToString("ddssMM")+".mp4";
        string foldername = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)+@"\record";
                

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                vw = new VideoFileWriter();
                System.IO.Directory.CreateDirectory(foldername);
                vw.Open(foldername+@"\"+path, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, 10, VideoCodec.MPEG4, 100000);
                timer1.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            gr = Graphics.FromImage(bp);
            gr.CopyFromScreen(0, 0, 0, 0, bp.Size);
            pictureBox1.Image = bp;
            vw.WriteVideoFrame(bp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1 = new Timer();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
        }

        private void stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            vw.Close();
        }

        private void Capture_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            timercapture.Start();
        }

        int i = 0;

        private void timercapture_Tick(object sender, EventArgs e)
        {
            i++;
            if (i >= 2)
            {
                i = 0;
                Bitmap mp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics gr = Graphics.FromImage(mp);
                gr.CopyFromScreen(0, 0, 0, 0, mp.Size);
                pictureBox1.Image = mp;
                pictureBox1.Image.Save(@"D:\shot.jpeg", ImageFormat.Jpeg);
                this.WindowState = FormWindowState.Normal;
                timercapture.Stop();
            }
        }
    }
}
