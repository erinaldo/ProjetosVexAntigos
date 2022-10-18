using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistranBLL;
using AprovacaoRequisicao.Library;
using System.Xml;
using System.Net;
using System.Web.Services.Protocols;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Net.Mail;
using System.Net.Mime;

namespace Robo.Email.Notas.Solutions.Windows.Testes
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        #region E V E N T S
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                reiniciarTimers();
            }
            catch (Exception)
            {
                reiniciarTimers();
            }
            
        }

        public void GravarExcecucao(string NomeTimer)
        {
            try
            {
                string sql = "update UltimaExecucaoRobo set UltimaExecucao=getDate() where Timer= '" + NomeTimer + "'; select 1";
                DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


            }
             catch (Exception ex)
            {
                reiniciarTimers();
            }
        }

        private void reiniciarTimers()
        {
            timer1.Enabled = false;            
            //timer2.Enabled = false;


            timer1.Enabled = true;            
            //timer2.Enabled = true;
        }

        DataTable dt;
        DateTime dataAtualizacao;
        int minRod = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            string emais = "", emailsFluxoCaixa = "", emailReBDAntigo = "", emailPainelFaturamento = "", emailOperacaoCliente = "";
            timer1.Enabled = false;
            Label2.Text = "Programa em Execução";
            Application.DoEvents();

            GravarExcecucao("TimerEmail");


            try
            {
                TimeSpan ts = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(dataAtualizacao);

                if (dt == null || dt.Rows.Count == 0 || ts.Hours > 1)
                {
                    dt = Sistran.Library.Robo.Robo.RetornarEmails2();
                    dataAtualizacao = DateTime.Now;

                    Label2.Text = "Checando envio de email";
                    Application.DoEvents();

                }

                DateTime horaAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                DateTime horae = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(textBox1.Text), 0, 0);

                DateTime horaean6 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
                DateTime horaean8 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                DateTime horaean10 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
                DateTime horaean12 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
                DateTime horaean14 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0);
                DateTime horaean16 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0);
                DateTime horaean18 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);

				//if (horaAtual == horae)
				//    montarEmailLogBanco();


				//if (horaAtual == horaean18)
				//{

				//    //envia o relatorio Mensal sr.Pereira
				//    DateTime hoje = DateTime.Now.Date;
				//    DateTime primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 01);
				//    DateTime primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(1);
				//    DateTime ultimoDiaDoMes = primeiroDiaDoProximoMes.AddDays(-1);

				//    //se for ultimo dia do mes
				//    if (ultimoDiaDoMes.Year == hoje.Year && ultimoDiaDoMes.Month == hoje.Month && ultimoDiaDoMes.Day == hoje.Day)
				//    {
				//        MontarEmailRE_Mensal_BaseNovaLogos("moises@sistecno.com.br", primeiroDiaDoMes, ultimoDiaDoMes);

				//    }
				//}


				if ((DateTime.Now.Minute % 59) == 0)
					DispararEmailCarga();

                foreach (DataRow item in dt.Rows)
                {
                    string[] horas = item["Horas"].ToString().Split(',');

                    for (int i = 0; i < horas.Length; i++)
                    {
                        DateTime hora = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(horas[i]), 0, 0);

                        if (hora == horaAtual)
                        {
                            if (item["reports"].ToString().Contains("SITUACAO DE ENTREGA"))
                                emais += item["email"].ToString() + ";";

                            if (item["reports"].ToString().Contains("RELATORIO DE PRODUCAO POR RELACAO DE ENTREGA"))
                            {
                                emailReBDAntigo += item["email"].ToString() + ";";
                                if (emailReBDAntigo.Contains("pereira@logoslogistica.com.br"))
                                {
                                    //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "andremachado@vexlogistica.com.br");
                                    //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "ecosta@vexlogistica.com.br");
                                    //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "gzanatta@vexlogistica.com.br");
                                    //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "ilima@vexlogistica.com.br");
                                    //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "patricia@vexlogistica.com.br");
                                    //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "pereira@logoslogistica.com.br");
                                }

                            }
                            if (item["reports"].ToString().Contains("FLUXO DE CAIXA"))
                                emailsFluxoCaixa += item["email"].ToString() + ";";

                            if (item["reports"].ToString().Contains("PAINEL DE FATURAMENTO"))
                                emailPainelFaturamento += item["email"].ToString() + ";";

                            if (item["reports"].ToString().Contains("OPERACAO CLIENTE"))
                                emailOperacaoCliente += item["email"].ToString() + ";";

                            
                        }
                    }
                }


                int tempo = int.Parse(ConfigurationSettings.AppSettings["Intervalo"]);



                if ((DateTime.Now.Minute % tempo) == 0 && DateTime.Now.Minute != minRod)
                {
                    enviarEmailPedido();
                    Label2.Text = "Email de Pedido";
                    Application.DoEvents();

                }

                string emailTotal = "";

                if (emais.Length > 5)
                {
                    Sistran.Library.Robo.Robo x = new Sistran.Library.Robo.Robo();
                    x.Iniciar(emais);
                    emailTotal += "<br>Notas Ficais Agurdando Embarque: <br> Emais: " + emais.Replace(";", "<br>");
                }

                if (emailReBDAntigo.Length > 5)
                {
                   
                    if (emailReBDAntigo.Contains("PEREIRA@LOGOSLOGISTICA.COM.BR"))
                    {
                        //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "andremachado@vexlogistica.com.br");
                        //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "ecosta@vexlogistica.com.br");
                        //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "gzanatta@vexlogistica.com.br");
                        //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "ilima@vexlogistica.com.br");
                        //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "patricia@vexlogistica.com.br");
                        //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "pereira@logoslogistica.com.br");

                       MontarEmailRE_BaseNovaLogos("andremachado@vexlogistica.com.br;pereira@logoslogistica.com.br; gzanatta@vexlogistica.com.br;", "OPERACAO DIVERSAS", "01");
                       MontarEmailRE_BaseNovaLogos("andremachado@vexlogistica.com.br;pereira@logoslogistica.com.br; gzanatta@vexlogistica.com.br;", "OPERACAO DIVERSAS", "02");

            //            MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "01");
              //          MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "02");


                        emailReBDAntigo = "eplima@vexlogistica.com.br;moises@sistecno.com.br;tbonassa@timecsc.com.br;gzanatta@vexlogistica.com.br";
                        MontarEmailRE_BaseNovaLogos(emailReBDAntigo, "", "");
                        MontarEmailRE_BaseNovaLogos("pereira@logoslogistica.com.br", "", "");
                    }

                }


                if (emailsFluxoCaixa.Length > 5)
                {
                    MontarFluxoDeCaixa(emailsFluxoCaixa);
                    emailTotal += "<br>Fluxo de Caixa: <br> Emais: " + emailsFluxoCaixa.Replace(";", "<br>");
                }

                DateTime agora = DateTime.Now;
                int dia_semana = (int)agora.DayOfWeek;

                if (emailPainelFaturamento.Length > 5 && dia_semana==2)
                {
                    MontarPainelDeFaturamento(emailPainelFaturamento);
                    emailTotal += "<br>Painel de Faturamento: <br> Emais: " + emailPainelFaturamento.Replace(";", "<br>");
                }

                if (emailOperacaoCliente.Length > 5)
                {
                    List<DadosOperacaoCliente> ricoy = new List<DadosOperacaoCliente>();
                    DadosOperacaoCliente r = new DadosOperacaoCliente();
                    r.IdCliente = "9";
                    r.NomeFantasia = "Ouro Azul";
                    r.RazaoSocial = "Ouro Azul";
                    ricoy.Add(r);

                    enviarOperacaoCliente(emailOperacaoCliente, ricoy);
                    emailTotal += "<br>Operação Cliente: <br> Emais: " + emailOperacaoCliente.Replace(";", "<br>");
                }


                if (emailOperacaoCliente.Length > 5)
                {
                    List<DadosOperacaoCliente> Barbosa = RetornarClientesBarbosa();
                    enviarOperacaoCliente(emailOperacaoCliente, Barbosa);
                    emailTotal += "<br>Operação Cliente: <br> Emais: " + emailOperacaoCliente.Replace(";", "<br>");
                }

                if (emailTotal.Length > 20 && emailTotal.Contains("@"))
                {
                    emailTotal += " <br> Data: " + DateTime.Now.ToString();
                    Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Aletra Robo de Email", emailTotal, "mail.grupologos.com.br", "logos0902", "SITUACAO DE ENTREGAS");
                }
         
                timer1.Enabled = true;
                Label2.Text = "Rodando";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                reiniciarTimers();

            }
            finally
            {

            }
        }

        //private void twx()
        //{
        //    try
        //    {

        //        GravarLog("Iniciou", "btnTWX_Click");
        //        ProcessarTwx();
        //        GravarLog("Finalizou", "btnTWX_Click");

        //    }
        //    catch (Exception ex)
        //    {
        //        GravarLog("Erro: " + ex.Message, "btnTWX_Click");
        //        reiniciarTimers();
        //    }
        //    finally
        //    {

        //    }
        //}


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

		string totComapracao = "0";

		public void DispararEmailCarga()
		{
			string dest = "edvaldo@sistecno.com.br; gzanatta@vexlogistica.com.br; andremachado@vexlogistica.com.br";

			DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS("exec Prc_Email_Carga", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

			if (totComapracao == dt.Compute("SUM(ValorDaNota)", "").ToString())
				return;

			if(dt.Compute("SUM(ValorDaNota)", "").ToString() != "")
			{
				decimal m = decimal.Parse(dt.Compute("SUM(ValorDaNota)", "").ToString().Replace(",", "."));

				if (m <= decimal.Parse(totComapracao.Replace(",", ".")))
					return;

			}



			totComapracao = dt.Compute("SUM(ValorDaNota)", "").ToString();

			#region Styles

			string html = "<html><head>";
			html += " <STYLE type='text/css'>";
			html += " body ";
			html += " { ";
			html += " margin: 0px; ";
			html += " background-color: #f8f8f8; ";
			html += " font-family: Verdana; ";
			html += " text-align: left; ";
			html += " font-size: 12PX; }";


			html += " .table  ";
			html += " { ";
			html += " background-color: #E0E0E0; ";
			html += " width: 50%; ";
			html += " font-family: Arial, Helvetica, sans-serif; ";
			html += " font-size: 7pt; ";
			html += " font-weight: bold; ";
			html += " } ";

			html += " .tableFundoClaro ";
			html += " { ";
			html += " background-color: #F8F8F8; ";
			html += " width: 100%; ";
			html += " font-family: Arial, Helvetica, sans-serif; ";
			html += " font-size: 7pt; ";
			html += " font-weight: bold; ";
			html += " } ";

			html += " .tableSemCorFundo ";
			html += " {	 ";
			html += " width: 50%; ";
			html += " font-family: Arial, Helvetica, sans-serif; ";
			html += " font-size: 7pt; ";
			html += " font-weight: bold; ";
			html += " } ";

			html += " .table2 ";
			html += " { ";
			html += " background-color:#E0E0E0 ;  ";
			html += " font-family: Arial, Helvetica, sans- ";
			html += " } ";

			html += " .tdpCenter ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " text-align: center ; ";
			html += " nowrap:nowrap; ";
			html += " font-weight:normal; ";
			html += " } ";

			html += " .tdp ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " nowrap:nowrap; ";
			html += " font-weight:normal; ";
			html += " text-align: left; ";
			html += " vertical-align:middle; ";

			html += " } ";
			html += " .tdpSemAlign ";
			html += " { ";
			html += " border: 0.5pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " nowrap:nowrap; ";
			html += " font-weight:normal; ";
			html += " } ";

			html += " .tdpSemAlignGray ";
			html += " { ";
			html += " border: 0.5pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " nowrap:nowrap; ";
			html += " font-weight:normal; ";
			html += " background-color:GrayText; ";
			html += " } ";


			html += " .tdpCenter ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " text-align: center ; ";
			html += " nowrap:now ";
			html += " } ";
			html += " .tdpR ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " text-align:right; ";
			html += " nowrap:nowrap; ";
			html += " font-weight:normal;	 ";
			html += " } ";

			html += "  .tdpVerdana ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " nowrap:nowrap; ";
			html += " } ";

			html += " .tdpCabecalho ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " height: 13pt; ";
			html += " font-size:9pt; ";
			html += " font-family:Verdana; ";
			html += " font-weight:bold; ";
			html += " text-transform: uppercase;	 ";
			html += " } ";

			html += " .tdpRVerdanaVerde ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " text-align: right;	 ";
			html += " nowrap:nowrap; ";
			html += " background-color:#20AE3F; ";
			html += " } ";

			html += " .tdpRVerdanaAmarelo ";
			html += " { ";
			html += " border: 0.1pt solid #FFFFFF; ";
			html += " font-size:8pt; ";
			html += " font-family:Verdana; ";
			html += " text-align: right;	 ";
			html += " nowrap:nowrap; ";
			html += " background-color:#DEDE40; ";
			html += " } ";

			html += " .tdpRVerdanaVermelho ";
			html += " { ";
			html += " 	border: 0.1pt solid #FFFFFF; ";
			html += " 	font-size:8pt; ";
			html += " 	font-family:Verdana; ";
			html += " 	text-align: right;	 ";
			html += " 	nowrap:nowrap; ";
			html += " 	background-color:#DE4040; ";
			html += "} ";

			html += " </STYLE> ";

			html += "</HEAD>";

			html += "<BODY>";
			html += "<div> CARGAS - Aguardando Embarque / Liberado para Separação <div>";


			html += "<BR><BR><TABLE>";

			#endregion

			#region Cabecalho
			html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
			html += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>ENTRADA";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL ORIGEM";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL DESTINO";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CNPJ REMETENTE";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>REMETENTE";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CNPJDESTINATÁRIO";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>DESTINTÁRIO";
			html += "</td>";

			
			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IDDOCUMENTO";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>TIPO DE DOCUMENTO";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NÚMERO";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VOLUMES";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO BRUTO";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
			html += "</td>";

			html += "</tr>";
			#endregion



			for (int i = 0; i < dt.Rows.Count; i++)
			{
				html += "<tr>";
				html += "<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][0].ToString() + "</td>";
				html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][1].ToString() + "</td>";
				html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][2].ToString() + "</td>";
				html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][3].ToString() + "</td>";
				html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][4].ToString() + "</td>";
				html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][5].ToString() + "</td>";
				html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][6].ToString() + "</td>";
				html += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][7].ToString() + "</td>";
				html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][8].ToString() + "</td>";
				html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][9].ToString() + "</td>";
				html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i][10].ToString() + "</td>";
				html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.000}", float.Parse(dt.Rows[i][11].ToString().ToString())) + "</td>";
				html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dt.Rows[i][12].ToString().ToString())) + "</td>";
				html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dt.Rows[i][13].ToString().ToString())) + "</td>";

				
				html += "</tr>";

			}


			#region totais
			html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
			html += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";


			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL";
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + dt.Compute("SUM(Volumes)", "");
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>"+ string.Format("{0:0,0.000}", float.Parse(dt.Compute("SUM(PesoBruto)", "").ToString()));
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", float.Parse(dt.Compute("SUM(ValorDaNota)", "").ToString()));
			html += "</td>";

			html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>"+ string.Format("{0:0,0.00}", float.Parse(dt.Compute("SUM(Frete)", "").ToString()));
			html += "</td>";

			html += "</tr>";
			#endregion


			Sistran.Library.EnviarEmails.EnviarEmail(dest, "moises@sistecno.com.br", "Aviso: CARGAS - Aguardando Embarque / Liberado para Separação", html, "mail.sistecno.com.br", "@oncetsis14", "CARGAS<sistema@grupologos.com.br>");



		}


		private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

			DispararEmailCarga();

           // MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "", "");
           // 
        
           // //Label2.Text = "Timers desativado";
           // //List<DadosOperacaoCliente> ricoy = new List<DadosOperacaoCliente>();
           // //DadosOperacaoCliente r = new DadosOperacaoCliente();
           // //r.IdCliente = "9";
           // //r.NomeFantasia = "Ouro Azul";
           // //r.RazaoSocial = "Ouro Azul";
           // //ricoy.Add(r);
           // //enviarOperacaoCliente("moises@sistecno.com.br", ricoy);  
           // //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br");
           //// MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", false, null, null);

           // //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "moises@Sistecno.com.br");
           // MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "andremachado@vexlogistica.com.br");
           // MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "ecosta@vexlogistica.com.br");
           // MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "gzanatta@vexlogistica.com.br");
           // MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "ilima@vexlogistica.com.br");
           // MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "patricia@vexlogistica.com.br");
           // MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO DIVERSAS", "pereira@logoslogistica.com.br");
         






           // //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO FRACIONADA");
           // //MontarEmailRE_BaseNovaLogos("moises@sistecno.com.br", "OPERACAO");
            
            Application.Exit();
        }

        
        private void enviarEmailPedido()
        {
            try
            {                
                EnviarEmailPd();
            }
            catch (Exception ex )
            {
                GravarLog(ex.Message, "EnviaEmailPedido");

                reiniciarTimers();
            }
            finally
            {
            }
        }


        public void EnviarEmailPd()
        {

            if (checkBox2.Checked == false)
                return;


            Label2.Text = "Inicio de Envio de Email de Pedido";

            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {

                string sql = "SELECT TOP 10 * FROM EMAILPENDENTE WHERE EMAIL <>'' AND DATAHORAENVIO IS NULL";
                DataTable d = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

                if (d.Rows.Count > 0)
                {
                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        message = new MailMessage();
                        client = new SmtpClient();

                        message.From = new MailAddress("website@logospromocional.com.br", "Grupo Logos - Logos Promocional <website@logospromocional.com.br>");
                        message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                        message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);

                        string desti = d.Rows[i]["email"].ToString() + (d.Rows[i]["CC"].ToString() != "" ? "," + d.Rows[i]["cc"].ToString() : "");
                        string[] destinatarios = desti.Split(',');


                        foreach (string dest in destinatarios)
                        {
                            if (dest.Trim() != "")
                            {
                                try
                                {
                                    message.To.Add(dest.Replace("<", "").Replace(">", ""));

                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        
                        string cont = "";
                        cont = d.Rows[i]["Conteudo"].ToString();                    

                        desti = desti.Replace(",", ";");
                        Sistran.Library.EnviarEmails.EnviarEmailPed(desti, "sistema@grupologos.com.br", d.Rows[i]["Assunto"].ToString(), cont, "mail.grupologos.com.br", "logos0902", "Grupo Logos - Logos Promocional <website@logospromocional.com.br>");

                        Label2.AppendText("Enviou Email de Pedido");
                        Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE EMAILPENDENTE SET DATAHORAENVIO=GETDATE() WHERE IdEmailPendente=" + d.Rows[i]["IdEmailPendente"].ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                }
            }
            catch (Exception ex)
            {
                Label2.AppendText(ex.Message + "- Email de Pedido - " + DateTime.Now.ToString());
                GravarLog(ex.Message, "EmailPD");
                reiniciarTimers();
            }
            finally
            {
                message.Dispose();
                message = null;
                client = null;
            }
        }


        private void MontarEmailRE_Mensal_BaseNovaLogos(string email, DateTime Inic, DateTime Fim)
        {
            DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_RE_MENSAL_LISTAR '" + Inic.ToString("yyyy-MM-dd") + "', '" + Fim.ToString("yyyy-MM-dd") + "'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

            #region Styles

            string html = "<html><head>";
            html += " <STYLE type='text/css'>";
            html += " body ";
            html += " { ";
            html += " margin: 0px; ";
            html += " background-color: #f8f8f8; ";
            html += " font-family: Verdana; ";
            html += " text-align: left; ";
            html += " font-size: 12PX; }";


            html += " .table  ";
            html += " { ";
            html += " background-color: #E0E0E0; ";
            html += " width: 50%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .tableFundoClaro ";
            html += " { ";
            html += " background-color: #F8F8F8; ";
            html += " width: 100%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .tableSemCorFundo ";
            html += " {	 ";
            html += " width: 50%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .table2 ";
            html += " { ";
            html += " background-color:#E0E0E0 ;  ";
            html += " font-family: Arial, Helvetica, sans- ";
            html += " } ";

            html += " .tdpCenter ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: center ; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " } ";

            html += " .tdp ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " text-align: left; ";
            html += " vertical-align:middle; ";

            html += " } ";
            html += " .tdpSemAlign ";
            html += " { ";
            html += " border: 0.5pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " } ";

            html += " .tdpSemAlignGray ";
            html += " { ";
            html += " border: 0.5pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " background-color:GrayText; ";
            html += " } ";


            html += " .tdpCenter ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: center ; ";
            html += " nowrap:now ";
            html += " } ";
            html += " .tdpR ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align:right; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal;	 ";
            html += " } ";

            html += "  .tdpVerdana ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " } ";

            html += " .tdpCabecalho ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " height: 13pt; ";
            html += " font-size:9pt; ";
            html += " font-family:Verdana; ";
            html += " font-weight:bold; ";
            html += " text-transform: uppercase;	 ";
            html += " } ";

            html += " .tdpRVerdanaVerde ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: right;	 ";
            html += " nowrap:nowrap; ";
            html += " background-color:#20AE3F; ";
            html += " } ";

            html += " .tdpRVerdanaAmarelo ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: right;	 ";
            html += " nowrap:nowrap; ";
            html += " background-color:#DEDE40; ";
            html += " } ";

            html += " .tdpRVerdanaVermelho ";
            html += " { ";
            html += " 	border: 0.1pt solid #FFFFFF; ";
            html += " 	font-size:8pt; ";
            html += " 	font-family:Verdana; ";
            html += " 	text-align: right;	 ";
            html += " 	nowrap:nowrap; ";
            html += " 	background-color:#DE4040; ";
            html += "} ";

            html += " </STYLE> ";

            html += "</HEAD>";

            html += "<BODY>";
            html += "<div> PERÍODO: " + Inic.ToShortDateString() + " ATÉ " + Fim.ToShortDateString() + " <div>";


            html += "<BR><BR><TABLE>";

            #endregion

            #region Cabecalho
            html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
            html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
            html += "</td>";

            html += "</tr>";
            #endregion

            DataView view = new DataView(dtGeral);
            DataTable dt = view.ToTable(true, "FILIAL");
            dt.Columns.Add("LucroTotal", typeof(decimal));

            float tot_despesas = 0;
            float tot_frete_mot = 0;
            float tot_imposto = 0;
            float tot_seguro = 0;
            float tot_txadm = 0;
            float tot_txTransf = 0;
            float lucro = 0;
            float vl_fret = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                tot_despesas = 0;
                tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_imposto = float.Parse(dtGeral.Compute("SUM(VLIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_seguro = float.Parse(dtGeral.Compute("SUM(VLSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txadm = float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txTransf = float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

                lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                float xx = 0;

                if (vl_fret > 0)
                    xx = (lucro / vl_fret) * 100;

                dt.Rows[i][1] = xx;
            }

            dt.DefaultView.Sort = "LucroTotal desc";
            dt = dt.DefaultView.ToTable(true);

            float vl_nota = 0;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vl_nota = 0;
                vl_fret = 0;
                tot_despesas = 0;
                tot_frete_mot = 0;
                tot_imposto = 0;
                tot_seguro = 0;
                tot_txadm = 0;
                tot_txTransf = 0;
                lucro = 0;

                html += "<tr>";
                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i]["FILIAL"].ToString() + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'") + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";

                vl_nota = float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                if (vl_nota == 0)
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
                else
                {
                    float perc_frete = (vl_fret / vl_nota);
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
                }

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";


                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";


                tot_despesas = 0;
                tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_imposto = float.Parse(dtGeral.Compute("SUM(VLIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_seguro = float.Parse(dtGeral.Compute("SUM(VLSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txadm = float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txTransf = float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

                lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                if (vl_fret > 0)
                {
                    float x = (lucro / vl_fret) * 100;


                    string css = "tdpRVerdanaVermelho";

                    if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                        css = "tdpRVerdanaVerde";
                    else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(7)))
                        css = "tdpRVerdanaAmarelo";
                    else
                        css = "tdpRVerdanaVermelho";


                    html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (x).ToString("#0.00") + "%</td>";
                }

                else
                {
                    html += "<td class='tdpRVerdanaVermelho' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
                }

            }


            #region Totaliza



            html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px;  font-weight: bold;'>";

            html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>TOTAL</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "") + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESO)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "").ToString())) + "</td>";

            vl_nota = float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "").ToString());
            vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "").ToString());

            if (vl_nota == 0)
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
            else
            {
                float perc_frete = (vl_fret / vl_nota);
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
            }

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "").ToString())) + "</td>";

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(adm)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(transf)", "").ToString())) + "</td>";


            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(lucro)", "").ToString())) + "</td>";


            tot_despesas = 0;
            tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "").ToString());
            tot_imposto = float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "").ToString());
            tot_seguro = float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "").ToString());
            tot_txadm = float.Parse(dtGeral.Compute("SUM(adm)", "").ToString());
            tot_txTransf = float.Parse(dtGeral.Compute("SUM(transf)", "").ToString());

            tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

            lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "").ToString());

            if (vl_fret > 0)
            {
                float x = (lucro / vl_fret) * 100;


                string css = "tdpRVerdanaVermelho";

                if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                    css = "tdpRVerdanaVerde";
                else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(7)))
                    css = "tdpRVerdanaAmarelo";
                else
                    css = "tdpRVerdanaVermelho";


                html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (x).ToString("#0.00") + "%</td>";
            }

            else
            {
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
            }

            #endregion



            html += "</BODY>";
            html += "</html>";

            //Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA(Mensal)", html, "mail.grupologos.com.br", "logos0902", "RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA");
            Sistran.Library.EnviarEmails.EnviarEmail(email, "moises@sistecno.com.br", "Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA(Mensal)", html, "mail.sistecno.com.br", "@oncetsis14", "RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA <sistema@grupologos.com.br>");

            Label2.Text = "Enviou a Relação de Entrega";

        }


        private void MontarEmailRE_BaseNovaLogos(string email, string grupo, string emailGrupo)
        {
            DataTable dtGeral = new DataTable();

            if (emailGrupo == "")
                dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_RE_LISTAR  '" + grupo + "'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            else //Email do sr.Pereira -24/03/2017
            {
                dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_RE_LISTAR_EMAIL'" + emailGrupo + "'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                //email = emailGrupo;
            }

            if (dtGeral.Rows.Count == 0)
                return;



            #region Styles

            string html = "<html><head>";
            html += " <STYLE type='text/css'>";
            html += " body ";
            html += " { ";
            html += " margin: 0px; ";
            html += " background-color: #f8f8f8; ";
            html += " font-family: Verdana; ";
            html += " text-align: left; ";
            html += " font-size: 12PX; }";


            html += " .table  ";
            html += " { ";
            html += " background-color: #E0E0E0; ";
            html += " width: 50%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .tableFundoClaro ";
            html += " { ";
            html += " background-color: #F8F8F8; ";
            html += " width: 100%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .tableSemCorFundo ";
            html += " {	 ";
            html += " width: 50%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .table2 ";
            html += " { ";
            html += " background-color:#E0E0E0 ;  ";
            html += " font-family: Arial, Helvetica, sans- ";
            html += " } ";

            html += " .tdpCenter ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: center ; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " } ";

            html += " .tdp ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " text-align: left; ";
            html += " vertical-align:middle; ";

            html += " } ";
            html += " .tdpSemAlign ";
            html += " { ";
            html += " border: 0.5pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " } ";

            html += " .tdpSemAlignGray ";
            html += " { ";
            html += " border: 0.5pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " background-color:GrayText; ";
            html += " } ";


            html += " .tdpCenter ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: center ; ";
            html += " nowrap:now ";
            html += " } ";
            html += " .tdpR ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align:right; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal;	 ";
            html += " } ";

            html += "  .tdpVerdana ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            //html += " text-align: left; ";
            html += " nowrap:nowrap; ";
            html += " } ";

            html += " .tdpCabecalho ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " height: 13pt; ";
            html += " font-size:9pt; ";
            html += " font-family:Verdana; ";
            html += " font-weight:bold; ";
            html += " text-transform: uppercase;	 ";
            html += " } ";

            html += " .tdpRVerdanaVerde ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: right;	 ";
            html += " nowrap:nowrap; ";
            html += " background-color:#20AE3F; ";
            html += " } ";

            html += " .tdpRVerdanaAmarelo ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: right;	 ";
            html += " nowrap:nowrap; ";
            html += " background-color:#DEDE40; ";
            html += " } ";

            html += " .tdpRVerdanaVermelho ";
            html += " { ";
            html += " 	border: 0.1pt solid #FFFFFF; ";
            html += " 	font-size:8pt; ";
            html += " 	font-family:Verdana; ";
            html += " 	text-align: right;	 ";
            html += " 	nowrap:nowrap; ";
            html += " 	background-color:#DE4040; ";
            html += "} ";

            html += " </STYLE> ";

            html += "</HEAD>";

            html += "<BODY>";
            html += "<TABLE>";

            #endregion

            #region Cabecalho
            html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
            html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>%FRETE MOT";
            html += "</td>";


            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
            html += "</td>";

            html += "</tr>";
            #endregion

            DataView view = new DataView(dtGeral);
            DataTable dt = view.ToTable(true, "FILIAL");
            dt.Columns.Add("LucroTotal", typeof(decimal));

            float tot_despesas = 0;
            float tot_frete_mot = 0;
            float tot_imposto = 0;
            float tot_seguro = 0;
            float tot_txadm = 0;
            float tot_txTransf = 0;
            float lucro = 0;
            float vl_fret = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                tot_despesas = 0;
                tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_imposto = float.Parse(dtGeral.Compute("SUM(VLIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_seguro = float.Parse(dtGeral.Compute("SUM(VLSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txadm = float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txTransf = float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

                lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                float xx = 0;

                if (vl_fret > 0)
                    xx = (lucro / vl_fret) * 100;

                dt.Rows[i][1] = xx;
            }

            dt.DefaultView.Sort = "LucroTotal desc";
            dt = dt.DefaultView.ToTable(true);

            float vl_nota = 0;
            // float vl_fret = 0;

            //float tot_despesas = 0;
            //float tot_frete_mot = 0;
            //float tot_imposto = 0;
            //float tot_seguro = 0;
            //float tot_txadm = 0;
            //float tot_txTransf = 0;
            //float lucro = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vl_nota = 0;
                vl_fret = 0;
                tot_despesas = 0;
                tot_frete_mot = 0;
                tot_imposto = 0;
                tot_seguro = 0;
                tot_txadm = 0;
                tot_txTransf = 0;
                lucro = 0;

                html += "<tr>";
                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i]["FILIAL"].ToString() + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'") + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";

                vl_nota = float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                if (vl_nota == 0)
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
                else
                {
                    float perc_frete = (vl_fret / vl_nota);
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
                }

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";

                float freteMoto = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                if (vl_fret > 0)
                    freteMoto = (freteMoto / vl_fret) * 100;
                else
                    freteMoto = 0;

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", freteMoto) + "%</td>";


                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";


                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";


                tot_despesas = 0;
                tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_imposto = float.Parse(dtGeral.Compute("SUM(VLIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_seguro = float.Parse(dtGeral.Compute("SUM(VLSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txadm = float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txTransf = float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

                lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                if (vl_fret > 0)
                {
                    float x = (lucro / vl_fret) * 100;


                    string css = "tdpRVerdanaVermelho";

                    if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                        css = "tdpRVerdanaVerde";
                    else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(7)))
                        css = "tdpRVerdanaAmarelo";
                    else
                        css = "tdpRVerdanaVermelho";


                    html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (x).ToString("#0.00") + "%</td>";
                }

                else
                {
                    html += "<td class='tdpRVerdanaVermelho' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
                }

            }


            #region Totaliza



            html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px;  font-weight: bold;'>";

            html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>TOTAL</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "") + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESO)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "").ToString())) + "</td>";

            vl_nota = float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "").ToString());
            vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "").ToString());

            if (vl_nota == 0)
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
            else
            {
                float perc_frete = (vl_fret / vl_nota);
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
            }

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "").ToString())) + "</td>";

            float frMot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "").ToString());

            if (vl_fret > 0)
                frMot = (frMot / vl_fret) * 100;
            else
                frMot = 0;

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", frMot) + "%</td>";

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(adm)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(transf)", "").ToString())) + "</td>";


            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(lucro)", "").ToString())) + "</td>";


            tot_despesas = 0;
            tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "").ToString());
            tot_imposto = float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "").ToString());
            tot_seguro = float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "").ToString());
            tot_txadm = float.Parse(dtGeral.Compute("SUM(adm)", "").ToString());
            tot_txTransf = float.Parse(dtGeral.Compute("SUM(transf)", "").ToString());

            tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

            lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "").ToString());

            if (vl_fret > 0)
            {
                float x = (lucro / vl_fret) * 100;


                string css = "tdpRVerdanaVermelho";

                if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                    css = "tdpRVerdanaVerde";
                else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(7)))
                    css = "tdpRVerdanaAmarelo";
                else
                    css = "tdpRVerdanaVermelho";


                html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (x).ToString("#0.00") + "%</td>";
            }

            else
            {
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
            }

            #endregion


            html += "</TABLE>";

            html += "<hr>";
            html += "<br>";


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataRow[] linhas = dtGeral.Select("FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'", "PER_LUCRO DESC");
                html += "<table border='0'>";

                html += "<tr>";
                html += " <TD COLSPAN=4 > <B>FILIAL: " + dt.Rows[i]["FILIAL"].ToString().ToUpper() + " <B> <TD>";
                html += "</tr>";

                html += "<tr>";

                html += "<td>";
                html += "<b>IMPOSTOS:</b> " + linhas[0]["Imposto"].ToString() + " %";
                html += "</td>";


                html += "<td>";
                html += "<b>SEGURO: </b>" + linhas[0]["Seguro"].ToString() + " %";
                html += "</td>";

                html += "<td>";
                html += "<b>TAXA ADMINISTRATIVA: </b>" + linhas[0]["TaxaAdministrativa"].ToString() + " %";
                html += "</td>";

                html += "<td>";
                html += "<b>TAXA TRANFERÊNCIA: </b>" + linhas[0]["TaxaDeTranferencia"].ToString() + " %";
                html += "</td>";

                html += "</tr>";



                html += "<tr>";

                html += "<td>";
                html += "<b>GERENTES:</b> " + (linhas[0]["GERENTE"].ToString() == "" ? "S/CAD." : linhas[0]["GERENTE"].ToString());
                html += "</td>";


                html += "<td>";
                html += "<b>ASSISTENTES:</b> " + (linhas[0]["ASSISTENTE"].ToString() == "" ? "S/CAD." : linhas[0]["ASSISTENTE"].ToString());
                html += "</td>";


                html += "<td>";
                html += "<b>LÍDERES OPERACIONAIS:</b> " + (linhas[0]["LIDEROPERACIONAL"].ToString() == "" ? "S/CAD." : linhas[0]["LIDEROPERACIONAL"].ToString());
                html += "</td>";


                html += "<td>";
                html += "<b>CONFERENTES:</b> " + (linhas[0]["CONFERENTE"].ToString() == "" ? "S/CAD." : linhas[0]["CONFERENTE"].ToString());
                html += "</td>";


                html += "<td>";
                html += "<b>SEPARADORES:</b> " + (linhas[0]["SEPARADOR"].ToString() == "" ? "S/CAD." : linhas[0]["SEPARADOR"].ToString());
                html += "</td>";

                html += "<td>";
                html += "<b>LIMPEZA:</b> " + (linhas[0]["LIMPEZA"].ToString() == "" ? "S/CAD." : linhas[0]["LIMPEZA"].ToString());
                html += "</td>";

                html += "<td>";
                html += "<b>OUTROS:</b> " + (linhas[0]["OUTROS"].ToString() == "" ? "S/CAD." : linhas[0]["OUTROS"].ToString());
                html += "</td>";



                html += "</tr>";
                html += "</TABLE>";
                html += "<hr>";


                ///////////////////////////////////////////////////////////////
                #region Cabecalho
                html += "<table class='table' cellspacing=1 celpanding=1 >";
                html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>DT";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>EMISSAO";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>MOTORISTA";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>CAP. CARGA KG";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE MOT";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
                html += "</td>";

                html += "</tr>";


                for (int io = 0; io < linhas.Length; io++)
                {

                    html += "<tr>";
                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["NUMERO"].ToString() + "</td>";

                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px;^text-align:center'>" + DateTime.Parse(linhas[io]["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                    html += "</td>";

                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["MOTORISTA"].ToString() + "</td>";
                    html += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["CAPACIDADEDECARGAKG"].ToString() + "</td>";

                    html += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + linhas[io]["ENTREGAS"].ToString() + "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PESO"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VALOR_DA_NOTA"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VALOR_DO_FRETE"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PERC_FRETE"].ToString())) + "%";
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["FRETE_MOTORISTA"].ToString()));
                    html += "</td>";



                    float frmot = float.Parse(linhas[io]["FRETE_MOTORISTA"].ToString());
                    float vlFrete = float.Parse(linhas[io]["VALOR_DO_FRETE"].ToString());

                    if (vlFrete > 0)
                        frmot = (frmot / vlFrete) * 100;
                    else
                        frmot = 0;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", frmot);
                    html += "%</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VLIMPOSTOS"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VLSEGURO"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["ADM"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["TRANSF"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["LUCRO"].ToString()));
                    html += "</td>";


                    string cs = "";
                    if ((decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) >= Convert.ToDecimal(12))
                        cs = "tdpRVerdanaVerde";
                    else if ((decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) < Convert.ToDecimal(12) && (decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) >= Convert.ToDecimal(10))
                        cs = "tdpRVerdanaAmarelo";
                    else
                        cs = "tdpRVerdanaVermelho";

                    html += "<td class='" + cs + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) + "%";
                    html += "</td>";

                    html += "</tr>";
                }
                #endregion


                #region Totaliza Filial
                html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>";
                html += "</td>";
                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>";
                html += "</td>";



                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + dtGeral.Compute("SUM(ENTREGAS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'");
                html += "</td>";

                //+string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PESO"].ToString()));
                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(PESO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";


                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";


                vl_nota = float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                float vl_frete = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                float result = 0;

                if (vl_nota > 0)
                    result = (vl_frete / vl_nota) * float.Parse("100");

                //FRETE_MOTORISTA
                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", result);
                html += "%</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";



                vl_frete = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                float vlMot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                result = 0;

                if (vl_nota > 0)
                    result = (vlMot / vl_frete) * float.Parse("100");


                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", result);
                html += "%</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(VLIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(VLSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + string.Format("{0:0,0.00}", dtGeral.Compute("SUM(LUCRO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'"));
                html += "</td>";

                float vl_lucro = float.Parse(dtGeral.Compute("SUM(LUCRO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                result = 0;
                if (vl_frete > 0)
                    result = (vl_lucro / vl_frete) * float.Parse("100");

                string csss = "";
                if (Convert.ToDecimal(result) >= Convert.ToDecimal(12))
                    csss = "tdpRVerdanaVerde";
                else if (Convert.ToDecimal(result) < Convert.ToDecimal(12) && (Convert.ToDecimal(result) >= Convert.ToDecimal(10)))
                    csss = "tdpRVerdanaAmarelo";
                else
                    csss = "tdpRVerdanaVermelho";

                html += "<td class='" + csss + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", result) + "%";
                html += "</td>";

                html += "</tr>";
                #endregion

                html += "</table>";
                html += "<hr>";
                html += "<br>";
            }

            html += "</BODY>";
            html += "</html>";

            if (grupo == "OPERACAO")
                grupo = " - CONSOLIDAÇÃO ";

            if (emailGrupo == "")
                Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA " + grupo + "(Diário*)", html, "mail.grupologos.com.br", "logos0902", "RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA");
            else
                Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "  Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA (Diário *) ", html, "mail.grupologos.com.br", "logos0902", "RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA");

            Label2.Text = "Enviou a Relação de Entrega";
            Application.DoEvents();
        }

        private void MontarEmailRERegiao_BaseNovaLogos(string email, bool mensal, DateTime? de, DateTime? ate, string GrupoDeEnvioEmail)
        {
            DataTable dtGeral;

            if (mensal == false)
                dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_RESULTADO_RE null, '2016-01-22', '2016-01-22', 1,  'dt'  ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            else
            {
                string S = "SELECT IDDT, IDROMANEIO,REGIAO,NUMERO,EMISSAO,PLACA,PMNOME,TIPODEDT,FLNUMERO,FLNOME,FILIAL,TRNOME,CAPACIDADEDECARGAKG,VOLUMES,VALORDANOTA, isnull( PBRUTOTOTAL,0) PBRUTOTOTAL, isnull(PESOBRUTO,0) PESOBRUTO,NOTASFISCAIS,ENTREGAS,FRETEMOTORISTARATEADO,FRETEMOTORISTA,FRETEEMPRESA,REENTREGA,QTD_REENTREGA,GERENTE,ASSISTENTE,LIDEROPERACIONAL,CONFERENTE,SEPARADOR,LIMPEZA,OUTROS, isnull(EMPILHADOR,0) EMPILHADOR,isnull(F.IMPOSTO,0) IMPOSTO , isnull(F.SEGURO,0) SEGURO , isnull(F.TAXAADMINISTRATIVA,0) TAXAADMINISTRATIVA, isnull(F.TAXADETRANFERENCIA,0) TAXADETRANFERENCIA  FROM FACE_RE  RE WITH (NOLOCK) INNER JOIN FILIAL   F WITH (NOLOCK)   ON F.NOME = FLNOME ";
                S += " WHERE  0=0 AND  EMISSAO BETWEEN '" + ((DateTime)de).ToString("yyyy-MM-dd") + "' AND '" + ((DateTime)ate).ToString("yyyy-MM-dd") + "' AND FLNOME <> 'VEX TRANSFERENCIA' AND FLNOME <> 'MATRIZ'  AND FLNOME <> 'VEX BARBOSA'   AND F.ATIVO='SIM'";
               // S += " and f.IdFilial=51";

                if (GrupoDeEnvioEmail != "")
                {
                    S = S.Replace("0=0", "GrupoDeEnvioEmail ='" + GrupoDeEnvioEmail + "' ");
                }


                dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS(S, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            }
            #region Styles

            string html = "<html><head>";
            html += " <STYLE type='text/css'>";
            html += " body ";
            html += " { ";
            html += " margin: 0px; ";
            html += " background-color: #f8f8f8; ";
            html += " font-family: Verdana; ";
            html += " text-align: left; ";
            html += " font-size: 12PX; }";


            html += " .table  ";
            html += " { ";
            html += " background-color: #E0E0E0; ";
            html += " width: 50%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .tableFundoClaro ";
            html += " { ";
            html += " background-color: #F8F8F8; ";
            html += " width: 100%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .tableSemCorFundo ";
            html += " {	 ";
            html += " width: 50%; ";
            html += " font-family: Arial, Helvetica, sans-serif; ";
            html += " font-size: 7pt; ";
            html += " font-weight: bold; ";
            html += " } ";

            html += " .table2 ";
            html += " { ";
            html += " background-color:#E0E0E0 ;  ";
            html += " font-family: Arial, Helvetica, sans- ";
            html += " } ";

            html += " .tdpCenter ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: center ; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " } ";

            html += " .tdp ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " text-align: left; ";
            html += " vertical-align:middle; ";

            html += " } ";
            html += " .tdpSemAlign ";
            html += " { ";
            html += " border: 0.5pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " } ";

            html += " .tdpSemAlignGray ";
            html += " { ";
            html += " border: 0.5pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal; ";
            html += " background-color:GrayText; ";
            html += " } ";


            html += " .tdpCenter ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: center ; ";
            html += " nowrap:now ";
            html += " } ";
            html += " .tdpR ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align:right; ";
            html += " nowrap:nowrap; ";
            html += " font-weight:normal;	 ";
            html += " } ";

            html += "  .tdpVerdana ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            //html += " text-align: left; ";
            html += " nowrap:nowrap; ";
            html += " } ";

            html += " .tdpCabecalho ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " height: 13pt; ";
            html += " font-size:9pt; ";
            html += " font-family:Verdana; ";
            html += " font-weight:bold; ";
            html += " text-transform: uppercase;	 ";
            html += " } ";

            html += " .tdpRVerdanaVerde ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: right;	 ";
            html += " nowrap:nowrap; ";
            html += " background-color:#20AE3F; ";
            html += " } ";

            html += " .tdpRVerdanaAmarelo ";
            html += " { ";
            html += " border: 0.1pt solid #FFFFFF; ";
            html += " font-size:8pt; ";
            html += " font-family:Verdana; ";
            html += " text-align: right;	 ";
            html += " nowrap:nowrap; ";
            html += " background-color:#DEDE40; ";
            html += " } ";

            html += " .tdpRVerdanaVermelho ";
            html += " { ";
            html += " 	border: 0.1pt solid #FFFFFF; ";
            html += " 	font-size:8pt; ";
            html += " 	font-family:Verdana; ";
            html += " 	text-align: right;	 ";
            html += " 	nowrap:nowrap; ";
            html += " 	background-color:#DE4040; ";
            html += "} ";

            html += " </STYLE> ";

            html += "</HEAD>";

            html += "<BODY>";

            if (mensal)
            {
                html += "<div> Período de: "+((DateTime)de).ToString("dd/MM/yyyy")+"  até "+((DateTime)ate).ToString("dd/MM/yyyy")+" </div><br>";
            }

            html += "<TABLE>";

            #endregion

            #region Cabecalho
            html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
            html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE MOT";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANSF";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>REENTREGA";
            html += "</td>";


            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>GERENTES";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>ASSISTENTES";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>LIDERES OP.";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CONFERENTES";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>SEPARADORES";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>LIMPEZA";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>EMPILHADOR";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>OUTROS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL DE FUNC.";
            html += "</td>";
            

            html += "</tr>";
            #endregion

            DataView view = new DataView(dtGeral);
            DataTable dt = view.ToTable(true, "FLNOME", "IMPOSTO", "TAXAADMINISTRATIVA", "TAXADETRANFERENCIA", "SEGURO", "GERENTE","ASSISTENTE","LIDEROPERACIONAL","CONFERENTE","SEPARADOR","LIMPEZA","OUTROS", "EMPILHADOR");
            dt.Columns.Add("LucroTotal", typeof(decimal));

            float tot_reentrega = 0;
            float tot_imposto = 0;
            float tot_seguro = 0;
            float tot_txadm = 0;
            float tot_txTransf = 0;
            float tot_lucro = 0;
            float lucro = 0;
            float vl_fret = 0;
            float vl_nota = 0;


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                lucro = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                vl_nota = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                lucro = lucro - float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                float tx = (float.Parse(dt.Rows[i]["Imposto"].ToString()) / 100);
                float vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

                lucro = lucro - vlcalc;

                tx = (float.Parse(dt.Rows[i]["SEGURO"].ToString()) / 100);
                vlcalc = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

                lucro = lucro - vlcalc;

                tx = (float.Parse(dt.Rows[i]["TaxaAdministrativa"].ToString()) / 100);
                vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

                lucro = lucro - vlcalc;

                tx = (float.Parse(dt.Rows[i]["TaxaDeTranferencia"].ToString()) / 100);
                vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

                lucro = lucro - vlcalc;

                vlcalc = float.Parse(dtGeral.Compute("SUM(REENTREGA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                lucro = lucro - vlcalc;

                //lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                float xx = 0;

                if (vl_fret > 0)
                    xx = (lucro / vl_fret) * 100;

                dt.Rows[i][13] = xx;
            }




            dt.DefaultView.Sort = "LucroTotal desc";
            dt = dt.DefaultView.ToTable(true);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vl_nota = 0;
                vl_fret = 0;
                lucro = 0;

                html += "<tr>";
                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i]["FLNOME"].ToString() + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "FLNOME='" + dt.Rows[i]["FLNOME"].ToString() + "'") + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESOBRUTO)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";

                lucro = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                vl_nota = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                if (vl_nota == 0)
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
                else
                {
                    float perc_frete = (vl_fret / vl_nota);
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
                }

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";
                lucro = lucro - float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                float calcPerc = float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                if(vl_fret>0)
                    calcPerc = (calcPerc/vl_fret)*100;
                else
                    calcPerc = 0;

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", calcPerc) + "%</td>";


                float tx = (float.Parse(dt.Rows[i]["Imposto"].ToString()) / 100);
                float vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
                lucro = lucro - vlcalc;
                tot_imposto += vlcalc;

                tx = (float.Parse(dt.Rows[i]["SEGURO"].ToString()) / 100);
                vlcalc = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
                lucro = lucro - vlcalc;
                tot_seguro += vlcalc;

                tx = (float.Parse(dt.Rows[i]["TaxaAdministrativa"].ToString()) / 100);
                vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
                lucro = lucro - vlcalc;
                tot_txadm += vlcalc;

                tx = (float.Parse(dt.Rows[i]["TaxaDeTranferencia"].ToString()) / 100);
                vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
                lucro = lucro - vlcalc;
                tot_txTransf += vlcalc;


                vlcalc = float.Parse(dtGeral.Compute("SUM(REENTREGA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
                lucro = lucro - vlcalc;
                tot_reentrega += vlcalc;


                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", lucro) + "</td>";
                tot_lucro += lucro;

                if (vl_fret > 0)
                {
                    float x = (lucro / vl_fret) * 100;


                    string css = "tdpRVerdanaVermelho";

                    if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                        css = "tdpRVerdanaVerde";
                    else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(7)))
                        css = "tdpRVerdanaAmarelo";
                    else
                        css = "tdpRVerdanaVermelho";


                    html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (x).ToString("#0.00") + "%</td>";
                }

                else
                {
                    html += "<td class='tdpRVerdanaVermelho' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
                }

               
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["GERENTE"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["GERENTE"].ToString()) + "</td>";
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["ASSISTENTE"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["ASSISTENTE"].ToString()) + "</td>";
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["LIDEROPERACIONAL"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["LIDEROPERACIONAL"].ToString()) + "</td>";
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["CONFERENTE"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["CONFERENTE"].ToString()) + "</td>";
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["SEPARADOR"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["SEPARADOR"].ToString()) + "</td>";
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["LIMPEZA"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["LIMPEZA"].ToString()) + "</td>";
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["EMPILHADOR"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["EMPILHADOR"].ToString()) + "</td>";
                html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["OUTROS"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["OUTROS"].ToString()) + "</td>";


                int totFuncFilial = (dt.Rows[i]["GERENTE"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["GERENTE"].ToString()));
                 totFuncFilial += (dt.Rows[i]["ASSISTENTE"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["ASSISTENTE"].ToString()));
                 totFuncFilial += (dt.Rows[i]["LIDEROPERACIONAL"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["LIDEROPERACIONAL"].ToString()));
                 totFuncFilial += (dt.Rows[i]["CONFERENTE"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["CONFERENTE"].ToString()));
                 totFuncFilial += (dt.Rows[i]["SEPARADOR"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["SEPARADOR"].ToString()));
                 totFuncFilial += (dt.Rows[i]["LIMPEZA"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["LIMPEZA"].ToString()));
                 totFuncFilial += (dt.Rows[i]["OUTROS"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["OUTROS"].ToString()));
                 totFuncFilial += (dt.Rows[i]["EMPILHADOR"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["EMPILHADOR"].ToString()));

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px; backgroud-color:#CCC'><b>" + totFuncFilial + "</b></td>";

              


            }


            #region Totaliza



            html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px;  font-weight: bold;'>";

            html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>TOTAL</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "") + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESOBRUTO)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "").ToString())) + "</td>";

            vl_nota = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "").ToString());
            vl_fret = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "").ToString());

            if (vl_nota == 0)
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
            else
            {
                float perc_frete = (vl_fret / vl_nota);
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
            }

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "").ToString())) + "</td>";


            float totPercmot = float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "").ToString());
            if(vl_fret>0)
                totPercmot = (totPercmot/vl_fret)*100;
            else
                totPercmot = 0;

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", totPercmot) + "%</td>";


            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", tot_imposto) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", tot_seguro) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", tot_txadm) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", tot_txTransf) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", tot_reentrega) + "</td>";

            // tot_lucro = tot_lucro - tot_imposto - tot_seguro - tot_txTransf - tot_txadm - tot_reentrega;
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", tot_lucro) + "</td>";


            lucro = tot_lucro;

            if (vl_fret > 0)
            {
                float x = (lucro / vl_fret) * 100;


                string css = "tdpRVerdanaVermelho";

                if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                    css = "tdpRVerdanaVerde";
                else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(7)))
                    css = "tdpRVerdanaAmarelo";
                else
                    css = "tdpRVerdanaVermelho";


                html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (x).ToString("#0.00") + "%</td>";
            }

            else
            {
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
            }



            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(GERENTE)", "").ToString() + "</b></td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(ASSISTENTE)", "").ToString() + "</b></td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIDEROPERACIONAL)", "").ToString() + "</b></td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(CONFERENTE)", "").ToString() + "</b></td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(SEPARADOR)", "").ToString() + "</b></td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIMPEZA)", "").ToString() + "</b></td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(EMPILHADOR)", "").ToString() + "</b></td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(OUTROS)", "").ToString() + "</b></td>";


            int totTotFunc = int.Parse(dt.Compute("SUM(GERENTE)", "").ToString());
            totTotFunc += int.Parse(dt.Compute("SUM(ASSISTENTE)", "").ToString());
            totTotFunc += int.Parse(dt.Compute("SUM(LIDEROPERACIONAL)", "").ToString());
            totTotFunc += int.Parse(dt.Compute("SUM(CONFERENTE)", "").ToString());
            totTotFunc += int.Parse(dt.Compute("SUM(SEPARADOR)", "").ToString());
            totTotFunc += int.Parse(dt.Compute("SUM(LIMPEZA)", "").ToString());
            totTotFunc += int.Parse(dt.Compute("SUM(EMPILHADOR)", "").ToString());
            totTotFunc += int.Parse(dt.Compute("SUM(OUTROS)", "").ToString());

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + totTotFunc + "</b></td>";

            



            

            #endregion


            html += "</TABLE>";

            if (mensal == false)
            {

                html += "<hr>";
                html += "<br>";

                dtGeral.Columns.Add("PercOrdem", System.Type.GetType("System.Decimal"));

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow[] linhas = dtGeral.Select("FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'", "NUMERO");

                    //calcula a ordenacao

                    for (int ic = 0; ic < linhas.Length; ic++)
                    {
                        float lucrMotReg = float.Parse(linhas[ic]["FRETEEMPRESA"].ToString());
                        lucrMotReg -= float.Parse(linhas[ic]["fretemotoristaRateado"].ToString());

                        float tx = (float.Parse(linhas[ic]["seguro"].ToString()) / 100);
                        float vlcalc = float.Parse(linhas[ic]["VALORDANOTA"].ToString()) * tx;
                        lucrMotReg -= vlcalc;


                        tx = (float.Parse(linhas[ic]["TaxaAdministrativa"].ToString()) / 100);
                        vlcalc = float.Parse(linhas[ic]["FRETEEMPRESA"].ToString()) * tx;
                        lucrMotReg -= vlcalc;


                        tx = (float.Parse(linhas[ic]["TaxaDeTranferencia"].ToString()) / 100);
                        vlcalc = float.Parse(linhas[ic]["FRETEEMPRESA"].ToString()) * tx;
                        lucrMotReg -= vlcalc;

                        lucrMotReg -= float.Parse(linhas[ic]["REENTREGA"].ToString());

                        lucrMotReg = (lucrMotReg / float.Parse(linhas[ic]["FRETEEMPRESA"].ToString())) * 100;

                        linhas[ic]["PercOrdem"] = lucrMotReg;

                    }


                    html += "<table border='0'>";
                    html += "<tr>";
                    html += " <TD COLSPAN=4 > <B>FILIAL: " + dt.Rows[i]["FLNOME"].ToString().ToUpper() + " <B> <TD>";
                    html += "</tr>";

                    html += "<tr>";

                    html += "<td>";
                    html += "IMPOSTOS: " + linhas[0]["Imposto"].ToString() + " %";
                    html += "</td>";


                    html += "<td>";
                    html += "SEGURO: " + linhas[0]["Seguro"].ToString() + " %";
                    html += "</td>";

                    html += "<td>";
                    html += "TAXA ADMINISTRATIVA: " + linhas[0]["TaxaAdministrativa"].ToString() + " %";
                    html += "</td>";

                    html += "<td>";
                    html += "TAXA TRANFERÊNCIA: " + linhas[0]["TaxaDeTranferencia"].ToString() + " %";
                    html += "</td>";

                    html += "</tr>";
                    html += "</TABLE>";

                    html += "<hr>";


                    ///////////////////////////////////////////////////////////////
                    #region Cabecalho
                    html += "<table class='table' cellspacing=1 celpanding=1 >";
                    html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                    html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>DT";
                    html += "</td>";

                    html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>EMISSAO";
                    html += "</td>";

                    html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>REGIAO";
                    html += "</td>";

                    html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>MOTORISTA";
                    html += "</td>";

                 

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRASNF";
                    html += "</td>";


                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>REENTREGA";
                    html += "</td>";

                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
                    html += "</td>";


                    html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
                    html += "</td>";

                    html += "</tr>";


                    var result = from row in linhas
                                 orderby row["NUMERO"] descending
                                 //                             orderby row["PercOrdem"], row["NUMERO"] descending
                                 select row;

                    linhas = result.ToArray();

                    for (int io = 0; io < linhas.Length; io++)
                    {

                        html += "<tr>";
                        html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["NUMERO"].ToString() + "</td>";

                        html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px;^text-align:center'>" + DateTime.Parse(linhas[io]["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                        html += "</td>";

                        html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["REGIAO"].ToString() + "</td>";
                        html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["PMNOME"].ToString() + "</td>";
                        html += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + linhas[io]["ENTREGAS"].ToString() + "</td>";

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PESOBRUTO"].ToString()));
                        html += "</td>";

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VALORDANOTA"].ToString()));
                        html += "</td>";

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["FRETEEMPRESA"].ToString()));
                        html += "</td>";

                        decimal vl = (decimal.Parse(linhas[io]["FRETEEMPRESA"].ToString()) / decimal.Parse(linhas[io]["VALORDANOTA"].ToString())) * 100;

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vl) + "%";
                        html += "</td>";

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["fretemotoristaRateado"].ToString()));
                        html += "</td>";


                        float tx = (float.Parse(linhas[io]["Imposto"].ToString()) / 100);

                        float vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;

                        float lucrMotReg = float.Parse(linhas[io]["FRETEEMPRESA"].ToString());
                        lucrMotReg -= float.Parse(linhas[io]["fretemotoristaRateado"].ToString());

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                        html += "</td>";


                        tx = (float.Parse(linhas[io]["seguro"].ToString()) / 100);
                        vlcalc = float.Parse(linhas[io]["VALORDANOTA"].ToString()) * tx;

                        lucrMotReg -= vlcalc;

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                        html += "</td>";

                        tx = (float.Parse(linhas[io]["TaxaAdministrativa"].ToString()) / 100);
                        vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;
                        lucrMotReg -= vlcalc;

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                        html += "</td>";


                        tx = (float.Parse(linhas[io]["TaxaDeTranferencia"].ToString()) / 100);
                        vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;
                        lucrMotReg -= vlcalc;

                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                        html += "</td>";


                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(linhas[io]["REENTREGA"].ToString()));
                        html += "</td>";
                        lucrMotReg -= float.Parse(linhas[io]["REENTREGA"].ToString());


                        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", lucrMotReg);
                        html += "</td>";


                        lucrMotReg = (lucrMotReg / float.Parse(linhas[io]["FRETEEMPRESA"].ToString())) * 100;

                        string cs = "";
                        if (lucrMotReg >= float.Parse("12"))
                            cs = "tdpRVerdanaVerde";
                        else if (lucrMotReg < float.Parse("12") && lucrMotReg >= float.Parse("10"))
                            cs = "tdpRVerdanaAmarelo";
                        else
                            cs = "tdpRVerdanaVermelho";

                        html += "<td class='" + cs + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", lucrMotReg) + "%";
                        html += "</td>";

                        html += "</tr>";
                    }
                    #endregion
                    html += "</table>";
                    html += "<hr>";
                    html += "<br>";



                }


            }


            html += "</BODY>";
            html += "</html>";

            if (mensal == false)
                Sistran.Library.EnviarEmails.EnviarEmail(email, "moises@sistecno.com.br", "Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA(Diário/Região)", html, "mail.sistecno.com.br", "@oncetsis14", "RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA");
            else
                Sistran.Library.EnviarEmails.EnviarEmail(email, "moises@sistecno.com.br", "Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA - "+GrupoDeEnvioEmail+"    (Período / Região)", html, "mail.sistecno.com.br", "@oncetsis14", "RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA");

            webBrowser1.DocumentText = html;

            Label2.Text = "Enviou a Relação de Entrega";

        }


        /// <summary>
        /// Envia o relatorio em relação a base GrupoLogos do Sistranet 2
        /// </summary>
        /// <param name="p"></param>
        //        private void MontarEmailRE_Novo(string email)
        //        {


        //            dtTodosOsDados = new DataTable();
        //            dtTodosOsDados = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_PRODUCAOREDIARIO_RESUMIDO ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


        //            sstr = "<table class='table' cellspacing=1 celpanding=1 >";

        //            if (dtTodosOsDados.Rows.Count > 0)
        //            {
        //                #region Cabecalho
        //                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        //                sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
        //                sstr += "</td>";

        //                sstr += "</tr>";
        //                #endregion

        //                #region Stilos

        //                string Tba = "<html><head>";
        //                Tba += " <STYLE type='text/css'>";
        //                Tba += " body ";
        //                Tba += " { ";
        //                Tba += " margin: 0px; ";
        //                Tba += " background-color: #f8f8f8; ";
        //                Tba += " font-family: Verdana; ";
        //                Tba += " text-align: left; ";
        //                Tba += " font-size: 12PX; }";


        //                Tba += " .table  ";
        //                Tba += " { ";
        //                Tba += " background-color: #E0E0E0; ";
        //                Tba += " width: 50%; ";
        //                Tba += " font-family: Arial, Helvetica, sans-serif; ";
        //                Tba += " font-size: 7pt; ";
        //                Tba += " font-weight: bold; ";
        //                Tba += " } ";

        //                Tba += " .tableFundoClaro ";
        //                Tba += " { ";
        //                Tba += " background-color: #F8F8F8; ";
        //                Tba += " width: 100%; ";
        //                Tba += " font-family: Arial, Helvetica, sans-serif; ";
        //                Tba += " font-size: 7pt; ";
        //                Tba += " font-weight: bold; ";
        //                Tba += " } ";

        //                Tba += " .tableSemCorFundo ";
        //                Tba += " {	 ";
        //                Tba += " width: 50%; ";
        //                Tba += " font-family: Arial, Helvetica, sans-serif; ";
        //                Tba += " font-size: 7pt; ";
        //                Tba += " font-weight: bold; ";
        //                Tba += " } ";

        //                Tba += " .table2 ";
        //                Tba += " { ";
        //                Tba += " background-color:#E0E0E0 ;  ";
        //                Tba += " font-family: Arial, Helvetica, sans- ";
        //                Tba += " } ";

        //                Tba += " .tdpCenter ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " text-align: center ; ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " font-weight:normal; ";
        //                Tba += " } ";

        //                Tba += " .tdp ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " font-weight:normal; ";
        //                Tba += " text-align: left; ";
        //                Tba += " vertical-align:middle; ";

        //                Tba += " } ";
        //                Tba += " .tdpSemAlign ";
        //                Tba += " { ";
        //                Tba += " border: 0.5pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " font-weight:normal; ";
        //                Tba += " } ";

        //                Tba += " .tdpSemAlignGray ";
        //                Tba += " { ";
        //                Tba += " border: 0.5pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " font-weight:normal; ";
        //                Tba += " background-color:GrayText; ";
        //                Tba += " } ";


        //                Tba += " .tdpCenter ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " text-align: center ; ";
        //                Tba += " nowrap:now ";
        //                Tba += " } ";
        //                Tba += " .tdpR ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " text-align:right; ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " font-weight:normal;	 ";
        //                Tba += " } ";

        //                Tba += "  .tdpVerdana ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                //Tba += " text-align: left; ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " } ";

        //                Tba += " .tdpCabecalho ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " height: 13pt; ";
        //                Tba += " font-size:9pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " font-weight:bold; ";
        //                Tba += " text-transform: uppercase;	 ";
        //                Tba += " } ";

        //                Tba += " .tdpRVerdanaVerde ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " text-align: right;	 ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " background-color:#20AE3F; ";
        //                Tba += " } ";

        //                Tba += " .tdpRVerdanaAmarelo ";
        //                Tba += " { ";
        //                Tba += " border: 0.1pt solid #FFFFFF; ";
        //                Tba += " font-size:8pt; ";
        //                Tba += " font-family:Verdana; ";
        //                Tba += " text-align: right;	 ";
        //                Tba += " nowrap:nowrap; ";
        //                Tba += " background-color:#DEDE40; ";
        //                Tba += " } ";

        //                Tba += " .tdpRVerdanaVermelho ";
        //                Tba += " { ";
        //                Tba += " 	border: 0.1pt solid #FFFFFF; ";
        //                Tba += " 	font-size:8pt; ";
        //                Tba += " 	font-family:Verdana; ";
        //                Tba += " 	text-align: right;	 ";
        //                Tba += " 	nowrap:nowrap; ";
        //                Tba += " 	background-color:#DE4040; ";
        //                Tba += "} ";

        //                Tba += " </STYLE> ";

        //                Tba += "</HEAD>";

        //                #endregion


        //                foreach (DataRow item in dtTodosOsDados.Rows)
        //                {
        //                    sstr += "<tr>";
        //                    sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["FILIAL"].ToString() + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["ENTREGAS"].ToString() + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["PESO"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["VALORDANOTA"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["FRETE"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["%FRETE"].ToString()).ToString("#0.000") + "%</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["FRETE_MOT"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["IMPOSTO"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["SEGURO"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["TXADM"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["TXTRANF"].ToString()).ToString("###,###.##") + "</td>";
        //                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(item["LUCRO"].ToString()).ToString("###,###.##") + "</td>";




        //                    string css = "tdpRVerdanaVermelho";

        //                    if ((decimal.Parse(item["PER_LUCRO"].ToString()) * 100) >= Convert.ToDecimal(12))
        //                        css = "tdpRVerdanaVerde";
        //                    else if ((decimal.Parse(item["PER_LUCRO"].ToString()) * 100) < Convert.ToDecimal(12) && (decimal.Parse(item["PER_LUCRO"].ToString()) * 100) >= Convert.ToDecimal(10))
        //                        css = "tdpRVerdanaAmarelo";
        //                    else
        //                        css = "tdpRVerdanaVermelho";

        //                    sstr += "<td class='" + css + "' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (decimal.Parse(item["PER_LUCRO"].ToString()) * 100).ToString("#0.000") + "%";
        //                    sstr += "</td>";
        //                    sstr += "</tr>";
        //                }

        //                #region Totais
        //                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        //                sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL";
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + dtTodosOsDados.Compute("SUM(ENTREGAS)", "").ToString();
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(PESO)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("AVG([%FRETE])", "").ToString()).ToString("#0.000") + "%";
        //                sstr += "</td>";


        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(FRETE_MOT)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";


        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(IMPOSTO)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(SEGURO)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";

        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(TXADM)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";


        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(TXTRANF)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";


        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "").ToString()).ToString("###,###.##");
        //                sstr += "</td>";


        //                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (decimal.Parse(dtTodosOsDados.Compute("AVG(PER_LUCRO)", "").ToString())*100).ToString("#0.000") + "%";
        //                sstr += "</td>";

        //                sstr += "</tr>";
        //                #endregion

        //                sstr += "</table>";

        //                sstr += "<hr/>";

        //                #region Quebra por Filial


        //                DataTable d = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_PRODUCAOREDIARIO ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        //                distinctDataTable = new DataTable();
        //                DataView dw = dtTodosOsDados.DefaultView;
        //                distinctDataTable = dw.ToTable(true, "Filial");

        //                for (int i = 0; i < distinctDataTable.Rows.Count; i++)
        //                {

        //                    DataRow[] o = d.Select("filial='" + distinctDataTable.Rows[i]["FILIAL"].ToString() + "'", "FILIAL ASC");

        //                    sstr += "<table>";

        //                    sstr += "<tr>";
        //                    sstr += "<td><b>FILIAL: " + o[0]["FILIAL"].ToString() + "</b><td>";
        //                    sstr += "</tr>";

        //                    sstr += "<tr>";
        //                    sstr += "<td>IMPOSTO: " + o[0]["TXIPOSTO"].ToString() + "% <td>";
        //                    sstr += "<td>SEGURO: " + o[0]["txSEGURO"].ToString() + "% <td>";
        //                    sstr += "<td>TAXA ADMINISTRATIVA: " + o[0]["TAXAADMINISTRATIVA"].ToString() + "% <td>";
        //                    sstr += "<td>TAXA DE TRANFERENCIA: " + o[0]["TAXADETRANFERENCIA"].ToString() + "% <td>";
        //                    sstr += "</tr>";
        //                    sstr += "</table>";


        //                    if (o.Length > 0)
        //                    {
        //                        #region Cabecalho
        //                        sstr += "<table class='table' cellspacing=1 celpanding=1 >";
        //                        sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        //                        sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>DT";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>EMISSAO";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>MOTORISTA";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
        //                        sstr += "</td>";

        //                        sstr += "</tr>";


        //                        for (int io = 0; io < o.Length; io++)
        //                        {

        //                            sstr += "<tr>";
        //                            sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + o[io]["NUMERO"].ToString() + "</td>";

        //                            sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px;^text-align:center'>" + DateTime.Parse(o[io]["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + o[io]["MOTORISTA"].ToString() + "</td>";
        //                            sstr += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + o[io]["ENTREGAS"].ToString() + "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["PESO"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["VALORDANOTA"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["FRETE"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["%FRETE"].ToString()).ToString("#0.000") + "%";
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["FRETE_MOT"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";


        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["IMPOSTO"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["SEGURO"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["TXADM"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["TXTRANF"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";

        //                            sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + decimal.Parse(o[io]["LUCRO"].ToString()).ToString("###,###.##");
        //                            sstr += "</td>";


        //                            string cs = "";
        //                            if ((decimal.Parse(o[io]["PER_LUCRO"].ToString()) * 100) >= Convert.ToDecimal(12))
        //                                cs = "tdpRVerdanaVerde";
        //                            else if ((decimal.Parse(o[io]["PER_LUCRO"].ToString()) * 100) < Convert.ToDecimal(12) && (decimal.Parse(o[io]["PER_LUCRO"].ToString()) * 100) >= Convert.ToDecimal(10))
        //                                cs = "tdpRVerdanaAmarelo";
        //                            else
        //                                cs = "tdpRVerdanaVermelho";

        //                            sstr += "<td class='" + cs + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(o[io]["PER_LUCRO"].ToString()) * 100).ToString("#0.000") + "%";
        //                            sstr += "</td>";

        //                            sstr += "</tr>";
        //                        }

        //                        #region Totais

        //                        DataRow[] orew = d.Select("FILIAL='" + o[0]["FILIAL"].ToString() + "'", "");
        //                        sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";


        //                        sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL***";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>";
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + d.Compute("Sum(ENTREGAS)", "FILIAL='"+ o[0]["FILIAL"].ToString() +"'").ToString();
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(PESO)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(VALORDANOTA)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(FRETE)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        if (decimal.Parse(d.Compute("Sum(VALORDANOTA)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()) > 0)
        //                            sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + ((decimal.Parse(d.Compute("Sum(FRETE)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()) / decimal.Parse(d.Compute("Sum(VALORDANOTA)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString())) * 100).ToString("#0.000");
        //                        else
        //                            sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>0";
        ////                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("avg([%FRETE])", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";


        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(FRETE_MOT)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(IMPOSTO)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(SEGURO)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(TXADM)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(TXTRANF)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(d.Compute("Sum(LUCRO)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()).ToString("###,###.##");
        //                        sstr += "</td>";

        //                        string css = ""; //PER_LUCRO

        //                        //decimal totDespesas = decimal.Parse(d.Compute("Sum(FRETE_MOT)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString());
        //                        //totDespesas+= decimal.Parse(d.Compute("Sum(IMPOSTO)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString());
        //                        //totDespesas += decimal.Parse(d.Compute("Sum(SEGURO)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString());
        //                        //totDespesas += decimal.Parse(d.Compute("Sum(TXADM)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString());
        //                        //totDespesas += decimal.Parse(d.Compute("Sum(TXTRANF)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString());


        //                        decimal totPerc =0;
        //                        if (decimal.Parse(d.Compute("Sum(FRETE)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()) > 0)
        //                        {
        //                            totPerc = decimal.Parse(d.Compute("Sum(LUCRO)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString()) / decimal.Parse(d.Compute("Sum(FRETE)", "FILIAL='" + o[0]["FILIAL"].ToString() + "'").ToString());
        //                        }else
        //                        {
        //                            totPerc = 0;
        //                        }

        //                        totPerc = totPerc * 100;


        //                        if (totPerc >= Convert.ToDecimal(12))
        //                            css = "tdpRVerdanaVerde";
        //                        else if (totPerc < Convert.ToDecimal(12) && totPerc >= Convert.ToDecimal(10))
        //                            css = "tdpRVerdanaAmarelo";
        //                        else
        //                            css = "tdpRVerdanaVermelho";

        //                        sstr += "<td class='" + css + "' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + totPerc.ToString("#0.000")+"%";
        //                        sstr += "</td>";

        //                        sstr += "</tr>";
        //                        #endregion


        //                        sstr += "</table>";
        //                        sstr += "<hr>";
        //                        sstr += "<br>";
        //                        #endregion
        //                    }

        //                }

        //                #endregion


        //                // Tba += " <body style='font-size: 12px;'> <B>Para maiores <a href='http://www.grupologos.com.br/reports.net/RelatorioProducaoRE.aspx'>detalhes</a> ou se não conseguir visualizar este e-mail clique <a href='http://www.grupologos.com.br/reports.net/RelatorioProducaoRE.aspx'>aqui</B></a><BR><BR>";
        //                string juncao = Tba + sstr;
        //                juncao += "</body></head><html>";
        //                new cEmail().enviarEmail("Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA(**)", juncao, email, "RelacaoDeEntrega");


        //            }
        //        }

        private List<DadosOperacaoCliente> RetornarClientesBarbosa()
        {
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS("SELECT IDCADASTRO IDCLIENTE,  CNPJCPF + ' - ' + FANTASIAAPELIDO  RAZAOSOCIALNOME, FANTASIAAPELIDO , CNPJCPF FROM CADASTRO C LEFT JOIN CLIENTE CLI ON CLI.IDCLIENTE = C.IDCADASTRO WHERE CNPJCPF in ('60.437.647/0012-60','60.437.647/0001-07','60.437.647/0017-74','60.437.647/0013-40','60.437.647/0018-55','60.437.647/0007-00','04.914.840/0001-10','60.437.647/0009-64','60.437.647/0011-89','09.247.417/0001-28','00.425.700/0001-28','06.193.077/0001-01','60.437.647/0020-70','60.437.647/0022-31','60.437.647/0021-50','60.437.647/0008-83','60.437.647/0004-50','60.437.647/0019-36','60.437.647/0025-84','60.437.647/0006-11','60.437.647/0006-11','10.228.304/0001-64','60.437.647/0005-30','60.437.647/0023-12','60.437.647/0024-01','60.437.647/0014-21','60.437.647/0015-02','60.437.988/0001-39', '60.437.988/0001-39', '60.437.647/0009-64', '60.437.647/0006-11', '51.052.496/0001-88', '60.437.647/0029-08')  ORDER BY C.RAZAOSOCIALNOME", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            List<DadosOperacaoCliente> lista = new List<DadosOperacaoCliente>();



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DadosOperacaoCliente item = new DadosOperacaoCliente();
                item.IdCliente = dt.Rows[i]["IDCLIENTE"].ToString();
                item.NomeFantasia = dt.Rows[i]["RAZAOSOCIALNOME"].ToString();
                item.RazaoSocial = "BARBOSA";
                lista.Add(item);
            }

            return lista;
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
        #endregion

        #region F L U X O  D E  C A I X A
        private void MontarFluxoDeCaixa(string emailss)
        {
            try
            {
                DataTable dtFluxoDeCaixa = Sistran.Library.GetDataTables.RetornarDataSetWS("SELECT * FROM GERARFLUXOCAIXA('EMAIL')", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
                DataTable dtPrametros = Sistran.Library.GetDataTables.RetornarDataSetWS("SELECT PFC.*, CNPJCPF , RAZAOSOCIALNOME FROM PARAMETROFLUXODECAIXA PFC INNER JOIN CADASTRO CADEMP ON CADEMP.IDCADASTRO = PFC.IDEMPRESA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];


                string m = dtFluxoDeCaixa.Rows[0]["PAR_PREVISAOAPAGAR"].ToString();
                if (m.ToUpper() == "AMBOS" || m.ToUpper() == "TODOS")
                    m = "TODOS";

                sstr = "<table class='table' cellspacing=1 celpanding=1 >";

                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='9'>PARAMETROS:";
                sstr += "</td>";


                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='1'>TÍTULOS:  ";
                sstr += "</td>";
                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='3'>" + m;
                sstr += "</td>";

                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='1'>CONSIDERAR SALDO ANTERIOR: ";
                sstr += "</td>";
                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='5'>" + dtPrametros.Rows[0]["ConsideraSaldoAnterior"];
                sstr += "</td>";
                sstr += "</tr>";


                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='5'>CONSIDERAR " + dtPrametros.Rows[0]["DiasAnteriores"] + " DIAS ANTERIORES";
                sstr += "</td>";


                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='5'>CONSIDERAR " + dtPrametros.Rows[0]["DiasPosteriores"] + " DIAS POSTERIORES";
                sstr += "</td>";
                sstr += "</tr>";

                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='5'>CLIENTES:";
                sstr += "</td>";


                m = "";
                for (int i = 0; i < dtPrametros.Rows.Count; i++)
                {
                    m += dtPrametros.Rows[i]["CNPJCPF"] + " - " + dtPrametros.Rows[i]["RAZAOSOCIALNOME"];
                    m += "<BR>";

                }

                sstr += "<td class='tdp'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='5'>" + m;
                sstr += "</td>";
                sstr += "</tr>";



                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='9'><HR> ";
                sstr += "</td>";
                sstr += "</tR>";

                if (dtFluxoDeCaixa.Rows.Count > 0)
                {
                    #region Cabecalho
                    sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                    sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>SALDO ACUMULADO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>SALDO CAIXA / BANCO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>RECEITAS ";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>RECEITAS (SALDO)";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>DESPESAS";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>DESPESAS (SALDO)";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SALDO DIA";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SALDO ACUMULADO";
                    sstr += "</td>";

                    sstr += "</tr>";
                    #endregion

                    for (int i = 0; i < dtFluxoDeCaixa.Rows.Count; i++)
                    {
                        sstr += "<tr>";

                        if (i == 0)
                        {
                            sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'> ATÉ ";
                            sstr += "</td>";
                        }
                        else if (i == dtFluxoDeCaixa.Rows.Count - 1)
                        {
                            sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'> A PARTIR";
                            sstr += "</td>";
                        }
                        else
                        {
                            sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'># ";
                            sstr += "</td>";
                        }


                        sstr += "<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtFluxoDeCaixa.Rows[i]["DATA"].ToString();
                        sstr += "</td>";

                        if (DateTime.Now.Day == DateTime.Parse(dtFluxoDeCaixa.Rows[i]["DATA"].ToString()).Day && DateTime.Now.Month == DateTime.Parse(dtFluxoDeCaixa.Rows[i]["DATA"].ToString()).Month)
                        {

                            sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDOCONTA"].ToString()) >= 1000 ? decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDOCONTA"].ToString()).ToString("#,0.00") : decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDOCONTA"].ToString()).ToString("#,0.00"));
                            sstr += "</td>";
                        }
                        else
                        {
                            sstr += "<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>-";
                            sstr += "</td>";
                        }

                        sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtFluxoDeCaixa.Rows[i]["CREDITO"].ToString()) >= 1000 ? decimal.Parse(dtFluxoDeCaixa.Rows[i]["CREDITO"].ToString()).ToString("#,0.00") : decimal.Parse(dtFluxoDeCaixa.Rows[i]["CREDITO"].ToString()).ToString("#,0.00"));
                        sstr += "</td>";

                        sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtFluxoDeCaixa.Rows[i]["CREDITO_ACUMULADO"].ToString()) >= 1000 ? decimal.Parse(dtFluxoDeCaixa.Rows[i]["CREDITO_ACUMULADO"].ToString()).ToString("#,0.00") : decimal.Parse(dtFluxoDeCaixa.Rows[i]["CREDITO"].ToString()).ToString("#,0.00"));
                        sstr += "</td>";

                        sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtFluxoDeCaixa.Rows[i]["DEBITO"].ToString()) >= 1000 ? decimal.Parse(dtFluxoDeCaixa.Rows[i]["DEBITO"].ToString()).ToString("#,0.00") : decimal.Parse(dtFluxoDeCaixa.Rows[i]["DEBITO"].ToString()).ToString("#,0.00"));
                        sstr += "</td>";

                        sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtFluxoDeCaixa.Rows[i]["DEBITO_ACUMULADO"].ToString()) >= 1000 ? decimal.Parse(dtFluxoDeCaixa.Rows[i]["DEBITO_ACUMULADO"].ToString()).ToString("#,0.00") : decimal.Parse(dtFluxoDeCaixa.Rows[i]["DEBITO"].ToString()).ToString("#,0.00"));
                        sstr += "</td>";


                        if (dtFluxoDeCaixa.Rows[i]["SALDO"].ToString() == "")
                        {
                            sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#,0.00");
                            sstr += "</td>";
                        }
                        else
                        {
                            sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Math.Abs(decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDO"].ToString())) >= 1000 ? decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDO"].ToString()).ToString("#,0.00") : decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDO"].ToString()).ToString("#,0.00"));
                            sstr += "</td>";
                        }
                        sstr += "<td class='tdpr' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Math.Abs(decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDOACUMULADO"].ToString())) >= 1000 ? decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDOACUMULADO"].ToString()).ToString("#,0.00") : decimal.Parse(dtFluxoDeCaixa.Rows[i]["SALDOACUMULADO"].ToString()).ToString("#,0.00"));
                        sstr += "</td>";
                        sstr += "</tr>";
                    }


                    sstr += "</table >";


                    string Tba = "<html><head>";
                    Tba += " <STYLE type='text/css'>";
                    Tba += " body ";
                    Tba += " { ";
                    Tba += " margin: 0px; ";
                    Tba += " background-color: #f8f8f8; ";
                    Tba += " font-family: Verdana; ";
                    Tba += " text-align: left; ";
                    Tba += " font-size: 12PX; }";


                    Tba += " .table  ";
                    Tba += " { ";
                    Tba += " background-color: #E0E0E0; ";
                    //Tba += " width: 50%; ";
                    Tba += " font-family: Arial, Helvetica, sans-serif; ";
                    Tba += " font-size: 7pt; ";
                    Tba += " font-weight: bold; ";
                    Tba += " } ";

                    Tba += " .tableFundoClaro ";
                    Tba += " { ";
                    Tba += " background-color: #F8F8F8; ";
                    //Tba += " width: 100%; ";
                    Tba += " font-family: Arial, Helvetica, sans-serif; ";
                    Tba += " font-size: 7pt; ";
                    Tba += " font-weight: bold; ";
                    Tba += " } ";

                    Tba += " .tableSemCorFundo ";
                    Tba += " {	 ";
                    //Tba += " width: 50%; ";
                    Tba += " font-family: Arial, Helvetica, sans-serif; ";
                    Tba += " font-size: 7pt; ";
                    Tba += " font-weight: bold; ";
                    Tba += " } ";

                    Tba += " .table2 ";
                    Tba += " { ";
                    Tba += " background-color:#E0E0E0 ;  ";
                    Tba += " font-family: Arial, Helvetica, sans- ";
                    Tba += " } ";

                    Tba += " .tdpCenter ";
                    Tba += " { ";
                    Tba += " border: 0.1pt solid #FFFFFF; ";
                    Tba += " font-size:8pt; ";
                    Tba += " font-family:Verdana; ";
                    Tba += " text-align: center ; ";
                    Tba += " nowrap:nowrap; ";
                    Tba += " font-weight:normal; ";
                    Tba += " } ";

                    Tba += " .tdp ";
                    Tba += " { ";
                    Tba += " border: 0.1pt solid #FFFFFF; ";
                    Tba += " font-size:8pt; ";
                    Tba += " font-family:Verdana; ";
                    Tba += " nowrap:nowrap; ";
                    Tba += " font-weight:normal; ";
                    Tba += " text-align: left; ";
                    Tba += " vertical-align:middle; ";

                    Tba += " } ";
                    Tba += " .tdpSemAlign ";
                    Tba += " { ";
                    Tba += " border: 0.5pt solid #FFFFFF; ";
                    Tba += " font-size:8pt; ";
                    Tba += " font-family:Verdana; ";
                    Tba += " nowrap:nowrap; ";
                    Tba += " font-weight:normal; ";
                    Tba += " } ";

                    Tba += " .tdpSemAlignGray ";
                    Tba += " { ";
                    Tba += " border: 0.5pt solid #FFFFFF; ";
                    Tba += " font-size:8pt; ";
                    Tba += " font-family:Verdana; ";
                    Tba += " nowrap:nowrap; ";
                    Tba += " font-weight:normal; ";
                    Tba += " background-color:GrayText; ";
                    Tba += " } ";


                    Tba += " .tdpCenter ";
                    Tba += " { ";
                    Tba += " border: 0.1pt solid #FFFFFF; ";
                    Tba += " font-size:8pt; ";
                    Tba += " font-family:Verdana; ";
                    Tba += " text-align: center ; ";
                    Tba += " nowrap:now ";
                    Tba += " } ";
                    Tba += " .tdpR ";
                    Tba += " { ";
                    Tba += " border: 0.1pt solid #FFFFFF; ";
                    Tba += " font-size:8pt; ";
                    Tba += " font-family:Verdana; ";
                    Tba += " text-align:right; ";
                    Tba += " nowrap:nowrap; ";
                    Tba += " font-weight:normal;	 ";
                    Tba += " } ";

                    Tba += "  .tdpVerdana ";
                    Tba += " { ";
                    Tba += " border: 0.1pt solid #FFFFFF; ";
                    Tba += " font-size:8pt; ";
                    Tba += " font-family:Verdana; ";
                    //Tba += " text-align: left; ";
                    Tba += " nowrap:nowrap; ";
                    Tba += " } ";

                    Tba += " .tdpCabecalho ";
                    Tba += " { ";
                    Tba += " border: 0.1pt solid #FFFFFF; ";
                    Tba += " height: 13pt; ";
                    Tba += " font-size:9pt; ";
                    Tba += " font-family:Verdana; ";
                    Tba += " font-weight:bold; ";
                    Tba += " text-transform: uppercase;	 ";
                    Tba += " } ";

                    Tba += " </STYLE> ";
                    Tba += "</HEAD>";

                    //Tba += " <body style='font-size: 12px;'> <B>Para maiores <a href='http://www.grupologos.com.br/reports.net/RelatorioProducaoRE.aspx'>detalhes</a> ou se não conseguir visualizar este e-mail clique <a href='http://www.grupologos.com.br/reports.net/RelatorioProducaoRE.aspx'>aqui</B></a><BR><BR>";
                    string juncao = Tba + sstr;
                    juncao += "</body></head><html>";
                    //Sistran.Library.EnviarEmails.EnviarEmail(emailss, "sistema@grupologos.com.br", "Aviso: FLUXO DE CAIXA", juncao, "mail.grupologos.com.br", "logos0902", "FLUXO DE CAIXA");

                    //  new cEmail().enviarEmail("Aviso: FLUXO DE CAIXA", juncao, emailss, "FluxoDeCaixa");
                    Sistran.Library.EnviarEmails.EnviarEmail(emailss, "sistema@grupologos.com.br", "Aviso: FLUXO DE CAIXA", juncao, "mail.grupologos.com.br", "logos0902", "FLUXO DE CAIXA");


                }
            }
            catch (Exception)
            {
                reiniciarTimers();
            }
        }

        #endregion

        #region R E

        DataTable dtTodosOsDados;
        //DataTable distinctDataTable;
        private void MontarEmailRE(string emails)
        {
            try
            {
                dtTodosOsDados = new DataTable();
                dtTodosOsDados = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC GERAR_PRODUCAO_RE '" + DateTime.Now.AddDays(0).ToString("dd/MM/yyyy") + "', '" + DateTime.Now.AddDays(0).ToString("dd/MM/yyyy") + "'", Sistran.Library.Robo.Robo.RetornarStringBaseAntiga()).Tables[0];
                //dtTodosOsDados = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC GERAR_PRODUCAO_RE '" + DateTime.Now.AddDays(0).ToString("01/01/2012") + "', '" + DateTime.Now.AddDays(0).ToString("31/01/2012") + "'", Sistran.Library.Robo.Robo.RetornarStringBaseAntiga()).Tables[0];


                dtTodosOsDados.Columns.Add("FreteMotorista", Type.GetType("System.Decimal"));
                dtTodosOsDados.Columns.Add("Lucro", Type.GetType("System.Decimal"));
                dtTodosOsDados.Columns.Add("PercLucro", Type.GetType("System.Decimal"));

                for (int i = 0; i < dtTodosOsDados.Rows.Count; i++)
                {
                    //calcula freteMotorista
                    if (decimal.Parse(dtTodosOsDados.Rows[i]["creditos"].ToString()) > decimal.Parse("0"))
                    {
                        dtTodosOsDados.Rows[i]["FreteMotorista"] = decimal.Parse(dtTodosOsDados.Rows[i]["creditos"].ToString()) - decimal.Parse(dtTodosOsDados.Rows[i]["debitos"].ToString());
                    }
                    else
                    {
                        decimal entregas = decimal.Parse(dtTodosOsDados.Rows[i]["entregas"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporentrega"].ToString());
                        decimal coleta = decimal.Parse(dtTodosOsDados.Rows[i]["coletas"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporcoleta"].ToString());
                        decimal kkm = decimal.Parse(dtTodosOsDados.Rows[i]["distanciapercorrida"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporkm"].ToString());
                        decimal kilo = decimal.Parse(dtTodosOsDados.Rows[i]["pesototalre"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporkilo"].ToString());

                        if (kilo < decimal.Parse(dtTodosOsDados.Rows[i]["valorminimopeso"].ToString()))
                        {
                            kilo = decimal.Parse(dtTodosOsDados.Rows[i]["valorminimopeso"].ToString());
                        }

                        decimal diaria = decimal.Parse(dtTodosOsDados.Rows[i]["diaria"].ToString());
                        decimal adicionalPercurso = decimal.Parse(dtTodosOsDados.Rows[i]["ad_percurso"].ToString());
                        decimal debitos = decimal.Parse(dtTodosOsDados.Rows[i]["debitos"].ToString());

                        dtTodosOsDados.Rows[i]["FreteMotorista"] = entregas + coleta + kkm + kilo + diaria + adicionalPercurso - debitos;
                    }

                    //lucro
                    DataRow[] rw = dtTodosOsDados.Select("filial='" + (dtTodosOsDados.Rows[i]["filial"].ToString()) + "'");
                    decimal impostoH = decimal.Parse(dtTodosOsDados.Rows[i]["Imposto"].ToString()) / 100;
                    decimal SeguroH = decimal.Parse(dtTodosOsDados.Rows[i]["Seguro"].ToString()) / 100;
                    decimal AdmH = decimal.Parse(dtTodosOsDados.Rows[i]["TaxaAdministrativa"].ToString()) / 100;
                    decimal TranfH = decimal.Parse(dtTodosOsDados.Rows[i]["TaxaDeTranferencia"].ToString()) / 100;

                    impostoH = (decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) * impostoH);
                    SeguroH = (decimal.Parse(dtTodosOsDados.Rows[i]["valordanota"].ToString()) * SeguroH);
                    AdmH = (decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) * AdmH);
                    TranfH = (decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) * TranfH);


                    dtTodosOsDados.Rows[i]["lucro"] = decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) - decimal.Parse(dtTodosOsDados.Rows[i]["FreteMotorista"].ToString()) - impostoH - SeguroH - AdmH - TranfH;

                    if (Convert.ToDecimal(dtTodosOsDados.Rows[i]["Frete"].ToString()) != Convert.ToDecimal(0))
                        dtTodosOsDados.Rows[i]["perclucro"] = (Convert.ToDecimal(dtTodosOsDados.Rows[i]["lucro"]) / Convert.ToDecimal(dtTodosOsDados.Rows[i]["Frete"].ToString())) * 100;
                    else
                        dtTodosOsDados.Rows[i]["perclucro"] = Convert.ToDecimal(0);

                }

                MontarTotais(emails);

            }
            catch (Exception)
            {
                reiniciarTimers();
            }
        }

        string sstr = "";
        DataTable dtApurarTotal = new DataTable();
        private void MontarTotais(string emailss)
        {
            try
            {
                #region resumido

                dtApurarTotal.Columns.Clear();
                dtApurarTotal.Columns.Add("FILIAL");
                dtApurarTotal.Columns.Add("ENTREGAS");
                dtApurarTotal.Columns.Add("PESO");
                dtApurarTotal.Columns.Add("VALORDANOTA");
                dtApurarTotal.Columns.Add("FRETE");
                dtApurarTotal.Columns.Add("PERCFRETE");
                dtApurarTotal.Columns.Add("FRETEMOT");
                dtApurarTotal.Columns.Add("IMPOSTOS");
                dtApurarTotal.Columns.Add("SEGURO");
                dtApurarTotal.Columns.Add("ADM");
                dtApurarTotal.Columns.Add("TRANF");
                dtApurarTotal.Columns.Add("LUCRO");
                dtApurarTotal.Columns.Add("PERCLUCRO");


                sstr = "<table class='table' cellspacing=1 celpanding=1 >";

                if (dtTodosOsDados.Rows.Count > 0)
                {
                    #region Cabecalho
                    sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                    sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
                    sstr += "</td>";

                    sstr += "</tr>";
                    #endregion
                }

                //pega as filiais
                distinctDataTable = new DataTable();
                DataView dw = dtTodosOsDados.DefaultView;
                distinctDataTable = dw.ToTable(true, "NomeFilial");
                distinctDataTable.Columns.Add("Ordem", Type.GetType("System.Decimal"));

                for (int i1 = 0; i1 < distinctDataTable.Rows.Count; i1++)
                {
                    decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "NomeFilial='" + distinctDataTable.Rows[i1]["NomeFilial"].ToString() + "'").ToString());
                    decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "NomeFilial='" + distinctDataTable.Rows[i1]["NomeFilial"].ToString() + "'").ToString());

                    if (dcalcfrete > 0)
                        distinctDataTable.Rows[i1]["Ordem"] = Convert.ToDecimal((lucro / dcalcfrete).ToString());
                    else
                        distinctDataTable.Rows[i1]["Ordem"] = 0;

                }

                DataView dv = distinctDataTable.DefaultView;
                dv.Sort = "Ordem Desc";
                distinctDataTable = dv.ToTable();



                decimal perFrete = 0;
                decimal perFretetot = 0, impostptot = 0, segutotot = 0, admtot = 0, tranftot = 0, freteMottot = 0, lucrotot = 0, totPerLucro = 0;

                foreach (DataRow item in distinctDataTable.Rows)
                {
                    DataRow rwdtApurarTotal = dtApurarTotal.NewRow();

                    sstr += "<tr>";

                    sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["NomeFilial"].ToString();
                    sstr += "</td>";
                    rwdtApurarTotal["FILIAL"] = item["NomeFilial"].ToString();

                    int calc = int.Parse(dtTodosOsDados.Compute("SUM(entregas)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + calc.ToString();
                    sstr += "</td>";
                    rwdtApurarTotal["ENTREGAS"] = calc;

                    decimal dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(peso)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##");
                    sstr += "</td>";
                    rwdtApurarTotal["PESO"] = dcalc;


                    dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##");
                    sstr += "</td>";
                    rwdtApurarTotal["VALORDANOTA"] = dcalc;


                    decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfrete.ToString("###,###.##");
                    sstr += "</td>";
                    rwdtApurarTotal["FRETE"] = dcalcfrete;

                    if (dcalc > 0)
                        perFrete = ((dcalcfrete / dcalc) * 100);
                    else
                        perFrete = 0;

                    perFretetot += perFrete;

                    if (dcalc != 0)
                    {
                        sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + ((dcalcfrete / dcalc) * 100).ToString("#0.000") + "%";
                        sstr += "</td>";
                    }
                    else
                    {
                        sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.000") + "%";
                        sstr += "</td>";
                    }
                    rwdtApurarTotal["PERCFRETE"] = perFrete;



                    decimal dcalcfreteMot = decimal.Parse(dtTodosOsDados.Compute("SUM(FreteMotorista)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
                    freteMottot += dcalcfreteMot;
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfreteMot.ToString("#0.00");
                    sstr += "</td>";
                    rwdtApurarTotal["FRETEMOT"] = dcalcfreteMot;


                    DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + item["NomeFilial"].ToString() + "'");

                    decimal imposto = decimal.Parse(rw[0]["Imposto"].ToString()) / 100;
                    impostptot += (dcalcfrete * imposto);
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * imposto).ToString("###,###.##");
                    sstr += "</td>";
                    rwdtApurarTotal["IMPOSTOS"] = (dcalcfrete * imposto);

                    decimal seguro = decimal.Parse(rw[0]["seguro"].ToString()) / 100;

                    segutotot += decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString()) * seguro;
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString()) * seguro).ToString("#0.00");
                    sstr += "</td>";
                    rwdtApurarTotal["SEGURO"] = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString()) * seguro;


                    decimal adm = decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()) / 100;
                    admtot += (dcalcfrete * adm);
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * adm).ToString("#0.00");
                    sstr += "</td>";

                    rwdtApurarTotal["ADM"] = (dcalcfrete * adm).ToString("#0.00");

                    decimal transf = decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()) / 100;
                    tranftot += (dcalcfrete * transf);
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * transf).ToString("#0.00");
                    sstr += "</td>";
                    rwdtApurarTotal["TRANF"] = (dcalcfrete * transf).ToString("#0.00");


                    decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
                    lucrotot += lucro;
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + lucro.ToString("#0.00");
                    sstr += "</td>";

                    rwdtApurarTotal["LUCRO"] = lucro;

                    if (dcalcfrete > 0)
                        totPerLucro += ((lucro / dcalcfrete) * 100);

                    decimal calcPercLucro = (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100) : 0);
                    string cs = "tdpRVerdanaVermelho";

                    if (calcPercLucro >= Convert.ToDecimal(12))
                        cs = "tdpRVerdanaVerde";
                    else if (calcPercLucro < Convert.ToDecimal(12) && calcPercLucro >= Convert.ToDecimal(10))
                        cs = "tdpRVerdanaAmarelo";
                    else
                        cs = "tdpRVerdanaVermelho";


                    sstr += "<td class='" + cs + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + ((calcPercLucro)).ToString("#0.000") + "%";
                    sstr += "</td>";
                    rwdtApurarTotal["PERCLUCRO"] = calcPercLucro;

                    dtApurarTotal.Rows.Add(rwdtApurarTotal);

                    sstr += "</tr>";
                }


                #region Totais
                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";


                sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL";
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + dtTodosOsDados.Compute("SUM(entregas)", "").ToString();
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(PESO)", "").ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "").ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "").ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (perFretetot / distinctDataTable.Rows.Count).ToString("#0.000") + "%";
                sstr += "</td>";


                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (freteMottot).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (impostptot).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (segutotot).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (admtot).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (tranftot).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (lucrotot).ToString("###,###.##");
                sstr += "</td>";
                decimal calxc = lucrotot / decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "").ToString());
                calxc = calxc * 100;

                string css = "tdpRVerdanaVermelho";

                if (calxc >= Convert.ToDecimal(12))
                    css = "tdpRVerdanaVerde";
                else if (calxc < Convert.ToDecimal(12) && calxc >= Convert.ToDecimal(10))
                    css = "tdpRVerdanaAmarelo";
                else
                    css = "tdpRVerdanaVermelho";

                sstr += "<td class='" + css + "' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + calxc.ToString("#0.000") + "%";
                sstr += "</td>";

                sstr += "</tr>";
                #endregion

                sstr += "</table>";

                #endregion

                for (int i = 0; i < distinctDataTable.Rows.Count; i++)
                {
                    sstr += "<br>";
                    sstr += "<hr>";


                    DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + distinctDataTable.Rows[i]["NomeFilial"].ToString() + "'");

                    sstr += "<b><font size='2pt'>FILIAL: " + distinctDataTable.Rows[i]["NomeFilial"].ToString() + "   &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp |   IMPOSTO: " + decimal.Parse(rw[0]["Imposto"].ToString()).ToString("#0.00") + "%   &nbsp &nbsp &nbsp | SEGURO: " + decimal.Parse(rw[0]["Seguro"].ToString()).ToString("#0.00") + "%    &nbsp &nbsp &nbsp | TX. ADM: " + decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()).ToString("#0.00") + "%   &nbsp &nbsp &nbsp | TX. TRANFERENCIA: " + decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()).ToString("#0.00") + "% </font> </b>";
                    ExpandirPorFilial(distinctDataTable.Rows[i]["NomeFilial"].ToString());
                }


                string Tba = "<html><head>";
                Tba += " <STYLE type='text/css'>";
                Tba += " body ";
                Tba += " { ";
                Tba += " margin: 0px; ";
                Tba += " background-color: #f8f8f8; ";
                Tba += " font-family: Verdana; ";
                Tba += " text-align: left; ";
                Tba += " font-size: 12PX; }";


                Tba += " .table  ";
                Tba += " { ";
                Tba += " background-color: #E0E0E0; ";
                Tba += " width: 50%; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .tableFundoClaro ";
                Tba += " { ";
                Tba += " background-color: #F8F8F8; ";
                Tba += " width: 100%; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .tableSemCorFundo ";
                Tba += " {	 ";
                Tba += " width: 50%; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .table2 ";
                Tba += " { ";
                Tba += " background-color:#E0E0E0 ;  ";
                Tba += " font-family: Arial, Helvetica, sans- ";
                Tba += " } ";

                Tba += " .tdpCenter ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: center ; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " } ";

                Tba += " .tdp ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " text-align: left; ";
                Tba += " vertical-align:middle; ";

                Tba += " } ";
                Tba += " .tdpSemAlign ";
                Tba += " { ";
                Tba += " border: 0.5pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " } ";

                Tba += " .tdpSemAlignGray ";
                Tba += " { ";
                Tba += " border: 0.5pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " background-color:GrayText; ";
                Tba += " } ";


                Tba += " .tdpCenter ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: center ; ";
                Tba += " nowrap:now ";
                Tba += " } ";
                Tba += " .tdpR ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align:right; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal;	 ";
                Tba += " } ";

                Tba += "  .tdpVerdana ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                //Tba += " text-align: left; ";
                Tba += " nowrap:nowrap; ";
                Tba += " } ";

                Tba += " .tdpCabecalho ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " height: 13pt; ";
                Tba += " font-size:9pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " font-weight:bold; ";
                Tba += " text-transform: uppercase;	 ";
                Tba += " } ";

                Tba += " .tdpRVerdanaVerde ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: right;	 ";
                Tba += " nowrap:nowrap; ";
                Tba += " background-color:#20AE3F; ";
                Tba += " } ";

                Tba += " .tdpRVerdanaAmarelo ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: right;	 ";
                Tba += " nowrap:nowrap; ";
                Tba += " background-color:#DEDE40; ";
                Tba += " } ";

                Tba += " .tdpRVerdanaVermelho ";
                Tba += " { ";
                Tba += " 	border: 0.1pt solid #FFFFFF; ";
                Tba += " 	font-size:8pt; ";
                Tba += " 	font-family:Verdana; ";
                Tba += " 	text-align: right;	 ";
                Tba += " 	nowrap:nowrap; ";
                Tba += " 	background-color:#DE4040; ";
                Tba += "} ";

                Tba += " </STYLE> ";

                Tba += "</HEAD>";





                Tba += " <body style='font-size: 12px;'> <B>Para maiores <a href='http://www.grupologos.com.br/reports.net/RelatorioProducaoRE.aspx'>detalhes</a> ou se não conseguir visualizar este e-mail clique <a href='http://www.grupologos.com.br/reports.net/RelatorioProducaoRE.aspx'>aqui</B></a><BR><BR>";
                string juncao = Tba + sstr;
                juncao += "</body></head><html>";
                //            Sistran.Library.EnviarEmails.EnviarEmail(emailss, "sistema@grupologos.com.br", "Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA", juncao, "mail.grupologos.com.br", "logos0902", "RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA");

                //new cEmail().enviarEmail("Aviso: RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA", juncao, emailss, "RelacaoDeEntrega");
            }
            catch (Exception)
            {
                reiniciarTimers();
            }
        }

        private void ExpandirPorFilial(string CodFilial)
        {
            try
            {
                #region ExpandirPorFilial
                sstr += "<table class='table' cellspacing=1 celpanding=1 >";

                if (dtTodosOsDados.Rows.Count > 0)
                {
                    #region Cabecalho
                    sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";


                    sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>RE";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>EMISSAO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>MOTORISTA";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
                    sstr += "</td>";

                    sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
                    sstr += "</td>";

                    sstr += "</tr>";
                    #endregion
                }

                //pega as filiais
                //dtTodosOsDados.Columns.Add("Ordem", Type.GetType("System.Decimal"));
                DataRow[] orw = dtTodosOsDados.Select("NomeFilial='" + CodFilial + "'", "PercLucro desc");

                decimal perFrete = 0;
                decimal perFretetot = 0, impostptot = 0, segutotot = 0, admtot = 0, tranftot = 0, freteMottot = 0, lucrotot = 0, totPerLucro = 0;

                foreach (DataRow item in orw)
                {

                    sstr += "<tr>";
                    sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["relacaodeentrega"].ToString();
                    sstr += "</td>";

                    sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + DateTime.Parse(item["emissao"].ToString()).ToString("dd/MM/yyyy");
                    sstr += "</td>";

                    sstr += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["nomemotorista"].ToString();
                    sstr += "</td>";

                    int calc = int.Parse(dtTodosOsDados.Compute("SUM(entregas)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + calc.ToString();
                    sstr += "</td>";

                    decimal dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(peso)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##");
                    sstr += "</td>";


                    dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##");
                    sstr += "</td>";

                    decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfrete.ToString("###,###.##");
                    sstr += "</td>";

                    if (dcalc > 0)
                        perFrete = ((dcalcfrete / dcalc) * 100);
                    else
                        perFrete = 0;
                    perFretetot += perFrete;
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perFrete).ToString("#0.000") + "%";
                    sstr += "</td>";



                    decimal dcalcfreteMot = decimal.Parse(dtTodosOsDados.Compute("SUM(FreteMotorista)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
                    freteMottot += dcalcfreteMot;
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfreteMot.ToString("#0.00");
                    sstr += "</td>";


                    DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + item["NomeFilial"].ToString() + "'");

                    decimal imposto = decimal.Parse(rw[0]["Imposto"].ToString()) / 100;
                    impostptot += (dcalcfrete * imposto);
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * imposto).ToString("###,###.##");
                    sstr += "</td>";

                    decimal seguro = decimal.Parse(rw[0]["seguro"].ToString()) / 100;

                    segutotot += decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString()) * seguro;
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString()) * seguro).ToString("#0.00");
                    sstr += "</td>";

                    decimal adm = decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()) / 100;
                    admtot += (dcalcfrete * adm);
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * adm).ToString("#0.00");
                    sstr += "</td>";

                    decimal transf = decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()) / 100;
                    tranftot += (dcalcfrete * transf);
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * transf).ToString("#0.00");
                    sstr += "</td>";


                    decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
                    lucrotot += lucro;
                    sstr += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + lucro.ToString("#0.00");
                    sstr += "</td>";

                    decimal dlcr = 0;

                    if (dcalcfrete > 0)
                        dlcr = ((lucro / dcalcfrete) * 100);

                    totPerLucro += dlcr;


                    string cs = "tdpRVerdanaVermelho";


                    if (dlcr >= Convert.ToDecimal(12))
                        cs = "tdpRVerdanaVerde";
                    else if (dlcr < Convert.ToDecimal(12) && dlcr >= Convert.ToDecimal(10))
                        cs = "tdpRVerdanaAmarelo";
                    else
                        cs = "tdpRVerdanaVermelho";

                    sstr += "<td class='" + cs + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dlcr).ToString("#0.000") + "%";
                    sstr += "</td>";

                    sstr += "</tr>";
                }

                #region Totais

                DataRow[] orew = dtApurarTotal.Select("filial='" + CodFilial + "'", "");
                sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";


                sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL";
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>";
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>";
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + orew[0]["ENTREGAS"];
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["PESO"].ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["VALORDANOTA"].ToString().ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["FRETE"].ToString().ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["PERCFRETE"].ToString()).ToString("#0.000") + "%";
                sstr += "</td>";


                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["FRETEMOT"].ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["IMPOSTOS"].ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["SEGURO"].ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["ADM"].ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["TRANF"].ToString()).ToString("###,###.##");
                sstr += "</td>";

                sstr += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["LUCRO"].ToString()).ToString("###,###.##");
                sstr += "</td>";

                string css = "";

                if (decimal.Parse(orew[0]["PERCLUCRO"].ToString()) >= Convert.ToDecimal(12))
                    css = "tdpRVerdanaVerde";
                else if (decimal.Parse(orew[0]["PERCLUCRO"].ToString()) < Convert.ToDecimal(12) && decimal.Parse(orew[0]["PERCLUCRO"].ToString()) >= Convert.ToDecimal(10))
                    css = "tdpRVerdanaAmarelo";
                else
                    css = "tdpRVerdanaVermelho";

                sstr += "<td class='" + css + "' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["PERCLUCRO"].ToString()).ToString("#0.000");
                sstr += "</td>";

                sstr += "</tr>";
                #endregion


                sstr += "</table>";

                #endregion
            }
            catch (Exception)
            {
                reiniciarTimers();
            }
        }

        #endregion

        #region R E S U M O  P O R  F I L I A L

        public DataTable distinctDataTable;

        public void GerarResumoPorFilial()
        {
            StrHtml = "";



            string strsql = "";

            strsql += " SELECT ";
            strsql += " DISTINCT U.LOGIN, UC.IDCLIENTE ";
            strsql += " FROM  ";
            strsql += " USUARIO U ";
            strsql += " INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIO = U.IDUSUARIO ";
            strsql += " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = UC.IDCLIENTE ";
            strsql += " WHERE SITE='ASP' ";
            strsql += " AND UC.IDCLIENTE <> 6915 ";
            strsql += " AND U.LOGIN <> 'ASP.NET' ";
            strsql += " AND U.LOGIN <> 'GIANEWEB' ";
            strsql += " AND U.LOGIN <> 'ST' ";
            strsql += " AND U.LOGIN <> 'TRANSPACIFICO_WEB' ";
            //strsql += " AND U.LOGIN <> 'RICOY' ";
            strsql += " ORDER BY 1  ";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
            DataView dw = dt.DefaultView;
            distinctDataTable = dw.ToTable(true, "LOGIN");
            string emailTxt = "";
            string clientes = "";
            string Resultclientes = "";
            StrHtml = "<html>";


            StrHtml += "<head>";
            //StrHtml += "<link href='http://www.grupologos.com.br//SistranWeb.NET/Styles/estilos.css' rel='stylesheet' type='text/css' />";


            StrHtml += "<style>";

            StrHtml += "body";
            StrHtml += "{";
            StrHtml += "margin: 0px;";
            StrHtml += "background-color: #f8f8f8;";
            StrHtml += "font-family: Verdana;";
            StrHtml += "font-size: 7pt;";

            StrHtml += "}";
            StrHtml += "form";
            StrHtml += "{";
            StrHtml += "margin: 0px;";
            StrHtml += "text-align: center;";
            StrHtml += "}";


            StrHtml += ".cabecalho";
            StrHtml += "{";
            StrHtml += "border-style: none;";
            StrHtml += "border-color: inherit;";
            StrHtml += "border-width: 0px;";
            StrHtml += "height: 25px;";
            StrHtml += "padding-left: 11px;";
            StrHtml += "background: url(../Imagens/vermelho.jpg);";
            StrHtml += "color:Black;";
            StrHtml += "text-align: center;";
            StrHtml += "}";
            StrHtml += ".table";
            StrHtml += "{";
            StrHtml += "background-color: #E0E0E0;";
            //StrHtml += "width: 100%;";
            StrHtml += "font-family: Arial, Helvetica, sans-serif;";
            StrHtml += "font-size: 7pt;";
            StrHtml += "font-weight: bold;";

            StrHtml += "}";

            StrHtml += ".tableSemTamanho";
            StrHtml += "{";
            StrHtml += "background-color: #E0E0E0;";
            StrHtml += "font-family: Arial, Helvetica, sans-serif;";
            StrHtml += "font-size: 7pt;";
            StrHtml += "font-weight: bold;";


            StrHtml += "}";


            StrHtml += ".tdp";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "font-weight:normal;";
            StrHtml += "text-align: left;";
            StrHtml += "vertical-align:middle;	";

            StrHtml += "}";


            StrHtml += ".tdp2";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "font-weight:normal;";
            StrHtml += "text-align: left;";
            StrHtml += "vertical-align:bottom;	";

            StrHtml += "}";

            StrHtml += ".tdpSemAlign";
            StrHtml += "{";
            StrHtml += "border: 0.5pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "font-weight:normal;";
            StrHtml += "}";


            StrHtml += ".tdpCenter";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align: center ;";
            StrHtml += "nowrap:now";
            StrHtml += "}";
            StrHtml += ".tdpR";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align:right;";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "font-weight:normal;	";
            StrHtml += "}";


            StrHtml += ".tdp7";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "height: 12pt;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Arial;";
            StrHtml += "}";

            StrHtml += ".tdpCabecalho";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "height: 13pt;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "font-weight:bold;";
            StrHtml += "text-transform: uppercase;	";
            StrHtml += "}";

            StrHtml += ".td_divisoria";
            StrHtml += "{";
            StrHtml += "border: 0pt solid #FFFFFF;";
            StrHtml += "background-color: #999999;";
            StrHtml += "}";

            StrHtml += ".txtVerdana";
            StrHtml += "{";
            StrHtml += "border: 1px solid #999999;";
            StrHtml += "font-family: Verdana;";
            StrHtml += "font-size: 9px;";
            StrHtml += "height: 12px;";
            StrHtml += "}";

            StrHtml += ".tableVerdana";
            StrHtml += "{";
            StrHtml += "	background-color: #E0E0E0;";
            //StrHtml += "width: 100%;";
            StrHtml += "font-family: Verdana;";
            StrHtml += "font-size: 7pt;	";
            StrHtml += "}";

            StrHtml += ".tdpVerdana";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align: left;";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "}";

            StrHtml += ".tdpVerdana7";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:7pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align: left;";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "font-weight:normal;";
            StrHtml += "}";

            StrHtml += ".tdpRVerdana";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align: right;	";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "}";

            StrHtml += ".tdpRVerdanaLeft";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align:left;	";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "}";


            StrHtml += ".tdpRVerdanaVerde";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align: right;	";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "background-color:#20AE3F;";
            StrHtml += "}";

            StrHtml += ".tdpRVerdanaAmarelo";
            StrHtml += "{";
            StrHtml += "border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align: right;	";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "background-color:#DEDE40;";
            StrHtml += "}";

            StrHtml += ".tdpRVerdanaVermelho";
            StrHtml += "{";
            StrHtml += "	border: 0.1pt solid #FFFFFF;";
            StrHtml += "font-size:8pt;";
            StrHtml += "font-family:Verdana;";
            StrHtml += "text-align: right;	";
            StrHtml += "nowrap:nowrap;";
            StrHtml += "background-color:#DE4040;";
            StrHtml += "}";
            StrHtml += "</style>";

            StrHtml += "</head>";
            StrHtml += "<body>";

            for (int i = 0; i < distinctDataTable.Rows.Count; i++)
            {
                DataRow[] o = dt.Select("LOGIN='" + distinctDataTable.Rows[i][0].ToString() + "'");

                for (int II = 0; II < o.Length; II++)
                {
                    clientes = o[II][1].ToString() + ",";
                }

                if (clientes.Length > 0)
                {
                    clientes = clientes.Substring(0, clientes.Length - 1);
                    Resultclientes = MontarTable(false, clientes, distinctDataTable.Rows[i][0].ToString());
                    if (existeDados)
                        StrHtml += "<hr>";
                }
            }

            StrHtml += "</body>";
            StrHtml += "</html>";
            //Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br;edvaldo@sistecno.com.br", "sistema@grupologos.com.br", "Resumo de Entrega por Filial", StrHtml, "mail.grupologos.com.br", "logos0902", "Resumo Por Filial");
            //new cEmail().enviarEmail("Resumo Por Filial", StrHtml, "moises@sistecno.com.br;edvaldo@sistecno.com.br", "ResumoPorFilial");
        }

        public string StrHtml = "";
        public DataTable dtRegra;

        protected string MontarTable(bool DesprezarNaoEntregues, string IdClientes, string nomeCliente)
        {
            existeDados = true;
            try
            {

                decimal total = Convert.ToDecimal(0);


                Sistran.Library.Enuns.tipoReportResumoFilial en = new Sistran.Library.Enuns.tipoReportResumoFilial();
                en = Sistran.Library.Enuns.tipoReportResumoFilial.filial;

                string[] dadosPesquisa = new string[6];
                dadosPesquisa[0] = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy"); // data inicio
                dadosPesquisa[1] = DateTime.Now.AddDays(-10).ToString("dd/MM/yyyy"); // data fim
                dadosPesquisa[2] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos(); // conexao
                dadosPesquisa[4] = IdClientes; // clientes
                dadosPesquisa[5] = "3"; // qtd de transit time

                DataTable dt = NotasFiscais.ListarResumoPorFilial(Convert.ToDateTime(dadosPesquisa[0]), Convert.ToDateTime(dadosPesquisa[1]), dadosPesquisa[4], dadosPesquisa[2], en, true, true, true);

                if (dt.Rows.Count > 0)
                {
                    StrHtml += "<b>";
                    StrHtml += "CLIENTE: " + nomeCliente;
                    StrHtml += "<BR>";
                    StrHtml += "PERÍODO: " + dadosPesquisa[0] + " À " + dadosPesquisa[1];
                    StrHtml += "<BR>";
                    StrHtml += "</b>";
                }

                dadosPesquisa[3] = "";
                dtRegra = Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT DISTINCT IDCLIENTEMETA, PRAZO, COR, PERCENTUAL FROM CLIENTEMETA WHERE IDCLIENTE IN (" + dadosPesquisa[4] + ")", dadosPesquisa[2]);

                if (dtRegra == null || dtRegra.Rows.Count == 0)
                    dtRegra = Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT DISTINCT IDCLIENTEMETA, PRAZO, COR, PERCENTUAL FROM CLIENTEMETA WHERE IDCLIENTE IN (444)", dadosPesquisa[2]);

                int QtdFiliais = (from DataRow dr in dt.Rows orderby (string)dr["NOMEDAFILIAL"] select (string)dr["NOMEDAFILIAL"]).Distinct().Count();


                //acerta as colunas

                int qtdColunas = 0;
                qtdColunas = (3 * Convert.ToInt32(dadosPesquisa[5])) + 4;
                decimal qtdTotalNf = (Convert.ToDecimal(dt.Compute("MAX(TOTALDENOTAS)", "")));

                StrHtml += @"<BR><table class='tableSemTamanho' cellspacing='1' celpanding='1'  runat='server' >";
                int contDefColun = 0;
                string NomeColuna = "";

                #region Cabeçalho

                //cabeçalho
                StrHtml += @"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                int TT = 0;
                for (int i = 0; i < qtdColunas; i++)
                {
                    if (i == 0)
                    {
                        StrHtml += @"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>";
                        StrHtml += @"</td>";
                    }
                    else
                    {
                        if ((CalcularMaiorZeros(dt, TT) && TT <= Convert.ToInt32(dadosPesquisa[5])) || TT == Convert.ToInt32(dadosPesquisa[5]))
                        {
                            switch (contDefColun)
                            {
                                case 0:
                                    contDefColun += 1;
                                    break;

                                case 1:
                                    if (TT < Convert.ToInt32(dadosPesquisa[5]))
                                        NomeColuna = "&nbsp;&nbsp;&nbsp;" + (TT * 24).ToString() + " HS.";
                                    else
                                        NomeColuna = "&nbsp;" + (TT * 24).ToString() + " HS. ACIMA";

                                    StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold;'>" + NomeColuna;
                                    StrHtml += @"</td>";
                                    contDefColun += 1;
                                    break;

                                case 2:
                                    NomeColuna = "&nbsp;&nbsp;&nbsp;% NF";
                                    StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NomeColuna;
                                    StrHtml += @"</td>";
                                    contDefColun = 0;
                                    TT++;
                                    break;
                            }
                        }
                        else
                        {
                            TT++;
                        }
                    }

                }


                StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold;width:1%'>&nbsp;&nbsp;&nbsp;TOTAL NF ENTREGUES";
                StrHtml += @"</td>";

                StrHtml += @"<td style='background-color: #FFFFFF;' rowspan=" + (QtdFiliais + 2).ToString() + ">&nbsp;&nbsp;&nbsp";
                StrHtml += @"</td>";

                StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold;width:1%'>&nbsp;&nbsp;&nbsp;NF NÃO ENTREGUES";
                StrHtml += @"</td>";


                StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;% NF";
                StrHtml += @"</td>";

                StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;TOTAL DE NOTAS";
                StrHtml += @"</td>";

                StrHtml += @"</tr>";
                #endregion

                int qtdColunasTemp = Convert.ToInt32(dadosPesquisa[5]);
                decimal[] tot = new decimal[qtdColunasTemp + 1];

                int qtdQtdLinhas = 0;


                #region Itens

                for (int i = 0; i < qtdColunas; i++)
                {
                    string NomeFilial = "";
                    for (int ii = 0; ii < dt.Rows.Count; ii++)
                    {
                        if (i == 0)
                        {

                            if (NomeFilial != dt.Rows[ii]["NOMEDAFILIAL"].ToString())
                            {
                                string filtemp = dt.Rows[ii]["NOMEDAFILIAL"].ToString();

                                if (filtemp.Contains("SAO JOSE DOS CAMPOS"))
                                    filtemp = "SJC";

                                if (filtemp.Contains("SAO BERNARDO DO CAMPO"))
                                    filtemp = "SBC";


                                //              StrHtml += @"<tr>";
                                //                StrHtml += @"<td class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal; width:1%'>" + filtemp;
                                //            StrHtml += @"</td>";
                                qtdQtdLinhas++;

                                DataRow[] orw = dt.Select("NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");

                                int contador = 0;
                                int row = 0;
                                decimal porcAtualAcmulado = Convert.ToDecimal("0.00");

                                foreach (DataRow item in orw)
                                {
                                    //CRIA AS COLUNAS ATE ENCONTRAR UM COLUNA VALIDA
                                    if (contador < Convert.ToInt32(item["PRAZOUTILIZADO"]))
                                    {
                                        for (int kkkk = contador; kkkk < Convert.ToInt32(item["PRAZOUTILIZADO"]); kkkk++)
                                        {
                                            string cs = "tdpR";
                                            //se nao tiver a regra continua a regra padrao
                                            if ((contador == 1 || contador == 2) && dtRegra.Rows.Count == 0)
                                            {

                                                //                          StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0";
                                                //                        StrHtml += @"</td>";

                                                switch (contador)
                                                {
                                                    case 1:
                                                        if (porcAtualAcmulado >= Convert.ToDecimal(80))
                                                            cs = "tdpRVerdanaVerde";
                                                        else if (porcAtualAcmulado >= Convert.ToDecimal(75) && porcAtualAcmulado < Convert.ToDecimal(80))
                                                            cs = "tdpRVerdanaAmarelo";
                                                        else
                                                            cs = "tdpRVerdanaVermelho";

                                                        //                              StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%";
                                                        break;

                                                    case 2:
                                                        if (porcAtualAcmulado >= Convert.ToDecimal(90))
                                                            cs = "tdpRVerdanaVerde";
                                                        else if (porcAtualAcmulado >= Convert.ToDecimal(85) && porcAtualAcmulado < Convert.ToDecimal(90))
                                                            cs = "tdpRVerdanaAmarelo";
                                                        else
                                                            cs = "tdpRVerdanaVermelho";

                                                        //                            StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                        break;

                                                    default:
                                                        //                          StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%";
                                                        break;
                                                }
                                                //StrHtml += @"</td>";
                                            }
                                            else if (contador <= int.Parse(dtRegra.Compute("Max(PRAZO)", "").ToString()) && contador > 0 && contador < Convert.ToInt32(dadosPesquisa[5]))
                                            {
                                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0";
                                                // StrHtml += @"</td>";

                                                DataRow[] oroeCor = dtRegra.Select("PRAZO=" + contador, "");
                                                cs = "tdpR";
                                                for (int icor = 0; icor < oroeCor.Length; icor++)
                                                {
                                                    if (decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString()) >= porcAtualAcmulado)
                                                    {
                                                        cs = "tdpRVerdana" + oroeCor[icor]["Cor"].ToString();
                                                        //break;
                                                    }
                                                }


                                                // StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";



                                                //StrHtml += @"</td>";
                                            }
                                            else if (contador <= int.Parse(dadosPesquisa[5]))
                                            {
                                                if (int.Parse(dadosPesquisa[5]) == contador)
                                                {
                                                    decimal qtdItemAtual = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + dadosPesquisa[5] + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                                    decimal qtdTotal = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                                    decimal v = Convert.ToDecimal(0);

                                                    if (qtdTotal > 0)
                                                        v = (qtdItemAtual / qtdTotal) * 100;

                                                    //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + dadosPesquisa[5] + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                                    //StrHtml += @"</td>";
                                                    porcAtualAcmulado += v;

                                                    //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    //StrHtml += @"</td>";
                                                }
                                                else
                                                {
                                                    if (CalcularMaiorZeros(dt, contador))
                                                    {
                                                        //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0";
                                                        //StrHtml += @"</td>";
                                                        //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                        //StrHtml += @"</td>";
                                                    }
                                                }

                                            }
                                            contador += 1;
                                            row += 1;
                                        }
                                    }

                                    if (contador == Convert.ToInt32(item["PRAZOUTILIZADO"]) && contador <= Convert.ToInt32(dadosPesquisa[5]))
                                    {
                                        #region "Itens Calculos"

                                        decimal qtdItemAtual = Convert.ToDecimal(0);
                                        decimal qtdTotal = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));


                                        if (contador < Convert.ToInt32(dadosPesquisa[5]))
                                        {
                                            qtdItemAtual = Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                                        }
                                        else if (contador == Convert.ToInt32(dadosPesquisa[5]))
                                        {
                                            qtdItemAtual = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + dadosPesquisa[5] + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                        }

                                        decimal porcAtual = Convert.ToDecimal(0);

                                        if (qtdTotal > 0)
                                        {
                                            porcAtual = (qtdItemAtual / qtdTotal) * 100;
                                        }
                                        else
                                        {
                                            porcAtual = Convert.ToDecimal(0);
                                        }

                                        if (contador == Convert.ToInt32(dadosPesquisa[5]))
                                        {
                                            //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + dadosPesquisa[5] + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                            //StrHtml += @"</td>";


                                            tot[row] += Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                                            tot[row] += Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO >=" + dadosPesquisa[5] + " AND NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                        }
                                        else
                                        {
                                            if (CalcularMaiorZeros(dt, contador))
                                            {
                                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + item["NOTASFISCAIS_ENTREGUE"].ToString();
                                                //StrHtml += @"</td>";


                                            }
                                            tot[row] += Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);

                                        }

                                        porcAtualAcmulado += porcAtual;

                                        if (dtRegra.Rows.Count == 0)// se nao tiver regra fica com a regra padrao
                                        {
                                            string cs = "tdpR";
                                            switch (contador)
                                            {

                                                case 1:
                                                    if (porcAtualAcmulado >= Convert.ToDecimal(80))
                                                        cs = "tdpRVerdanaVerde";
                                                    else if (porcAtualAcmulado >= Convert.ToDecimal(75) && porcAtualAcmulado < Convert.ToDecimal(80))
                                                        cs = "tdpRVerdanaAmarelo";
                                                    else
                                                        cs = "tdpRVerdanaVermelho";

                                                    // StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    break;
                                                case 2:
                                                    if (porcAtualAcmulado >= Convert.ToDecimal(90))
                                                        cs = "tdpRVerdanaVerde";
                                                    else if (porcAtualAcmulado >= Convert.ToDecimal(85) && porcAtualAcmulado < Convert.ToDecimal(90))
                                                        cs = "tdpRVerdanaAmarelo";
                                                    else
                                                        cs = "tdpRVerdanaVermelho";

                                                    //StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    break;

                                                default:
                                                    //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    break;
                                            }
                                            //StrHtml += @"</td>";
                                        }
                                        else if (contador > 0)// aplica a regra de cores
                                        {
                                            if (CalcularMaiorZeros(dt, contador))
                                            {

                                                DataRow[] oroeCor = dtRegra.Select("PRAZO=" + contador, "");
                                                string cs = "tdpR";

                                                decimal verde = Convert.ToDecimal(0);
                                                decimal amarelo = Convert.ToDecimal(0);
                                                decimal vermelho = Convert.ToDecimal(0);

                                                for (int icor = 0; icor < oroeCor.Length; icor++)
                                                {
                                                    if (oroeCor[icor]["Cor"].ToString() == "Verde")
                                                        verde = decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString());

                                                    if (oroeCor[icor]["Cor"].ToString() == "Amarelo")
                                                        amarelo = decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString());

                                                    if (oroeCor[icor]["Cor"].ToString() == "Vermelho")
                                                        vermelho = decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString());

                                                }

                                                if (porcAtualAcmulado <= vermelho)
                                                {
                                                    cs = "tdpRVerdanaVermelho";
                                                }
                                                else if (porcAtualAcmulado < verde && porcAtualAcmulado <= amarelo)
                                                {
                                                    cs = "tdpRVerdanaAmarelo";
                                                }
                                                else if (porcAtualAcmulado > amarelo && porcAtualAcmulado <= verde)
                                                    cs = "tdpRVerdanaVerde";

                                                if (verde == 0 && vermelho == 0 && porcAtualAcmulado <= amarelo)
                                                {
                                                    cs = "tdpRVerdanaAmarelo";
                                                }

                                                //StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                //StrHtml += @"</td>";
                                            }
                                        }
                                    }
                                        #endregion
                                    row += 1;
                                    contador += 1;
                                }

                                if (contador <= qtdColunasTemp)
                                {
                                    for (int iii = contador; iii <= qtdColunasTemp; iii++)
                                    {
                                        if (contador == 1 || contador == 2 && dtRegra.Rows.Count == 0)
                                        {
                                            //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0";
                                            //StrHtml += @"</td>";

                                            string cs = "tdpR";
                                            switch (contador)
                                            {
                                                case 1:
                                                    if (porcAtualAcmulado >= Convert.ToDecimal(80))
                                                        cs = "tdpRVerdanaVerde";
                                                    else if (porcAtualAcmulado >= Convert.ToDecimal(75) && porcAtualAcmulado < Convert.ToDecimal(80))
                                                        cs = "tdpRVerdanaAmarelo";
                                                    else
                                                        cs = "tdpRVerdanaVermelho";

                                                    //  StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    break;
                                                case 2:
                                                    if (porcAtualAcmulado >= Convert.ToDecimal(90))
                                                        cs = "tdpRVerdanaVerde";
                                                    else if (porcAtualAcmulado >= Convert.ToDecimal(85) && porcAtualAcmulado < Convert.ToDecimal(90))
                                                        cs = "tdpRVerdanaAmarelo";
                                                    else
                                                        cs = "tdpRVerdanaVermelho";

                                                    //StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    break;

                                                default:
                                                    // StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> " + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    break;
                                            }
                                            StrHtml += @"</td>";

                                        }
                                        else if (contador <= int.Parse(dtRegra.Compute("Max(PRAZO)", "").ToString()) && contador > 0)
                                        {
                                            if (CalcularMaiorZeros(dt, contador))
                                            {
                                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0";
                                                //StrHtml += @"</td>";

                                                DataRow[] oroeCor = dtRegra.Select("PRAZO=" + contador, "");
                                                string cs = "tdpR";
                                                for (int icor = 0; icor < oroeCor.Length; icor++)
                                                {
                                                    if (decimal.Parse(oroeCor[icor]["PERCENTUAL"].ToString()) >= porcAtualAcmulado)
                                                    {
                                                        cs = "tdpRVerdana" + oroeCor[icor]["Cor"].ToString();
                                                        // break;
                                                    }
                                                }

                                                //StrHtml += @"<td class='" + cs + "' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                //StrHtml += @"</td>";
                                            }
                                        }
                                        else
                                        {

                                            if (contador == int.Parse(dadosPesquisa[5]))
                                            {
                                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0";
                                                //StrHtml += @"</td>";
                                                /////
                                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                //StrHtml += @"</td>";
                                                //
                                            }
                                            else
                                            {
                                                if (CalcularMaiorZeros(dt, contador))
                                                {
                                                    //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0";
                                                    //StrHtml += @"</td>";

                                                    //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%";
                                                    //StrHtml += @"</td>";
                                                }
                                            }
                                        }

                                        contador += 1;
                                    }
                                }


                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");
                                //StrHtml += @"</td>";
                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");
                                //StrHtml += @"</td>";


                                decimal perc1 = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                decimal perc2 = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'"));
                                decimal perc4 = ((perc1 / perc2) * 100);

                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + perc4.ToString("#0.00") + "%";
                                //StrHtml += @"</td>";

                                //StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");
                                //StrHtml += @"</td>";
                                //StrHtml += @"</tr>";

                                NomeFilial = dt.Rows[ii]["NOMEDAFILIAL"].ToString();
                            }
                        }
                    }
                }
                // StrHtml += @"</tr>";
                #endregion

                //#region Rodape

                StrHtml += @"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";


                decimal totporcAcm = Convert.ToDecimal("0.00");
                TT = 0;
                decimal totAtualPorcent = Convert.ToDecimal(0);
                for (int i = 0; i < qtdColunas; i++)
                {
                    if (i == 0)
                    {
                        StrHtml += @"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>Total de Notas";
                        StrHtml += @"</td>";
                    }
                    else
                    {

                        if ((CalcularMaiorZeros(dt, TT) && TT <= Convert.ToInt32(dadosPesquisa[5])) || TT == Convert.ToInt32(dadosPesquisa[5]))
                        {
                            switch (contDefColun)
                            {
                                case 0:
                                    NomeColuna = "&nbsp;" + TT.ToString();
                                    contDefColun += 1;
                                    break;

                                case 1:

                                    int? qtd = 0;
                                    if (TT == Convert.ToInt32(dadosPesquisa[5]))
                                    {
                                        qtd = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()));
                                    }
                                    else
                                    {
                                        qtd = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()));
                                    }

                                    StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + qtd;
                                    StrHtml += @"</td>";
                                    contDefColun += 1;
                                    break;

                                case 2:

                                    int? qtds = 0;

                                    if (TT == Convert.ToInt32(dadosPesquisa[5]))
                                    {
                                        qtds = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO>=" + TT.ToString()));

                                    }
                                    else
                                    {
                                        qtds = Convert.ToInt32(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()) == DBNull.Value ? 0 : dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + TT.ToString()));
                                    }

                                    decimal AtualPorcent = (Convert.ToDecimal(qtds) / Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", "")) * 100);
                                    totAtualPorcent += AtualPorcent;

                                    StrHtml += @"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + totAtualPorcent.ToString("#0.00") + "%";
                                    StrHtml += @"</td>";
                                    contDefColun = 0;
                                    TT++;
                                    break;
                            }
                        }
                        else
                        {
                            TT++;
                        }
                    }
                }

                StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "");
                StrHtml += @"</td>";
                StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "");
                StrHtml += @"</td>";



                decimal perc1tot = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", ""));
                decimal perc2tot = Convert.ToDecimal(dt.Compute("SUM(TOTALDENOTAS)", ""));
                decimal perc4tot = ((perc1tot / perc2tot) * 100);

                StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + perc4tot.ToString("#0.00") + "%";
                StrHtml += @"</td>";

                StrHtml += @"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(TOTALDENOTAS)", "");
                StrHtml += @"</td>";

                StrHtml += @"</tr>";

                //#endregion


                StrHtml += @"</table>";
                return StrHtml;

            }
            catch (Exception ex)
            {
                existeDados = false;
                return StrHtml + ex.Message;

            }
        }

        public bool existeDados = true;
        protected bool CalcularMaiorZeros(DataTable dts, int tt)
        {
            int? qtd = 0;
            qtd = Convert.ToInt32(dts.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + tt.ToString()) == DBNull.Value ? 0 : dts.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "PRAZOUTILIZADO=" + tt.ToString()));

            int maxTT = Convert.ToInt32(dts.Compute("max(PRAZOUTILIZADO)", ""));

            if ((qtd == 0 && maxTT > 3) || tt == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region P A I N E L  D E  F A T U R A M E N T O
        private void MontarPainelDeFaturamento(string emailss)
        {
            try
            {

                string Tba = "<html><head>";
                Tba += " <STYLE type='text/css'>";

                Tba += " body ";
                Tba += " { ";
                Tba += " margin: 0px; ";
                Tba += " background-color: #f8f8f8; ";
                Tba += " font-family: Verdana; ";
                Tba += " text-align: left; ";
                Tba += " font-size: 12PX; }";

                Tba += " .tdpCabecalhoR";
                Tba += " {";
                Tba += " border: 0.1pt solid #FFFFFF;";
                Tba += " height: 13pt;";
                Tba += " font-size:8pt;";
                Tba += " font-family:Verdana;";
                Tba += " font-weight:bold;";
                Tba += " text-transform: uppercase;	";
                Tba += " text-align:right;";

                Tba += "}";

                Tba += " .table  ";
                Tba += " { ";
                Tba += " background-color: #E0E0E0; ";
                //Tba += " width: 50%; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .tableFundoClaro ";
                Tba += " { ";
                Tba += " background-color: #F8F8F8; ";
                //Tba += " width: 100%; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .tableSemCorFundo ";
                Tba += " {	 ";
                //Tba += " width: 50%; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .table2 ";
                Tba += " { ";
                Tba += " background-color:#E0E0E0 ;  ";
                Tba += " font-family: Arial, Helvetica, sans- ";
                Tba += " } ";

                Tba += " .tdpCenter ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: center ; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " } ";

                Tba += " .tdp ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " text-align: left; ";
                Tba += " vertical-align:middle; ";

                Tba += " } ";
                Tba += " .tdpSemAlign ";
                Tba += " { ";
                Tba += " border: 0.5pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " } ";

                Tba += " .tdpSemAlignGray ";
                Tba += " { ";
                Tba += " border: 0.5pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " background-color:GrayText; ";
                Tba += " } ";


                Tba += " .tdpCenter ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: center ; ";
                Tba += " nowrap:now ";
                Tba += " } ";
                Tba += " .tdpR ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align:right; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal;	 ";
                Tba += " } ";

                Tba += "  .tdpVerdana ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " } ";

                Tba += " .tdpCabecalho ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " height: 13pt; ";
                Tba += " font-size:9pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " font-weight:bold; ";
                Tba += " text-transform: uppercase;	 ";
                Tba += " } ";

                Tba += " </STYLE> ";
                Tba += "</HEAD><body>";


                string juncao = Tba + montarExibicaoMesAtual();
                juncao += "</body></head><html>";

                //Sistran.Library.EnviarEmails.EnviarEmail(emailss, "sistema@grupologos.com.br", "Aviso: Painel de Faturamento", juncao, "mail.grupologos.com.br", "logos0902", "Faturamento Grupo Logos");
                //new cEmail().enviarEmail("Faturamento Grupo Logos", juncao, emailss, "FaturamentoGrupoLogos");

                Sistran.Library.EnviarEmails.EnviarEmail(emailss, "sistema@grupologos.com.br", "Aviso: FATURAMENTO GRUPO LOGOS", juncao, "mail.grupologos.com.br", "logos0902", "FATURAMENTO GRUPO LOGOS");

            }
            catch (Exception)
            {
                reiniciarTimers();
            }
        }


        int contadorCliente = 0;

        private string montarExibicaoMesAtual()
        {
            try
            {
                contadorCliente = 0;
                string htm = "";
                DataTable dtCliente = SistranBLL.Cliente.RetornarListaClienteFaturamento(true, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                #region Cabeçalho

                DataTable dtMesesCliente = GerarMesesClientes();
                DataTable dtDiasCliente = GerarDiasClientes();


                htm += "<table class='table' cellspacing=1 celpanding=1 width='200px'>";

                //linha de cabeçalho 1
                htm += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

                htm += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>código";
                htm += "</td>";

                htm += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>cliente";
                htm += "</td>";

                htm += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>filial";
                htm += "</td>";




                //calcula as colunas meses
                int mesAtual = DateTime.Now.Month;
                int anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

                htm += "<td colspan='" + DateTime.Now.Day + "' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'> Faturamento Diário - Mês: " + DateTime.Now.Month + " / " + DateTime.Now.Year;
                htm += "</td>";

                htm += "<td colspan='1' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>total";
                htm += "</td>";


                //linha de cabeçalho 2

                htm += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

                htm += "<td colspan='3' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>";
                htm += "</td>";

                for (int i = 1; i <= DateTime.Now.Day; i++)
                {
                    htm += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>DIA " + i;
                    htm += "</td>";
                }

                htm += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Faturado";
                htm += "</td>";


                htm += "</tr>";

                #endregion

                #region itens / dias

                foreach (DataRow item in dtCliente.Rows)
                {
                    contadorCliente++;

                    decimal totvlFaturado = 0;

                    htm += "<tr>";
                    htm += "<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["IDCLIENTE"].ToString();
                    htm += "</td>";

                    htm += "<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["RAZAOSOCIALNOME"].ToString();
                    htm += "</td>";

                    htm += "<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["FILIAL"].ToString();
                    htm += "</td>";

                    for (int i = 1; i <= DateTime.Now.Day; i++)
                    {
                        DataRow[] or = dtDiasCliente.Select("dia=" + i + " and idcliente=" + item["idcliente"].ToString() + " AND IDFILIAL=" + item["IDFILIAL"].ToString());

                        decimal vlFaturado = 0;
                        if (or != null && or.Length > 0)
                        {
                            vlFaturado = decimal.Parse(dtDiasCliente.Compute("sum(faturado)", "dia=" + i + "   and idcliente=" + item["idcliente"].ToString() + " AND IDFILIAL=" + item["IDFILIAL"].ToString()).ToString());
                            totvlFaturado += vlFaturado;
                        }

                        if (vlFaturado > 0)
                            htm += "<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>" + vlFaturado.ToString("##,0.00");
                        else
                            htm += "<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";


                        htm += "</td>";

                    }

                    if (totvlFaturado > 0)
                        htm += "<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado.ToString("##,0.00") + "</b>";
                    else
                        htm += "<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>&nbsp";


                    htm += "</td>";
                    htm += "</tr>";

                }

                #endregion


                #region Linha final  Total

                decimal totvlFaturado_ = 0;

                htm += "<tr  style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                htm += "<td class='tdpCabecalho' nowrap=nowrap align='left' style='font-size:7pt;height:10px'><b>Total</b>";
                htm += "</td>";

                htm += "<td class='tdpCabecalhoR' nowrap=nowrap  style='font-size:7pt;height:10px'><b>" + contadorCliente + " </b>";
                htm += "</td>";

                htm += "<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>&nbsp";
                htm += "</td>";

                decimal tottot = 0;
                for (int i = 1; i <= DateTime.Now.Day; i++)
                {
                    totvlFaturado_ = 0;
                    DataRow[] or = dtDiasCliente.Select("dia=" + i);

                    decimal vlFaturado_ = 0;
                    if (or != null && or.Length > 0)
                    {
                        vlFaturado_ = decimal.Parse(dtDiasCliente.Compute("sum(faturado)", "dia=" + i).ToString());
                        totvlFaturado_ += vlFaturado_;
                    }

                    if (vlFaturado_ > 0)
                        htm += "<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado_.ToString("##,0.00") + "</b>";
                    else
                        htm += "<td class='tdpR'  nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";


                    htm += "</td>";

                    tottot += totvlFaturado_;

                }

                if (tottot > 0)
                    htm += "<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'><b>" + tottot.ToString("##,0.00") + "</b>";
                else
                    htm += "<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt;'>&nbsp";


                htm += "</td>";
                htm += "</tr>";




                #endregion

                htm += "</table>";

                htm += montarExibicaoHistorico(dtMesesCliente, dtDiasCliente);

                return htm;
            }
            catch (Exception)
            {
                reiniciarTimers();
                return "";
            }

        }

        private string montarExibicaoHistorico(DataTable dtMesesCliente, DataTable dtDiasCliente)
        {
            try
            {
                string htm = "<hr><br>Histórico dos últimos 12 meses";
                #region Cabeçalho
                DataTable dtCliente = SistranBLL.Cliente.RetornarListaClienteFaturamento(false, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                DataTable dtEmAbertoAnterior = SistranBLL.Cliente.RetornarEmAbertoAnteriores(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                htm += "<table class='table' cellspacing=1 celpanding=1 width='200px'>";

                //linha de cabeçalho 1
                htm += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

                htm += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>código";
                htm += "</td>";

                htm += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>cliente";
                htm += "</td>";

                htm += "<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Em Aberto ";
                htm += "</td>";

                //calcula as colunas meses
                int mesAtual = DateTime.Now.Month;
                int anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());



                for (int i = 1; i <= 12; i++)
                {
                    htm += "<td colspan='2' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>" + mesAtual + "/" + anoMinimo;
                    htm += "</td>";

                    mesAtual++;

                    if (mesAtual > 12)
                    {
                        mesAtual = 1;
                        anoMinimo++;
                    }
                }

                htm += "<td colspan='2' class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Totais";
                htm += "</td>";

                //linha de cabeçalho 2

                htm += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

                htm += "<td colspan='2' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>";
                htm += "</td>";

                htm += "<td colspan='1' class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>(Anterior à " + DateTime.Now.AddMonths(-12).Month.ToString() + "/" + DateTime.Now.AddMonths(-12).Year.ToString() + ")";
                htm += "</td>";

                // calcula as colunas meses
                mesAtual = DateTime.Now.Month;
                anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

                for (int i = 1; i <= 12; i++)
                {
                    htm += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Faturado";
                    htm += "</td>";

                    htm += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Aberto";
                    htm += "</td>";


                    mesAtual++;

                    if (mesAtual > 12)
                    {
                        mesAtual = 1;
                        anoMinimo++;
                    }
                }

                htm += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Faturado";
                htm += "</td>";

                htm += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>Aberto";
                htm += "</td>";

                htm += "</tr>";

                #endregion

                decimal totAbertoAnteriores = 0;

                foreach (DataRow item in dtCliente.Rows)
                {
                    htm += "<tr>";
                    htm += "<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["IDCLIENTE"].ToString();
                    htm += "</td>";

                    htm += "<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["RAZAOSOCIALNOME"].ToString();
                    htm += "</td>";


                    decimal abertoAnterioCliente = 0;
                    if (dtEmAbertoAnterior.Compute("max(EMABERTO)", "idcliente=" + item["IDCLIENTE"].ToString()) != DBNull.Value)
                    {
                        abertoAnterioCliente = decimal.Parse((dtEmAbertoAnterior.Compute("max(EMABERTO)", "idcliente=" + item["IDCLIENTE"].ToString()).ToString()));
                    }

                    if (abertoAnterioCliente > 0)
                        htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px; color:red'>" + abertoAnterioCliente.ToString("#,0.00");
                    else
                        htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px; color:red'>";

                    htm += "</td>";
                    totAbertoAnteriores += abertoAnterioCliente;



                    mesAtual = DateTime.Now.Month;
                    anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

                    decimal totvlFaturado = 0;
                    decimal totvlAberto = 0;

                    for (int i = 1; i <= 12; i++)
                    {

                        DataRow[] or = dtMesesCliente.Select("ano=" + anoMinimo + " and mes=" + mesAtual + " and idcliente=" + item["idcliente"].ToString());

                        decimal vlFaturado = 0;
                        decimal vlAberto = 0;

                        if (or != null && or.Length > 0)
                        {

                            vlFaturado = decimal.Parse(dtMesesCliente.Compute("sum(faturado)", "ano=" + anoMinimo + " and mes=" + mesAtual + "   and idcliente=" + item["idcliente"].ToString()).ToString());
                            totvlFaturado += vlFaturado;

                            vlAberto = decimal.Parse(dtMesesCliente.Compute("sum(emAberto)", "ano=" + anoMinimo + " and mes=" + mesAtual + "   and idcliente=" + item["idcliente"].ToString()).ToString());
                            totvlAberto += vlAberto;
                        }

                        if (vlFaturado > 0)
                            htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>" + vlFaturado.ToString("#,0.00");
                        else
                            htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;";


                        htm += "</td>";

                        if (vlAberto > 0)
                            htm += "<td class='tdpR'  nowrap=nowrap style='font-size:7pt; color:red'>" + vlAberto.ToString("##,0.00");
                        else
                            htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;";


                        htm += "</td>";

                        mesAtual++;

                        if (mesAtual > 12)
                        {
                            mesAtual = 1;
                            anoMinimo++;
                        }
                    }

                    if (totvlFaturado > 0)
                        htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado.ToString("#,0.00") + "</b>";
                    else
                        htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>&nbsp;";

                    htm += "</td>";

                    if (totvlAberto > 0)
                        htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;color:red'><b>" + totvlAberto.ToString("#,0.00") + "</b>";
                    else
                        htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;'>&nbsp;";


                    htm += "</td>";
                    htm += "</tr>";

                }


                #region Linha Total Final

                htm += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' >";
                htm += "<td class='tdpCabecalho' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>Total";
                htm += "</td>";

                htm += "<td class='tdpCabecalho' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>";
                htm += "</td>";


                htm += "<td class='tdpR' nowrap=nowrap style='font-size:7pt;color:red'><b>" + (totAbertoAnteriores == 0 ? "" : totAbertoAnteriores.ToString("#,0.00")) + "</b>";
                htm += "</td>";

                mesAtual = DateTime.Now.Month;
                anoMinimo = int.Parse(dtMesesCliente.Compute("min(ano)", "").ToString());

                decimal totvlFaturado_ = 0;
                decimal totvlAberto_ = 0;

                for (int i = 1; i <= 12; i++)
                {

                    DataRow[] or = dtMesesCliente.Select("ano=" + anoMinimo + " and mes=" + mesAtual);

                    decimal vlFaturado_ = 0;
                    decimal vlAberto_ = 0;

                    if (or != null && or.Length > 0)
                    {

                        vlFaturado_ = decimal.Parse(dtMesesCliente.Compute("sum(faturado)", "ano=" + anoMinimo + " and mes=" + mesAtual).ToString());
                        totvlFaturado_ += vlFaturado_;

                        vlAberto_ = decimal.Parse(dtMesesCliente.Compute("sum(emAberto)", "ano=" + anoMinimo + " and mes=" + mesAtual).ToString());
                        totvlAberto_ += vlAberto_;
                    }

                    if (vlFaturado_ > 0)
                        htm += "<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'><b>" + vlFaturado_.ToString("#,0.00") + "</b>";
                    else
                        htm += "<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;";


                    htm += "</td>";

                    if (vlAberto_ > 0)
                        htm += "<td class='tdpCabecalhoR'  nowrap=nowrap style='font-size:7pt; color:red;'><b>" + vlAberto_.ToString("##,0.00") + "</b>";
                    else
                        htm += "<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;";


                    htm += "</td>";

                    mesAtual++;

                    if (mesAtual > 12)
                    {
                        mesAtual = 1;
                        anoMinimo++;
                    }
                }

                if (totvlFaturado_ > 0)
                    htm += "<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'><b>" + totvlFaturado_.ToString("#,0.00") + "</b>";
                else
                    htm += "<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>";

                htm += "</td>";

                if (totvlAberto_ > 0)
                    htm += "<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt; color:red'><b>" + totvlAberto_.ToString("#,0.00") + "</b>";
                else
                    htm += "<td class='tdpCabecalhoR' nowrap=nowrap style='font-size:7pt;'>";


                htm += "</td>";
                htm += "</tr>";


                #endregion

                htm += "</table>";
                return htm;
            }
            catch (Exception)
            {
                reiniciarTimers();
                return "";
            }
        }

        protected DataTable GerarMesesClientes()
        {
            try
            {
                return SistranBLL.Cliente.GerarMesesClienteFaturamento(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            }
            catch (Exception)
            {
                reiniciarTimers();
                return null;
            }
        }

        protected DataTable GerarDiasClientes()
        {
            try
            {
                return SistranBLL.Cliente.GerarDiasClienteFaturamento(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            }
            catch (Exception)
            {
                reiniciarTimers();
                return null;
            }
        }

        #endregion

        #region LogBD

        private void montarEmailLogBanco()
        {
            DataSet dt = Sistran.Library.GetDataTables.RetornarDataSet("exec sp_informacoes_bd", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            string html = "<html><body><div>";


            for (int i = 0; i < dt.Tables.Count; i++)
            {

                html += "<table border='1' >";

                html += "<tr>";
                html += "<td colspan='" + dt.Tables[i].Columns.Count + "'>";

                switch (i)
                {
                    case 0:
                        html += "MONITORAMENTO DO ESPAÇO EM DISCO";
                        break;

                    case 1:
                        html += "MONITORAMENTO DOS ARQUIVOS SQL";
                        break;


                    case 2:
                        html += "UTILIZAÇÃO DO ARQUIVO DE LOG";
                        break;

                    case 3:
                        html += "BACKUP";
                        break;

                    case 4:
                        html += "JOBS EXECUTADOS";
                        break;

                    case 5:
                        html += "JOBS QUE FALHARAM";
                        break;

                }

                html += "</td>";
                html += "</tr>";


                html += "<tr>";
                for (int cols = 0; cols < dt.Tables[i].Columns.Count; cols++)
                {
                    html += "<td>";
                    html += dt.Tables[i].Columns[cols].ColumnName;
                    html += "</td>";
                }
                html += "</tr>";



                for (int linha = 0; linha < dt.Tables[i].Rows.Count; linha++)
                {
                    html += "<tr>";
                    for (int cols = 0; cols < dt.Tables[i].Columns.Count; cols++)
                    {
                        html += "<td>";
                        html += (dt.Tables[i].Rows[linha][cols].ToString() == "" ? "." : dt.Tables[i].Rows[linha][cols].ToString());
                        html += "</td>";
                    }
                    html += "</tr>";
                }

                html += "</table>";

                html += "<br>";
                html += "<br>";
                html += "<hr>";


            }
            html += "Email Gerado as: " + DateTime.Now;

            html += "</div></body></html>";
            //new cEmail().enviarEmail("LogBancoGrupoLogos", html, "moises@sistecno.com.br; edvaldo@sistecno.com.br", "LogBDGrupoLogos");
            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br; edvaldo@sistecno.com.br", "sistema@grupologos.com.br", "Aviso: LogBancoGrupoLogos", html, "mail.grupologos.com.br", "logos0902", "LogBancoGrupoLogos");



            // Cdean();

        }



        #endregion

        #region O P E R A Ç Ã O  C L I E N T E

        public void enviarOperacaoCliente(string email, List<DadosOperacaoCliente> OperacaoCliente)
        {
            sstr = "";
            try
            {
                for (int iCli = 0; iCli < OperacaoCliente.Count; iCli++)
                {

                    DataSet ds;

                    if (OperacaoCliente[iCli].IdCliente == "9")
                    {
                        ds = Sistran.Library.GetDataTables.RetornarDataSetWS("exec RETORNAROPERACAOCLIENTE_RICOY " + OperacaoCliente[iCli].IdCliente, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }
                    else
                    {
                        ds = Sistran.Library.GetDataTables.RetornarDataSetWS("exec RetornarOperacaoCliente " + OperacaoCliente[iCli].IdCliente, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    }


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        //---------------------------
                        if (i == 0)
                        {
                            sstr += "<table class='table' cellspacing=1 celpanding=1 >";


                            sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                            sstr += "<td class='tdpCenter' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='9'> Clique <A HREF='http://www.grupologos.com.br/reports.net/OperacaoCliente.aspx?idcliente=" + OperacaoCliente[iCli].IdCliente + "&Nome=" + OperacaoCliente[iCli].NomeFantasia + "'>aqui</a> para mais detalhes  </td>";
                            sstr += "</tr>";


                            sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                            sstr += "<td class='tdpCabecalho'  align='left' nowrap=nowrap style='font-size:8pt;width:1%' COLSPAN='9'> " + OperacaoCliente[iCli].NomeFantasia.ToUpper() + " - NOTAS FISCAIS EMITIDAS </td>";
                            sstr += "</tr>";


                            sstr += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                            //sstr += "<td class='tdpCabecalho'  align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA</td>";

                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS <BR>EMITIDAS</td>";
                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO</td>";

                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS <BR>SEM DT </td>";
                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO</td>";

                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FIDCAIS <BR>COM DT</td>";
                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS <BR>ENTREGUES</td>";
                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS FISCAIS <BR>NAO ENTREGUES</td>";


                            sstr += "<td class='tdpCabecalho'  align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% DE <BR>ENTREGUES</td>";
                            sstr += "</tr>";
                            
                            sstr += "<tr'>";                            
                            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Tables[0].Compute("SUM(NFEMITIDAS)", "").ToString()) + "</td>";
                            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + decimal.Parse(ds.Tables[0].Compute("SUM(PESOEMITIDAS)", "").ToString()).ToString("#,0") + "</td>";
                            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Tables[0].Compute("sum(SEMDT)", "").ToString()) + " </td>";
                            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + decimal.Parse(ds.Tables[0].Compute("SUM(PESOSEMDT)", "").ToString()).ToString("#,0") + "</td>";

                           
                            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Tables[0].Compute("sum(COMDT)", "").ToString()) /* .Rows[i]["COMDT"].ToString()) */+ "</td>";
                            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Tables[0].Compute("sum(ENTREGUES)", "").ToString()) /*.Rows[i]["ENTREGUES"].ToString())*/ + "</td>";
                            sstr += "<td class='tdpR'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + int.Parse(ds.Tables[0].Compute("sum(NAOENTREGUES)", "").ToString()) /*.Rows[i]["NAOENTREGUES"].ToString())*/ + "</td>";
                            decimal x = decimal.Parse(ds.Tables[0].Rows[i]["PEC_ENTREGAS"].ToString());
                            string s = "tdpR";


                            sstr += "<td class='" + s + "'  align='right' nowrap=nowrap style='font-size:7pt;width:1%'>" + decimal.Parse(ds.Tables[0].Compute("AVG(PEC_ENTREGAS)", "").ToString()) /*.Rows[i]["PEC_ENTREGAS"].ToString()).ToString("#0.00")*/ + "</td>";
                            sstr += "</tr>";

                            sstr += "<tr style='background-color:white;'><td colspan='9'><hr>";
                            sstr += "</td>";
                            sstr += "</tr>";
                            sstr += "</table>";

                        }
                        else
                            break;
                      

                    }
                    
                }

                string Tba = "<html><head>";
                Tba += " <STYLE type='text/css'>";
                Tba += " body ";
                Tba += " { ";
                Tba += " margin: 0px; ";
                Tba += " background-color: #f8f8f8; ";
                Tba += " font-family: Verdana; ";
                Tba += " text-align: left; ";
                Tba += " font-size: 12PX; }";


                Tba += " .table  ";
                Tba += " { ";
                Tba += " background-color: #E0E0E0; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .tableFundoClaro ";
                Tba += " { ";
                Tba += " background-color: #F8F8F8; ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .tableSemCorFundo ";
                Tba += " {	 ";
                Tba += " font-family: Arial, Helvetica, sans-serif; ";
                Tba += " font-size: 7pt; ";
                Tba += " font-weight: bold; ";
                Tba += " } ";

                Tba += " .table2 ";
                Tba += " { ";
                Tba += " background-color:#E0E0E0 ;  ";
                Tba += " font-family: Arial, Helvetica, sans- ";
                Tba += " } ";

                Tba += " .tdpCenter ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: center ; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " } ";

                Tba += " .tdp ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " text-align: left; ";
                Tba += " vertical-align:middle; ";

                Tba += " } ";
                Tba += " .tdpSemAlign ";
                Tba += " { ";
                Tba += " border: 0.5pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " } ";

                Tba += " .tdpSemAlignGray ";
                Tba += " { ";
                Tba += " border: 0.5pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal; ";
                Tba += " background-color:GrayText; ";
                Tba += " } ";


                Tba += " .tdpCenter ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: center ; ";
                Tba += " nowrap:now ";
                Tba += " } ";
                Tba += " .tdpR ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align:right; ";
                Tba += " nowrap:nowrap; ";
                Tba += " font-weight:normal;	 ";
                Tba += " } ";

                Tba += "  .tdpVerdana ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                //Tba += " text-align: left; ";
                Tba += " nowrap:nowrap; ";
                Tba += " } ";

                Tba += " .tdpCabecalho ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " height: 13pt; ";
                Tba += " font-size:9pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " font-weight:bold; ";
                Tba += " text-transform: uppercase;	 ";
                Tba += " } ";

                Tba += " .tdpRVerdanaVerde ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: right;	 ";
                Tba += " nowrap:nowrap; ";
                Tba += " background-color:#20AE3F; ";
                Tba += " } ";

                Tba += " .tdpRVerdanaAmarelo ";
                Tba += " { ";
                Tba += " border: 0.1pt solid #FFFFFF; ";
                Tba += " font-size:8pt; ";
                Tba += " font-family:Verdana; ";
                Tba += " text-align: right;	 ";
                Tba += " nowrap:nowrap; ";
                Tba += " background-color:#DEDE40; ";
                Tba += " } ";

                Tba += " .tdpRVerdanaVermelho ";
                Tba += " { ";
                Tba += " 	border: 0.1pt solid #FFFFFF; ";
                Tba += " 	font-size:8pt; ";
                Tba += " 	font-family:Verdana; ";
                Tba += " 	text-align: right;	 ";
                Tba += " 	nowrap:nowrap; ";
                Tba += " 	background-color:#DE4040; ";
                Tba += "} ";

                Tba += " </STYLE> ";
                Tba += "</HEAD>";

                if (sstr != "")
                {
                    string juncao = Tba + sstr;
                    juncao += "</body></head><html>";
                    //                new cEmail().enviarEmail("Operação Cliente - " + OperacaoCliente[0].RazaoSocial, juncao, email, "OperacaoCliente");
                    Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "Operação Cliente - " + OperacaoCliente[0].RazaoSocial, juncao, "mail.grupologos.com.br", "logos0902", "Operação Cliente - " + OperacaoCliente[0].RazaoSocial);
                }

            }
            catch (Exception)
            {
                reiniciarTimers();
            }
        }
        #endregion


        #region   R O G E
        private void btnRoge_Click(object sender, EventArgs e)
        {
            //return;
            CdEanReposicaoRoge();
        }
        private void CdEanReposicaoRoge()
        {
            XmlDocument xdoc = new XmlDocument();
            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "INICIOU em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");

            try
            {
                xdoc.Load("http://wslogos01.roge.com.br:8086/csp/roge/WSRoge.WSLogos.cls?soap_method=RetornarDadosCDEAN");
                XmlNodeList xnList = xdoc.GetElementsByTagName("SQL");

                if (xnList.Count == 0)
                {
                    new cEmail().enviarEmail("Roge cdEan", "Nao Encontrou itens para atualizar em: " + DateTime.Now.ToString() + " ITENS PROCESSADOS: " + xnList.Count.ToString(), "moises@sistecno.com.br", "AlertaDoSite");

                    throw new Exception("Nao Encontrou itens para atualizar" + DateTime.Now.ToString());
                }

                string sql = "";
                int erros = 0;
                //string cbErros = "";

                if (xnList.Count > 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql("update ReposicaoRogeEan set Status='D' ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }


                for (int i = 0; i < xnList.Count; i++)
                {
                    try
                    {
                        string cb = xnList[i]["eancodigo"] == null ? "" : xnList[i]["eancodigo"].InnerText;
                        string desc = xnList[i]["nome"] == null ? "" : xnList[i]["nome"].InnerText;

                        sql += "  IF EXISTS (SELECT CodigoDeBarras FROM ReposicaoRogeEan WHERE CodigoDeBarras='" + cb + "') ";
                        sql += " UPDATE ReposicaoRogeEan SET STATUS='U', DataInclusao=GETDATE(), Descricao='" + desc.Trim().ToUpper() + "' WHERE CodigoDeBarras='" + cb.Trim() + "'";
                        sql += "  ELSE ";
                        sql += " INSERT INTO ReposicaoRogeEan (CodigoDeBarras, Status, Descricao) VALUES ('" + cb.Trim() + "', 'I', '" + desc.Trim().ToUpper() + "') ";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        sql = "";
                    }
                    catch (Exception ex1)
                    {
                        erros++;
                        MessageBox.Show(ex1.Message);
                    }
                }
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "Finalizou em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");
            }
            catch (Exception ex)
            {
                new cEmail().enviarEmail("Roge cdEan", ex.Message, "moises@sistecno.com.br", "AlertaDoSite");
                MessageBox.Show(ex.Message);

            }
        }


        private void Cdean()
        {
            XmlDocument xdoc = new XmlDocument();
            Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "INICIOU em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");

            try
            {
                xdoc.Load("http://wslogos01.roge.com.br:8086/csp/roge/WSRoge.WSLogos.cls?soap_method=RetornarDadosCDEAN");
                XmlNodeList xnList = xdoc.GetElementsByTagName("SQL");

                if (xnList.Count == 0)
                {
                    new cEmail().enviarEmail("Roge cdEan", "Nao Encontrou itens para atualizar em: " + DateTime.Now.ToString() + " ITENS PROCESSADOS: " + xnList.Count.ToString(), "moises@sistecno.com.br", "AlertaDoSite");

                    throw new Exception("Nao Encontrou itens para atualizar" + DateTime.Now.ToString());
                }

                string sql = "";
                int erros = 0;
                string cbErros = "";

                if (xnList.Count > 0)
                {
                    Sistran.Library.GetDataTables.ExecutarComandoSql("DELETE FROM COLETORCONFERENCIACDEAN ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                }


                for (int i = 0; i < xnList.Count; i++)
                {
                    try
                    {
                        string cb = xnList[i]["CodigoBarras"] == null ? "" : xnList[i]["CodigoBarras"].InnerText;
                        string uv = xnList[i]["UnidadeVenda"] == null ? "" : xnList[i]["UnidadeVenda"].InnerText;
                        string duv = xnList[i]["DescricaoUnidadeVenda"] == null ? "" : xnList[i]["DescricaoUnidadeVenda"].InnerText;
                        string qtd = xnList[i]["QuantidadeUnidadeVenda"] == null ? "0" : xnList[i]["QuantidadeUnidadeVenda"].InnerText;

                        sql += "  IF EXISTS (SELECT CodigoBarras FROM COLETORCONFERENCIACDEAN WHERE CODIGOBARRAS='" + cb + "') ";
                        sql += " UPDATE COLETORCONFERENCIACDEAN SET UnidadeVenda='" + (uv == "" ? "UN" : uv) + "', DescricaoUnidadeVenda='" + (duv == "" ? "UNIDADE" : duv) + "', QuantidadeUnidadeVenda=" + qtd + ", UltimaAtualizacao=getDate() WHERE CodigoBarras='" + cb + "'";
                        sql += "  ELSE ";
                        sql += " INSERT INTO COLETORCONFERENCIACDEAN (CODIGOBARRAS, UNIDADEVENDA, DESCRICAOUNIDADEVENDA, QUANTIDADEUNIDADEVENDA, UltimaAtualizacao) VALUES ('" + cb + "', '" + (uv == "" ? "UN" : uv) + "', '" + (duv == "" ? "UNIDADE" : duv) + "', " + qtd.ToString() + ", getdate()) ";
                        Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                        sql = "";
                    }
                    catch (Exception)
                    {
                        erros++;
                        cbErros += xnList[i]["CodigoBarras"].ToString() + "<br>";
                    }
                }
                Sistran.Library.EnviarEmails.EnviarEmail("moises@sistecno.com.br", "sistema@grupologos.com.br", "Roge cdEan ", "Finalizou em: " + DateTime.Now.ToString(), "mail.grupologos.com.br", "logos0902", "CDEAN");
            }
            catch (Exception ex)
            {
                new cEmail().enviarEmail("Roge cdEan", ex.Message, "moises@sistecno.com.br", "AlertaDoSite");
            }
        }

        #endregion

        //private void btnTWX_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GravarLog("Iniciou", "btnTWX_Click");
        //        ProcessarTwx();
        //        GravarLog("Finalizou", "btnTWX_Click");

        //    }
        //    catch (Exception ex)
        //    {
        //        GravarLog("Erro: " + ex.Message, "btnTWX_Click");

        //    }
        //}


        public void AcertarDadosDTRomaneio(int iddocumento)
        {
            try
            {
                string strsql = " SELECT top 1 DTR.IDDT, DTR.IDROMANEIO, DT.EMISSAO, DT.DATADESAIDA , RS.IDRASTREAMENTO ";
                strsql += "FROM DOCUMENTO D ";
                strsql += "INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDDOCUMENTO = D.IDDOCUMENTO ";
                strsql += "INNER JOIN DTROMANEIO DTR ON DTR.IDROMANEIO = RD.IDROMANEIO ";
                strsql += "INNER JOIN DT ON DT.IDDT = DTR.IDDT ";
                strsql += "LEFT JOIN RASTREAMENTO RS ON RS.IDDT = DT.IDDT ";
                strsql += "WHERE D.IDDOCUMENTO =  " + iddocumento;
                strsql += " Order by 1 desc";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (dt.Rows.Count == 0)
                    return;

                strsql = "";

                //se a data de saida for nulo acerta para aparecer no monitoramento
                if (dt.Rows[0]["DATADESAIDA"].ToString() == "")
                    strsql += "Update dt set dataDeSaida = getdate() where iddt = " + dt.Rows[0]["IDDT"].ToString();

                if (dt.Rows[0]["IDRASTREAMENTO"].ToString() == "")
                {
                    string x = Sistran.Library.GetDataTables.RetornarIdTabela("RASTREAMENTO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    strsql += "; INSERT INTO RASTREAMENTO (IdRastreamento, IdRastreador, IdDt, Latitude,Longitude,Satelites,DataHora, PontodeOcorrencia, LATI , LONGI, DataHoraTransmissao)";
                    strsql += " VALUES (" + x + ", 1, " + dt.Rows[0]["IDDT"].ToString() + ", 0,0,null,getdate(), 'NAO', NULL , NULL, GETDATE())";
                }

                if (strsql.Length > 10)
                    Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            }
            catch (Exception)
            { }
        }


        public int InserirDocumentoOcorrencia(
                                            string connDestino,
                                            string IDDocumento, string IDFilial,
                                            string IDOcorrencia, DateTime DataOcorrencia,
                                            string Descricao, string finalizadora,
                                            DateTime? DataDeConclusao)
        {
            int IdDocOco = 0;
            try
            {

                // se ja tem uma finalizadora igual
                string sql = " SELECT TOP 1 IDDOCUMENTOOCORRENCIA, O.IDOCORRENCIA  FROM DOCUMENTOOCORRENCIA D INNER JOIN OCORRENCIA O ON  O.IDOCORRENCIA = D.IDOCORRENCIA WHERE IDDOCUMENTO=" + IDDocumento + " AND FINALIZADOR = 'SIM' and o.IdOcorrencia=" + IDOcorrencia + " ORDER BY IDDOCUMENTOOCORRENCIA DESC";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (dt.Rows.Count > 0)
                {
                    return int.Parse(dt.Rows[0][0].ToString());
                }


                sql = " SELECT TOP 1 IDDOCUMENTOOCORRENCIA, O.IDOCORRENCIA  FROM DOCUMENTOOCORRENCIA D INNER JOIN OCORRENCIA O ON  O.IDOCORRENCIA = D.IDOCORRENCIA WHERE IDDOCUMENTO=" + IDDocumento + " AND FINALIZADOR = 'SIM'  ORDER BY IDDOCUMENTOOCORRENCIA DESC";
                dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (dt.Rows.Count > 0)
                {
                    return 0;
                }


                AcertarDadosDTRomaneio(int.Parse(IDDocumento));

                IdDocOco = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
                string strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
                strsql += " IDDocumentoOcorrencia, ";
                strsql += " IdRomaneio, ";
                strsql += " IDDocumento,";
                strsql += " IDFilial,";
                strsql += " IDOcorrencia,";
                strsql += " DataOcorrencia,";
                strsql += " Descricao,";
                strsql += " Sistema";
                strsql += " ) VALUES (";
                strsql += IdDocOco + " ,";
                strsql += "(SELECT TOP 1 RD.IDROMANEIO FROM ROMANEIODOCUMENTO RD WHERE RD.IDDOCUMENTO = " + IDDocumento + " ORDER BY 1 DESC)" + " ,";
                strsql += IDDocumento + " ,";
                strsql += Convert.ToInt32(IDFilial) + " ,";

                //se a data de conclusao for null coloca a ocorrencia se nao apenas uma observação que se caracteriza pelo null no idocorrencia

                if (DataDeConclusao == null)
                    strsql += int.Parse(IDOcorrencia) + " ,";
                else
                    strsql += " null ,";


                //se as datas sao diferentes pega a menor data de ocorrencia

                if (DataDeConclusao != null && DataDeConclusao < DataOcorrencia)
                    DataOcorrencia = (DateTime)DataDeConclusao;

                strsql += " convert(datetime,'" + DataOcorrencia + "', 103) ,";
                strsql += " '" + Descricao.ToUpper().Trim() + " - TWX',";
                strsql += "'SIM'";
                strsql += " );   ";


                if (DataDeConclusao == null && finalizadora == "SIM")
                    strsql += "UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " " + (finalizadora == "SIM" ? " , DATADECONCLUSAO= convert(datetime,'" + DataOcorrencia + "', 103)" : "") + "  WHERE IDDocumento=" + IDDocumento + " ;";


                if (finalizadora == "SIM")
                {
                    strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='PROCESSO FINALIZADO' WHERE IDDOCUMENTO = " + IDDocumento + " ; ";
                    strsql += "UPDATE DOCUMENTO SET DATADECONCLUSAO= convert(datetime,'" + (DataOcorrencia == null ? DateTime.Now : DataOcorrencia) + "', 103)" + " WHERE IDDocumento=" + IDDocumento + "  AND DATADECONCLUSAO IS NULL;";

                }
                else
                {
                    if (DataDeConclusao == null)
                    {
                        strsql += " UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO SOLUCAO' WHERE IDDOCUMENTO = " + IDDocumento + " ; ";
                        strsql += " UPDATE DOCUMENTO SET IDDOCUMENTOOCORRENCIA = " + IdDocOco + " WHERE IDDocumento=" + IDDocumento + " ;";
                    }
                }

                Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                GravarLog("Inseriu a DocumentoOcorrencia: " + IdDocOco, "InserirDocumentoOcorrencia");


                //SE INSERIR A DATA DE CONCLUSAO CALCULA O PRAZO UTILIZADO
                if (DataDeConclusao == null && finalizadora == "SIM")
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWEb(" EXEC SP_PRAZO_UTILIZADO_ID " + IDDocumento.ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());


                return IdDocOco;
            }
            catch (Exception EX)
            {
                GravarLog("Erro: " + EX.Message, "InserirDocumentoOcorrencia");
                return IdDocOco;
            }
        }


        //public int GravarXml(XmlDocument xml)
        //{
        //    string id = Sistran.Library.GetDataTables.RetornarIdTabela("XmlComprovei", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //    string sql = "Insert Into XmlComprovei Values(" + id + ", @Arquivo) ";
        //    SqlConnection cnn = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //    SqlCommand cmm = new SqlCommand();

        //    cmm.CommandText = sql;
        //    cmm.CommandType = CommandType.Text;
        //    cmm.Connection = cnn;


        //    SqlParameter par = new SqlParameter("@arquivo", xml.InnerXml);
        //    cmm.Parameters.Add(par);

        //    try
        //    {
        //        cnn.Open();
        //        cmm.ExecuteNonQuery();
        //        return int.Parse(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //}


        //public void ProcessarTwx()
        //{

        //    try
        //    {
        //        //  return;

        //        GravarLog("Iniciou", "ProcessarTwx");


        //        string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
        //        string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);

        //        GravarLog("Ambiente: " + ambiente, "ProcessarTwx");
        //        GravarLog("URL: " + tbUrl, "ProcessarTwx");


        //        string varName = "";
        //        string varType = "";
        //        string xmlPath = "";
        //        string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Configuration.ConfigurationSettings.AppSettings["Usuario"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["Senha"]));

        //        varName = "qtdDocumentos";
        //        varType = "integer";

        //        xmlPath = "Retorno/Documentos/Documento";
        //        xmlPath = "Retorno/Documentos/item";
        //        WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
        //        string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
        //                       "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
        //                       "<SOAP-ENV:Body>" +
        //                       "<tns:getDocumentsFromPOD xmlns:tns=\"urn:WebServicePOD\">" +
        //                       "<" + varName + " xsi:type=\"xsd:" + varType + "\">" + ConfigurationSettings.AppSettings["QtdDocumentosPorChamada"] + "</" + varName + ">" +
        //                       "</tns:" + "getDocumentsFromPOD" + ">" +
        //                       "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        //        byte[] data = Encoding.ASCII.GetBytes(postData);
        //        request.Method = "POST";
        //        request.ContentType = "text/xml; charset=ISO-8859-1";
        //        request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
        //        request.Headers.Add("Authorization", "Basic " + auth);
        //        request.ContentLength = data.Length;


        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }


        //        Cursor.Current = Cursors.WaitCursor;
        //        WebResponse response = (HttpWebResponse)request.GetResponse();
        //        StreamReader sr = new StreamReader(response.GetResponseStream());

        //        XmlDocument xmlAwnser = new XmlDocument();
        //        xmlAwnser.LoadXml(sr.ReadToEnd());

        //        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
        //        nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //        xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);

        //        string documents = "";
        //        int countDocument = 1;

        //        // GravarXML(xmlAwnser.InnerXml, DateTime.Now);

        //        int idXML = GravarXml(xmlAwnser);                

        //        foreach (XmlNode xmlDocument in xmlAwnser.SelectNodes(xmlPath))
        //        {
        //            GravarLog("", "");
        //            GravarLog("---------------------------------------------------------------------------------------------------", "");
        //            GravarLog("##: " + countDocument, "");


        //            if (xmlDocument["Erro"] == null)
        //            {
        //                string sKey = (xmlDocument["Chave"] != null) ? xmlDocument["Chave"].InnerText : "";
        //                GravarLog("Chave da Nota: " + sKey, "ProcessarTwx");

        //                //PEGA A NOTA
        //                string sql = "SELECT * FROM DOCUMENTO WHERE DOCUMENTODOCLIENTE4='" + sKey + "'";
        //                DataTable dtNota = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                //se nao achar a nota vai para a proxima
        //                if (dtNota.Rows.Count == 0)
        //                {
        //                    GravarLog("Não Encontro o documento:" + sKey, "ProcessarTwx");
        //                    continue;

        //                }

        //                GravarLog("IDDOCUMENTO: " + dtNota.Rows[0]["IDDOCUMENTO"].ToString(), "ProcessarTwx");
        //                GravarLog("NUMERO: " + dtNota.Rows[0]["Numero"].ToString(), "ProcessarTwx");

        //                int qtdOcorrencia = 0;
        //                if (xmlDocument.SelectNodes("Ocorrencias/Ocorrencia") != null)
        //                {
        //                    qtdOcorrencia = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia").Count;
        //                }

        //                for (int i = qtdOcorrencia; i >= 1; i--)
        //                {
        //                    XmlNode xmlOccurrence = xmlDocument.SelectNodes("Ocorrencias/Ocorrencia")[i - 1];

        //                    //}


        //                    //foreach (XmlNode xmlOccurrence in xmlDocument.SelectNodes("Ocorrencias/Ocorrencia"))
        //                    //{
        //                    string occurenceNumber = xmlOccurrence["Numero"].InnerText;
        //                    string[] oco = xmlOccurrence["Motivo"].InnerText.Split('-');


        //                    oco[0] = oco[0].Trim();
        //                    oco[1] = oco[1].Trim();

        //                    GravarLog("NumeroOcorrencia: " + occurenceNumber, "ProcessarTwx");
        //                    GravarLog("CodOcorrencia: " + oco[0], "ProcessarTwx");
        //                    GravarLog("DescOcorrencia: " + oco[1], "ProcessarTwx");

        //                    string sqlaux = "SELECT * FROM OCORRENCIA WHERE IDOCORRENCIASERIE=3 AND CODIGO='" + oco[0] + "' ";
        //                    DataTable dtOco = Sistran.Library.GetDataTables.RetornarDataTableWin(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                    int idOcorrencia = 0;
        //                    string finalzadora = "SIM";

        //                    // se nao existir a ocorrencia insere
        //                    if (dtOco.Rows.Count == 0)
        //                    {
        //                        GravarLog("Gravando Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

        //                        idOcorrencia = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("OCORRENCIA", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()));
        //                        sqlaux = "insert into ocorrencia (IDOcorrencia, IDEmpresa, IDOcorrenciaAcao, Codigo, Nome, Responsabilidade, NomeReduzido, PagaEntrega, Finalizador, Sistema,  Ativo, RestringirCarregamento, AbrirFecharOcorrencia, ApareceSiteCliente, IdOcorrenciaSerie)";
        //                        sqlaux += "VALUES (" + idOcorrencia + ", NULL, 5, '" + oco[0] + "', '" + (oco[1].Trim().ToUpper().Length >= 60 ? oco[1].Trim().ToUpper().Substring(0, 59) : oco[1].Trim().ToUpper()) + "', 'CLIENTE', '" + (oco[1].Trim().ToUpper().Length >= 30 ? oco[1].Trim().ToUpper().Substring(0, 29) : oco[1].Trim().ToUpper()) + "', 'NAO', 'SIM', NULL,  'SIM', NULL, 'AMBOS', NULL, 3)";

        //                        Sistran.Library.GetDataTables.ExecutarRetornoIDWIN(sqlaux, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                        GravarLog("Gravou Uma Nova Ocorrencia: " + oco[1], "ProcessarTwx");

        //                    }
        //                    else
        //                    {
        //                        GravarLog("Já existe a Ocorrencia apenas seta a Variavel: " + oco[1], "ProcessarTwx");
        //                        idOcorrencia = int.Parse(dtOco.Rows[0]["IDOCORRENCIA"].ToString());
        //                        finalzadora = dtOco.Rows[0]["Finalizador"].ToString();
        //                    }


        //                    DateTime dataOco = DateTime.Now;

        //                    if (xmlOccurrence["Data"].InnerText != "")
        //                    {
        //                        dataOco = DateTime.Parse(xmlOccurrence["Data"].InnerText);
        //                        GravarLog("DataDaOcorrencia: " + dataOco, "ProcessarTwx");

        //                    }

        //                    DateTime? DataDeConclusaoDocumento = null;
        //                    int IdDocOco = 0;
        //                    if (dtNota.Rows[0]["DataDeConclusao"].ToString().Length > 0)
        //                    {

        //                        DataDeConclusaoDocumento = DateTime.Parse(dtNota.Rows[0]["DataDeConclusao"].ToString());
        //                        GravarLog("DataDoDocumento já Finalizado: " + dtNota.Rows[0]["DataDeConclusao"].ToString(), "ProcessarTwx");

        //                    }

        //                    try
        //                    {
        //                        IdDocOco = InserirDocumentoOcorrencia(
        //                                                     Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos(),
        //                                                     dtNota.Rows[0]["IDDOCUMENTO"].ToString(),
        //                                                     dtNota.Rows[0]["IDFILIALATUAL"].ToString(),
        //                                                     idOcorrencia.ToString(),
        //                                                     dataOco,
        //                                                     oco[1],
        //                                                     finalzadora,
        //                                                     DataDeConclusaoDocumento);
        //                    }
        //                    catch (Exception)
        //                    {
        //                    }

        //                    try
        //                    {
        //                        if (IdDocOco > 0)
        //                        {
        //                            XmlNode xmlPhoto = xmlOccurrence.SelectSingleNode("Foto");

        //                            if (xmlPhoto != null)
        //                            {
        //                                if (!String.IsNullOrEmpty(xmlPhoto.InnerText))
        //                                {
        //                                    string dado = xmlPhoto["Dado"].InnerText;
        //                                    if (!String.IsNullOrEmpty(dado))
        //                                    {
        //                                        if (IdDocOco > 0)
        //                                        {
        //                                            byte[] info = Convert.FromBase64String(dado);

        //                                            string id = Sistran.Library.GetDataTables.RetornarIdTabela("DOCUMENTOOCORRENCIAARQUIVO", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //                                            string strsql = "INSERT INTO DocumentoOcorrenciaArquivo (IDDocumentoOcorrenciaArquivo, IDDocumentoOcorrencia, Arquivo) VALUES (" + id + ", " + IdDocOco + ", @IMAGEM)";
        //                                            SqlCommand command = new SqlCommand();

        //                                            SqlConnection vv = new SqlConnection(Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //                                            command.CommandText = strsql.ToString();
        //                                            command.CommandType = CommandType.Text;
        //                                            command.Connection = vv;
        //                                            command.Parameters.Add(new SqlParameter("@IMAGEM", info));
        //                                            vv.Open();
        //                                            command.ExecuteNonQuery();
        //                                            vv.Close();
        //                                            vv.Dispose();
        //                                            GravarLog("Gravou a foto - IdOcorrenciaFoto: " + id, "ProcessarTwx");
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    catch (Exception)
        //                    {
        //                    }
        //                }

        //                countDocument++;
        //            }
        //            else
        //            {
        //                documents = xmlDocument["Erro"].InnerText;
        //            }
        //        }



        //        if (String.IsNullOrEmpty(documents))
        //        {
        //            documents = xmlAwnser.FirstChild["Erro"].InnerText;
        //            GravarLog(documents, "Erro");

        //        }

        //        request = null;
        //        response = null;
        //        xmlAwnser = null;

        //        Cursor.Current = Cursors.Default;
        //    }
        //    catch (Exception)
        //    {
        //        reiniciarTimers();
        //    }
        //}
        
        public void GravarLog(string MenssagemLog, string NomeFuncao)
        {
            try
            {
                StreamWriter valor = new StreamWriter(@".\log\\Log_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".txt", true, Encoding.Unicode);
                valor.Write(DateTime.Now.ToString() + " | " + NomeFuncao + " | " + MenssagemLog + "\r\n");
                valor.Close();
            }
            catch (Exception)
            {
            }
        }

        public void GravarXML(string conteudo, DateTime d)
        {
            try
            {
                string no = d.ToString().Replace(".", "_").Replace(":", "_").Replace("/", "_") + ".xml";
                StreamWriter valor = new StreamWriter(@".\xml\\" + no, true, Encoding.Unicode);
                valor.Write(conteudo);
                valor.Close();

                GravarLog("NomeXml: " + no, "ProcessarTwx");

            }
            catch (Exception)
            {
            }
        }

        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        timer1.Enabled = false;
        //        timer2.Enabled = false;

        //        GravarExcecucao("Timer2");
        //        enviarEmailPedido();
        //        Enviar_Comprovei();
        //        //reiniciarTimers();
        //    }
        //    catch (Exception)
        //    {
        //        reiniciarTimers();
        //    }
        //    finally
        //    {
        //        timer1.Enabled = true;
        //    }

        //}


        //public string Base64Encode(string plainText)
        //{
        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        //    return System.Convert.ToBase64String(plainTextBytes);
        //}


        //private void EnviarComprovei()
        //{

        //    Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE DOCUMENTO SET NOMEDOARQUIVO1=NULL, DOCUMENTODOCLIENTE4=NULL WHERE  LTRIM(RTRIM(DOCUMENTODOCLIENTE4))='' AND DATADEEMISSAO>='2015-09-20' AND TIPODEDOCUMENTO='NOTA FISCAL'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        //    if (checkBox1.Checked == false)
        //        return;

        //    string ambiente = System.Configuration.ConfigurationSettings.AppSettings["AmbienteTwx"];
        //    string tbUrl = (ambiente == "Homologacao" ? System.Configuration.ConfigurationSettings.AppSettings["Url_Homologacao"] : System.Configuration.ConfigurationSettings.AppSettings["Url_Producao"]);
        //    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("logos" + ":" + "admin"));


        //    DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_INTEGRAR_COMPROVEI ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

        //    DataView view = new DataView(dtGeral);
        //    DataTable dtds = view.ToTable(true, "IDDOCUMENTO");
        //    const string quote = "\"";
        //    string xml = "";
        //    string ch = "";

        //    // Label2.Text = "Enviando Comprovei";

        //    for (int i = 0; i < dtds.Rows.Count; i++)
        //    {
        //        xml = "";

        //        if (i == 1000)
        //            return;

        //        try
        //        {
        //            xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "UTF-8" + quote + " ?>";
        //            xml += "<Documentos>";
        //            DataRow[] dr = dtGeral.Select("IDDOCUMENTO=" + dtds.Rows[i]["IDDOCUMENTO"].ToString(), "");

        //            ch = dr[0]["CHAVE"].ToString();

        //            // Label2.Text = "Enviando Comprovei: " + ch;

        //            bool gerouCahve = false;

        //            if (ch.Contains("SERNUMERONOT"))
        //            {
        //                ch = ch.Replace("SER", dr[0]["SERIE"].ToString().PadLeft(3, '0'));
        //                ch = ch.Replace("NUMERONOT", dr[0]["NUMERO"].ToString().PadLeft(9, '0'));

        //                gerouCahve = true;
        //            }



        //            xml += "<Documento>";
        //            xml += "<Tipo>NFE</Tipo>";
        //            xml += "<TipoParada>E</TipoParada>";
        //            xml += "<Modelo>55</Modelo>";
        //            xml += "<Numero>" + dr[0]["NUMERO"].ToString() + "</Numero>";
        //            xml += "<Valor>" + dr[0]["VALORDANOTA"].ToString().Replace(",", ".") + "</Valor>";

        //            if (dr[0]["IDCLIENTE"].ToString() == "65286")
        //            {
        //                //se o cliente for fastshop procura a serie na chave da nota

        //                if (ch.Length == 44)
        //                {
        //                    xml += "<Serie>" + ch.Substring(22, 3) + "</Serie>";
        //                }
        //            }
        //            else
        //                xml += "<Serie>" + (dr[0]["SERIE"].ToString().Trim() == "NFE" ? "001" : dr[0]["SERIE"].ToString().Trim()) + "</Serie>";



        //            xml += "<Emissao>" + DateTime.Parse(dr[0]["DATADEEMISSAO"].ToString()).ToString("yyyyMMdd") + "</Emissao>";
        //            xml += "<Atualizacao>" + DateTime.Parse(dr[0]["ATUALIZACAO"].ToString()).ToString("yyyyMMdd") + "</Atualizacao>";
        //            xml += "<Chave>" + ch + "</Chave>";
        //            xml += "<cnpj>" + dr[0]["CNPJ"].ToString() + "</cnpj>";

        //            xml += "<cnpjEmissor>" + dr[0]["CNPJEMISSOR"].ToString() + "</cnpjEmissor>";
        //            xml += "<cnpjTransportador>" + dr[0]["CNPJTRANSPORTADOR"].ToString() + "</cnpjTransportador>";

        //            xml += "</Documento>";
        //            xml += "<Cliente>";
        //            xml += "<Codigo>" + dr[0]["CODIGOCLIENTE"].ToString() + "</Codigo>";
        //            xml += "<Contato>" + dr[0]["CONTATO"].ToString() + "</Contato>";
        //            xml += "<Telefone>" + dr[0]["TELEFONE"].ToString() + "</Telefone>";
        //            xml += "<Email>" + dr[0]["EMAIL"].ToString() + "</Email>";
        //            xml += "<Razao>" + dr[0]["RAZAO"].ToString().Replace("&", "") + "</Razao>";
        //            xml += "<Endereco>" + dr[0]["ENDERECO"].ToString() + ", " + dr[0]["NUMERO_END"].ToString() + "</Endereco>";
        //            xml += "<Bairro>" + dr[0]["BAIRRO"].ToString() + "</Bairro>";
        //            xml += "<Cidade>" + dr[0]["CIDADE"].ToString() + "</Cidade>";
        //            xml += "<Estado>" + dr[0]["ESTADO"].ToString() + "</Estado>";
        //            xml += "<Pais>BRASIL</Pais>";
        //            xml += "<CEP>" + dr[0]["CEP"].ToString() + "</CEP>";
        //            xml += "<Regiao>" + dr[0]["REGIAO"].ToString() + "</Regiao>";
        //            xml += "<TipoCliente></TipoCliente>";
        //            xml += "<Mensagem></Mensagem>";
        //            xml += "</Cliente>";

        //            xml += "<SKUs>";
        //            bool jaInseriu = false;

        //            for (int ii = 0; ii < dr.Length; ii++)
        //            {
        //                if (dr[ii]["CODIGOPR"].ToString() != "")
        //                {
        //                    xml += "<SKU codigo=" + quote + dr[ii]["CODIGOPR"].ToString() + quote + ">";
        //                    xml += "<PesoBruto>" + dr[ii]["PESOBRUTO"].ToString() + "</PesoBruto>";
        //                    xml += "<PesoLiquido>" + dr[ii]["PESOLIQUIDO"].ToString() + "</PesoLiquido>";
        //                    xml += "<Volumes>" + dr[ii]["VOLUMES"].ToString() + "</Volumes>";
        //                    xml += "<Descricao>" + dr[ii]["DESCRICAO"].ToString().Replace("'", "").Replace("&", "e") + "</Descricao>";
        //                    xml += "<Qde>" + dr[ii]["QUANTIDADE"].ToString().Replace(",", ".") + "</Qde>";
        //                    xml += "<Uom>" + dr[ii]["UNIDADEDEMEDIDA"].ToString() + "</Uom>";
        //                    xml += "<Barcode>" + dr[ii]["BARCODE"].ToString() + "</Barcode>";
        //                    xml += "</SKU>";
        //                }
        //                else
        //                {
        //                    if (jaInseriu == false)
        //                    {
        //                        xml += "<SKU codigo=" + quote + dr[0]["NUMERO"].ToString() + quote + ">";
        //                        xml += "<PesoBruto>0</PesoBruto>";
        //                        xml += "<PesoLiquido>0</PesoLiquido>";
        //                        xml += "<Volumes>1</Volumes>";
        //                        xml += "<Descricao>DANFE " + dr[0]["NUMERO"].ToString() + "</Descricao>";
        //                        xml += "<Qde>1</Qde>";
        //                        xml += "<Uom>FL</Uom>";
        //                        xml += "<Barcode>" + ch + "</Barcode>";
        //                        xml += "</SKU>";
        //                        jaInseriu = true;
        //                    }
        //                }
        //            }
        //            xml += "</SKUs> ";

        //            xml += " </Documentos> ";

        //            string auxXML = xml;
        //            xml = Base64Encode(xml);

        //            if (ch.Contains("NFE"))
        //            {
        //                ch = ch.Replace("NFE", "000");
        //                gerouCahve = true;
        //            }

        //            string NomeArquivo = ch + ".xml";

        //            WebRequest request = (HttpWebRequest)WebRequest.Create(tbUrl);
        //            string postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" +
        //                           "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<SOAP-ENV:Body>" +
        //                           "<tns:importDocsToPOD xmlns:tns=\"urn:WebServicePOD\">" +
        //                           "<conteudoArquivo xsi:type=\"xsd:string\">" + xml + "</conteudoArquivo>" +
        //                           "<nomeArquivo xsi:type=\"xsd:string\">" + NomeArquivo + "</nomeArquivo>" +
        //                           "</tns:" + "importDocsToPOD" + ">" +
        //                           "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        //            byte[] data = Encoding.ASCII.GetBytes(postData);
        //            request.Method = "POST";
        //            request.ContentType = "text/xml; charset=ISO-8859-1";
        //            request.Headers.Add("SOAPAction", "urn:WebServicePOD#getDocumentsFromPOD");
        //            request.Headers.Add("Authorization", "Basic " + auth);
        //            request.ContentLength = data.Length;


        //            using (var stream = request.GetRequestStream())
        //            {
        //                stream.Write(data, 0, data.Length);
        //            }


        //            Cursor.Current = Cursors.WaitCursor;
        //            WebResponse response = (HttpWebResponse)request.GetResponse();
        //            StreamReader sr = new StreamReader(response.GetResponseStream());

        //            XmlDocument xmlAwnser = new XmlDocument();
        //            xmlAwnser.LoadXml(sr.ReadToEnd());


        //            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlAwnser.NameTable);
        //            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //            xmlAwnser.LoadXml(xmlAwnser.DocumentElement.SelectSingleNode("soap:Body", nsmgr).FirstChild.FirstChild.OuterXml);


        //            string retorno = xmlAwnser["status"].InnerText;

        //            retorno = retorno.Replace("'", "");
        //            retorno = retorno.Replace("/", "");
        //            retorno = retorno.Replace("\\", "");
        //            retorno = retorno.Replace("??", "CA");

        //            string sql = "UPDATE DOCUMENTO SET NOMEDOARQUIVO1='ENVIADO COMPROVEI - " + DateTime.Now + "' " + (gerouCahve == true ? ", DocumentoDoCliente4='" + ch + "'" : "") + " WHERE IDDOCUMENTO = " + dtds.Rows[i]["IDDOCUMENTO"].ToString();
        //            sql += " INSERT INTO DOCUMENTOEDI (IDDOCUMENTOEDI, TIPO, IDDOCUMENTO, DATA, NOMEDOARQUIVO) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("DocumentoEdi", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()) + ", 'COMPROVEI' , " + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", GETDATE(), '" + NomeArquivo + " - " + (retorno.Contains(";") ? "" : retorno) + "') ";
        //            sql += " INSERT INTO LOGCOMPROVEI (IdDocumento, Chave, DataHora, Resultado, XML) values (" + dtds.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + ch + "', getDate(), '" + retorno + "', 'xml') ";

        //            Sistran.Library.GetDataTables.ExecutarComandoSql(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        //            GravarXML(auxXML, DateTime.Now);
        //            GravarLog(ch, retorno);
        //            // Label2.Text = "Terminou Envio Comprovei";

        //        }
        //        catch (Exception ex)
        //        {
        //            GravarLog("Chave: " + ch + ". Erro:" + ex.Message, "Enviar Comprovei");
        //            // Label2.Text = ex.Message;
        //        }
        //    }
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    timer2.Enabled = false;
        //    // Thread t = new Thread(Enviar_Comprovei);
        //    //t.Start();
        //    Enviar_Comprovei();

        //    // EnviarComprovei();
        //    Application.Exit();
        //}

        private void button5_Click(object sender, EventArgs e)
        {
            List<DadosOperacaoCliente> ricoy = new List<DadosOperacaoCliente>();
            DadosOperacaoCliente r = new DadosOperacaoCliente();
            r.IdCliente = "9";
            r.NomeFantasia = "Ouro Azul";
            r.RazaoSocial = "Ouro Azul";
            ricoy.Add(r);

            enviarOperacaoCliente("moises@sistecno.com.br", ricoy);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //timer2.Enabled = false;

            //string arquivo = @"C:\DESENVOLVIMENTO\Projetos\Logos\RoboDeEmails\Robo.Email.Notas.Solutions.Windows\bin\Debug\Xml\22_10_2015 10_00_20.xml";
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(arquivo);

            //GravarXml(xmlDoc);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse(dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text), "OPERACAO DIVERSAS");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse(dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text), "OPERACAO FRACIONADA");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse(dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text), "OPERACAO");


            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-01-01"), DateTime.Parse("2016-01-31"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-02-01"), DateTime.Parse("2016-02-29"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-03-01"), DateTime.Parse("2016-03-31"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-04-01"), DateTime.Parse("2016-04-30"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-05-01"), DateTime.Parse("2016-05-31"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-06-01"), DateTime.Parse("2016-06-30"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-07-01"), DateTime.Parse("2016-07-31"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-08-01"), DateTime.Parse("2016-08-31"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-09-01"), DateTime.Parse("2016-09-30"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-10-01"), DateTime.Parse("2016-10-31"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-11-01"), DateTime.Parse("2016-11-30"), "");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"), "");

            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse(dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text), "OPERACAO DIVERSAS");
            //MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse(dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text), "OPERACAO FRACIONADA");
            MontarEmailRERegiao_BaseNovaLogos("moises@sistecno.com.br", true, DateTime.Parse(dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text), "");
          

            timer1.Enabled = true;
            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime agora = DateTime.Now;
            int dia_semana = (int)agora.DayOfWeek;

            if (dia_semana == 2)
            {
                MontarPainelDeFaturamento("moises@sistecno.com.br");
                
            }
        }
    }
}
public class DadosOperacaoCliente
{
    public string IdCliente { get; set; }
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
}

public class AuthHeader : SoapHeader
{
    public string Username;
    public string Password;
}