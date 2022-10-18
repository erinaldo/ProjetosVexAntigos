using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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

        private Thread segundoPlano;

        private void Menu_Load(object sender, EventArgs e)
        {
           // Cursor.Current = Cursors.WaitCursor;


            if (user.UltimaEmpresa == null || user.UltimaFilial == null)
            {
                user.UltimaEmpresa = 1;
                user.UltimaFilial = 1;
                MessageBox.Show("Antes de começar por favor selecione uma filial");
            }

            //VerificarUnidadeDeVenda();

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

            Cursor.Current = Cursors.Default;

        }

        private void VerificarUnidadeDeVenda()
        {
            string sql = "SELECT TOP (1) * FROM ReposicaoRogeEan";
            DataTable d = Classes.BbColetor.RetornarDataTable(sql );

            if (d.Rows.Count == 0)
            {
                //DialogResult dig = MessageBox.Show("A tabela de Base de Produtos não existe, deseja Atualizar?", "Conferência Roge", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                //if (dig == DialogResult.Yes)
                //    AtualizarUnidadeDeVendaPrimeiraVez();               
            }

            Cursor.Current = Cursors.Default;    

        }

        private void AtualizarUnidadeDeVenda()
        {
            /*
            Cursor.Current = Cursors.WaitCursor;    

            try
            {


                DataTable dtUniVend=null;
                bool erro = false;
                label1.Text = "Ini: "+DateTime.Now.ToString();

                try
                {
                    erro = true;

                    string  cnx = "Data Source=192.168.10.4;Initial Catalog=GrupoLogos;User ID=sa;Password=@logos09022005$";
                  dtUniVend = Classes.BdExterno.RetornarDT("SELECT CodigoDeBarras, Status FROM ReposicaoRogeEan where status in('I','D')", cnx);
                }
                catch (Exception)
                {
                    erro = true;
                }

                if (erro)
                {
                    ReposicaoInterno.ReposicaoRogeInterno ri = new SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno();
                    dtUniVend = ri.RetornarUnidadeDeVendaClassificado();
                }

                List<string> cmd = new List<string>();
                int contador = 0;
                for (int i = 0; i < dtUniVend.Rows.Count; i++)
                {
                    contador ++;
                    //VERIFICAR SE JA EXISTE NO COLETOR
                    if (dtUniVend.Rows[i]["status"].ToString() == "D")
                    {
                        cmd.Add("DELETE FROM ReposicaoRogeEan WHERE CodigoDeBarras='" + dtUniVend.Rows[i]["CodigoDeBarras"].ToString() + "'");
                    }
                    else
                    {
                        string m = "select count(*) from ReposicaoRogeEan where CodigoDeBarras='" + dtUniVend.Rows[i]["CodigoDeBarras"].ToString() + "' ";
                        string x = Classes.BbColetor.RetornarDataTable(m).Rows[0][0].ToString();
                        
                        if(x=="0")
                            cmd.Add("INSERT INTO ReposicaoRogeEan (CodigoDeBarras) values ('" + dtUniVend.Rows[i]["CodigoDeBarras"].ToString() + "')");
                    }
                    if ((i % 50) == 0 || i == (dtUniVend.Rows.Count-1))
                    {
                        if (cmd.Count > 0)
                        {
                            Classes.BbColetor.excSql_trans(cmd);
                            cmd = new List<string>();
                        }
                    }
                }
                MessageBox.Show("Fim da baixa dos produtos. Total: " + dtUniVend.Rows.Count + " Atualizados: "+ contador);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
            finally
            {
                label2.Text = "Fim: " + DateTime.Now.ToString();

                Cursor.Current = Cursors.Default;    
            }
             * */
        }


        private void AtualizarUnidadeDeVendaPrimeiraVez()
        {
            /*
            Cursor.Current = Cursors.WaitCursor;
            VarGlobal.UltimaConexao = DateTime.Now.ToString();

            try
            {
                DataTable dtUniVend = null;
                bool erro = false;
                label1.Text = "Ini: " + DateTime.Now.ToString();

                try
                {
                    erro = true;
                    

                    //string cnx = "Data Source=192.168.10.4;Initial Catalog=GrupoLogos;User ID=sa;Password=@logos09022005$";
                    //dtUniVend = Classes.BdExterno.RetornarDT("SELECT CodigoDeBarras, Status FROM ReposicaoRogeEan", cnx);
                }
                catch (Exception)
                {
                    erro = true;
                }

                if (erro)
                {
                    ReposicaoInterno.ReposicaoRogeInterno ri = new SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno();
                    dtUniVend = ri.RetornarUnidadeDeVenda();
                }
                List<string> cmd = new List<string>();
                int contador = 0;
                for (int i = 0; i < dtUniVend.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        cmd.Add("delete from ReposicaoRogeEan ");
                        Classes.BbColetor.excSql(cmd);
                        cmd = new List<string>();
                    }

                    cmd.Add("INSERT INTO ReposicaoRogeEan (CodigoDeBarras) values ('" + dtUniVend.Rows[i]["CodigoDeBarras"].ToString() + "')");
                    if ((i % 1000) == 0 || i == (dtUniVend.Rows.Count - 1))
                    {
                        if (cmd.Count > 0)
                        {
                            Classes.BbColetor.excSql(cmd);
                            cmd = new List<string>();
                        }
                    }
                    contador++;
                }
                MessageBox.Show("Fim da baixa dos produtos. Total de Produtos: " + dtUniVend.Rows.Count + " Atualizados: " + contador);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                label2.Text = "Fim: " + DateTime.Now.ToString();
                Cursor.Current = Cursors.Default;
            }
             * */
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
            x = new TreeNode();

            x.Text = "REPOSIÇÃO / FALTAS";
            x.Tag = "-99";
            treeView1.Nodes.Add(x);

            x = new TreeNode();

            x.Text = "CONF. GAIOLA";
            x.Tag = "-98";
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


            
            if (e.Node.Tag.ToString() == "-98")
            {
                ConfGaiolas f = new ConfGaiolas();
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

            try
            {
                if (VarGlobal.UltimaConexao == "")
                {
                    VarGlobal.UltimaConexao = DateTime.Now.ToString();
                    //AtualizarUnidadeDeVenda();
                }
            }
            catch (Exception)
            {
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AtualizarUnidadeDeVenda();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            AtualizarUnidadeDeVenda();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            AtualizarUnidadeDeVendaPrimeiraVez();
        }
    }
}