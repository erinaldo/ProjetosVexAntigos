using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.IO;

public partial class frmDetalheProduto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt;
            if (Request.QueryString["tipo"] != null)
                dt = (DataTable)Session["GRID"];
            else
            {
                dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(Request.QueryString["click"]), false);
//                dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(Session["click"].ToString()));
                dt.Columns.Add("numero");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["numero"] = i.ToString();
            }

            Session["dt_click"] = dt;
            criarRegistro(Request.QueryString["Codigo"]);
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            CarregarListbox();
        }
    }
    private void CarregarListbox()
    {
        ListBox1.DataSource = new SistranBLL.Cliente.Divisao().RetornarListaDivisoesProduto(lblCodigo.Text);
        ListBox1.DataTextField = "Nome";
        ListBox1.DataValueField = "IDClienteDivisao";
        ListBox1.DataBind();
    }

    private void criarRegistro(string Codigo)
    {
        DataTable dt = (DataTable)Session["dt_click"];

        DataRow[] ow = dt.Select("CODIGO='" + Codigo + "'", "");

        if (ow.Length > 0)
        {
            lblReg.Text = ow[0]["numero"].ToString();
            lblCodigo.Text = ow[0]["CODIGO"].ToString();
            lblCodCliente.Text = ow[0]["CODIGODOCLIENTE"].ToString();
            lblDescricao.Text = ow[0]["DESCRICAO"].ToString();
            lblSaldo.Text = Convert.ToDecimal(ow[0]["SALDO"]).ToString("#0");
            lblDataCadastro.Text = ow[0]["DATADECADASTRO"].ToString();
            lblDataLimite.Text = ow[0]["DATALIMITEDEUSO"].ToString();
            lblAtivo.Text = ow[0]["ATIVO"].ToString();
            lblConsumoMensal.Text = ow[0]["CONSUMOMENSAL"].ToString();
            lblSaldoMinimo.Text = ow[0]["SALDOMINIMO"].ToString();

            GerarImagem(ow[0]["IDPRODUTO"].ToString(), ow[0]["IdProdutoCliente"].ToString());
        }       
    }

    private void GerarImagem(string IdProduto, string IdProdutoCliente)
    {
        string x = IdProduto.ToString();
        if (File.Exists("~/imgReport/" + x + ".jpg"))
        {
            Image1.ImageUrl = "imgReport/" + x + ".jpg";
        }
        else
        {

            DataTable dImagem = new SistranBLL.Produto().RetornarImagemProduto(Convert.ToInt32(IdProduto));
            if (dImagem.Rows.Count > 0)
            {
                byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
                //string x = IdProduto.ToString();
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + x + ".jpg"));
                Image1.ImageUrl = "imgReport/" + x + ".jpg";
            }
            else
            {
                Image1.ImageUrl = "~/Images/naoDisponivel.jpg";
            }
        }
        RadGrid1.DataSource = new SistranBLL.Produto().ListarProdutosByIdProdutoCliente(IdProdutoCliente);
        RadGrid1.DataBind();

    }

    protected void btnAnterior_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = (DataTable)Session["dt_click"];
        DataRow[] ow = dt.Select("numero='" + (Convert.ToInt32(lblReg.Text) - 1).ToString() + "'", "");

        if (ow.Length > 0)
        {
            lblReg.Text = ow[0]["numero"].ToString();
            lblCodigo.Text = ow[0]["CODIGO"].ToString();
            lblCodCliente.Text = ow[0]["CODIGODOCLIENTE"].ToString();
            lblDescricao.Text = ow[0]["DESCRICAO"].ToString();
            lblSaldo.Text = Convert.ToDecimal(ow[0]["SALDO"]).ToString("#0");
            lblDataCadastro.Text = ow[0]["DATADECADASTRO"].ToString();
            lblDataLimite.Text = ow[0]["DATALIMITEDEUSO"].ToString();
            lblAtivo.Text = ow[0]["ATIVO"].ToString();
            lblConsumoMensal.Text = ow[0]["CONSUMOMENSAL"].ToString();
            lblSaldoMinimo.Text = ow[0]["SALDOMINIMO"].ToString();
            GerarImagem(ow[0]["IDPRODUTO"].ToString(), ow[0]["IdProdutoCliente"].ToString());

            ListBox1.DataSource = new SistranBLL.Cliente.Divisao().RetornarListaDivisoesProduto(ow[0]["CODIGO"].ToString());
            ListBox1.DataTextField = "Nome";
            ListBox1.DataValueField = "IDClienteDivisao";
            ListBox1.DataBind();
        }

    }

    protected void btnPosterior_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = (DataTable)Session["dt_click"];
        DataRow[] ow = dt.Select("numero='" + (Convert.ToInt32(lblReg.Text) + 1).ToString() + "'", "");

        if (ow.Length > 0)
        {
            lblReg.Text = ow[0]["numero"].ToString();
            lblCodigo.Text = ow[0]["CODIGO"].ToString();
            lblCodCliente.Text = ow[0]["CODIGODOCLIENTE"].ToString();
            lblDescricao.Text = ow[0]["DESCRICAO"].ToString();
            lblSaldo.Text = Convert.ToDecimal(ow[0]["SALDO"]).ToString("#0");
            lblDataCadastro.Text = ow[0]["DATADECADASTRO"].ToString();
            lblDataLimite.Text = ow[0]["DATALIMITEDEUSO"].ToString();
            lblAtivo.Text = ow[0]["ATIVO"].ToString();
            lblConsumoMensal.Text = ow[0]["CONSUMOMENSAL"].ToString();
            lblSaldoMinimo.Text = ow[0]["SALDOMINIMO"].ToString();
            GerarImagem(ow[0]["IDPRODUTO"].ToString(), ow[0]["IdProdutoCliente"].ToString());

            ListBox1.DataSource = new SistranBLL.Cliente.Divisao().RetornarListaDivisoesProduto(ow[0]["CODIGO"].ToString());
            ListBox1.DataTextField = "Nome";
            ListBox1.DataValueField = "IDClienteDivisao";
            ListBox1.DataBind();

        }
    }
}