using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Configuration;

namespace MonitorServidores
{
    public partial class Form1 : Form
    {

        private Ping checador;
        private List<string> servidores;
        private int contador;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            checador = new Ping();
            string ser = ConfigurationManager.AppSettings["servidores"];

            servidores =new List<string>(ser.Split('|'));
           
            contador = 0;

            timer1.Start();
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                if (checador.Send(servidores[rota(contador)]).Status == IPStatus.Success)
                {
                    
                    //notifyIcon1.BalloonTipTitle = "Servidor OK";
                    //notifyIcon1.BalloonTipText =servidores[contador];
                    //notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    //notifyIcon1.ShowBalloonTip(1000);
                }
                else
                {
                   
                    notifyIcon1.BalloonTipTitle = "Servidor Error";
                    notifyIcon1.BalloonTipText = servidores[contador];
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                    notifyIcon1.ShowBalloonTip(1000);
                }
                contador += 1;
                timer1.Start();
            }
            catch (Exception oe)
            {
                
                notifyIcon1.BalloonTipTitle = "Servidor Error";
                notifyIcon1.BalloonTipText = servidores[contador];
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                notifyIcon1.ShowBalloonTip(1000);
                contador += 1;
                timer1.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int rota(int i)
        {
            if (i == servidores.Count)
            {
                contador = 0;
                return contador;
            }
            else
            {
                return contador;
            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
   }
}
