using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Sistecno.UI.Web.UC
{
    public partial class dtrPesquisa : System.Web.UI.UserControl
    {
        public event EventHandler UserControlPesquisarClicked; // associa ao evento pesquisar do pai
        public List<Sistecno.BLL.Helpers.CamposSearch> camposPesquisa { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            camposPesquisa = camposPesquisa;
            if (this.camposPesquisa == null)
                return;


            phPesquisa.Controls.Add(new LiteralControl(@"<div style='text-align:left;'>"));
            phPesquisa.Controls.Add(new LiteralControl(@"<table border='0'>"));
            phPesquisa.Controls.Add(new LiteralControl(@" <tr>"));

            for (int i = 0; i < this.camposPesquisa.Count; i++)
            {
                phPesquisa.Controls.Add(new LiteralControl(@" <td><span><b>" + camposPesquisa[i].TextoExibicao + "</b></span></td>"));
            }
            phPesquisa.Controls.Add(new LiteralControl(@" <td></td>"));

            phPesquisa.Controls.Add(new LiteralControl(@" </tr>"));

            phPesquisa.Controls.Add(new LiteralControl(@" <tr>"));
            for (int i = 0; i < this.camposPesquisa.Count; i++)
            {
                #region Adição de TextBox
                if (camposPesquisa[i].TipoControle == "" || camposPesquisa[i].TipoControle == "TextBox")
                {
                    TextBox txt = new TextBox();
                    txt.ID = "txtFiltro" + camposPesquisa[i].NomeCampo;
                    txt.CssClass = "input-xs";

                    switch (camposPesquisa[i].FuncaoJS)
                    {
                        case "SomenteNumero":
                            txt.Attributes.Add("onKeyPress", "return SomenteNumero(event);");
                            break;

                        case "CPF":
                            txt.Attributes.Add("onKeyPress", "MascararCPF(this);");
                            txt.MaxLength = 14;
                            break;

                        case "CNPJ":
                            txt.Attributes.Add("onKeyPress", "MascaraCNPJ(this);");
                            txt.MaxLength = 18;
                            break;
                    }

                    txt.Width = new Unit(camposPesquisa[i].Largura);
                    phPesquisa.Controls.Add(new LiteralControl(@" <td style='padding-right: 5px;'>"));
                    phPesquisa.Controls.Add(txt);
                    phPesquisa.Controls.Add(new LiteralControl(@" </td>"));
                }

                #endregion

                #region Adição Combo
                if (camposPesquisa[i].TipoControle == "Combo")
                {
                    if (camposPesquisa[i].DataSource == null)
                        throw new Exception("Para carregar um combo é necessário um DataSource");

                    DropDownList dp = new DropDownList();
                    dp.ID = "txtFiltro" + camposPesquisa[i].NomeCampo;

                    Util.Combo.CarregarCombo(camposPesquisa[i].DataSource, ref dp, false, true, camposPesquisa[i].NomeCampo, camposPesquisa[i].TextoExibicao);
                    dp.Width = new Unit(camposPesquisa[i].Largura);

                    phPesquisa.Controls.Add(new LiteralControl(@" <td>"));
                    phPesquisa.Controls.Add(dp);
                    phPesquisa.Controls.Add(new LiteralControl(@" </td>"));
                }
                #endregion

            }

            Button btnPesquisar = new Button();
            btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.CssClass = "btn btn-primary btn-xs";

            phPesquisa.Controls.Add(new LiteralControl(@" <td>"));
            phPesquisa.Controls.Add(btnPesquisar);
            phPesquisa.Controls.Add(new LiteralControl(@" </td>"));
            phPesquisa.Controls.Add(new LiteralControl(@" </tr>"));
            phPesquisa.Controls.Add(new LiteralControl(@"</table>"));            
            phPesquisa.Controls.Add(new LiteralControl(@"</div><BR>"));
            pnlPesquisa.Controls.Add(phPesquisa);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < camposPesquisa.Count; i++)
            {

                if (camposPesquisa[i].TipoControle == "Combo")
                {
                    DropDownList txt = (DropDownList)pnlPesquisa.FindControl("txtFiltro" + camposPesquisa[i].NomeCampo);
                    if (txt != null)
                    {
                        camposPesquisa[i].Valor = txt.SelectedValue;
                    }
                }
                else
                {
                    TextBox txt = (TextBox)pnlPesquisa.FindControl("txtFiltro" + camposPesquisa[i].NomeCampo);

                    if (txt != null)
                        camposPesquisa[i].Valor = txt.Text;

                }
            }

             UserControlPesquisarClicked(this, EventArgs.Empty);
        }
    }
}