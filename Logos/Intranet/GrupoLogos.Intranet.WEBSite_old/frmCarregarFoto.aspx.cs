using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class frmCarregarFoto : System.Web.UI.Page
{
    DataTable dtFoto;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtFoto"] == null)
            {
                dtFoto = new DataTable("img");
                dtFoto.Columns.Add("idTemp", typeof(string));
                dtFoto.Columns.Add("id");
                dtFoto.Columns.Add("conteudo", typeof(byte[]));
                dtFoto.Columns.Add("excluido", typeof(string)); ;
                dtFoto.Columns.Add("texto", typeof(string)); ;
                Session["dtFoto"] = dtFoto;
            }
            else
            {
                dtFoto = (DataTable)Session["dtFoto"];
            }

            if (Session["lblId"].ToString() != "novo" && Session["lblId"] != null && dtFoto.Rows.Count==0)
            {
                switch (Request.QueryString["tipo"].ToString())
                {
                    case "motorista":
                        PesquisarDoCadastro();
                        break;

                    case "Veiculo":
                        break;
                }
            }
        }
    }

    private void PesquisarDoCadastro()
    {
        dtFoto = Sistran.Library.GetDataTables.FotoMotorista(Session["lblId"].ToString());
        Session["dtFoto"] = dtFoto;
        montarVisaoFoto();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        dtFoto = (DataTable)Session["dtFoto"];
        if (FileUpload1.HasFile)
        {

            int intTamanho = System.Convert.ToInt32(FileUpload1.PostedFile.InputStream.Length);

            byte[] imageBytes = new byte[intTamanho];
            FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);

            if (lblIdFoto.Text == "")
            {
                string nome = Guid.NewGuid().ToString();
                DataRow r = dtFoto.NewRow();
                r[0] = nome;
                r[1] = "0";
                r[2] = imageBytes;
                r[3] = "";
                r[4] = txtDescricao.Text.ToUpper().ToUpper();
                dtFoto.Rows.Add(r);
                FileUpload1.SaveAs(Server.MapPath("imgReport") + "\\" + nome + ".jpg");
            }
            else
            {
                for (int i = 0; i < dtFoto.Rows.Count; i++)
                {
                    if (dtFoto.Rows[i][0].ToString() == lblIdFoto.Text)
                    {
                        dtFoto.Rows[i][4] = txtDescricao.Text.ToUpper();
                        dtFoto.Rows[i][2] = imageBytes;
                        FileUpload1.SaveAs(Server.MapPath("imgReport") + "\\" + dtFoto.Rows[i][0].ToString() + ".jpg");
                    }
                }
            }
        }
        else
        {
            if (lblIdFoto.Text != "")
            {
                for (int i = 0; i < dtFoto.Rows.Count; i++)
                {
                    if (dtFoto.Rows[i][0].ToString() == lblIdFoto.Text)
                    {
                        dtFoto.Rows[i][4] = txtDescricao.Text.ToUpper();
                    }
                }
            }
        }
        Session["dtFoto"] = dtFoto;
        montarVisaoFoto();
        txtDescricao.Text = "";
        lblIdFoto.Text = "";
        FileUpload1.Focus();
    }

    private void montarVisaoFoto()
    {
        dtFoto = (DataTable)Session["dtFoto"];

        DataTable dtFototmp = new DataTable("imgTemp");
        dtFototmp.Columns.Add("idTemp");
        dtFototmp.Columns.Add("id");
        dtFototmp.Columns.Add("conteudo", typeof(byte[]));
        dtFototmp.Columns.Add("excluido");
        dtFototmp.Columns.Add("texto");

        for (int i = 0; i < dtFoto.Rows.Count; i++)
        {
            if (dtFoto.Rows[i]["excluido"].ToString() != "SIM")
            {
                DataRow r = dtFototmp.NewRow();
                r[0] = dtFoto.Rows[i][0];
                r[1] = dtFoto.Rows[i][1];
                r[2] = dtFoto.Rows[i][2];
                r[3] = dtFoto.Rows[i][3];
                r[4] = dtFoto.Rows[i][4];
                dtFototmp.Rows.Add(r);
            }
        }

        DataList1.DataSource = dtFototmp;//.Select("excluido<>'SIM'");
        DataList1.DataBind();
        Session["dtFoto"] = dtFoto;
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgDL = (Image)e.Item.FindControl("imgDL");
            DataRowView linha = (DataRowView)e.Item.DataItem;

            byte[] imagem = (byte[])linha["conteudo"];
            MemoryStream ms = new MemoryStream(imagem);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            returnImage.Save(Server.MapPath(@"imgReport/" + linha[0].ToString() + ".jpg"));
            imgDL.ImageUrl = "imgReport/" + linha[0].ToString() + ".jpg";
        }
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        dtFoto = (DataTable)Session["dtFoto"];
        Label lblIdTemp = (Label)e.Item.FindControl("lblIdTemp");
        Label lblIdBanco = (Label)e.Item.FindControl("lblIdBanco");
        Label lblDescricao = (Label)e.Item.FindControl("lblDescricao");

        if (e.CommandArgument.ToString().Contains("ExcluirImagem"))
        {
            for (int i = 0; i < dtFoto.Rows.Count; i++)
            {
                if (dtFoto.Rows[i][0].ToString() == lblIdTemp.Text && dtFoto.Rows[i][1].ToString()==lblIdBanco.Text)
                {
                    dtFoto.Rows[i][3] = "SIM";
                }
            }
        }
        else if (e.CommandArgument.ToString().Contains("AlterarImagem"))
        {
            lblIdFoto.Text = lblIdTemp.Text;
            txtDescricao.Text = lblDescricao.Text;
        }
       Session["dtFoto"]= dtFoto;
        montarVisaoFoto();
    }
}
