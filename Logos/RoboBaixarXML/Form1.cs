using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic;

namespace DownloadXMLNFeExemploBasico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButCaptcha_Click(object sender, EventArgs e)
        {
            Processar("");
        }

        string ultimaId = "";
        int contador = 0;

        private void LerResultadoCaptcha(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Application.DoEvents();

            if (ultimaId == webBrowser1.DocumentText)
            {
                timer2.Enabled = true;
                return;
            }

            if (contador >= 20)
            {
                Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXml set QuantidadeTentativas=isnull(QuantidadeTentativas,0)+1 Where Lote='" + txtLoteProcessamento.Text + "' and  Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                timer2.Enabled = true;
                contador = 0;
                return;
            }



            if (webBrowser1.DocumentText == "ERROR_CAPTCHA_UNSOLVABLE" || webBrowser2.DocumentText == "ERROR_CAPTCHA_UNSOLVABLE")
            {
                Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXml set Processado=null, Erro='ERROR_CAPTCHA_UNSOLVABLE',QuantidadeTentativas=isnull(QuantidadeTentativas,0)+1 Where Lote='" + txtLoteProcessamento.Text + "' and Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                return;
            }


            if (webBrowser1.DocumentText.Contains("OK|"))
            {
                EditCaptcha.Text = "";

                if (webBrowser2.DocumentText.Contains("OK|"))
                {

                    EditCaptcha.Text = webBrowser2.DocumentText.Replace("OK|", "").Replace(" ", "");
                    Application.DoEvents();

                    string cmoi = Directory.GetCurrentDirectory() + "\\XMLS\\" + txtLoteProcessamento.Text + "\\";

                    if (!Directory.Exists(cmoi))
                        Directory.CreateDirectory(cmoi);

                    string cc = cmoi + EditChave.Text + ".xml";

                    if (File.Exists(cc))
                        File.Delete(cc);


                    if (UDownloadXMLNFeDLL.BaixarXMLNFeSemCert(EditChave.Text, EditCaptcha.Text, cc))
                    {
                        Web1.Navigate(cmoi + EditChave.Text + ".xml");
                        //Sistran.Library.GetDataTables.RetornarDataTableWin("delete from DocumentoXml Where Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXml set Processado=GetDate() Where Lote='" + txtLoteProcessamento.Text + "' and  Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        //timer1.Enabled = true;
                        Application.DoEvents();
                        ultimaId = webBrowser1.DocumentText;
                        contador = 0;
                        //return;

                        textBox1.Text = "Nota Fiscal: " + EditChave.Text + ". Importação Efetuada com Sucesso!";
                        Application.DoEvents();

                    }
                    else
                    {

                        try
                        {
                            Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXml set QuantidadeTentativas=isnull(QuantidadeTentativas,0)+1, erro='Captcha Errada' Where Lote='" + txtLoteProcessamento.Text + "' and  Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                            textBox1.Text = "Captcha Errada1";
                            Application.DoEvents();

                            MessageBox.Show(UDownloadXMLNFeDLL.Msg());
                            webBrowser2.Refresh();
                            //return;
                        }
                        catch (Exception)
                        {
                            throw;
                        }


                        Application.DoEvents();
                    }

                }
                else if (webBrowser2.DocumentText == "CAPCHA_NOT_READY")
                {
                    textBox1.Text = "Tentando ....";
                    Application.DoEvents();

                    textBox1.Text = "Tentando Novamente Processar a Captcha";
                    Application.DoEvents();

                    Thread.Sleep(2500);
                    webBrowser2.Navigate("http://2captcha.com/res.php?key=a34589e24502089ff12c0a586405dfa8&action=get&id=" + IdCaptcha);
                    webBrowser2.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(LerResultadoCaptcha);

                    contador++;
                    textBox1.Text = "Tentando Novamente Processar a Captcha. " + contador + "X.";
                    Application.DoEvents();
                    return;

                }

            }
            else
            {
                Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXml set QuantidadeTentativas=isnull(QuantidadeTentativas,0)+1 Where lote='" + txtLoteProcessamento.Text + "' and Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            }
            textBox1.Text = "";
            Application.DoEvents();
            timer2.Enabled = true;
            return;
        }

        string IdCaptcha = "";
        private void submeter(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.DocumentText.Contains("OK|"))
            {
                if (!chkManual.Checked)
                {
                    Application.DoEvents();
                    IdCaptcha = webBrowser1.DocumentText.Replace("OK|", "").Trim();
                    textBox1.Text = "Aguardando Captcha: " + IdCaptcha;
                    Application.DoEvents();
                    Thread.Sleep(10000);
                    webBrowser2.Navigate("http://2captcha.com/res.php?key=a34589e24502089ff12c0a586405dfa8&action=get&id=" + IdCaptcha);
                    webBrowser2.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(LerResultadoCaptcha);
                }

            }
            else
            {
                HtmlElementCollection elc = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("type").Equals("submit"))
                    {
                        el.InvokeMember("Click");
                        break;
                    }

                }
            }
        }



        private void ButBaixar2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
                timer2.Enabled = true;
            }
            Application.DoEvents();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cmoi = Directory.GetCurrentDirectory() + "\\XMLS";

            if (!Directory.Exists(cmoi))
                Directory.CreateDirectory(cmoi);
        }

        public void Processar(string chave)
        {
            if (txtLoteProcessamento.Text == "")
            {
                MessageBox.Show("Informe o Lote.");
                return;
            }

            string where = " and Processado is null";

            if (chave != "")
                where = " and chave='" + chave + "' ";

            DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin("Select top 1 *  from DocumentoXml where 0=0  " + where + " and Lote='" + txtLoteProcessamento.Text + "' And Processado is null order by QuantidadeTentativas", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


            if (dtNota.Rows.Count == 0)
            {
                timer2.Enabled = false;
                string cm = Directory.GetCurrentDirectory() + "\\XMLS\\" + txtLoteProcessamento.Text;
                Clipboard.SetText(cm);
                MessageBox.Show("Não há mais notas neste Lote. Local dos Arquivos: " + cm);
                return;
            }


            Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXML set InicioProcessamento=getDate() where Chave='" + dtNota.Rows[0][0].ToString() + "' ; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            EditChave.Text = dtNota.Rows[0][0].ToString();

            if (UDownloadXMLNFeDLL.Captcha("Captcha.gif"))
                Img1.ImageLocation = "Captcha.gif";
            else
            {
                textBox1.Text = UDownloadXMLNFeDLL.Msg();
                Application.DoEvents();
            }

            string base64String = "";
            using (Image image = Image.FromFile("Captcha.gif"))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }

            if (base64String == "")
                return;
            IdCaptcha = "";

            string html = "<html><head></head>";
            html += "<form  method='post' action='http://2captcha.com/in.php'>";
            html += "<input type='hidden' name='method' value='base64'>";
            html += "<input type='text' name='key' value='a34589e24502089ff12c0a586405dfa8'>";
            html += "<input type='text' name='phrase' value='1'>";
            html += "<textarea name='body'>";
            html += base64String;
            html += "</textarea>";
            html += "<input type='submit' value='download and get the ID'>";
            html += "</form></html>";
            webBrowser1.DocumentText = html;
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(submeter);
        }

        public void ProcessarDigitarCapcha_Automatico(string chave)
        {
            try
            {
                string where = " and Processado is null";

                if (chave != "")
                    where = " and chave='" + chave + "' ";
                DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin("Select top 1 *  from DocumentoXml where 0=0  " + where + " and Lote='" + txtLoteProcessamento.Text + "' order by QuantidadeTentativas", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (dtNota.Rows.Count == 0)
                {
                    //Application.Exit();
                    string cm = Directory.GetCurrentDirectory() + "\\XMLS\\" + txtLoteProcessamento.Text;
                    MessageBox.Show("Final de Procesamento. Local dos Arquivos:" + cm);
                    timer1.Enabled = false;


                    return;
                }

                EditChave.Text = dtNota.Rows[0][0].ToString();

                if (UDownloadXMLNFeDLL.Captcha("Captcha.gif"))
                    Img1.ImageLocation = "Captcha.gif";
                else
                {
                    textBox1.Text = UDownloadXMLNFeDLL.Msg();
                    Application.DoEvents();
                }

                string base64String = "";
                using (Image image = Image.FromFile("Captcha.gif"))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        base64String = Convert.ToBase64String(imageBytes);
                    }
                }



                EditCaptcha.Text = Microsoft.VisualBasic.Interaction.InputBox("Informe a Captcha", "Informe a Captcha", "", 300, 300);

                if (EditCaptcha.Text == "")
                {
                    MessageBox.Show("Operação Cancelada.");
                    timer2.Enabled = false;
                    timer1.Enabled = false;
                    return;
                }

                string cmoi = Directory.GetCurrentDirectory() + "\\XMLS\\" + txtLoteProcessamento.Text + "\\";

                if (!Directory.Exists(cmoi))
                    Directory.CreateDirectory(cmoi);

                string cc = cmoi + EditChave.Text + ".xml";

                if (File.Exists(cc))
                    File.Delete(cc);


                if (UDownloadXMLNFeDLL.BaixarXMLNFeSemCert(EditChave.Text, EditCaptcha.Text, cc))
                {
                    Web1.Navigate(cmoi + EditChave.Text + ".xml");
                    Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXml set Processado=GetDate(), Erro=null Where lote='" + txtLoteProcessamento.Text + "' and  Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    //  timer1.Enabled = true;
                    // timer2.Enabled = true;
                }
                else
                {
                    Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXml set QuantidadeTentativas=isnull(QuantidadeTentativas,0)+1 Where lote='" + txtLoteProcessamento.Text + "' and  Chave='" + EditChave.Text + "'; select 1 ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    textBox1.Text = "Captcha Errada2";
                    if (UDownloadXMLNFeDLL.Msg() != null)
                        textBox1.Text = UDownloadXMLNFeDLL.Msg();

                }
                Application.DoEvents();



                timer1.Enabled = true;
                timer2.Enabled = true;
            }
            catch (Exception ex)
            {
				string m = ex.Message;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;

            if (txtLoteProcessamento.Text == "")
            {
                MessageBox.Show("Informe um Lote de Processamento");
                return;
            }

            this.Cursor = Cursors.WaitCursor;


            if (chkManual.Checked)
            {
                ProcessarDigitarCapcha_Automatico("");
            }
            else
                Processar("");

            this.Cursor = Cursors.Default;
            //Web1.DocumentText = "";
            //EditCaptcha.Text = "";
            //EditChave.Text = "";
            //txtLoteProcessamento.Text = "";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            EditCaptcha.Text = "";
            EditChave.Text = "";
            Web1.DocumentText = "";
            webBrowser1.DocumentText = "";
            webBrowser2.DocumentText = "";

            webBrowser1.Dispose();
            webBrowser2.Dispose();

            webBrowser1 = new WebBrowser();
            webBrowser2 = new WebBrowser();

            if (timer1.Enabled == false)
                timer1.Enabled = true;

            ultimaId = "";
        }

        string lote = "";
        private void btnIncluirChave_Click(object sender, EventArgs e)
        {
            if (txtLoteProcessamento.Text == "")
            {
                MessageBox.Show("Informe o Lote");
                return;
            }


            if (txtIncluirChave.Text.Length == 44)
            {
                //if(!Directory.Exists(txtLoteProcessamento.Text.Replace("-","").Replace(".","")));
                //    Directory.CreateDirectory(txtLoteProcessamento.Text.Replace("-","").Replace(".",""));

                Sistran.Library.GetDataTables.RetornarDataTableWin("Insert into DocumentoXML(Chave, Lote) Values ('" + txtIncluirChave.Text.Trim() + "', '" + txtLoteProcessamento.Text + "')", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                lblContador.Text = Sistran.Library.GetDataTables.RetornarDataTableWin("Select count(distinct Chave) from DocumentoXML Where Lote = '" + txtLoteProcessamento.Text + "'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Rows[0][0].ToString() + " Notas.";

            }
            else
            {
                MessageBox.Show("Chave Inválida");
            }
            txtIncluirChave.Text = "";
            txtIncluirChave.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtIncluirChave.Text.Length == 44)
            {
                Sistran.Library.GetDataTables.RetornarDataTableWin("Insert into DocumentoXML(Chave, Lote) Values ('" + txtIncluirChave.Text.Trim() + "', '" + txtLoteProcessamento.Text + "')", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (chkManual.Checked)
                    ProcessarDigitarCapcha_Automatico(txtIncluirChave.Text);
                //else
                //    Processar(txtIncluirChave.Text);

                timer2.Enabled = false;
                timer1.Enabled = false;
                txtIncluirChave.Text = "";
                txtIncluirChave.Focus();
                MessageBox.Show("Xml Importado com Sucesso.");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblContador.Text = "0";

            if (tabControl1.SelectedIndex == 1)
            {
                timer2.Enabled = false;
                timer1.Enabled = false;
                chkManual.Checked = true;
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                chkManual.Checked = false;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                timer2.Enabled = false;
                timer1.Enabled = false;
                CarregarPendentesDoLote();
            }

            else if (tabControl1.SelectedIndex == 3)
            {
                timer2.Enabled = false;
                timer1.Enabled = false;
                CarregarLotesPendentes();
            }
        }

        private void CarregarLotesPendentes()
        {
            string Sql = "SELECT LOTE, SUM((CASE ISNULL(PROCESSADO, '')  WHEN '' THEN 1 ELSE 0 END  )) PENDENTES FROM DOCUMENTOXML GROUP BY LOTE HAVING SUM((CASE ISNULL(PROCESSADO, '')  WHEN '' THEN 1 ELSE 0 END  ))  >0 ORDER BY 2 DESC";
            grdLotesPendentes.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(Sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            grdLotesPendentes.AutoResizeColumns();
        }

        private void CarregarPendentesDoLote()
        {
            //if (txtLoteProcessamento.Text == "")
            //{
            //    MessageBox.Show("Favor Preencher o Lote");
            //    txtLoteProcessamento.Focus();
            //    tabControl1.SelectedIndex = 0;
            //    return;
            //}

            string Sql = "SELECT LOTE, COUNT(CHAVE) QUANTIDADE, SUM(CASE ISNULL( PROCESSADO,0) WHEN 0  THEN  0 ELSE 1 END) PRONTOS, COUNT(CHAVE) - SUM(CASE ISNULL( PROCESSADO,0) WHEN 0  THEN  0 ELSE 1 END) PENDENTE FROM DOCUMENTOXML GROUP BY LOTE HAVING  COUNT(CHAVE) - SUM(CASE ISNULL( PROCESSADO,0) WHEN 0  THEN  0 ELSE 1 END) >0";
            dataGridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(Sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            dataGridView1.AutoResizeColumns();
        }

        private void txtIncluirChave_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtIncluirChave.Text.Length == 44 || e.KeyCode == Keys.Enter)
            {
                btnIncluirChave.Focus();
            }
        }

        private void btnExcluirLote_Click(object sender, EventArgs e)
        {
            if (linhaAtual > -1)
            {
                Int32 selectedRowCount = grdLotesPendentes.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();


                    DialogResult resp = MessageBox.Show("Tem Certeza que deseja Excluir o Lote: " + grdLotesPendentes.Rows[linhaAtual].Cells[0].Value.ToString() + " ?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resp == System.Windows.Forms.DialogResult.Yes)
                    {
                        Sistran.Library.GetDataTables.RetornarDataTableWin("Update DocumentoXML set Processado=getdate() where Lote='" + grdLotesPendentes.Rows[linhaAtual].Cells[0].Value.ToString() + "' and processado is null; Select 1", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        CarregarLotesPendentes();
                        linhaAtual = -1;
                    }

                }
            }
        }


        int linhaAtual = -1;
        private void grdLotesPendentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            linhaAtual = int.Parse(e.RowIndex.ToString());

            if (linhaAtual >= 0)
            {
                grdLotesPendentes.Rows[linhaAtual].Selected = true;
            }
        }
    }
}