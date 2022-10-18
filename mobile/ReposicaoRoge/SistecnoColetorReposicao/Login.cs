using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;

namespace SistecnoColetor
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (txtLogin.Text == "" || txtSenha.Text == "")
                {
                    throw new Exception("Informe Usuário e Senha.");
                }

                string cnx = "";
                Classes.DTO.Usuario usu = new SistecnoColetor.Classes.DTO.Usuario();
                usu.Login = txtLogin.Text;
                usu.Senha = txtSenha.Text;
                Classes.DTO.Usuario ret=null;

                if (txtEmpresa.Text == "2") //logos
                {

                    //if (new Classes.WS().InternetAtiva() != "SIM")
                    //    throw new Exception("Verifique as configurações de conexão e se a internet esta OK!!!");

                    cnx = new Classes.WS().StringConexao(txtEmpresa.Text);
                    wsUsuarioEmpresa.Usuario user = new wsUsuarioEmpresa.Usuario();
                    wsUsuarioEmpresa.Usuario userRet = new wsUsuarioEmpresa.Usuario();


                    user.Login = txtLogin.Text;
                    user.Senha = txtSenha.Text;
                    


                    userRet = new wsUsuarioEmpresa.wssLogin().Logar(user);

                    if (userRet != null && userRet.IDUsuario>0)
                    {
                        if (userRet.Ativo == "NAO")
                            throw new Exception("Usuario Inativo");


                        ret = new SistecnoColetor.Classes.DTO.Usuario();

                        ret.IDUsuario = userRet.IDUsuario;
                        ret.IDCadastro = userRet.IDCadastro;
                        ret.IDGrupo = (int?)userRet.IDGrupo;
                        ret.IDPerfil =  (int?)userRet.IDPerfil;
                        ret.UltimaEmpresa =  (int?)userRet.UltimaEmpresa;
                        ret.UltimaFilial =  (int?)userRet.UltimaFilial;
                        ret.Nome = userRet.Nome;
                        ret.Login = userRet.Login;
                        ret.Senha = userRet.Senha;
                        ret.TipoDeSistema = userRet.TipoDeSistema;
                        ret.NomeEmpresa = userRet.NomeEmpresa;
                        ret.NomeFilial = userRet.NomeFilial;
                        ret.Ativo = "SIM";
                        ret.EmpresaClienteLogado = 2;
                    }


                }


                //if (cnx == "")
                //{
                //    MessageBox.Show("Banco de Dados Não Encontrado");
                //    return;
                //}

                

                if (ret == null)
                    throw new Exception("Usuário ou senha inválido");


                //seta a variavel global
                VarGlobal.Conexao = cnx;
                VarGlobal.Usuario = ret;
                VarGlobal.iip = "";
                VarGlobal.iiporta = "";
                VarGlobal.UltimaConexao = "";
                VarGlobal.EmeiColetor = "";
                VarGlobal.numeroColetor = "";

                if (txtEmpresa.Text == "2")
                    AlimentarTabelasEmpresaFilial();


                string sql = "Select * from Conf";
                DataTable dt = Classes.BbColetor.RetornarDataTable(sql);

                if (dt.Rows.Count > 0)
                {
                    VarGlobal.EmeiColetor = dt.Rows[0]["EMEI"].ToString();
                    VarGlobal.numeroColetor = dt.Rows[0]["NUMERO"].ToString();
                }



                Cursor.Current = Cursors.Default;
                Menu f = new Menu(ret, cnx);
                //MessageBox.Show("Ter");
                f.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                if (ex.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED"))
                    MessageBox.Show("Verifique a Conexao de Internet.");
                else
                {
                    if (ex.Message.ToUpper().Contains("SQL"))
                        MessageBox.Show("Verifique os dados da Conexao");
                    else if (ex.Message.ToString().ToUpper().Contains("USUÁRIO NÃO ENCONTRADO"))
                    {
                        MessageBox.Show("USUÁRIO NÃO ENCONTRADO");
                    }
                    else
                        MessageBox.Show("Verifique a Conexão de Internet." + ex.Message);
                }
            }
        }

        private void AlimentarTabelasEmpresaFilial()
        {
            try
            {

                DataTable dtaux = Classes.BbColetor.RetornarDataTable("SELECT COUNT(*) FROM EMPRESA");

                if (dtaux.Rows[0][0].ToString() == "0")
                {
                    DataTable dt = new wsUsuarioEmpresa.wssLogin().RetornarEmpresa();

                    string sql = "";
                    List<string> lsql = new List<string>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql = "insert into Empresa values (" + dt.Rows[i]["IDEMPRESA"].ToString() + ", '" + dt.Rows[i]["Nome"].ToString() + "'); ";
                        lsql.Add(sql);
                    }

                    DataTable dtFil = new wsUsuarioEmpresa.wssLogin().RetornarFilial();

                    for (int i = 0; i < dtFil.Rows.Count; i++)
                    {
                        lsql.Add("INSERT INTO Filial VALUES (" + dtFil.Rows[i]["IDFILIAL"].ToString() + ",  '" + dtFil.Rows[i]["NOME"].ToString() + "' , " + dtFil.Rows[i]["IDEMPRESA"].ToString() + " )");
                    }

                    if (lsql.Count > 0)
                        Classes.BbColetor.excSql(lsql);


                }
            }
            catch (Exception ex)
            {
                string ret = ex.Message;
            }
        }

        private void textBox2_GotFocus(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", textBox1.Text);
        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            if (VarGlobal.iip !=null && VarGlobal.iip != "")
            {
                //txtPorta.Text = VarGlobal.iiporta;
                //txtIp.Text = VarGlobal.iip;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}