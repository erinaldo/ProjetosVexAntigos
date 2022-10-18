using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System;

namespace SistranBLL
{
    public class Cadastro
    {
        public DataTable Read(string Condicoes)
        {
            return new SistranDAO.Cadastro().Read(Condicoes);
        }

        public int TransacaoInserirCadastroMotorista(string Conn, SistranMODEL.Cadastro oCad, SistranMODEL.Cadastro.Motorista oMot, SistranMODEL.Cadastro.Motorista.MotoristaHistorico oMotHst)
        {
            return new SistranDAO.Cadastro().TransacaoInserirCadastroMotorista(Conn, oCad, oMot, oMotHst);
        }

        public int TransacaoInserirCadastroProprietario(string Conn,SistranMODEL.Cadastro oCad,SistranMODEL.Cadastro.Proprietario oProp)
        {
            return new SistranDAO.Cadastro().TransacaoInserirCadastroProprietario(Conn, oCad, oProp);
        }

        public int TransacaoInserirCadastroTranportadora(string Conn,  SistranMODEL.Cadastro oCad, SistranMODEL.Cadastro.Tranportadora oTrans)
        {
            return new SistranDAO.Cadastro().TransacaoInserirCadastroTrasnportadora(Conn, oCad, oTrans);
        }

        public int Inserir(string CNPJ, string InscricaoRG, string Razao, string FantasiaApelido, string Endereco, string Numero, string Complemento, string IDCidade, string IDBairro, string CEP)
        {
            return new SistranDAO.Cadastro().Inserir(CNPJ, InscricaoRG, Razao, FantasiaApelido, Endereco, Numero, Complemento, IDCidade, IDBairro, CEP);
        }

        public void Alterar(string CNPJ, string InscricaoRG, string Razao, string FantasiaApelido, string Endereco, string Numero, string Complemento, string IDCidade, string IDBairro, string CEP, string IdCadastro)
        {
            SistranDAO.Cadastro O = new SistranDAO.Cadastro();
            O.Alterar(CNPJ, InscricaoRG, Razao, FantasiaApelido, Endereco, Numero, Complemento, IDCidade, IDBairro, CEP, IdCadastro);
        }

        public int InserirCadastroUsuario(string CNPJ, string nome)
        {
            return new SistranDAO.Cadastro().InserirCadastroUsuario(CNPJ, nome);
        }
        
        public class CadastroComplemento
        {
            public int inserir(SistranMODEL.Cadastro.CadastroComplemento oCadCompl)
            {
                return new SistranDAO.Cadastro.CadastroComplemento().inserir(oCadCompl);
            }

            public DataTable readByIdCadastro(int IdCadastro)
            {
                return new SistranDAO.Cadastro.CadastroComplemento().readByIdCadastro(IdCadastro);
            }
        }

        public sealed class CadastroContato
        {
            public int Inserir(string IDCADASTRO, string IDCONTATO)
            {
                return new SistranDAO.Cadastro.CadastroContato().Inserir(IDCADASTRO, IDCONTATO);
            }

            public DataTable ListarEmailsAprovadoresPedidos()
            {
                return new SistranDAO.Cadastro.CadastroContato().ListarEmailsAprovadoresPedidos();
            }

            public sealed class CadastroContatoEndereco
            {
                public int Inserir(string IDCADASTRO, string email)
                {
                    return new SistranDAO.Cadastro.CadastroContato.CadastroContatoEndereco().Inserir(IDCADASTRO, email);
                }

                public DataTable RetornarTelefone(int IdCadastro)
                {
                    return new SistranDAO.Cadastro.CadastroContato.CadastroContatoEndereco().RetornarTelefone(IdCadastro);
                }

                public DataTable TipoDeTelefome()
                {
                    return new SistranDAO.Cadastro.CadastroContato.CadastroContatoEndereco().TipoDeTelefome();
                }

            }
        }

        public class CadastroReferencia
        {
            public DataTable Listar(int idCadastro)
            {
                return new SistranDAO.Cadastro.CadastroReferencia().Listar(idCadastro);
            }
        }

