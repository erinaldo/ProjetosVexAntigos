using System.Text;
using System.Data;
using System.Web;
using System.Data.Common;
using System.IO;
using System;
using System.Collections.Generic;



namespace SistranDAO
{
    public sealed class KPI_Irwin
    {
        //string conexao = "Data Source=192.168.10.5;Initial Catalog=STNNOVO;User ID=site_ASP;Password=asp7998;";
        string conexao = "Data Source=192.168.10.4;Initial Catalog=GrupoLogos;User ID=site_ASP;Password=asp7998;";
        string caminho = "\\\\192.168.10.1\\Inetpub\\wwwroot\\BKP\\ScriptsKPI_V2\\";
        //string caminho = "\\\\192.168.10.1\\Inetpub\\wwwroot\\BKP\\ScriptsKPI\\";

        public DataTable Pesquisar(string mesAno, string descricao, int idGrupoDeProduto)
        {
            string m = "SELECT ";
            m += " IDKPIIRWINMOV, ";
            m += " MESANO, ";
            m += " DIA, ";
            m += " VALOR,  ";
            m += " KM.IDGRUPODEPRODUTO,    ";
            m += " KM.IDKPIIRWIN, ";
            m += " CHAVE, ";
            m += " K.Descricao, ";
            m += " GP.Descricao GRUPO, ";
            m += " DESCRICAOUNIDADEDEMEDIDA, ";
            m += " DESCRICAOTARGUET, ";
            m += " TARGET, ";
            m += " UNIDADETARGET  ";
            m += " FROM KPIIRWIN_V2 K ";
            m += " INNER JOIN KPIIRWINMOV_V2 KM ON KM.IDKPIIRWIN = K.IDKPIIRWIN ";
            m += " INNER JOIN GrupoDeProduto GP on GP.IDGrupoDeProduto = KM.IdGrupoDeProduto ";
            m += " WHERE KM.MESANO LIKE '" + mesAno + "%' AND K.DESCRICAO LIKE '" + descricao + "%' ";
            m += " AND IDCliente in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            m += " AND GP.IDGrupoDeProduto =" + idGrupoDeProduto;
            return Sistran.Library.GetDataTables.RetornarDataTable(m);
        }

        public DataTable GerarPlanilha(string MesAno, string idGrupoDeProduto, string versao)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = conexao;

            if (versao == "V2")
            {
                cd.CommandText = "EXEC GERAR_RPLANILHA_KPI_V2 " + idGrupoDeProduto + ", '" + MesAno + "'";

            }
            else
            {
                cd.CommandText = "EXEC GERAR_RPLANILHA_KPI " + idGrupoDeProduto + ", '" + MesAno + "'";
            }
            cd.Connection = cn;
            cn.Open();

            da.SelectCommand = cd;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            finally
            {
                cd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return ds.Tables[0];
        }



        public void ExecutarScripts()
        {
            try
            {


                DateTime data = DateTime.Parse(Sistran.Library.GetDataTables.ExecutarRetornoIDServico("SELECT ULTIMOPROCESSAMENTO FROM KPIIRWINPROCESSAMENTO", conexao));

                enviaEmailLog("Iniciou", "KPI - Grupo Logos. Inicio: " + DateTime.Now.ToShortDateString());


                //data = data.AddDays(-1);
                DateTime dataExecutarAte = DateTime.Now.AddDays(-1);
                string mesAno = "";

                while (data <= dataExecutarAte)
                {
                    string dia = data.Day.ToString();
                    mesAno = data.Month.ToString() + "/" + data.Year.ToString();

                    string[] mesanos = mesAno.Split('/');
                    if (mesanos[0].Length == 1)
                    {
                        mesAno = "0" + mesAno;
                    }

                    DirectoryInfo dir = new DirectoryInfo(caminho);
                    FileInfo[] files = dir.GetFiles();

                    for (int i = 0; i < files.Length; i++)
                    {
                        if (File.Exists(files[i].FullName))
                        {
                            string textoScript = File.ReadAllText(files[i].FullName);
                            textoScript = textoScript.Replace("##DIA", dia);
                            textoScript = textoScript.Replace("##MESANO", mesAno);
                            textoScript = textoScript.Replace("USE GRUPOLOGOS", "");
                            textoScript = textoScript.Replace("GO", "");


                            textoScript = textoScript.Replace("04/2012", mesAno);
                            textoScript = textoScript.Replace("05/2012", mesAno);
                            textoScript = textoScript.Replace("06/2012", mesAno);
                            textoScript = textoScript.Replace("07/2012", mesAno);
                            textoScript = textoScript.Replace("08/2012", mesAno);

                            textoScript = textoScript.Replace("SET @DIA =1", "SET @DIA =" + dia);
                            textoScript = textoScript.Replace("SET @DIA = 1", "SET @DIA =" + dia);
                            textoScript = textoScript.Replace("WHILE @DIA <= 31", "WHILE @DIA <= " + dia);

                            try
                            {
                                if(textoScript != "")
                                    Sistran.Library.GetDataTables.ExecutarComandoSql(textoScript, conexao);
                            }
                            catch (Exception ex)
                            {
                                enviaEmailLog("Erro: " + ex.Message + "<br>" + ex.Source, "KPI - Grupo Logos. Erro: " + DateTime.Now.ToShortDateString() + "=" + File.Exists(files[i].FullName));
                                //throw ex;
                            }
                        }
                    }
                    Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE KPIIRWINPROCESSAMENTO SET ULTIMOPROCESSAMENTO=CONVERT(DATETIME, '" + data + "', 103)", conexao);

                    data = data.AddDays(1);
                }

                //gera o email
                //OFFICE PRODUCT
                //DECOR
                //RUBERMAID

                DataTable dt403 = new SistranBLL.KPI_Irwin().GerarPlanilha(mesAno, "403", "V2"); // decor
                string htmlDECOR = montarCorpoEmail("DECOR", DateTime.Now.AddDays(-1).ToString(), dt403);

                DataTable dt405 = new SistranBLL.KPI_Irwin().GerarPlanilha(mesAno, "405", "V2"); // rubermeaid
                string htmlRUBERMAID = montarCorpoEmail("RUBERMAID", DateTime.Now.AddDays(-1).ToString(), dt405);

                DataTable dt387 = new SistranBLL.KPI_Irwin().GerarPlanilha(mesAno, "387", "V2");
                string htmlOP = montarCorpoEmail("OFFICE PRODUCT", DateTime.Now.AddDays(-1).ToString(), dt387);

                string CorpoInteiro = htmlOP + "<br>" + htmlDECOR + "<br>" + htmlRUBERMAID;

                List<listaEmailIrwin> email = new List<listaEmailIrwin>();
                listaEmailIrwin item = new listaEmailIrwin();



                item = new listaEmailIrwin();
                item.email = "moises@sstecno.com.br";
                email.Add(item);
                /*
                item = new listaEmailIrwin();
                item.email = "acorreia@grupologos.com.br";
                email.Add(item);

                item = new listaEmailIrwin();
                item.email = "edvaldo@sistecno.com.br";
                email.Add(item);
                */

         

                Sistran.Library.EnviarEmails.enviarEmailGrupoLogos("KPI - Grupo Logos. PROCESSADO ATÉ: " + DateTime.Now.AddDays(-1).ToShortDateString(), CorpoInteiro, email);
                enviaEmailLog("Finalizou", "KPI - Grupo Logos. Finalizou: " + DateTime.Now.ToShortDateString());

            }
            catch (Exception ex1)
            {
                enviaEmailLog("Erro: " + ex1.Message + "<br>" + ex1.Source, "KPI - Grupo Logos. Erro: " + DateTime.Now.ToShortDateString());
                //throw ex;
            }

        }

