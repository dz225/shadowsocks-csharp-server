using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace shadowsocks_.net
{
    public partial class MainForm : Form
    {
        Config config;
        Server server;

        public static MainForm instanse = null;

        public MainForm()
        {
            instanse = this;
            InitializeComponent();
        }

        public static MainForm GetInstance()
        {
            return instanse;
        }

        public delegate void FlushLog(string str); 
        public void Log(string str)
        {
            if (this.label10.InvokeRequired)
            {
                FlushLog fc = new FlushLog(Log); 
                this.Invoke(fc, new object[1] { str});
            }
            else
            {
                this.label10.Text = str;
            }
        }

        private void reload(Config config)
        {
            if (server != null)
            {
                server.Stop();
            }
            server = new Server(config);
            server.Start();
        }

        private void SetConfigLab(Config config)
        {
            label5.Text = config.server;
            label6.Text = config.server_port.ToString();
            label7.Text = config.method;
            label8.Text = config.password;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try
            {
                Config config = Config.Load();
                this.config = config;
                reload(config);
                SetConfigLab(config);
                this.Hide();
            }
            catch (FormatException)
            {
                MessageBox.Show("there is format problem");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UpDateStatusList()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(3500, "I'm here", "wwwwwww", ToolTipIcon.Info);
                this.ShowInTaskbar = false;
            }
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            this.Show();
        }
    }
}
