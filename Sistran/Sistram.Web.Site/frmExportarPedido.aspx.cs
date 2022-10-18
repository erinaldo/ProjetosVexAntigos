using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;

public partial class frmExportarPedido : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);                      
        }

    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                ExportarArquivoPedido(ConfigurationSettings.AppSettings["CaminhoExportacao"]);
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Dados Exportados com Sucesso.')", true);

            }
            else
            {
                if (uplArquivo.HasFile == false)
                    throw new Exception("Selecione um arquivo.");

                ImportarNF();

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);                 
            
        }
    }

    private void ImportarNF()
    {
        if (Path.GetExtension(uplArquivo.FileName) == ".txt")
        {
            string endereco = System.Web.HttpContext.Current.Server.MapPath("~/imgReport");
            string n = uplArquivo.FileName;
            string datahora = DateTime.Now.ToString("yyyyMMddmmss");
            n = "txt" + datahora + DateTime.Now.Minute + n.Substring(n.LastIndexOf('.'));
            uplArquivo.SaveAs(endereco + "\\" + n);
            System.IO.StreamReader objStreamReader;
            objStreamReader = File.OpenText(endereco + "\\" + n);
            Importacao bll = new Importacao();
            bll.ProcessarArquivoNF(objStreamReader, endereco + "\\" + n);

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "IMPORTOU ATRAVES DE UM ARQUIVO DE NF", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            throw new Exception("Processo concluído com sucesso!");

        }
    }

    private void ExportarArquivoPedido(string Caminho)
    {
        DataTable dtEx = new SistranBLL.Pedido().ExportarPedido("ENVIAR PARA FATURAMENTO");      
        if (dtEx.Rows.Count == 0)
        {
            throw new Exception("Não existem registros.");
        }

        StreamWriter valor = new StreamWriter(Caminho + "\\PEDIDO." + DateTime.Now.ToString("ddMMyyyy.hhmm") + ".txt", true, Encoding.ASCII);
        string linha = "";
        foreach (DataRow item in dtEx.Rows)
        {


            string CnpjRemetente = SistranBLL.Cliente.ReadCNPJByIdCliente(Convert.ToInt32(item["IDREMETENTE"].ToString()));
            string CnpjDestinatario = SistranBLL.Cliente.ReadCNPJByIdCliente(Convert.ToInt32(item["IDDESTINATARIO"].ToString()));

            string NumerodoPedido = item["NUMERO"].ToString();
            string NumerodaNotaFiscal = item["NUMERO"].ToString();
            //string SeriedaNotaFiscal = "PED";
            string Volumes = item["Volumes"].ToString();
            string PesoBruto = item["PesoBruto"].ToString();
            string Valordopedido = item["ValorDaNota"].ToString();
            string ObservacaodoPedido = "Pedido Realizado Pelo Site";
            string CodidodoProduto = item["Codigo"].ToString();
            string QuantidadePedido = item["Quantidade"].ToString();
            string QuantidadeFaturada = "0";


            linha += CnpjRemetente.PadRight(18) + ";";
            linha += CnpjDestinatario.PadRight(18) + ";";


            if (NumerodoPedido.Length < 6)
            {
                int x = 6 - NumerodoPedido.Length;

                for (int i = 0; i < x; i++)
                {
                    NumerodoPedido = "0" + NumerodoPedido; 
                }
            }

            linha += NumerodoPedido + ";";           
            
            linha += ";";
            linha += ";";
            linha += QuantidadePedido + ";";
            linha += Convert.ToDecimal(PesoBruto).ToString("#0.000") + ";";
            linha += Convert.ToDecimal(Valordopedido).ToString("#0.00") + ";";
            linha += ObservacaodoPedido + ";";
            linha += CodidodoProduto + ";";
            linha += QuantidadeFaturada + ";";
            valor.WriteLine(linha);
            linha = "";

        }
        valor.Close();
        
        //Altera a Situcao
        Pedido OpED = new Pedido();
        OpED.AlterarSituacao("ENVIAR PARA FATURAMENTO", "ENVIADO PARA FATURAMENTO");


        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "EXPORTOU OS PEDIDOS PARA UM ARQUIVO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            uplArquivo.Enabled = false;
        }
        else
        {
            uplArquivo.Enabled = true;
        }
    }
}