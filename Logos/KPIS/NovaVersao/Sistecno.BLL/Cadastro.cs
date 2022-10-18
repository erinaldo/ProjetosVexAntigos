using System.Collections.Generic;
using System.Data;
using Sistecno.DAL;

namespace Sistecno.BLL
{
    public class Cadastro
    {
        public void GravarImagens(int idCadastro, int IdCadastroImagem, byte[] imagem, string cnx)
        {
            new DAL.Cadastro().GravarImagens(idCadastro, IdCadastroImagem, imagem, cnx);
        }

        public DataTable RetornarCuringa(string texto, string IdCadastro, string cnx)
        {
          return  new DAL.Cadastro().RetornarCuringa(texto, IdCadastro,  cnx);
        }

        public DataTable Retornar(int idCadastro, string cnx)
        {
            return new DAL.Cadastro().Retornar(idCadastro, cnx);
        }

        public DataTable RetronarClientesColetor(string cnx)
        {
            return new DAL.Cadastro().RetronarClientesColetor(cnx);
        }

        public DAL.Models.Cadastro  PreencherPropriedades(DAL.Models.Cadastro  objOrigem)
        {
            Sistecno.DAL.Models.Cadastro  c = new DAL.Models.Cadastro ();
            c = objOrigem;
            c.IDCadastro = 0;
            return c;
        }

        public int GravarComImagem(DAL.Models.Cadastro  cadastro, DataTable contatos, byte[] LogoTipo, string cnx)
        {
            if (cadastro.IDCadastro == 0)
                cadastro.IDCadastro = new DAL.Cadastro().Gravar(cadastro, contatos, LogoTipo, cnx);
            else
              new DAL.Cadastro().Alterar(cadastro, contatos, LogoTipo, cnx);

            return cadastro.IDCadastro;

        }

        public int Gravar(DAL.Models.Cadastro  cadastro, string cnx)
        {
            if (cadastro.IDCadastro == 0)
                cadastro.IDCadastro = new DAL.Cadastro().Gravar(cadastro, cnx);
            else
                new DAL.Cadastro().Alterar(cadastro, null, null,  cnx);

            return cadastro.IDCadastro;
        }

        public int Gravar(DAL.Models.Cadastro  cadastro, DataTable contatos, DAL.Models.Proprietario proprietario, string cnx)
        {
            if (cadastro.IDCadastro == 0)
                cadastro.IDCadastro = new DAL.Cadastro().Gravar(cadastro, contatos, proprietario, cnx);
            else
                new DAL.Cadastro().Alterar(cadastro, contatos, null, cnx);

            return cadastro.IDCadastro;

        }

        public DAL.Models.Cadastro RetornarTabela(int idMotorista, string cnx)
        {
            return new DAL.Cadastro().RetornarTabela(idMotorista, cnx);
        }

        public DAL.Models.Cadastro  RetornarByCnpj(string cnpj, string cnx)
        {
            return new DAL.Cadastro().RetornarByCnpj(cnpj, cnx);
        }

        public void Inserir(DAL.Models.Cadastro obj, List<DAL.Models.CadastroContatoEndereco> ListEndereco, string caminhoScript, string cnx, string nomeArqXml, string CaminhoENomeSriptGeral, int idPlano, DAL.Email confemail, string rntc)
        {
            //new DAL.Cadastro().Inserir(obj, ListEndereco, cnx, nomeArqXml, CaminhoENomeSriptGeral, idPlano, confemail, rntc);
        }

        public DataTable Retornar(List<ParametrosPesquisa> swhere, string cnx)
        {
            string where = " WHERE 0=0 AND RAZAOSOCIALNOME <>'' ";

            if (swhere != null)
            {
                for (int i = 0; i < swhere.Count; i++)
                {
                    if (swhere[i].Valor.Length > 0)
                    {
                        switch (swhere[i].Tipo)
                        {
                            case "int":
                                where += " AND " + swhere[i].Campo + "=" + swhere[i].Valor;
                                break;

                            case "string":
                                where += " AND " + swhere[i].Campo + " like '" + swhere[i].Valor + "%'";
                                break;
                        }
                    }

                }
            }
            return new DAL.Cadastro().Retornar(where, cnx);
        }

        public DataSet RetornarTodosCampos(int codigo, string cnx/*, string cnpjcpf*/)
        {
            return new DAL.Cadastro().RetornarTodosCampos(codigo, cnx/*, cnpjcpf*/);
        }

        public class Motorista : Cadastro
        {
            public DataTable RetornarTodosCampos(int IdMotorista, string cnx)
            {
                return new DAL.Cadastro.Motorista().RetornarTodosCampos(IdMotorista, cnx);
            }

