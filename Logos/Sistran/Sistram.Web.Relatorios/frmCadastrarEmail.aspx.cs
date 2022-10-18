using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using ChartDirector;
using Sistran;


public partial class frmCadastrarEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            carregarGrid();
        }
    }

    private void carregarGrid()
    {
        RadGrid16.DataSource = Sistran.Library.Robo.Robo.RetornarEmails();
        RadGrid16.DataBind();
    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {

    }
    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {

    }
    protected void btNovo_Click(object sender, EventArgs e)
    {        
        dvDados.Visible = true;
        btNovo.Enabled = false;
        RadGrid16.Enabled = false;
        txtNome.Focus();
    }
    protected void btSalvar0_Click(object sender, EventArgs e)
    {
        dvDados.Visible = false;
        limparCampos();
        btNovo.Enabled = true;
        RadGrid16.Enabled = true;
    }

    private void limparCampos()
    {
        txtEmail.Text = "";
        txtNome.Text = "";
        lblerro.Text = "";
        lblCodigo.Text = "0";

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }

        for (int i = 0; i < chkReports.Items.Count; i++)
        {
            chkReports.Items[i].Selected = false;
        }
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        string shoras = "";
        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            if (CheckBoxList1.Items[i].Selected == true)
            {
                shoras += CheckBoxList1.Items[i].Text + ",";
            }
        }

        string sreports = "";
        for (int i = 0; i < chkReports.Items.Count; i++)
        {
            if (chkReports.Items[i].Selected == true)
            {
                sreports += chkReports.Items[i].Text + ",";
            }
        }


        if (shoras.Length > 0)
        {
            shoras = shoras.Substring(0, shoras.Length - 1);
        }

        if (sreports.Length > 0)
        {
            sreports = sreports.Substring(0, sreports.Length - 1);
        }


        if (txtNome.Text == "" || txtEmail.Text == "" || shoras.Length ==0 || sreports.Length==0)
        {
            lblerro.Text = "Verifique os dados.";
            return;
        }

        HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
        
        if (lblCodigo.Text == "0")
        {
            string strsql = "insert into AvisoRoboEmail(IDAvisoKPI, Nome, Email, Horas, reports) values ((select max(isnull(IDAvisoKPI,0))+1 from AvisoRoboEmail) , '" + txtNome.Text.ToUpper().Trim().Replace("'", "") + "', '" + txtEmail.Text.ToUpper().Trim().Replace("'", "") + "', '" + shoras.ToUpper().Trim().Replace("'", "") + "','" + sreports.ToUpper().Trim().Replace("'", "") + "')";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        }
        else
        {
            string strsql = "UPDATE AvisoRoboEmail SET Nome='" + txtNome.Text.ToUpper().Trim().Replace("'", "") + "', Email='" + txtEmail.Text.ToUpper().Trim().Replace("'", "") + "', Horas='" + shoras.Replace("'", "") + "', reports='"+ sreports +"'  WHERE IDAvisoKPI=" + lblCodigo.Text;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
        }
        dvDados.Visible = false;
        limparCampos();
        btNovo.Enabled = true;
        RadGrid16.Enabled = true;
        dvDados.Visible = false;
        carregarGrid();
    }

    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "Excluir")
        {
            string strsql = "DELETE FROM AvisoRoboEmail WHERE IDAVISOKPI=" + e.CommandName.ToString();
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            carregarGrid();
        }
        else
        {

            limparCampos();
            dvDados.Visible = true;
            btNovo.Enabled = false;
            RadGrid16.Enabled = false;
            txtNome.Focus();
            lblCodigo.Text = e.CommandName.ToString();

            DataTable dtDetalhe = Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT * FROM AvisoRoboEmail WHERE IDAVISOKPI=" + lblCodigo.Text, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            if (dtDetalhe.Rows.Count > 0)
            {
                txtNome.Text = dtDetalhe.Rows[0]["nome"].ToString().ToUpper();
                txtEmail.Text = dtDetalhe.Rows[0]["email"].ToString().ToUpper();
                string[] m = dtDetalhe.Rows[0]["horas"].ToString().Split(',');
                string[] mReports = dtDetalhe.Rows[0]["reports"].ToString().Split(',');

                for (int i = 0; i < m.Length; i++)
                {
                    for (int ii = 0; ii < CheckBoxList1.Items.Count; ii++)
                    {
                        if (m[i] == CheckBoxList1.Items[ii].Value)
                        {
                            CheckBoxList1.Items[ii].Selected = true;
                        }
                    }
                }

                for (int i = 0; i < mReports.Length; i++)
                {
                    for (int ii = 0; ii < chkReports.Items.Count; ii++)
                    {
                        if (mReports[i] == chkReports.Items[ii].Value)
                        {
                            chkReports.Items[ii].Selected = true;
                        }
                    }
                }
            }
        }
        carregarGrid();
    }
}