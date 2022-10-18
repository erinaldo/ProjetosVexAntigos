using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.IO;

public partial class frmProdutoComFoto :  System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        CarregarMenuDivisao();
    }


    #region Divisao
    private void CarregarMenuDivisao()
    {

        pnlMenu.Visible = true;
        PlaceHolderMenuDivisao.Controls.Clear();
        //List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        SistranBLL.Cliente.Divisao pdBLL = new SistranBLL.Cliente.Divisao();
        DataTable dtPai = pdBLL.RetornarPais(Convert.ToInt32(Session["IDEmpresa"]));

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<table  cellspacing=0 celpanding=0 widht=200px bgcolor='#EBEBEB' >"));

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr >"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td align='left' nowrap=nowrap style='font-size:10px; border-bottom: 1px solid #dddddd'><b>Selecione a Divisão</b>"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

        int c = 0;
        foreach (DataRow item in dtPai.Rows)
        {
            if (c == 0)
                Session["PrimeiraDivisao"] = item["IDClienteDivisao"].ToString() + "|" + item["NOME"].ToString();

            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>"));
            PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(item["NOME"].ToString(), false, item["IDClienteDivisao"].ToString()));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

            DataTable dtFilho = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(item["IDClienteDivisao"]));

            foreach (DataRow itemFilho in dtFilho.Rows)
            {

                if (ProcurarBotaoMenuDivisao(itemFilho["IDClienteDivisao"].ToString()) == false)
                {
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;"));
                    PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemFilho["NOME"].ToString(), false, itemFilho["IDClienteDivisao"].ToString()));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));
                }

                DataTable dtFilhoFilho = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemFilho["IDClienteDivisao"]));

                foreach (DataRow itemff in dtFilhoFilho.Rows)
                {
                    if (ProcurarBotaoMenuDivisao(itemff["IDClienteDivisao"].ToString()) == false)
                    {

                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;"));
                        PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemff["NOME"].ToString(), false, itemff["IDClienteDivisao"].ToString()));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                        DataTable dtFilhoNivel3 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemff["IDClienteDivisao"]));
                        foreach (DataRow itemNivel3 in dtFilhoNivel3.Rows)
                        {
                            //nivel 3
                            if (ProcurarBotaoMenuDivisao(itemNivel3["IDClienteDivisao"].ToString()) == false)
                            {

                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel3["NOME"].ToString(), false, itemNivel3["IDClienteDivisao"].ToString()));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                                DataTable dtFilhoNivel4 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemNivel3["IDClienteDivisao"]));
                                foreach (DataRow itemNivel4 in dtFilhoNivel4.Rows)
                                {
                                    //nivel 4
                                    if (ProcurarBotaoMenuDivisao(itemNivel4["IDClienteDivisao"].ToString()) == false)
                                    {

                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                        PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel4["NOME"].ToString(), false, itemNivel4["IDClienteDivisao"].ToString()));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                                        DataTable dtFilhoNivel5 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemNivel4["IDClienteDivisao"]));
                                        foreach (DataRow itemNivel5 in dtFilhoNivel5.Rows)
                                        {
                                            //nivel 4
                                            if (ProcurarBotaoMenuDivisao(itemNivel5["IDClienteDivisao"].ToString()) == false)
                                            {

                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                                PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel5["NOME"].ToString(), false, itemNivel5["IDClienteDivisao"].ToString()));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));
                                            }

                                        }

                                    }

                                }

                            }
                        }
                    }
                }
            }
        }
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</table>"));

    }

    private bool ProcurarBotaoMenuDivisao(string id)
    {
        foreach (Control cc in PlaceHolderMenuDivisao.Controls)
        {
            Button tb = cc as Button;

            if (tb != null)
            {
                if (tb.ID == id)
                {
                    return true;
                }

            }
        }
        return false;
    }

    private Button criarBotaoDivisao(string p, bool bold, string IdClienteDivisao)
    {
        Button bot = new Button();
        bot.Text = p;
        bot.BorderStyle = BorderStyle.None;
        bot.Font.Name = "Verdana";
        bot.Style.Add("font-size", "7pt");
        bot.Font.Bold = bold;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += new EventHandler(Button_ClickDivisao);
        bot.ID = IdClienteDivisao;
        bot.BackColor = System.Drawing.Color.Transparent;
        return bot;
    }


    private void Button_ClickDivisao(object sender, System.EventArgs e)
    {
        try
        {
            Button b = (Button)sender;
            lblMensagem.Text = "Item Selecionado: " + b.Text;
            lblMensagem0.Text = b.ID;
            Button3.Attributes.Add("OnClick", "javascript:window.open('rptfoto.aspx?idClienteDivisao=" + lblMensagem0.Text + "&Divisao="+Server.HtmlEncode(b.Text)+"');");

            PnlMensagem.Visible = true;
            Button3.Visible = true;
            rpt.DataSource = new SistranBLL.Produto().ConsultarProdutoComFoto(b.ID);
            rpt.DataBind();
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }
    #endregion
    
    
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Image ImageButton2 = (Image)e.Item.FindControl("ImageButton2");
        Label lblMedida = (Label)e.Item.FindControl("lblMedida");
        
        if (ImageButton2 != null)
        {
            DataRowView bb = (DataRowView)e.Item.DataItem;
            lblMedida.Text = bb.Row["Altura"].ToString() + " x " + bb.Row["Largura"].ToString() + " x " + bb.Row["Comprimento"].ToString() + " = " + (Convert.ToDouble(bb.Row["Altura"]) * Convert.ToDouble(bb.Row["Largura"]) * Convert.ToDouble(bb.Row["Comprimento"])).ToString("#0.00000");

            ImageButton2.ImageUrl = "~/Images/naoDisponivel.jpg";              
                try
                {
                    byte[] imagem = (byte[])bb.Row["foto"];
                    MemoryStream ms = new MemoryStream(imagem);
                    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    returnImage.Save(Server.MapPath(@"imgReport/" + bb.Row["CODIGO"].ToString() + ".jpg"));
                    ImageButton2.ImageUrl = "imgReport/" + bb.Row["CODIGO"].ToString() + ".jpg";
                }
                catch (Exception)
                {
                    ImageButton2.ImageUrl = "~/Images/naoDisponivel.jpg";

                }             


        }
    }
}