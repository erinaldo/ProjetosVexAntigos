using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueSoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<ItensAAtualizar> listaRet;
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await PegarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task PegarProdutos()
        {
            return;

            string sql = "select distinct  p.codigoDeBarras, d.IdRemetente, pe.IdProdutoCliente, r.IdRomaneio " +
                            " from RomaneioDocumento rd " +
                            " Inner join Documento d on d.IdDocumento = rd.IdDocumento " +
                            " Inner join DocumentoItem di on di.IdDocumento = rd.IdDocumento " +
                            " Inner join ProdutoEmbalagem pe on pe.IdProdutoCliente = di.IdProdutoCliente " +
                            " inner join Produto p on p.IdProduto = pe.Idproduto " +
                            " inner join Romaneio r on r.IdRomaneio = rd.IdRomaneio " +
                            " where  r.IdFilial = 48  and r.Tipo='ENTRADA' and r.StatusBlueSoft = 'A PROCESSAR'" +
                            " and p.CodigoDeBarras <> ''";

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            //List<ItensAAtualizar> listaRet = new List<ItensAAtualizar>();
            int idRemetente = 0;
            int idPc = 0;
            int idRomaneio = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri("https://erp.bluesoft.com.br/barbosa/");

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("X-CustomToken", txtToken.Text);

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/comercial/produtos?gtin=" + dt.Rows[i][0].ToString());
                    idRemetente = Int32.Parse(dt.Rows[i]["IdRemetente"].ToString());
                    idPc = Int32.Parse(dt.Rows[i]["IdProdutoCliente"].ToString());
                    idRomaneio = Int32.Parse(dt.Rows[i]["IdRomaneio"].ToString());

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        var lis = JsonConvert.DeserializeObject<cabec>(EmpResponse);
                        listaRet = new List<ItensAAtualizar>();
                        if (lis.data.Count > 0)
                        {
                            for (int ix = 0; ix < lis.data.Count; ix++)
                            {
                                await PesquisarPorCodigoProdutoBlueSoft(lis.data[i].produtoKey, idRemetente.ToString(), idPc.ToString(), idRomaneio.ToString());

                                if (listaRet.Count > 0)
                                    AtualizarBaseLogos(listaRet);

                            }
                        }

                        textBox4.Text = EmpResponse.ToString();
                    }
                }
            }


        }

        private async Task PesquisarPorCodigoProdutoBlueSoft(int produtoKey, string idRemetente, string idPc, string idRomaneio)
        {

            ItensAAtualizar ret = new ItensAAtualizar();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri("https://erp.bluesoft.com.br/barbosa/");

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-CustomToken", txtToken.Text);

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/comercial/produtoKey?gtin=" + produtoKey);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    var lis = JsonConvert.DeserializeObject<cabec>(EmpResponse);

                    if (lis.data.Count > 0)
                    {
                        for (int ix = 0; ix < lis.data.Count; ix++)
                        {
                            for (int igetin = 0; igetin < lis.data[ix].gtins.Count; igetin++)
                            {
                                listaRet.Add(new ItensAAtualizar()
                                {
                                    CodigoBarras = lis.data[ix].gtins[igetin],
                                    Especie = lis.data[ix].embalagemKey,
                                    Fator = Convert.ToInt32(lis.data[ix].fatorEstoque.ToString().Replace(",", ".")),
                                    IdRemetente = int.Parse(idRemetente),
                                    Descricao = lis.data[ix].descricao,
                                    IdProdutoCliente = int.Parse(idPc),
                                    IdRomaneio = int.Parse(idRomaneio)
                                });
                            }
                        }
                    }
                    textBox4.Text = EmpResponse.ToString();
                }
            }
        }

        private void AtualizarBaseLogos(List<ItensAAtualizar> listaRet)
        {
            for (int i = 0; i < listaRet.Count; i++)
            {
                try
                {
                    string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                    //string cnx = "Data Source=192.168.0.5;Initial Catalog=StnNovo;User ID=sa;Password=@oncetsis12122014;";

                    string sql = "select distinct  pe.IDProdutoEmbalagem,  p.IDProduto, pc.IdProdutoCliente" +
                                " from Produto p " +
                                " Inner join ProdutoEmbalagem pe on pe.IdProduto = p.IdProduto " +
                                " Inner join ProdutoCliente pc on pc.IdProdutoCliente = pc.IdProdutoCliente " +
                                " where p.CodigoDeBarras = '" + listaRet[i].CodigoBarras + "'" +
                                " and pc.IdCliente =" + listaRet[i].IdRemetente;

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                    string sqlaux = "";
                    //verifica se ja tem a embalagem
                    if (dt.Rows.Count > 0)
                    {
                        sqlaux = "Update ProdutoEmbalagem set UnidadeDoCliente=" + listaRet[i].Fator + ", UnidadeDeMedida='" + listaRet[i].Especie + "' where IdProdutoEmbalagem=" + dt.Rows[0]["IdProdutoEmbalagem"].ToString();
                        sqlaux += "; Update Produto set Especie='" + listaRet[i].Especie + "' where IdProduto=" + dt.Rows[0][1].ToString();
                    }
                    else // se nao tem a embalagem / Produto
                    {
                        string idProdutoEmbalagem = Sistran.Library.GetDataTables.RetornarIdTabela("ProdutoEmbalagem", cnx);
                        string idProduto = Sistran.Library.GetDataTables.RetornarIdTabela("Produto", cnx);

                        sqlaux = "Insert Into Produto (IDProduto, CodigoDeBarras, DataDeCadastro, Especie) Values (" + idProduto + ", '" + listaRet[i].CodigoBarras + "',GetDate(), '" + listaRet[i].Especie + "') ;";
                        sqlaux += "Insert Into ProdutoEmbalagem(IDProdutoEmbalagem, IDProdutoCliente, IDProduto, Conteudo, UnidadeDoCliente,  UnidadeDeMedida, DataDeCadastro, Ativo)" +
                                   " Values (" + idProdutoEmbalagem + ", " + listaRet[i].IdProdutoCliente + ", " + idProduto + ", '" + (listaRet[i].Descricao.Length > 60 ? listaRet[i].Descricao.Substring(0, 59) : listaRet[i].Descricao) + "', " + listaRet[i].Fator + ", '" + listaRet[i].Fator + "', GetDate(), 'SIM')";

                    }

                    sqlaux += "; Update Romaneio set StatusBlueSoft='PROCESSADO' WHERE IdRomaneio=" + listaRet[i].IdRomaneio;
                    //Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sqlaux, cnx);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //string js = "";
            string aspa = "\"";
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            //string sql = "Select IdRomaneio from Dt  Inner join DtRomaneio dtr on dtr.IdDt = Dt.IdDt where LiberadoParaBlueSoft = 'SIM' and EnviadoBlueSoft  is null";
            string sql = "Select IdRomaneio, * from Dt  Inner join DtRomaneio dtr on dtr.IdDt = Dt.IdDt where dt.numero ='15094' and IDFilial = 48";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable ret = Sistran.Library.GetDataTables.RetornarDataTableWin("exec prc_EnvioCargas_Barbosa " + dt.Rows[i][0].ToString(), cnx);

                if (ret.Rows.Count == 0)
                    continue;


                DataView view = new DataView(ret);
                DataTable distinctValues = view.ToTable(true, "DocumentoDoCliente4");

                //envia por nota
                for (int iPorNota = 0; iPorNota < distinctValues.Rows.Count; iPorNota++)
                {
                    DataRow[] r = ret.Select("DocumentoDoCliente4='" + distinctValues.Rows[iPorNota][0] + "'");

                    /*js = "{";
                    js += aspa + "cnpj" + aspa + ": " + aspa + r[0]["CNPJCPF"].ToString().Replace("-", "").Replace("/", "").Replace(".", "").Trim() + aspa + ",";
                    js += aspa + "chaveNotaFiscal" + aspa + ": " + aspa + r[0]["DocumentoDoCliente4"].ToString() + aspa + ",";
                    js += aspa + "produtos" + aspa + ":[";

                    for (int ix = 0; ix < r.Length; ix++)
                    {
                        js += "{";
                        js += aspa + "gtin" + aspa + ": " + aspa + r[ix]["gtin"].ToString() + aspa + ",";
                        js += aspa + "quantidade" + aspa + ": " + aspa + r[ix]["Quantidade"].ToString() + aspa + ",";
                        js += aspa + "fatorEstoque" + aspa + ": " + aspa + "" + aspa + ",";
                        //js += aspa + "fatorEstoque" + aspa + ": " + aspa + "1" + aspa + ",";
                        js += aspa + "validadeProduto" + aspa + ": " + aspa + (r[ix]["Validade"].ToString().Replace(" 00:00:00", "") == "" ? "10/10/2019" : r[ix]["Validade"].ToString().Replace(" 00:00:00", "")) + aspa + ", ";
                        js += aspa + "lote" + aspa + ": " + aspa + r[ix]["Lote"].ToString() + aspa + "";
                        js += "}";

                    }
                    js += "]";
                    js += "}";*/

                    envio envio = new envio();
                    envio.chaveNotaFiscal = r[0]["DocumentoDoCliente4"].ToString().Trim();
                    envio.cnpj = r[0]["CNPJCPF"].ToString().Replace("-", "").Replace("/", "").Replace(".", "").Trim();


                    List<produtos> p = new List<produtos>();
                    for (int ix = 0; ix < r.Length; ix++)
                    {
                        p.Add(new produtos()
                        {
                            fatorEstoque = "",
                            gtin = r[ix]["gtin"].ToString(),
                            lote = r[ix]["Lote"].ToString(),
                            quantidade = r[ix]["Quantidade"].ToString(),
                            validadeProduto = (r[ix]["Validade"].ToString().Replace(" 00:00:00", "") == "" ? "10/10/2019" : r[ix]["Validade"].ToString().Replace(" 00:00:00", ""))

                        });
                    }

                    envio.produtos = p;

                    if (envio.produtos.Count > 0)
                    {
                        var js = JsonConvert.SerializeObject(envio);
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Clear();
                            //Define request data format  
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Add("X-CustomToken", "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ7XCJ1c2VyTmFtZVwiOlwiZWR2YWxkb192ZXhcIixcInNlY3JldFwiOlwiUVBJS1BEbnZ5TnZubnhicmpOdVFBRm5vZmNVek1tUUpUQXZIRlBFVWxrXCJ9In0.iqFPRsjm69YxdCBYWElsh1n7JeHoh1G92SW0zCh0c7G8Gf_yP0HsR19ezaxMHgR_M2QeQ-q_SlX7nMD9LTFJfw");
                            var Res = await client.PostAsync("https://erp.bluesoft.com.br/barbosa/api/comercial/recebimentocarga/", new StringContent(js, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();

                            var x =  JsonConvert.DeserializeObject<erros>(Res);

                            


                        }
                    }
                }


            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox4.Text = "Iniciou";
            PegarProdutos();
            textBox4.Text = "Finalizou as: " + DateTime.Now;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 60000;
            timer1.Enabled = true;

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            envio envio = new envio();
            envio.chaveNotaFiscal = "35171060437647003041550050000912031000912038";
            envio.cnpj = "60437647002070";

            List<produtos> p = new List<produtos>();
            p.Add(new produtos()
            {
                fatorEstoque = "",
                gtin = "37898422742783",
                lote = "",
                quantidade = "50",
                validadeProduto = "19/10/2019"

            });

            envio.produtos = p;

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-CustomToken", "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ7XCJ1c2VyTmFtZVwiOlwiZWR2YWxkb192ZXhcIixcInNlY3JldFwiOlwiUVBJS1BEbnZ5TnZubnhicmpOdVFBRm5vZmNVek1tUUpUQXZIRlBFVWxrXCJ9In0.iqFPRsjm69YxdCBYWElsh1n7JeHoh1G92SW0zCh0c7G8Gf_yP0HsR19ezaxMHgR_M2QeQ-q_SlX7nMD9LTFJfw");


                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                //HttpResponseMessage Res = await client.PostAsync("api/comercial/recebimentocarga", new StringContent(js ));

                var js = JsonConvert.SerializeObject(envio);
                var Res = await client.PostAsync("https://erp.bluesoft.com.br/barbosa/api/comercial/recebimentocarga/", new StringContent(js, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();

                //var xx =  PerformActionSafe(() => (client.PostAsync("https://erp.bluesoft.com.br/barbosa/api/comercial/recebimentocarga/", new StringContent(js, Encoding.UTF8, "application/json")).Result));

                //  var b = Res.Content;
            }


        }

        public static HttpResponseMessage PerformActionSafe(Func<HttpResponseMessage> action)
        {
            try
            {
                return action();
            }
            catch (AggregateException aex)
            {
                Exception firstException = null;
                if (aex.InnerExceptions != null && aex.InnerExceptions.Any())
                {
                    firstException = aex.InnerExceptions.First();

                    if (firstException.InnerException != null)
                        firstException = firstException.InnerException;
                }

                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content =
                        new StringContent(firstException != null
                                            ? firstException.ToString()
                                            : "Encountered an AggreggateException without any inner exceptions")
                };

                return response;
            }
        }


    }

    public class erros
    {
        public List<string> mensagem { get; set; }
    }
    
    public class cabec
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public List<data> data { get; set; }

}

    public class data
    {
        public int produtoKey { get; set; }
        public int produtoUnitarioKey { get; set; }
        public string descricao { get; set; }
        public string embalagemKey { get; set; }
        public float fatorEstoque { get; set; }
        public bool obrigatoriedadeDataValidade { get; set; }
        public bool obrigatoriedadeLote { get; set; }
        public string  ultimaAlteracao { get; set; }
        public List<string> gtins { get; set; }     
    }

    
    public class ItensAAtualizar
    {
        public string  CodigoBarras { get; set; }
        public int Fator { get; set; }
        public string Especie { get; set; }
        public string Descricao { get; set; }
        public int IdRemetente { get; set; }
        public int IdProdutoCliente { get; set; }
        public int IdRomaneio { get; set; }
    }

    public class envio
    {
        public string cnpj { get; set; }
        public string chaveNotaFiscal { get; set; }
        public List<produtos> produtos { get; set; }
    }

    public class produtos
    {
        public string gtin { get; set; }
        public string quantidade { get; set; }
        public string fatorEstoque { get; set; }
        public string validadeProduto { get; set; }
        public string lote { get; set; }        	
    }
}