            public DAL.Models.Motorista RetornarTable(int idCadastro, string cnx)
            {
                return new DAL.Cadastro.Motorista().RetornarTable(idCadastro, cnx);
            }

            public int GravarMotorista(DAL.Models.Cadastro  cadastro, DAL.Models.CadastroComplemento cadastroComplemento, DataTable contatos, Sistecno.DAL.Models.Motorista motorista, string cnx)
            {
                DAL.Cadastro.Motorista DAL = new DAL.Cadastro.Motorista();

                if (motorista.IDMotorista > 0)
                {
                    DAL.AlterarMotorista(cadastro, cadastroComplemento, contatos, motorista, cnx); //alterar
                    return cadastro.IDCadastro;
                }
                else
                    return DAL.GravarMotorista(cadastro, cadastroComplemento, contatos, motorista, cnx);

            }

            public DataTable RetornarMotorista(List<ParametrosPesquisa> swhere, string cnx)
            {
                string where = " WHERE 0=0 AND RAZAOSOCIALNOME <>'' ";

                if (swhere != null)
                {
                    for (int i = 0; i < swhere.Count; i++)
                    {
                        if (swhere[i].Valor.Length > 0)
                        {
                            switch (swhere[i].Tipo)
                            {
                                case "int":
                                    where += " AND " + swhere[i].Campo + "=" + swhere[i].Valor;
                                    break;

                                case "string":
                                    where += " AND " + swhere[i].Campo + " like '" + swhere[i].Valor + "%'";
                                    break;
                            }
                        }

                    }
                }
                return new DAL.Cadastro.Motorista().RetornarMotorista(where, cnx);
            }
        }

        public class TipoDeContato
        {
            public DataTable Retornar(string cnx)
            {
                try
                {
                    return new DAL.Cadastro.TipoDeContato().Retornar(cnx);

                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }

        public class ContatoEndereco
        {

            public DataTable RetornarByIdCasdastro(int idCadastro, string cnx)
            {
                return new DAL.Cadastro.ContatoEndereco().RetornarByIdCasdastro(idCadastro, cnx);
            }

            public DataTable RetornarByChave(string chaves, string cnx)
            {
                DataTable d = new DAL.Cadastro.ContatoEndereco().RetornarEmailsByChave(chaves, cnx);
                DataTable dt = new DataTable();
                dt.Columns.Add("NOME");
                dt.Columns.Add("ENDERECO");

                for (int iprincipal = 0; iprincipal < d.Rows.Count; iprincipal++)
                {



                    for (int i = 0; i < d.Columns.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (d.Rows[iprincipal][i + 1].ToString().Length > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = d.Rows[iprincipal][i].ToString();
                                dr[1] = d.Rows[iprincipal][i + 1].ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
                return dt;

            }
        }

        public class CadadastroComplemento
        {
            public DAL.Models.CadastroComplemento RetornarTabela(int idCadastro, string cnx)
            {
                return new DAL.Cadastro.CadadastroComplemento().RetornarTabela(idCadastro, cnx);
            }
        }

        public class Proprietario
        {
            public void MotoristaToProprietario(int idMotorista, bool p, string cnx)
            {
                new DAL.Cadastro.Proprietario().MotoristaToProprietario(idMotorista, p, cnx);
            }

            public int Inserir(int idCadastro, string cnx)
            {
                return new DAL.Cadastro.Proprietario().Inserir(idCadastro, cnx);
            }

            public DataTable Retornar(List<ParametrosPesquisa> swhere, string cnx)
            {
                string where = " WHERE 0=0 AND RAZAOSOCIALNOME <>'' ";

                if (swhere != null)
                {
                    for (int i = 0; i < swhere.Count; i++)
                    {
                        if (swhere[i].Valor.Length > 0)
                        {
                            switch (swhere[i].Tipo)
                            {
                                case "int":
                                    where += " AND " + swhere[i].Campo + "=" + swhere[i].Valor;
                                    break;

                                case "string":
                                    where += " AND " + swhere[i].Campo + " like '" + swhere[i].Valor + "%'";
                                    break;
                            }
                        }

                    }
                }
                return new DAL.Cadastro.Proprietario().Retornar(where, cnx);
            }

        }

        public class Trasnportadora
        {
            public DataTable RetornaDadosTranspotadora(string cnpj, string cnx)
            {
                return new DAL.Cadastro.Trasnportadora().RetornaDadosTranspotadora(cnpj, cnx);
            }
        }

        public class Agregado
        {
            public DataTable RetornaDadosAgregado(string cpf, string cnx)
            {
                return new DAL.Cadastro.Agregado().RetornaDadosAgregado(cpf, cnx);

            }
        }
    }
}