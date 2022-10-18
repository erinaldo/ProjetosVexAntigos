using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistecno.DAL.BD;
using Util;

namespace Sistecno.UI.Web.UC
{
    public partial class dtrPesquisaGenerica : System.Web.UI.UserControl
    {
        public class RestricoesPesquisa
        {
            public string  CampoDoBD { get; set; }
            public string Valor { get; set; }
        }


        //public event EventHandler UserControlSelecionarClicked; // associa ao evento pesquisar do pai
        public List<Sistecno.BLL.Helpers.CamposSearch> camposPesquisa { get; set; }
        public List<RestricoesPesquisa> restricoesPesquisa { get; set; }
        public string sqlBasico { get; set; }
        public string ret { get; set; }

        public delegate void delChamarEvento();
        public event delChamarEvento evChamarEvento;
        

        protected void Page_Load(object sender, EventArgs e)
        {           

            if (!IsPostBack)
            {
                lblId.Value = "";
                return;
            }

            if (lblId.Value != "")
            {                
               ret = lblId.Value;
                this.evChamarEvento();
                lblId.Value = "";
            }
            MontarCampos();
        }

      

        public void MontarCampos()
        {           
            if (this.camposPesquisa == null)
                return;

            PlaceHolder phPesquisa = new PlaceHolder();

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
            btnPesquisar.ID = "btnPesquisa";
            btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.CssClass = "btn btn-primary btn-xs";

            phPesquisa.Controls.Add(new LiteralControl(@" <td>"));
            phPesquisa.Controls.Add(btnPesquisar);
            phPesquisa.Controls.Add(new LiteralControl(@" </td>"));
            phPesquisa.Controls.Add(new LiteralControl(@" </tr>"));
            phPesquisa.Controls.Add(new LiteralControl(@"</table>"));
            phPesquisa.Controls.Add(new LiteralControl(@"</div><BR>"));           
            pnlPesquisa1.Controls.Add(phPesquisa);
            
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
           executarPesquisa();   
        }

        private void executarPesquisa()
        {
            lblId.Value = "";

            string where = " where 0=0 ";

            if (restricoesPesquisa != null)
            {
                for (int i = 0; i < restricoesPesquisa.Count; i++)
                {
                    where += " AND " + restricoesPesquisa[i].CampoDoBD + "= '" + restricoesPesquisa[i].Valor + "'";
                }
            }

            for (int i = 0; i < camposPesquisa.Count; i++)
            {
                if (camposPesquisa[i].TipoControle == "Combo")
                {
                    DropDownList txt = (DropDownList)pnlPesquisa1.FindControl("txtFiltro" + camposPesquisa[i].NomeCampo);
                    if (txt != null)
                    {
                        camposPesquisa[i].Valor = txt.SelectedValue;
                        where += " AND " + camposPesquisa[i].NomeCampo + " like '" + camposPesquisa[i].Valor + "%'";
                    }
                }
                else
                {
                    TextBox txt = (TextBox)pnlPesquisa1.FindControl("txtFiltro" + camposPesquisa[i].NomeCampo);

                    if (txt != null)
                    {
                        camposPesquisa[i].Valor = txt.Text;
                        where += " AND " + camposPesquisa[i].NomeCampo.Replace("_", "") + " like '" + camposPesquisa[i].Valor + "%'";
                    }
                }
            }
            CriarGrid(where);         
        }

        DataTable dados;
        private void CriarGrid(string swhere)
        {
            htm.Controls.Clear();
            List<PropriedadesGrid> Colunas = new List<PropriedadesGrid>();

            dados = cDb.RetornarDataTable(this.sqlBasico + swhere + " ORDER BY 1 DESC ", Session["CNX"].ToString());
            for (int i = 0; i < dados.Columns.Count; i++)
            {
                PropriedadesGrid p = new PropriedadesGrid();
                p.TextoColuna = dados.Columns[i].ColumnName.ToUpper();
                p.Tipo = dados.Columns[i].DataType.Name.ToString().ToUpper();
                Colunas.Add(p);
            }          

            htm.Controls.Add(new LiteralControl("<table  class='table table-striped table-bordered table-hover'> "));
            htm.Controls.Add(new LiteralControl("<thead>"));
            htm.Controls.Add(new LiteralControl("<tr>"));

            for (int ii = 0; ii < Colunas.Count; ii++)
            {
                string alinhamento = Util.GradeDeDados.AlinhamentoColuna(Colunas[ii].Tipo.ToUpper());
                htm.Controls.Add(new LiteralControl("<th style='text-align:" + alinhamento + "'>" + Colunas[ii].TextoColuna + "</th>"));
            }            
            htm.Controls.Add(new LiteralControl("<th></th>"));

            htm.Controls.Add(new LiteralControl("</tr>"));
            htm.Controls.Add(new LiteralControl("</thead>"));
            htm.Controls.Add(new LiteralControl("<tbody>"));

            for (int i = 0; i < dados.Rows.Count; i++)
            {
                htm.Controls.Add(new LiteralControl("<tr>"));
                string alinhamento = "left";

                for (int c = 0; c < dados.Columns.Count; c++)
                {
                    alinhamento = Util.GradeDeDados.AlinhamentoColuna(dados.Columns[c].DataType.Name.ToString().ToUpper());
                    htm.Controls.Add(new LiteralControl("<td style='text-align: " + alinhamento + "'>" + dados.Rows[i][c].ToString() + "</td>"));
                }
                
                htm.Controls.Add(new LiteralControl("<td style='text-align: right'>"));
                htm.Controls.Add(CriarBotao(dados.Rows[i][0].ToString()));
                htm.Controls.Add(new LiteralControl("</td>"));
                htm.Controls.Add(new LiteralControl("</tr>"));
            }

            htm.Controls.Add(new LiteralControl("</tbody>"));
            htm.Controls.Add(new LiteralControl("</table>"));            
        }

        private Button CriarBotao(string id)
        {
            Button botao = new Button();
            botao.Text = "Selecionar";
            botao.CssClass = "btn btn-primary btn-xs";
            botao.ID = "btn" + id;
            botao.Attributes.Add("onclick", "setarId("+id+")");
           // botao.Click += new EventHandler(botao_Click);

            //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            //trigger.ControlID = botao.ID;
            //trigger.EventName = "Click";
            //UpdatePanel4.Triggers.Add(trigger);  
            return botao;
        }

        private void botao_Click(object sender, EventArgs e)
        {
            //ret = ((Button)sender).ID.Replace("btn", "");
            //Session["linhaSel"] = dados.Select(dados.Columns[0].ColumnName + "=" + ret, "");
            //evChamarEvento();
        //    UserControlSelecionarClicked(this, EventArgs.Empty);
        }
    }
}