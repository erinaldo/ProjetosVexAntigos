using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Globalization;

namespace Sistram.Web.Captacao
{
    public partial class frmCadAnexos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            if (!IsPostBack)
            {
                HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                HttpContext.Current.Session["Conn"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                carregarGrid();
            }
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnNao_Click(object sender, EventArgs e)
        {


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

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            // dvCadastrarImagem.Visible = true;
        }

        protected void btnConfirmarImagem_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                int intTamanho = System.Convert.ToInt32(FileUpload1.PostedFile.InputStream.Length);
                byte[] imageBytes = new byte[intTamanho];
                FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);

                bool bValido = false;
                string sFileName = FileUpload1.FileName;

                string fileExtension = System.IO.Path.GetExtension(sFileName).ToLower();
                foreach (string ext in new string[] { ".gif", ".jpeg", ".jpg", ".png" })
                {
                    if (fileExtension == ext)
                        bValido = true;
                }

                if (!bValido || txtDescFoto.Text.Trim() == "")
                {
                    txtDescFoto.Focus();
                    return;
                }


                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection cn = factory.CreateConnection();
                DbCommand cd = factory.CreateCommand();


                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
                cd.Connection = cn;
                cn.Open();

                string m = "";
                m += " INSERT INTO CADASTROIMAGEM(";
                m += " IDCADASTROIMAGEM,";
                m += " IDCADASTRO,";
                m += " IMAGEM,";
                m += " NOME";
                m += " ) VALUES";
                m += " (";
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROIMAGEM");

                m += ID + " ,";
                m += Request.QueryString["idmotorista"] + " ,";
                m += " @IMAGEM ,";
                m += " '" + txtDescFoto.Text.ToUpper().Replace("'", "") + "'";
                m += " )";


                cd.Parameters.Add(new SqlParameter("@IMAGEM", imageBytes));
                cd.CommandText = m;
                cd.CommandType = CommandType.Text;
                cd.ExecuteNonQuery();
                
            }

            carregarGrid();
        }

        private void carregarGrid()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            cd.CommandText = "SELECT * FROM CADASTROIMAGEM WHERE IDCADASTRO=" + Request.QueryString["idmotorista"];
            cd.CommandType = CommandType.Text;
            cd.Connection = cn;

            cn.Open();
            DbDataAdapter da = factory.CreateDataAdapter();
            da.SelectCommand = cd;
            DataSet ds = new DataSet();
            da.Fill(ds);


            da.Dispose();
            cn.Close();

            grdAnexos.DataSource = ds.Tables[0].DefaultView;
            grdAnexos.DataBind();

            txtDescFoto.Text = "";
            FileUpload1.Focus();

        }

        protected void btnCancelarImagem_Click(object sender, EventArgs e)
        {
            // dvCadastrarImagem.Visible = false;
        }

        protected void grdAnexos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image imgGrid = (Image)e.Row.FindControl("imgGrid");

            if (imgGrid != null)
            {
                DataRowView f = (DataRowView)e.Row.DataItem;
                //f.DataView[0]["Imagem"];

                byte[] imagem = (byte[])f[2];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + f[0] + ".jpg"));
                imgGrid.ImageUrl = "imgReport/" + f[0] + ".jpg";
            }
        }

        protected void grdAnexos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandArgument.ToString().ToLower())
            {
                case "excluir":
                    excluir(e.CommandName.ToString());
                    break;
            }
        }

        private void excluir(string id)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();
            cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            cd.CommandText = "DELETE FROM CADASTROIMAGEM WHERE IDCADASTROIMAGEM= " + id;
            cd.CommandType = CommandType.Text;
            cd.Connection = cn;
            cn.Open();
            cd.ExecuteNonQuery();            
            cn.Close();
            carregarGrid();
        }

        protected void btnConconcluirCadastro_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmConcluir.aspx");
        }
    }
}