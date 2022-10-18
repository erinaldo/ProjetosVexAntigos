using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pop3;
using System.IO;
using System.Collections;

namespace CapturaDeEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            LerConfiguracoes();
        }

        private void LerConfiguracoes()
        {
             DataSet ds = new DataSet();

             if (File.Exists("conf.xml"))
             {
                 ds.ReadXml("conf.xml");

                 txtAssunto.Text = ds.Tables[0].Rows[0]["Assunto"].ToString();
                 txtCaminhoDosArquivos.Text = ds.Tables[0].Rows[0]["PastaDestino"].ToString();
                 txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                 txtEmailRemetente.Text = ds.Tables[0].Rows[0]["EmailRemetente"].ToString();
                 txtPop.Text = ds.Tables[0].Rows[0]["Pop"].ToString();
                 txtSenha.Text = ds.Tables[0].Rows[0]["Senha"].ToString();
                 txtTempo.Text = ds.Tables[0].Rows[0]["TempoDeExecucao"].ToString();

                 var arq = ds.Tables[0].Rows[0]["TipoDeArquivos"].ToString().Split(',');


                 for (int i = 0; i < arq.Length; i++)
                 {

                 }
                 

             }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RealizarLeitura();
            timer1.Interval = int.Parse(txtTempo.Text);
        }

        private void RealizarLeitura()
        {
            //string arq = "";
            //for (int i = 0; i < chkTipoArquivos.SelectedIndex; i++)
            //{
            //     arq += chkTipoArquivos.CheckedItems[i].ToString() + ",";
            //}

            try
            {
                Pop3Client email = new Pop3Client(txtEmail.Text, txtSenha.Text, txtPop.Text);
                email.OpenInbox();

                while (email.NextEmail())
                {
                    bool baixar = false;
                    if (email.Subject.ToUpper().Contains(txtAssunto.Text.ToUpper()) && txtAssunto.Text != "")
                        baixar = true;

                    if(email.From.ToLower().Contains(txtEmailRemetente.Text) && txtEmailRemetente.Text !="")
                        baixar = true;
                    if (email.IsMultipart)
                    {                   

                        IEnumerator enumerator = email.MultipartEnumerator;
                        while (enumerator.MoveNext())
                        {
                            Pop3Component multipart = (Pop3Component) enumerator.Current;

                            if (multipart.IsAttachment)
                            {
                                if (!multipart.IsBody)
                                {
                                    if (multipart.Data != null)
                                    {
                                        baixar = false;
                                        for (int i = 0; i < chkTipoArquivos.SelectedIndex; i++)
                                        {
                                            if (chkTipoArquivos.CheckedItems[i].ToString().Replace(".", "").Trim().Contains(multipart.FileExtension))
                                            {
                                                baixar = true;
                                                break;
                                            }

                                        }

                                        if (baixar)
                                        {
                                            byte[] arrBytesLeitura = Convert.FromBase64String(multipart.Data);
                                            File.WriteAllBytes(txtCaminhoDosArquivos.Text + "\\" + multipart.Name, arrBytesLeitura);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                email.CloseConnection();

            }
            catch (Pop3LoginException)
            {
                MessageBox.Show("Não é possível conectar ao e-mail.");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // validar


            if (button1.Text == "Parar")
            {
                timer1.Enabled = false;
                button1.Text = "Aplicar e Iniciar";
                return;
            }

            button1.Text = "Parar";
            DataSet ds = new DataSet();

            if (File.Exists("conf.xml"))
            {
                ds.ReadXml("conf.xml");

                DataRow dr = ds.Tables[0].Rows[0];
                dr["Email"] = txtEmail.Text;
                dr["Senha"] = txtSenha.Text;
                dr["Pop"] = txtPop.Text;

                string arq = "";

                for (int i = 0; i < chkTipoArquivos.CheckedItems.Count; i++)
                {
                    arq += chkTipoArquivos.CheckedItems[i].ToString() + ",";
                }

                dr["TipoDeArquivos"] = arq;

                dr["EmailRemetente"] = txtEmailRemetente.Text;
                dr["Assunto"] = txtAssunto.Text;
                dr["TempoDeExecucao"] = txtTempo.Text;
                dr["PastaDestino"] = txtCaminhoDosArquivos.Text;

               
                ds.WriteXml("conf.xml");
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Email");
                dt.Columns.Add("Senha");
                dt.Columns.Add("Pop");
                dt.Columns.Add("TipoDeArquivos");
                dt.Columns.Add("EmailRemetente");
                dt.Columns.Add("Assunto");
                dt.Columns.Add("TempoDeExecucao");
                dt.Columns.Add("PastaDestino");

                DataRow dr = dt.NewRow();
                dr["Email"] = txtEmail.Text;
                dr["Senha"] = txtSenha.Text;
                dr["Pop"] = txtPop.Text;

                string arq = "";

                for (int i = 0; i < chkTipoArquivos.CheckedItems.Count; i++)
                {
                    arq += chkTipoArquivos.CheckedItems[i].ToString() + ",";
                }

                dr["TipoDeArquivos"] = arq;
                
                dr["EmailRemetente"] = txtEmailRemetente.Text;
                dr["Assunto"] = txtAssunto.Text;
                dr["TempoDeExecucao"] = txtTempo.Text;
                dr["PastaDestino"] = txtCaminhoDosArquivos.Text;

                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

                ds.WriteXml("conf.xml");
            }

            RealizarLeitura();
            timer1.Enabled = true;

        }
    }
}