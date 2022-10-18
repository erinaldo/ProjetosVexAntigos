using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;

namespace Sistram.Web.Captacao
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                HttpContext.Current.Session["Conn"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                cboCidade.Items.Clear();
                cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                CarregarCboEstado();
                CarregarListaFilial();

                cboModelo.Items.Insert(0, (new ListItem("SELECIONE UMA MARCA", "0")));
                CarregarComboMarca();
                CarregarComboRastreador();
            }
        }

        private void CarregarComboRastreador()
        {
            cboRastreador.Items.Clear();
            cboRastreador.DataSource = new SistranBLL.Veiculo.Rastreador().Listar();
            cboRastreador.DataTextField = "Nome";
            cboRastreador.DataValueField = "IDVEICULORASTREADOR";
            cboRastreador.DataBind();
            cboRastreador.Items.Insert(0, (new ListItem("SELECIONE", "0")));
        }

        private void CarregarComboMarca()
        {
            cboMarca.Items.Clear();
            cboMarca.DataSource = new SistranBLL.Veiculo.Marca().Listar();
            cboMarca.DataTextField = "Nome";
            cboMarca.DataValueField = "IDVeiculoMarca";
            cboMarca.DataBind();
            cboMarca.Items.Insert(0, (new ListItem("SELECIONE", "0")));
        }

        private void CarregarComboModelo()
        {
            cboModelo.Items.Clear();
            cboModelo.DataSource = new SistranBLL.Veiculo.Modelo().Listar(cboMarca.SelectedValue);
            cboModelo.DataTextField = "Nome";
            cboModelo.DataValueField = "IDVeiculoModelo";
            cboModelo.DataBind();

            if (cboModelo.Items.Count == 0)
            {
                cboModelo.Items.Insert(0, (new ListItem("NENHUM MODELO CADASTRADO", "0")));
            }
            else
            {
                cboModelo.Items.Insert(0, (new ListItem("SELECIONE", "0")));
            }
        }
        private void CarregarCboEstado()
        {
            cboEstado.DataSource = new SistranBLL.Localizacao.Estado().Listar();
            cboEstado.DataTextField = "UF";
            cboEstado.DataValueField = "IDESTADO";
            cboEstado.DataBind();
            cboEstado.Items.Insert(0, new ListItem("SELECIONE", "0"));
            cboCidade.Enabled = true;
        }



        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.SelectedValue == "0")
            {
                cboCidade.Items.Clear();
                cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                cboCidade.Enabled = false;
                return;
            }
            else
            {

                CarregarCoboCidade(cboEstado.SelectedValue);
            }
        }

        private void CarregarCoboCidade(string idEstado)
        {
            cboCidade.Items.Clear();
            cboCidade.DataSource = new Localizacao.Cidade().ReadbyIdEstado(Convert.ToInt32(idEstado));
            cboCidade.DataTextField = "NOME";
            cboCidade.DataValueField = "IDCIDADE";
            cboCidade.DataBind();
            cboCidade.Items.Insert(0, (new ListItem("SELECIONE", "0")));
            cboCidade.Enabled = true;
        }


        private void CarregarListaFilial()
        {
            lstTodasFiliais.DataSource = new SistranBLL.Filial().ListarDisponiveisByIDMotorista("", 0);
            lstTodasFiliais.DataTextField = "NOME";
            lstTodasFiliais.DataValueField = "IDFILIAL";
            lstTodasFiliais.DataBind();
        }

        protected void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarComboModelo();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool Selecionou = false;
            string filiais = "";

            for (int i = 0; i < lstTodasFiliais.Items.Count; i++)
            {
                if (lstTodasFiliais.Items[i].Selected == true)
                {
                    Selecionou = true;
                    filiais += lstTodasFiliais.Items[i].Value + ",";
                }
            }


            if (Selecionou == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Selecione uma Filial ')</script>", false);
            }
            else
                filiais = filiais.Substring(0, filiais.Length - 1);




        }


        protected void txtPlaca_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPlaca.Text.Length > 0)
                {
                    if (txtPlaca.Text.Length < 7)
                    {
                        throw new Exception("Placa Inválida");
                    }

                    txtPlaca.Text = txtPlaca.Text.Replace(".", "");
                    txtPlaca.Text = txtPlaca.Text.Replace("-", "");
                    txtPlaca.Text = txtPlaca.Text.Replace("/", "");
                    txtPlaca.Text = txtPlaca.Text.Replace(@"\", "");

                    string s = txtPlaca.Text;
                    string ss = "";

                    ss = (s.Substring(0, 3) + "-" + s.Substring(3, 4)).ToUpper();
                    txtPlaca.Text = ss;

                    txtCNH.Focus();                   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
                return;
            }
        }
    }
}