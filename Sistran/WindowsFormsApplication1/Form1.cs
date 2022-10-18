using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Sistran.Library;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            processar();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        public string[] ProcurarPeloBing(string Log, string Lat)
        {
            try
            {
                string baseUri = "http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&key=AphEY9h6PfA7eSWfyKlVHGyPRRe0vNHx0PCEENGsmqp3wWYm_eKCHjTddBsvSDCB";
                //string baseUri = "http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&key=HTyoRgOqBO4gN9SL8ceN~h2Uqv9KKr4_F1Lqe2cVBbw~AsHsYOFxZiU8Jv2aDKhDohShli1JAiPvcwFBHZzBC-IOB8cA6VDQkIoxFfh1Zug6";
                string requestUri = string.Format(baseUri, Lat, Log);


                string url = requestUri;
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        DataSet dsResult = new DataSet();
                        dsResult.ReadXml(reader);


                        if (dsResult.Tables["Response"].Rows[0]["statusDescription"].ToString().ToUpper() == "OK")
                        {
                            string[] ret = dsResult.Tables["Location"].Rows[0]["Name"].ToString().Split(',');
                            return ret;

                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public string[] ProcurarPeloGoogle(string Log, string Lat)
        {
            string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
            string requestUri = string.Format(baseUri, Lat, Log);

            string url = requestUri;
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    if (dsResult.Tables["GeocodeResponse"].Rows[0]["status"].ToString().ToUpper() == "OK")
                    {
                        string[] ret = dsResult.Tables["result"].Rows[0][1].ToString().Split(',');
                        return ret;

                    }
                }
            }
            return null;
        }

        public int RetornarCidade(string NomeCidade, string CEP, string cnx)
        {
            NomeCidade = removerAcentos(NomeCidade).Trim();

            string sql = "select IdCidade from Cidade where Nome = '" + NomeCidade + "' and IdEstado=26 ";
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdCidade"]);

            sql = "Select * from CidadeFaixaDeCep cfc  ";
            sql += " inner join Cidade c on c.IdCidade = cfc.IdCidade ";
            sql += " where " + CEP + " between CepInicial and CepFinal ";

            dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdCidade"]);



            return 0;
        }

        public int RetornarBairro(string Nome, string IdClidade, string cnx)
        {
            Nome = removerAcentos(Nome);

            string sql = "select IdBairro from Bairro where Nome = '" + Nome + "' and IdCidade=" + IdClidade;
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdBairro"]);
            else
            {
                if (Nome != "")
                {
                    string Id = Sistran.Library.GetDataTables.RetornarIdTabela("Bairro", cnx);
                    sql = "Insert into Bairro (IdBairro, Nome, IdCidade) values (" + Id + ", '" + Nome + "', " + IdClidade + ")";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                    return int.Parse(Id);
                }
                else return 0;
            }

        }

        public struct Pe
        {
            public string IdCidadeP { get; set; }
            public string IdBairroP { get; set; }
            public string CEPP { get; set; }
            public string EnderecoP { get; set; }
            public string nBairroP { get; set; }
        }

        public static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            processar();
        }

        private void processar()
        {
            try
            {
                timer1.Enabled = false;

                string sql = "Select numero, Latitude + ', ' + Longitude, IDDocumento, IDDestinatario, * " +
                            " from Documentopedido DP" +
                            " where ativo ='SIM'" +
                            " and datadeentrada >='2018-05-30' " +
                            " and DATADOPROCESSAMENTO is null" +
                            " and IDCliente = 150000" +
                            " and DataPlanejada is not null and IdEnderecoCidade=0 and IdDestinatario <> 150000 " +
                    //  " and IdDocumento= 267589" +
                            " ORDER BY dp.DataPlanejada ";

                string cnx = "Data Source=192.168.10.20;Initial Catalog=homerefill;User ID=sa;Password=@oncetsis05083#;";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ///string[] m;
                    string[]  m = ProcurarPeloBing(dt.Rows[i]["longitude"].ToString(), dt.Rows[i]["Latitude"].ToString());


                    if (m == null)
                        continue;

                    //string[] m = ProcurarPeloGoogle(dt.Rows[i]["longitude"].ToString(), dt.Rows[i]["Latitude"].ToString());


                    int idcid = 0;
                    int bar = 0;
                    string bairro = "";
                    string cep = "";

                    if (m.Length <= 2)
                    {
                        sql = "Update documentoPedido set Ativo='PEN'  where IdDocumento = " + dt.Rows[i]["IdDocumento"].ToString();
                        
                        try
                        {
                            Sistran.Library.GetDataTables.RetornarDataTableWS(sql + " ; select 1" , cnx);

                        }
                        catch (Exception ex)
                        {
                        }
                        continue;
                    }



                    if (m.Length == 3)
                    {
                        try
                        {
                            idcid = RetornarCidade(m[1].Split('-')[0].Trim().Replace("-", ""), m[2].Trim().Replace("-", ""), cnx);

                            if (m[0].Split('-').Length > 1)
                                bairro = m[0].Split('-')[1].Trim().Replace("'","");
                            else
                                bairro = "-";

                            cep = m[2].Trim().Replace("-", "");
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        idcid = RetornarCidade(m[2].Split('-')[0].Trim().Replace("-", ""), m[3].Trim().Replace("-", ""), cnx);
                        bairro = m[1].Replace("'", "");
                        cep = m[3].Trim().Replace("-", "");

                    }

                    
                    if (bairro.Contains("-"))
                        bairro = bairro.Split('-')[1].Trim();

                    try
                    {
                        int x = int.Parse(cep);
                    }
                    catch (Exception)
                    {
                        cep = "00000000";
                    }


                    bar = RetornarBairro(bairro, idcid.ToString(), cnx);

                    sql = "Update documentoPedido set IdEnderecoBairro= " + bar + ",  IdEnderecoCidade= " + idcid + ", EnderecoCep='" + cep + "' where IdDocumento = " + dt.Rows[i]["IdDocumento"].ToString();
                    sql += "; Update Cadastro set idCidade=" + idcid + "  , IdBairro=" + bar + " , cep='" + cep + "' where IdCadastro = " + dt.Rows[i]["IdDestinatario"].ToString();
                    sql += " select 1 ";

                    try
                    {
                        Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                    }
                    catch (Exception ex)
                    {
                    }

                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                ProcurarPeloGoogle("-47.1059199", "-23.5110688");
                //br.com.grupologos.www.HR x = new br.com.grupologos.www.HR();
                //for (int i = 0; i < 100; i++)
                //{
                //    DataTable dt = x.SaldoAtual("homerefill", "3215000");
                //    dt.WriteXml("c:\\temp\\saldoatual" + i + ".xml");
                //}               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}