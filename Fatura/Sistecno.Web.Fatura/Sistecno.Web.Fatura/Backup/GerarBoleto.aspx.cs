using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;
using System.Data;
using Sistran.Library.Fatura;

namespace Sistecno.Web.Fatura
{
    public partial class GerarBoleto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GerarBoletos();
            FaturaHistorico.gravarLog("Gerou boleto", Session["idtitulo"].ToString(), Session["cnx"].ToString());
        }

        private void GerarBoletos()
        {
            try
            {
                string strsql = "EXEC RETORNAR_DADOS_BOLETO " + Session["idtitulo"].ToString();
                DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString());

                if (ds.Tables[0].Rows.Count == 0)
                    Response.Write("javascipt:window.close();");


                Cedente c = new Cedente(ds.Tables[0].Rows[0]["CNPJCPFCEDENTE"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim(), ds.Tables[0].Rows[0]["CEDENTE"].ToString(), ds.Tables[0].Rows[0]["AGENCIA"].ToString(), ds.Tables[0].Rows[0]["CONTA"].ToString(), ds.Tables[0].Rows[0]["CONTADIGITO"].ToString());
                c.Codigo = int.Parse(ds.Tables[0].Rows[0]["CARTEIRA"].ToString());


                decimal valorTotal = decimal.Parse(ds.Tables[0].Rows[0]["VALOR"].ToString());
                if (DateTime.Now > DateTime.Parse(ds.Tables[0].Rows[0]["DATADEVENCIMENTO"].ToString()))
                {
                    TimeSpan ts = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(ds.Tables[0].Rows[0]["DATADEVENCIMENTO"].ToString());
                    decimal juros = decimal.Parse(ds.Tables[0].Rows[0]["JUROS"].ToString());
                    decimal jurosDiario = decimal.Parse(ds.Tables[0].Rows[0]["JurosDiario"].ToString());
                    decimal multa = decimal.Parse(ds.Tables[0].Rows[0]["Multa"].ToString());

                    valorTotal += multa + juros;
                    valorTotal += (jurosDiario * ts.Days);
                }

                Boleto b = new Boleto(DateTime.Parse(DateTime.Parse(ds.Tables[0].Rows[0]["DATADEVENCIMENTO"].ToString()).ToShortDateString()), valorTotal, ds.Tables[0].Rows[0]["CARTEIRA"].ToString(), ds.Tables[0].Rows[0]["NOSSONUMERO"].ToString(), c);

            //    b.ValorDesconto = 100;

                b.NumeroDocumento = ds.Tables[0].Rows[0]["NUMERODOCUMENTO"].ToString();
                b.Sacado = new Sacado(ds.Tables[0].Rows[0]["CNPJSACADO"].ToString(), ds.Tables[0].Rows[0]["SACADO"].ToString());
                b.Sacado.Endereco.End = ds.Tables[0].Rows[0]["ENDERECO"].ToString() + ", " + ds.Tables[0].Rows[0]["NUMERO"].ToString();
                b.Sacado.Endereco.Bairro = "";
                b.Sacado.Endereco.Cidade = ds.Tables[0].Rows[0]["CIDADE"].ToString();
                b.Sacado.Endereco.CEP = ds.Tables[0].Rows[0]["CEP"].ToString();
                b.Sacado.Endereco.UF = ds.Tables[0].Rows[0]["UF"].ToString();



                Instrucao_Bradesco i = new Instrucao_Bradesco();
                i.Descricao = ds.Tables[0].Rows[0]["DESCRICAO"].ToString();
                b.Instrucoes.Add(i);

                b.EspecieDocumento = new EspecieDocumento(237);
                BoletoBancario bb = new BoletoBancario();
                bb.CodigoBanco = 237;
                bb.Boleto = b;
                bb.MostrarCodigoCarteira = true;
                bb.Boleto.Valida();
                bb.MostrarComprovanteEntrega = false;

                panelDados.Controls.Add(bb);
            }
            catch (Exception ex)
            {
                panelDados.Controls.Add(new LiteralControl("Boleto Indisponível"));
            }
        }
    }
}