        private void enviaEmailLog(string msg, string titulo)
        {
            List<listaEmailIrwin> email = new List<listaEmailIrwin>();
            listaEmailIrwin item = new listaEmailIrwin();


            //item = new listaEmailIrwin();
            //item.email = "elton@sistecno.com.br";
            //email.Add(item);

            Sistran.Library.EnviarEmails.enviarEmailGrupoLogos(titulo, msg, email);
        }

        private string montarCorpoEmail(string linhaDesc, string DataDeProcessamento, DataTable dt)
        {
            string Tba = "<HTML>";
            Tba += "<HEAD>";
            Tba += " <STYLE type='text/css'>";
            Tba += " body ";
            Tba += " { ";
            Tba += " margin: 0px; ";
            Tba += " background-color: #f8f8f8; ";
            Tba += " font-family: Verdana; ";
            Tba += " text-align: center; ";
            Tba += " font-size: 7pt; ";

            Tba += " } ";
            Tba += " form ";
            Tba += " { ";
            Tba += " margin: 0px; ";
            Tba += " text-align: center; ";
            Tba += " } ";


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

            Tba += " </STYLE> ";

            Tba += "</HEAD>";


            Tba += "<BODY>";

            Tba += "<table cellspacing='1' celpanding='1' width=50% id='teste' runat='server' border=0>";
            Tba += "<tr>";

            Tba += "<td colspan=2 class='tdpCabecalho'> Linha: " + linhaDesc.ToUpper() + " Processado Até:" + Convert.ToDateTime(DataDeProcessamento).ToShortDateString();

            Tba += "</td>";

            Tba += "<td class='tdpCabecalho'>";
            Tba += "</td>";


            Tba += "</tr>";
            Tba += "</table>";




            Tba += "<table class='table' cellspacing='1' celpanding='1' width=50% id='teste' runat='server' border=1>";

            Tba += "<TR style='background-image:url(http://www.grupologos.com.br/SistranWeb.NET/Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>DESCRIÇÃO</TD>";
            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;UNIDADE</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;TARGET</TD>";



            int dia = DateTime.Parse(DataDeProcessamento).Day;



            for (int i = 1; i <= dia; i++)
            {
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;" + (i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString()) + "</TD>";
            }

            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;TOTAL</TD>";

            Tba += "</TR>";


            foreach (DataRow item in dt.Rows)
            {

                Tba += "<TR>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["DESCRICAO"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["DESCRICAOUNIDADEDEMEDIDA"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["DESCRICAOTARGUET"].ToString() + "</TD>";

                for (int i = 1; i <= dia; i++)
                {
                    Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["DIA" + i.ToString()].ToString() + "</TD>";

                }

                Tba += "<TD class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["total"].ToString() + "</TD>";
                Tba += "</TR>";

            }


            Tba += "</TABLE>";
            Tba += "<BODY>";
            Tba += "</HTML>";

            Tba += "<b>*  Valores 0 <br>- Valores Manuais</b>";
            Tba += "<HR>";
            return Tba;

        }
    }
}

public class listaEmailIrwin
{
    public string email { get; set; }
}