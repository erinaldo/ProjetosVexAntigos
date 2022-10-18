using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.ComponentModel;
using System.Data;

namespace Sistram.Web.Captacao
{
    public partial class Checar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                HttpContext.Current.Session["Conn"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
              
            }
        }

        DataTable dt;
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            txtCpfCnpj.Text = FormatarCnpj(txtCpfCnpj.Text);

            //139.854.018-88 --14
            //12.367.719/0001-44 --18

            bool valido = false;

            if (txtCpfCnpj.Text.Trim().Length == 14)
            {
                valido = Validacao.ValidaCPF.IsCpf(txtCpfCnpj.Text.Trim());
            }


            if (txtCpfCnpj.Text.Trim().Length == 18)
            {
                valido = Validacao.ValidaCNPJ.IsCnpj(txtCpfCnpj.Text.Trim());
            }


            if (valido == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('CNPJ ou CPF Inválido')</script>", false);
                return;
            }


            string strsql = "SELECT C.IDCADASTRO, C.RAZAOSOCIALNOME, C.CNPJCPF, SENHA FROM CADASTRO C INNER JOIN MOTORISTA MOT ON MOT.IDMOTORISTA = C.IDCADASTRO WHERE C.CNPJCPF='"+txtCpfCnpj.Text.Trim()+"'";

            dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

            if (dt.Rows.Count == 0)
            {
                Response.Redirect("frmCadastrarMotorista.aspx?i=novo&cnpj="+ txtCpfCnpj.Text);
            }
            else
            {
                lblNomeMotorista.Text = "VOCÊ É: <b>"+ dt.Rows[0]["RAZAOSOCIALNOME"].ToString() + "?</b>";
                lblIdcadastro.Text = dt.Rows[0]["IDCADASTRO"].ToString();
                btnSim.Visible = true;
                btnNao.Visible = true;
                btnSalvar.Visible = false;

                lbSenhaRecup.Text = dt.Rows[0]["SENHA"].ToString();

            }
        }

   protected void btnNao_Click(object sender, EventArgs e)
        {
            txtCpfCnpj.Text = "";
            lblNomeMotorista.Text = "";
            btnSim.Visible = false;
            btnNao.Visible = false;
            btnSalvar.Visible = true;
            lblSenha.Visible = false;
            txtSenha.Visible = false;
            btnConfirma.Visible = false;
            txtCpfCnpj.Focus();

        }

        #region HELPERS

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

        #endregion

        protected void btnSim_Click(object sender, EventArgs e)
        {
            //Response.Redirect("frmCadastrarMotorista.aspx?i="+lblIdcadastro.Text);
            lblSenha.Visible = true;
            txtSenha.Visible = true;
            btnConfirma.Visible = true;
            btnSim.Visible = false;
            btnNao.Visible = false;
            txtSenha.Focus();
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            if(lbSenhaRecup.Text==txtSenha.Text)
                Response.Redirect("frmCadastrarMotorista.aspx?i=" + lblIdcadastro.Text);
        }

     
    }
}