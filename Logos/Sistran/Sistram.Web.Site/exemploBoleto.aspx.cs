using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;//
//using BoletoNet;

public partial class exemploBoleto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //    BoletoNet.Cedente c = new Cedente("59.323.998/0001-08",  "Uniabc" , "432", "0806498");       
        //    c.Codigo = int.Parse("0806498");


        //    BoletoNet.Boleto b = new BoletoNet.Boleto(DateTime.Parse("10/10/2012"), double.Parse("656,40"), "03",  "20005446", c );
        //    //Dim b = New Boleto(CDate(txtVencimento.Text), CDbl(txtValorBoleto.Text), "102", txtNossoNumeroBoleto.Text, c)

        //    //'Dependendo da carteira, é necessário o número do documento
        //    //b.NumeroDocumento = txtNumeroDocumentoBoleto.Text
        //    b.NumeroDocumento = "20005446";


        //    //'Informa os dados do sacado
        //    b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
        //    b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
        //    b.Sacado.Endereco.Bairro = "Testando";
        //    b.Sacado.Endereco.Cidade = "Testando";
        //    b.Sacado.Endereco.CEP ="70000000";
        //    b.Sacado.Endereco.UF = "DF";

        //    BoletoNet.Instrucao_Bradesco i = new Instrucao_Bradesco(237);
        //    i.Descricao = "Não Receber após o vencimento";
        //    b.Instrucoes.Add(i);

        //    //'Espécie do Documento - [R] Recibo
        //    b.EspecieDocumento = new EspecieDocumento_Bradesco();

        //    BoletoNet.BoletoBancario bb = new  BoletoBancario();
        //    bb.CodigoBanco = 237; 
        //    bb.Boleto = b;
        //    bb.MostrarCodigoCarteira = true;
        //    bb.Boleto.Valida();

        //    //'true -> Mostra o compravante de entrega
        //    //'false -> Oculta o comprovante de entrega
        //    bb.MostrarComprovanteEntrega = true;


        //    //panelDados.Visible =false;

        //   //
        //    panelDados.Controls.Add(bb);
        //    //End If
        //}
    }
}