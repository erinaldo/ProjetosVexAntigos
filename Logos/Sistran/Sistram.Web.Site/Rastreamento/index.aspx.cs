using System;
using System.Web.UI;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Globalization;

public partial class Rastreamento_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");
        //btnRastrear.Attributes.Add("OnClick", "javascript:fullScreen('frmResultado.aspx?id="+Guid.NewGuid().ToString() +"');");
        if (!IsPostBack)
        {            
            Session["Valores"] = null;
            Session.Clear();
            Session.Abandon();
        }
    }

    protected void btnRastrear_Click(object sender, EventArgs e)
    {
        txtNotas.Text = txtNotas.Text.Replace(" ", "").Replace(",", "").Replace(".", "");
        Session["Valores"] = FormatarCnpj(txtCNPJ.Text) + "|" + "'" + txtNotas.Text.Replace("\r\n", "',").Replace("\n", "',").Replace(",", ",'") + "'";

        if (Contar())
            ScriptManager.RegisterClientScriptBlock(Up, this.GetType(), "User", "window.open('frmResultado.aspx')", true);
        else
            ScriptManager.RegisterClientScriptBlock(Up, this.GetType(), "AlertProcurar", "alert('Nenhum item encontrado.')", true);
    }

    private bool Contar()
    {
        string[] Valores = Session["Valores"].ToString().Split('|');
        string[] conexoes = FuncoesGerais.CarregarConexoesRastreamento();
       

        int qt = 0;
        for (int i = 0; i < conexoes.Length; i++)
        {
            if (conexoes[i] != "")
                qt += SistranBLL.Rastreamento.Contar(Valores[1], Valores[0], conexoes[i]);            
        }
        if (qt > 0)
            return true;
        else
            return false;       
    }

    protected void txtCNPJ_TextChanged(object sender, EventArgs e)
    {
        if (txtCNPJ.Text.Length >= 11)
        {
            txtCNPJ.Text = FormatarCnpj(txtCNPJ.Text);
            txtNotas.Focus();           
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Up, this.GetType(), "AlertLogin", "alert('CPF/CNPJ invalido.')", true);
            txtCNPJ.Text = "";
            txtCNPJ.Focus();
        }
    }

    private string FormatarCnpj(string s)
    {
        s = s.Replace(".", "");
        s = s.Replace("-", "");
        s = s.Replace("/", "");
        s = s.Replace(@"\", "");

        if (s.Length == 0)
        {
            return "";
        }

        if (s.Length <= 11)
        {
            MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
            mtpCpf.Set(ZerosEsquerda(s, 11));
            return mtpCpf.ToString();
        }
        else
        {
            MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
            mtpCnpj.Set(ZerosEsquerda(s, 11));
            return mtpCnpj.ToString();
        }
    }

    public static string ZerosEsquerda(string strString, int intTamanho)
    {

        string strResult = "";

        for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
        {
            strResult += "0";
        }

        return strResult + strString;

    }
}
