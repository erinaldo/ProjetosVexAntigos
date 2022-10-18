using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistecnoColetor
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        string cnx = "";
        Classes.DTO.Usuario user;
        public Menu(Classes.DTO.Usuario User, string Cnx)
        {
            cnx = Cnx;
            user = User;
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            if (user.UltimaEmpresa == null || user.UltimaFilial == null)
            {
                user.UltimaEmpresa = 1;
                user.UltimaFilial = 1;
                //Inicial f = new Inicial(user, cnx);
                //f.ShowDialog();

                MessageBox.Show("Antes de começar por favor selecione uma filial");
            }


            statusBar1.Text = "FILIAL: " + user.NomeFilial;

            string senhaws = DateTime.Now.Year.ToString() +
                               DateTime.Now.Month.ToString() +
                               DateTime.Now.Day.ToString() +
                               DateTime.Now.Hour.ToString();
            
            if (user.EmpresaClienteLogado == 2)
            {
                MontarMenuLogos();
                return;
            }

            DataTable dtPemissoes = new Classes.BLL.Menu().RetornarMenusPermissionados(user.IDUsuario, cnx);

            string sqlM = "SELECT MO.IDMODULOOPCAO, MO.DESCRICAO, MO.PROGRAMA, MO.IDOPERACAOCOLETOR, OC.NOME OPERACAO FROM MODULOOPCAO MO LEFT JOIN OPERACAOCOLETOR OC ON OC.IDOPERACAOCOLETOR = MO.IDOPERACAOCOLETOR WHERE MO.TIPO='CLW'";
            DataTable dtBase = Classes.BdExterno.RetornarDT(sqlM, VarGlobal.Conexao);
            //DataTable dtBase = new wsLogin.Login().RetornarProgramasColetor(senhaws);

            DataView view = new DataView(dtBase);
            DataTable distinctValues = view.ToTable(true, "IDOPERACAOCOLETOR", "OPERACAO");

            bool passou = false;
            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {

                TreeNode x = new TreeNode();

                if (i == 0)
                {
                    x.Text = "MENU";
                    x.Tag = "0";
                    treeView1.Nodes.Add(x);
                    x = new TreeNode();

                    x.Text = "TROCAR FILIAL";
                    x.Tag = "-2";
                    treeView1.Nodes.Add(x);

                    x = new TreeNode();
                }

                if (distinctValues.Rows[i]["OPERACAO"].ToString() != "")
                {
                    x.Text = distinctValues.Rows[i]["OPERACAO"].ToString();
                    x.Tag = "0";
                    DataRow[] o = dtBase.Select("IDOPERACAOCOLETOR=" + distinctValues.Rows[i]["IDOPERACAOCOLETOR"].ToString(), "");

                    for (int ii = 0; ii < o.Length; ii++)
                    {
                        TreeNode xx = new TreeNode();
                        xx.Text = o[ii]["DESCRICAO"].ToString().ToUpper();
                        xx.Tag = o[ii]["PROGRAMA"].ToString();


                        if (dtPemissoes.Select("IDMODULOOPCAO = " + o[ii]["IDMODULOOPCAO"].ToString(), "").Length > 0)
                        {
                            x.Nodes.Add(xx);
                        }
                    }
                    treeView1.Nodes.Add(x);
                }
                else
                {

                    if (passou == false)
                    {
                        passou = true;
                        x.Text = "Outros";
                        x.Tag = "0";
                        DataRow[] o = dtBase.Select("IDOPERACAOCOLETOR IS NULL", "");

                        for (int ii = 0; ii < o.Length; ii++)
                        {
                            TreeNode xx = new TreeNode();
                            xx.Text = o[ii]["DESCRICAO"].ToString().ToUpper();
                            xx.Tag = o[ii]["PROGRAMA"].ToString();

                            if (dtPemissoes.Select("IDMODULOOPCAO = " + o[ii]["IDMODULOOPCAO"].ToString(), "").Length > 0)
                                x.Nodes.Add(xx);

                        }
                        treeView1.Nodes.Add(x);

                    }
                }
            }
            treeView1.ExpandAll();
        }

        private void MontarMenuLogos()
        {
            TreeNode x = new TreeNode();
            x.Text = "MENU";
            x.Tag = "0";
            treeView1.Nodes.Add(x);
            x = new TreeNode();

            x.Text = "TROCAR FILIAL";
            x.Tag = "-5";
            treeView1.Nodes.Add(x);

            x.Text = "Reposição / Faltas";
            x.Tag = "-99";
            treeView1.Nodes.Add(x);
        }

        private void btnLogar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAlterarEmpresaFilial_Click(object sender, EventArgs e)
        {



        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show(e.Node.Tag.ToString());

            if (e.Node.Tag.ToString() == "0")
                return;

            if (e.Node.Tag.ToString() == "-2")
            {
                Inicial f = new Inicial(user, cnx);
                f.ShowDialog();
                return;
            }


            if (e.Node.Tag.ToString() == "-5")
            {
                FiliaisLogos f = new FiliaisLogos(user);
                f.ShowDialog();
                return;
            }

            if (e.Node.Tag.ToString() == "-99")
            {
                CLW00004 f = new CLW00004();
                f.ShowDialog();
                return;
            }

            try
            {
                Form form = (Form)Activator.CreateInstance(Type.GetType("SistecnoColetor." + e.Node.Tag.ToString()));
                VarGlobal.NomePrograma = e.Node.Text;        
                form.ShowDialog();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message , "Atenção");
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            SistecnoColetor.frmLogin f = new frmLogin();
            this.Hide();
            f.Show();
            return;
        }

        private void Menu_Activated(object sender, EventArgs e)
        {
            treeView1.SelectedNode = treeView1.Nodes[0];
        }
    }
}