using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    private static List<SistranMODEL.Usuario> LUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            if (!IsPostBack)
            {
                LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
                SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
                ddlCliente.DataSource = LUser;
                ddlCliente.DataTextField = "RazaoSocialNome";
                ddlCliente.DataValueField = "EmpresaId";
                ddlCliente.DataBind();

                if (Session["ListClientes"] != null)
                {
                    for (int i = 0; i < ((ListBox)Session["ListClientes"]).Items.Count; i++)
                    {
                        ListBox1.Items.Add(new ListItem(((ListBox)Session["ListClientes"]).Items[i].Text, ((ListBox)Session["ListClientes"]).Items[i].Value));
                    }
                }

                if (ListBox1.Items.Count == 0 && Session["ListClientes"] == null)
                {
                    ListBox lstVirtual = new ListBox();
                    lstVirtual.DataSource = LUser;
                    lstVirtual.DataTextField = "RazaoSocialNome";
                    lstVirtual.DataValueField = "EmpresaId";
                    lstVirtual.DataBind();
                    Session["ListClientes"] = lstVirtual;
                    lblTodos.Visible = true;
                }


                Label lblCodEmpresa = (Label)Master.FindControl("lblCodEmpresa");

                if (Session["IDEmpresa"] != null)
                {
                    ddlCliente.SelectedValue = Session["IDEmpresa"].ToString();
                }


                if (lblCodEmpresa.Text == "")
                {
                    Session["IDEmpresa"] = ddlCliente.SelectedValue;
                }
                else
                {
                    Session["IDEmpresa"] = lblCodEmpresa.Text;
                }
            }

            if (ListBox1.Items.Count == ddlCliente.Items.Count || ListBox1.Items.Count==0)
            {
                ListBox1.Visible = false;
                lblTodos.Visible = true;                
            }
            else
            {
                ListBox1.Visible = true;
                lblTodos.Visible = false;
            }

                //vem do e-mail aprovação pedido
            if (Request.QueryString["id_documento"] != null)
                Response.Redirect("frmAprovaReprovaPedido.aspx?id_documento=" + Request.QueryString["id_documento"] + "&tipo=" + Request.QueryString["tipo"]);

            DataTable dtMenu = SistranBLL.Menu.MontarMenu(LUser[0].UsuarioId);
            DataRow[] m = dtMenu.Select("PROGRAMA='HOME'");

            if (m.Length > 0)
            {
                //Response.Redirect("ResumoPorFilialColunas.aspx?opc=Resumo Por Filial*****");                                 //comentar
                Session["pgInicial"] = m[0]["link"].ToString() + "?opc=Home";
            }
            else
            {
                //Response.Redirect("frmInventario.aspx?opc=INVENTARIO");                                
                Session["pgInicial"] = "Vazio.aspx?opc=" + "Home";
            }

            if (Request.QueryString["acao"] != null)
            {             

                Response.Redirect(Session["pgInicial"].ToString(), false);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('"+ ex.Message.Replace("'","´") +"')", true);
        }
    }

    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void btnAdicionarFilial_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < ListBox1.Items.Count; i++)
        {
            if (ListBox1.Items[i].Value == ddlCliente.SelectedValue && ListBox1.Visible == true)
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Cliente já selecionado.')", true);
                return;
            }
        }

        if (ListBox1.Items.Count == ddlCliente.Items.Count)
        {
            ListBox1.Items.Clear();
        }

        ListBox1.Items.Add(new ListItem(ddlCliente.SelectedItem.Text, ddlCliente.SelectedValue));
        Session["ListClientes"] = ListBox1;

        if (ListBox1.Items.Count > 0)
        {
            ListBox1.Visible = true;
            lblTodos.Visible = false;
        }
    }

    protected void btnRemoverFilial_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < ListBox1.Items.Count; i++)
        {
            if (ListBox1.Items[i].Value == ListBox1.SelectedValue)
            {
                ListBox1.Items.RemoveAt(i);

                if (ListBox1.Items.Count == 0)
                {
                    ListBox lstVirtual = new ListBox();
                    lstVirtual.DataSource = LUser;
                    lstVirtual.DataTextField = "RazaoSocialNome";
                    lstVirtual.DataValueField = "EmpresaId";
                    lstVirtual.DataBind();
                    Session["ListClientes"] = lstVirtual;
                    ListBox1.Visible = false;
                    lblTodos.Visible = true;
                }
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Item removido com sucesso.')", true);
                return;
            }
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["pgInicial"].ToString());
    }
}