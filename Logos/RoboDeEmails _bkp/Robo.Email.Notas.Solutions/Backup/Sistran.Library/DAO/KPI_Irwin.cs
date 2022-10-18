using System.Text;
using System.Data;
using System.Web;
using System.Data.Common;
using System.IO;
using System;


namespace SistranDAO
{
    public sealed class KPI_Irwin
    {
        string conexao = "Data Source=192.168.10.4;Initial Catalog=GrupoLogos;User ID=sa;Password=@logos09022005$;";
       // string caminho = "\\\\192.168.10.1\\Inetpub\\wwwroot\\www.grupologos.com.br\\SistranWeb.NET\\KPI\\sqlKPIIrwin\\";
        string caminho = "C:\\TesteScriptsKPI\\";

        
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
            m += " FROM KPIIRWIN K ";
            m += " INNER JOIN KPIIRWINMOV KM ON KM.IDKPIIRWIN = K.IDKPIIRWIN ";
            m += " INNER JOIN GrupoDeProduto GP on GP.IDGrupoDeProduto = KM.IdGrupoDeProduto ";
            m += " WHERE KM.MESANO LIKE '" + mesAno + "%' AND K.DESCRICAO LIKE '" + descricao + "%' ";
            m += " AND IDCliente in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            m += " AND GP.IDGrupoDeProduto =" + idGrupoDeProduto;
            return Sistran.Library.GetDataTables.RetornarDataTable(m);
        }

        public DataTable GerarPlanilha(string MesAno, string idGrupoDeProduto)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            DbDataAdapter da = factory.CreateDataAdapter();
            cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();

            cd.CommandText = "EXEC GERAR_RPLANILHA_KPI " + idGrupoDeProduto + ", '" + MesAno + "'";
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
            DateTime data = DateTime.Parse( Sistran.Library.GetDataTables.ExecutarRetornoIDServico("SELECT ULTIMOPROCESSAMENTO FROM KPIIRWINPROCESSAMENTO", conexao));

            //data = data.AddDays(-1);
            DateTime dataExecutarAte = DateTime.Now.AddDays(-1);

            while (data <= dataExecutarAte)
            {
                string dia = data.Day.ToString();
                string mesAno = data.Month.ToString() + "/" + data.Year.ToString();

                DirectoryInfo dir = new DirectoryInfo(caminho);
                FileInfo[] files = dir.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    if (File.Exists(files[i].FullName))
                    {
                        string textoScript = File.ReadAllText(files[i].FullName);
                        textoScript = textoScript.Replace("##DIA", dia);
                        textoScript = textoScript.Replace("##MESANO", mesAno);
                        textoScript= textoScript.Replace("USE GRUPOLOGOS","");
                        textoScript= textoScript.Replace("GO","");

                        string[] mesanos = mesAno.Split('/');

                        if (mesanos[0].Length == 1)
                        {
                            mesAno = "0" + mesAno;
                        }

                        textoScript = textoScript.Replace("01/2012", mesAno);
                        textoScript = textoScript.Replace("SET @DIA =1", "SET @DIA =" + dia);
                        textoScript = textoScript.Replace("SET @DIA = 1", "SET @DIA =" + dia);
                        textoScript = textoScript.Replace("WHILE @DIA <= 31", "WHILE @DIA <= "+ dia);

                        try
                        {
                            Sistran.Library.GetDataTables.ExecutarComandoSql(textoScript, conexao);
                        }
                        catch (Exception ex)
                        {
                           // MessageBox.Show("Oh noes!", "My Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw ex;
                        }
                    }
                }
                Sistran.Library.GetDataTables.ExecutarComandoSql("UPDATE KPIIRWINPROCESSAMENTO SET ULTIMOPROCESSAMENTO=CONVERT(DATETIME, '"+ data +"', 103)", conexao);

                data = data.AddDays(1);
            }

            
        }
    }
}