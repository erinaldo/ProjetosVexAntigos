using System;
using System.Data;
using SistranBLL;

public partial class frmImportTeste : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtRegistrosTemp = Sistran.Library.ImportTemp.LeTabelaTemp(TextBox1.Text);

            string NomeDivisao = "";
            string idsDivisao = "";
            SistranBLL.Cliente.Divisao oc = new Cliente.Divisao();

            foreach (DataRow item in dtRegistrosTemp.Rows)
            {

                if (NomeDivisao != item["DIVISAO"].ToString())
                {
                    idsDivisao = "";

                    string codProd = item["CODIGO"].ToString();

                    if (codProd == "")
                    {
                        codProd = item["CODIGODOCLIENTE"].ToString();
                    }
                    oc.DesabilitarEstoqueDivisao(codProd);

                    DataTable dtDivisao = Sistran.Library.ImportTemp.LeDivisao(item["DIVISAO"].ToString());

                    foreach (DataRow itemDivi in dtDivisao.Rows)
                    {
                        idsDivisao += itemDivi["IDCLIENTEDIVISAO"].ToString() + ",";

                    }
                }

                string[] iddiv = idsDivisao.Split(',');

                if (iddiv.Length > 0)
                {

                    for (int i = 0; i < iddiv.Length; i++)
                    {
                        try
                        {
                            if (iddiv[i] != "")
                            {
                                string codProd = item["CODIGO"].ToString();

                                if (codProd == "")
                                {
                                    codProd = item["CODIGODOCLIENTE"].ToString();
                                }

                                if (codProd != "")
                                {
                                    oc.InserirEstoqueDivisao(codProd, iddiv[i]);
                                }
                            }
                        }
                        catch (Exception exx)
                        {
                            Response.Write(exx.Message + "<br>");
                        }
                    }



                    NomeDivisao = item["DIVISAO"].ToString();

                }
            }
            Response.Write("fiM");
        }
        catch (Exception EX)
        {
            Response.Write(EX.Message + "<br>");
        }
    }
}