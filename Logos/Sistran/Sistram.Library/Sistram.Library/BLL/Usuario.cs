using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace SistranBLL
{
    public sealed class Usuario
    {

        public static string GetStringNoAccents(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };


            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }


            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", "'" };


            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }


            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  " " **/
            str = str.Replace("\\s+", " ");
            return str;
        }


        public static List<SistranMODEL.Usuario> Login(string usuariologin, string usuarioSenha, string Conn, bool intranet)
        {
            try
            {
                if (string.IsNullOrEmpty(usuariologin) || string.IsNullOrEmpty(usuarioSenha))
                {
                    throw new Exception("Por favor Preencha o usuário e a senha");
                    //erro -  retornar mensagem para preencher usuario e senha
                }
                else
                {
                    //faz a busca do nome do banco no xml - 
                    List<SistranMODEL.Usuario> ILusuario = SistranDAO.Usuario.RetornaUsuarioLogin_Alone(GetStringNoAccents(usuariologin), GetStringNoAccents(usuarioSenha), Conn, intranet);

                    if (ILusuario.Count > 0)
                    {
                        return ILusuario;
                    }
                    else
                    {
                        throw new Exception("Nome de usuário ou senha inválido. Tente novamente. O sistema diferencia letras maiúsculas de minúsculas.");
                        //erro -  retornar mensagem para preencher usuario e senha
                    }
                }
               
            }
            catch(Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public string ConsultarLoginByEmail(string email)
        {
            return new SistranDAO.Usuario().ConsultarLoginByEmail(email);
        }

        #region Funcoes Novas

        public static List<SistranMODEL.Usuario> Login_Alone(string usuariologin, string usuarioSenha, string Conn, bool intranet)
        {
            try
            {
                if (string.IsNullOrEmpty(usuariologin) || string.IsNullOrEmpty(usuarioSenha))
                {
                    throw new Exception("Por favor Preencha o usuário e a senha");
                    //erro -  retornar mensagem para preencher usuario e senha
                }
                else
                {
                    //faz a busca do nome do banco no xml - 
                    List<SistranMODEL.Usuario> ILusuario = SistranDAO.Usuario.RetornaUsuarioLogin_Alone(GetStringNoAccents(usuariologin), GetStringNoAccents(usuarioSenha), Conn, intranet);

                    if (ILusuario.Count > 0)
                    {
                        return ILusuario;
                    }
                    else
                    {
                        throw new Exception("Nome de usuário ou senha inválido. Tente novamente. O sistema diferencia letras maiúsculas de minúsculas.");
                        //erro -  retornar mensagem para preencher usuario e senha
                    }
                }

            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public DataTable Listar(string login, string nome, string cpf)
        {
            return new SistranDAO.Usuario().Listar(login, nome, cpf);
        }

        public DataTable ListarIntranet(string login, string nome, string cpf)
        {
            return new SistranDAO.Usuario().ListarIntranet(login, nome, cpf);
        }
        
        public DataTable Listar(string login, string nome)
        {
            return new SistranDAO.Usuario().Listar(login, nome);
            
        }

        public DataTable ListarPerfil(string s)
        {
            return new SistranDAO.Usuario().ListarPerfil(s);
        }

        public DataTable Consultar(string idUsuario)
        {
            return new SistranDAO.Usuario().Consultar(idUsuario);
        }

        public DataTable ConsultarIntranet(string idUsuario)
        {
            return new SistranDAO.Usuario().ConsultarIntranet(idUsuario);
        }

        public void HabDesbUusario(string IdCadastro, string ACAO)
        {
            SistranDAO.Usuario o = new SistranDAO.Usuario();
            o.HabDesbUusario(IdCadastro,ACAO);
        }

        public void HabDesbPeril(string IdUsuario, string ACAO)
        {
            SistranDAO.Usuario o = new SistranDAO.Usuario();
            o.HabDesbPeril(IdUsuario, ACAO);
        }

        public DataTable ConsultarPerfil(string idUsuario)
        {
            return new SistranDAO.Usuario().ConsultarPerfil(idUsuario);
        }

        //retorna arrray com idcadstro e IdUsuario 
        public string[] GravarUsuarios(string IdCadastro, string IdUsuario, string CpfCnpj, string Nome, string Login, string email, string senha, ListBox Divisoes, string Perfil)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)HttpContext.Current.Session["USUARIO"];
            string[] dados = new string[2];

            if (IdUsuario == "")
            {
                // novo usuario
                //inclui na table cadastro
                string c = "";
                if (CpfCnpj.Trim().Length == 14 || CpfCnpj.Trim().Length == 18)
                {
                    c = CpfCnpj;
                }
                else
                {
                    c = "0";
                }

                //if (IdCadastro == "" || IdCadastro == "0")
                    IdCadastro = new SistranBLL.Cadastro().InserirCadastroUsuario(c, Nome).ToString();

                if (IdUsuario == "" || IdUsuario == "0")
                    IdUsuario = new SistranDAO.Usuario().Incluir(IdCadastro.ToString(), Nome, Login, senha, Perfil).ToString();

                int IDCadastroContato = new SistranBLL.Cadastro.CadastroContato().Inserir(IdCadastro, ILusuario[0].UsuarioId.ToString());
                int IDCadastroContatoEndereco = new SistranBLL.Cadastro.CadastroContato.CadastroContatoEndereco().Inserir(IdCadastro, email.ToLower());

                UsuarioCliente oU = new UsuarioCliente();
                int IDUsuarioCliente = oU.Inserir(IdUsuario, HttpContext.Current.Session["IDEmpresa"].ToString());

                // insere na UsuarioClienteDivisao
                if (Divisoes.Items.Count > 0)
                {
                    SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao oUCD = new SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao();
                    oUCD.DeletarByIdUsuario(IdUsuario);
                    for (int i = 0; i < Divisoes.Items.Count; i++)
                    {
                        oUCD.Inserir(IDUsuarioCliente.ToString(), Divisoes.Items[i].Value);
                    }

                }

                dados[0] = IdCadastro.ToString();
                dados[1] = IdUsuario.ToString();
                return dados;
            }
            else
            {
                SistranDAO.Usuario ou = new SistranDAO.Usuario();

                int IDCadastroContato = new SistranBLL.Cadastro.CadastroContato().Inserir(IdCadastro, ILusuario[0].UsuarioId.ToString());
                int IDCadastroContatoEndereco = new SistranBLL.Cadastro.CadastroContato.CadastroContatoEndereco().Inserir(IdCadastro, email.ToLower());

                ou.Alterar(IdUsuario, Nome.ToUpper(), Login.ToUpper(), senha.ToUpper(), CpfCnpj, IdCadastro.ToString(), Perfil);

                UsuarioCliente oUsuarioCliente = new UsuarioCliente();
                int IDUsuarioClientes = oUsuarioCliente.Inserir(IdUsuario, HttpContext.Current.Session["IDEmpresa"].ToString());


                // insere na UsuarioClienteDivisao
                if (Divisoes.Items.Count > 0)
                {
                    SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao oUCD = new SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao();
                    int IDUsuarioCliente = new SistranDAO.Usuario.UsuarioCliente().Consultar(IdUsuario, HttpContext.Current.Session["IDEmpresa"].ToString());

                    oUCD.DeletarByIdUsuario(IdUsuario);
                    for (int i = 0; i < Divisoes.Items.Count; i++)
                    {
                        oUCD.Inserir(IDUsuarioCliente.ToString(), Divisoes.Items[i].Value);
                    }

                }
            }

            return dados;
        }

        public string[] GravarUsuariosIntranet(string IdCadastro, string IdUsuario, string CpfCnpj, string Nome, string Login, string email, string senha)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)HttpContext.Current.Session["USUARIO"];
            string[] dados = new string[2];

            if (IdUsuario == "")
            {
                // novo usuario
                //inclui na table cadastro
                string c = "";
                if (CpfCnpj.Trim().Length == 14 || CpfCnpj.Trim().Length == 18)
                {
                    c = CpfCnpj;
                }
                else
                {
                    c = "0";
                }

                //if (IdCadastro == "" || IdCadastro == "0")
                IdCadastro = new SistranBLL.Cadastro().InserirCadastroUsuario(c, Nome).ToString();

                //if (IdUsuario == "" || IdUsuario == "0")
                //    IdUsuario = new SistranDAO.Usuario().Incluir(IdCadastro.ToString(), Nome, Login, senha, Perfil).ToString();

                //int IDCadastroContato = new SistranBLL.Cadastro.CadastroContato().Inserir(IdCadastro, ILusuario[0].UsuarioId.ToString());
                //int IDCadastroContatoEndereco = new SistranBLL.Cadastro.CadastroContato.CadastroContatoEndereco().Inserir(IdCadastro, email.ToLower());

                UsuarioCliente oU = new UsuarioCliente();
                int IDUsuarioCliente = oU.Inserir(IdUsuario, HttpContext.Current.Session["IDEmpresa"].ToString());

                // insere na UsuarioClienteDivisao
                //if (Divisoes.Items.Count > 0)
                //{
                //    SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao oUCD = new SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao();
                //    oUCD.DeletarByIdUsuario(IdUsuario);
                //    for (int i = 0; i < Divisoes.Items.Count; i++)
                //    {
                //        oUCD.Inserir(IDUsuarioCliente.ToString(), Divisoes.Items[i].Value);
                //    }

                //}

                dados[0] = IdCadastro.ToString();
                dados[1] = IdUsuario.ToString();
                return dados;
            }
            else
            {
                SistranDAO.Usuario ou = new SistranDAO.Usuario();

               // int IDCadastroContato = new SistranBLL.Cadastro.CadastroContato().Inserir(IdCadastro, ILusuario[0].UsuarioId.ToString());
               // int IDCadastroContatoEndereco = new SistranBLL.Cadastro.CadastroContato.CadastroContatoEndereco().Inserir(IdCadastro, email.ToLower());

              //  ou.AlterarIntranet(IdUsuario, Nome.ToUpper(), Login.ToUpper(), senha.ToUpper(), CpfCnpj, IdCadastro.ToString());

                //UsuarioCliente oUsuarioCliente = new UsuarioCliente();
                //int IDUsuarioClientes = oUsuarioCliente.Inserir(IdUsuario, HttpContext.Current.Session["IDEmpresa"].ToString());


                // insere na UsuarioClienteDivisao
                //if (Divisoes.Items.Count > 0)
                //{
                //    SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao oUCD = new SistranDAO.Usuario.UsuarioCliente.UsuarioClienteDivisao();
                //    int IDUsuarioCliente = new SistranDAO.Usuario.UsuarioCliente().Consultar(IdUsuario, HttpContext.Current.Session["IDEmpresa"].ToString());

                //    oUCD.DeletarByIdUsuario(IdUsuario);
                //    for (int i = 0; i < Divisoes.Items.Count; i++)
                //    {
                //        oUCD.Inserir(IDUsuarioCliente.ToString(), Divisoes.Items[i].Value);
                //    }

                //}
            }

            return dados;
        }

        public int GravarPerfil(string idUsuario, string Nome)
        {
            SistranDAO.Usuario o = new SistranDAO.Usuario();

            if (idUsuario == "novo")
            {
              return  o.inserirPerfil(Nome);    
            }
            else
            {
                o.AlterarPerfil(idUsuario, Nome);
                return Convert.ToInt32(idUsuario);
            }
        }


        #endregion


        public sealed class LogBDBLL
        {
            public static void GravarLog(int idUser, string login, string acao, string pagina, string conn)
            {
                SistranDAO.Usuario.LogBDDAO.GravarLog(idUser, login, acao, pagina, conn);
            }
        }

        public class UsuarioCliente
        {
            public int Inserir(string IdUsuario, string IdCliente)
            {
                SistranDAO.Usuario.UsuarioCliente oUser = new SistranDAO.Usuario.UsuarioCliente();
                return oUser.Inserir(IdUsuario, IdCliente);
            }
        }
    }
}