        public class Motorista
        {
            public DataTable PesquisarLiberacao(string nome, string cpf, string ativo, string inativo, int? IdMotorista, string liberado, string naoLiberado, bool vencida, bool AguardandoLiberacao)
            {
                return new SistranDAO.Cadastro.Motorista().PesquisarLiberacao(nome, cpf, ativo, inativo, IdMotorista, liberado, naoLiberado, vencida,  AguardandoLiberacao);
            }

            public DataTable Pesquisar(string nome, string cpf, string ativo, string inativo, int? IdMotorista, string liberado, string naoLiberado, bool vencida)
            {
                return new SistranDAO.Cadastro.Motorista().Pesquisar(nome, cpf, ativo, inativo, IdMotorista, liberado, naoLiberado, vencida);
            }

            public DataTable Pesquisar(string nome, string cpf, string ativo, string inativo, int? IdMotorista, string liberado, string naoLiberado, bool vencida,
                                       string campoGerenciadora, DateTime? gerInicio, DateTime? gerFim, string tipoFavorecido, string valorFavorecido, 
                                       string contratado, int codFilial, DateTime? dataBloqeioIni, DateTime? dataBloqueioFim)
            {
                return new SistranDAO.Cadastro.Motorista().Pesquisar(nome, cpf, ativo, inativo, IdMotorista, liberado, naoLiberado, vencida, campoGerenciadora, gerInicio, gerFim, tipoFavorecido, valorFavorecido, contratado, codFilial, dataBloqeioIni, dataBloqueioFim);
            }

            public DataTable CarregarReportMotorista(bool ativo, bool liberado, bool habilitacaoVencida)
            {
                return new SistranDAO.Cadastro.Motorista().CarregarReportMotorista(ativo, liberado, habilitacaoVencida);
            }

            public DataTable Pesquisar(string cpf)
            {
                return new SistranDAO.Cadastro.Motorista().Pesquisar(cpf);
            }

            public void alterar(SistranMODEL.Cadastro.Motorista oMot, int idCadastro)
            {
                new SistranDAO.Cadastro.Motorista().alterar(oMot, idCadastro);
            }

            public void AlterarLiberacao(string IdMotorista, string Liberado)
            {
                new SistranDAO.Cadastro.Motorista().AlterarLiberacao(IdMotorista, Liberado);
            }

            public class MotoristaHistorico
            {
                public List<SistranMODEL.Cadastro.Motorista.MotoristaHistorico> RetornarHistorico(int IDMotorista)
                {
                    return new SistranDAO.Cadastro.Motorista.MotoristaHistorico().RetornarHistorico(IDMotorista);
                }

                public void inserir(SistranMODEL.Cadastro.Motorista.MotoristaHistorico oMotHst)
                {
                    new SistranDAO.Cadastro.Motorista.MotoristaHistorico().inserir(oMotHst);
                }
            }

            public class MotoristaFilial
            {
                public void Inserir(int idMotorista, ListBox ListDeFiliais)
                {
                    new SistranDAO.Cadastro.Motorista.MotoristaFilial().Inserir(idMotorista, ListDeFiliais);
                }
            }
        }

        public class Proprietario
        {
            public DataTable Pesquisar(string nome, string cpf, int? IdProprietario)
            {
                return new SistranDAO.Cadastro.Proprietario().Pesquisar(nome, cpf, IdProprietario);
            }

            public DataTable Pesquisar(string cpf)
            {
                return new SistranDAO.Cadastro.Proprietario().Pesquisar(cpf);
            }

            public DataTable CarregarReportProprietario()
            {
                return new SistranDAO.Cadastro.Proprietario().CarregarReportProprietario();
            }

            public void GravarTelefonesProprietario(SistranMODEL.Cadastro.Proprietario o)
            {
                new SistranDAO.Cadastro.Proprietario().GravarTelefonesProprietario(o);
            }
        }

        public class Transportadora
        {
            public DataTable Pesquisar(int? idTransportadora, string cpf, string nome)
            {
                return new SistranDAO.Cadastro.Transportadora().Pesquisar(idTransportadora, cpf, nome);
            }

            public DataTable CarregarCboCC()
            {
                return new SistranDAO.Cadastro.Transportadora().CarregarCboCC();
            }
        }

        
    }
}