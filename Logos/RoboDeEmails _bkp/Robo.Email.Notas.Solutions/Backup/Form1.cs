using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Robo.Email.Notas.Solutions.Windows.Testes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {            
            timer1.Enabled = true;           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string emais = "";
            timer1.Enabled = false;
            try
            {
                DataTable dt = Sistran.Library.Robo.Robo.RetornarEmails();

                DateTime horaAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                foreach (DataRow item in dt.Rows)
                {
                    string[] horas = item["Horas"].ToString().Split(',');

                    for (int i = 0; i < horas.Length; i++)
                    {
                        DateTime hora = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(horas[i]), 0, 0);

                        if (hora == horaAtual)
                        {
                            emais += item[2].ToString() + ";";
                        }
                    }
                }

                if (emais.Length > 5)
                {
                    Sistran.Library.Robo.Robo x = new Sistran.Library.Robo.Robo();
                    x.Iniciar(emais);
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {            
            notifyIcon1.Visible = true;
            notifyIcon1.Text = this.Text;
            notifyIcon1.BalloonTipTitle = this.Text;
            notifyIcon1.BalloonTipText = "Clique duas vezes no ícone para retornar à aplicação!";            
            this.ShowInTaskbar = false;            
            notifyIcon1.ShowBalloonTip(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sistran.Library.Robo.Robo.EscreverLog("Final do servico");
            Application.Exit();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sistran.Library.Robo.Robo x = new Sistran.Library.Robo.Robo();
            x.Iniciar("moises@sistcno.com.br");
        }
    }
}
