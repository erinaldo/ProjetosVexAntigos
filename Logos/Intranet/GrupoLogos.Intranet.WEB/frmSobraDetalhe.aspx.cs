using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class frmSobraDetalhe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarCboFilial(cboFilialOrigem);
            CarregarCboFilial(cboFilialDestino);
            carregarCampos();
            txtCliente.ReadOnly = true;
        }
    }

    private void carregarCampos()
    {
        if (this.Request.QueryString["id"] != null)
        {
            string strsql = " SELECT S.*, CADCLI.RAZAOSOCIALNOME FROM SOBRA S INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO=S.IDCLIENTE  WHERE IDSOBRA=" + Request.QueryString["id"];
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);


            if (dt.Rows.Count > 0)
            {
                cboFilialDestino.SelectedValue = dt.Rows[0]["IDFILIALDESTINO"].ToString();
                cboFilialOrigem.SelectedValue = dt.Rows[0]["IDFILIAL"].ToString();
                txtNomeColaborador.Text = dt.Rows[0]["NOMEDOCOLABORADOR"].ToString().ToUpper();

                txtNotaFiscal.Text = dt.Rows[0]["NUMERONOTAFISCAL"].ToString().ToUpper();
                txtPreNota.Text = dt.Rows[0]["PRENOTAFISCAL"].ToString().ToUpper();
                cboTipoDeVolume.SelectedValue = dt.Rows[0]["TIPODEVOLUME"].ToString().ToUpper();
                txtDescricaoVolume.Text = dt.Rows[0]["DESCRICAODOVOLUME"].ToString().ToUpper();

                if (dt.Rows[0]["QUANTIDADE"].ToString().ToUpper() != "")
                {
                    txtQuantidade.Text = Convert.ToDecimal(dt.Rows[0]["QUANTIDADE"]).ToString("#0.00");

                }
                txtNomeMotorista.Text = dt.Rows[0]["NOMEMOTORISTAEMBARQUE"].ToString().ToUpper();
                txtPlaca.Text = dt.Rows[0]["PLACACARRETAEMBARQUE"].ToString().ToUpper();
                txtRota.Text = dt.Rows[0]["ROTADOVEICULO"].ToString().ToUpper();
                lblIdCliente.Text = dt.Rows[0]["IDCLIENTE"].ToString().ToUpper();
                txtCliente.Text = dt.Rows[0]["RAZAOSOCIALNOME"].ToString().ToUpper();
                txtI.Text = dt.Rows[0]["DATADEEMBARQUEDOVOLUME"].ToString().ToUpper();
                txtCliente.ReadOnly = true;


            }
        }
    }

    private void CarregarCboFilial(DropDownList cboFilial)
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();
        cboFilial.Items.Insert(0, new ListItem("Selecione"));
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        dvEscolherCliente.Visible = true;
        btnAbriPesquisarCliente.Visible = false;
        txtFiltroNome.Focus();
        txtFiltroNome.Text = "";
        rdListClientes.Items.Clear();
    }

    protected void btnPesquisarFiltro_Click(object sender, EventArgs e)
    {
        rdListClientes.Items.Clear();
        if (txtFiltroNome.Text.Trim() == "")
        {
            return;
        }

        DataTable dtRet = new SistranBLL.Cliente().RetornarClientesIntranet(txtFiltroNome.Text, "");
        for (int i = 0; i < dtRet.Rows.Count; i++)
        {
            string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper();

            if (nome.Length > 40)
                nome = nome.Substring(0, 40) + "...";
            rdListClientes.Items.Insert(i, new ListItem(nome, dtRet.Rows[i]["IDCLIENTE"].ToString()));
        }

        rdListClientes.Visible = true;
        tblEscolherClientes.Visible = true;

    }

    protected void rdListClientes_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblIdCliente.Text = rdListClientes.SelectedValue;
        txtCliente.Text = rdListClientes.SelectedItem.Text.ToUpper();

        dvEscolherCliente.Visible = false;
        btnAbriPesquisarCliente.Visible = true;



    }
    protected void btnGravarTudo_Click(object sender, EventArgs e)
    {
        try
        {

            string strsql = "";
            if (this.Request.QueryString["id"] == null)
            {
                 strsql = " INSERT INTO SOBRA (IDSOBRA,DATA,IDFILIAL,NOMEDOCOLABORADOR,IDCLIENTE,NUMERONOTAFISCAL,PRENOTAFISCAL,TIPODEVOLUME,DESCRICAODOVOLUME,QUANTIDADE,DATADEEMBARQUEDOVOLUME,NOMEMOTORISTAEMBARQUE,PLACACARRETAEMBARQUE,ROTADOVEICULO,FINALIZADO, IDFILIALDESTINO) VALUES ( ";
                strsql += Sistran.Library.GetDataTables.RetornarIdTabela("Sobra") + " , ";
                strsql += " getdate(), ";
                strsql += cboFilialOrigem.SelectedValue + " , ";
                strsql += " '" + txtNomeColaborador.Text.ToUpper().Trim() + "', ";
                strsql += lblIdCliente.Text + " , ";
                strsql += " '" + txtNotaFiscal.Text.ToUpper().Trim() + "', ";
                strsql += " '" + txtPreNota.Text.ToUpper().Trim() + "', ";
                strsql += " '" + cboTipoDeVolume.SelectedValue + "', ";
                strsql += " '" + txtDescricaoVolume.Text.ToUpper().Trim() + "', ";
                strsql += txtQuantidade.Text.Replace(",", ".") + " , ";
                strsql += "convert(datetime, '" + DateTime.Parse(txtI.Text).ToShortDateString() + "', 103) , ";
                strsql += " '" + txtNomeMotorista.Text.ToUpper().Trim() + "', ";
                strsql += " '" + txtPlaca.Text.ToUpper().Trim() + "', ";
                strsql += " '" + txtRota.Text.ToUpper().Trim() + "', ";
                strsql += " 'NAO', " + cboFilialDestino.SelectedValue + ") ";
            }
            else
            {
                strsql = " UPDATE SOBRA SET ";
                strsql += " IDFILIAL=" + cboFilialOrigem.SelectedValue +",";
                strsql += " NOMEDOCOLABORADOR= '" + txtNomeColaborador.Text.ToUpper().Trim() + "',";
                strsql += " IDCLIENTE="+ lblIdCliente.Text + ",";
                strsql += " NUMERONOTAFISCAL='" + txtNotaFiscal.Text.ToUpper().Trim() + "',";
                strsql += " PRENOTAFISCAL='" + txtPreNota.Text.ToUpper().Trim() + "',";
                strsql += " TIPODEVOLUME='" + cboTipoDeVolume.SelectedValue + "',";
                strsql += " DESCRICAODOVOLUME='" + txtDescricaoVolume.Text.ToUpper().Trim() + "',";
                strsql += " QUANTIDADE="+  txtQuantidade.Text.Replace(",", ".") +",";
                strsql += " DATADEEMBARQUEDOVOLUME=convert(datetime, '" + DateTime.Parse(txtI.Text).ToShortDateString() + "', 103),";
                strsql += " NOMEMOTORISTAEMBARQUE='" + txtNomeMotorista.Text.ToUpper().Trim() + "',";
                strsql += " PLACACARRETAEMBARQUE='" + txtPlaca.Text.ToUpper().Trim() + "',";
                strsql += " ROTADOVEICULO='" + txtRota.Text.ToUpper().Trim() + "',";
                strsql += " FINALIZADO='NAO', ";
                strsql += " IDFILIALDESTINO= " + cboFilialDestino.SelectedValue;
                strsql += " WHERE IDSOBRA=" + Request.QueryString["id"].ToString();
            }
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

            Response.Redirect("frmsobra.aspx?opc=SOBRAS IDENTIFICADAS NAS FILIAIS");


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}