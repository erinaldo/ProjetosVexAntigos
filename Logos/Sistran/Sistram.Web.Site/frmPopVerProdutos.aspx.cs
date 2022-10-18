using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class frmPopVerProdutos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Produtos do Endereço: " + Request.QueryString["id"].ToString() ;
        DataTable DTs =  new SistranBLL.Deposito().RetornarProdutosEndereco(Request.QueryString["id"].ToString());
        Repeater1.DataSource = DTs;
        Repeater1.DataBind();        
    }
    
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Image Image1 = (Image)e.Item.FindControl("Image1");
        Label lblCodigo = (Label)e.Item.FindControl("lblCodigo");

        Label lblCodigoCliente = (Label)e.Item.FindControl("lblCodigoCliente");
        Label lblDescricao = (Label)e.Item.FindControl("lblDescricao");
        Label lblCodigoBarras = (Label)e.Item.FindControl("lblCodigoBarras");
        Label lblAltura = (Label)e.Item.FindControl("lblAltura");
        Label lblLargura = (Label)e.Item.FindControl("lblLargura");
        Label lblsaldo = (Label)e.Item.FindControl("lblsaldo");        

        
        DataRowView dv = (DataRowView)e.Item.DataItem;


        if (Image1 != null)
        {
            lblCodigoCliente.Text = dv["CODIGO"].ToString();
            lblCodigo.Text = dv["CODIGO"].ToString();
            lblDescricao.Text = dv["DESCRICAO"].ToString();         
            lblsaldo.Text = Convert.ToDecimal(dv["SALDO"]).ToString("#0.000");        
        }
    }
}
