using System.Data;

namespace SistranBLL
{
    public sealed class Cliente
    {

        public static DataTable RetornarEmAbertoAnteriores(string con)
        {
            return SistranDAO.Cliente.RetornarEmAbertoAnteriores(con);

        }

        public static DataTable RetornarListaClienteFaturamento(bool considerarFilial)
        {
            return SistranDAO.Cliente.RetornarListaClienteFaturamento(considerarFilial);
        }

        public static DataTable RetornarListaClienteFaturamento()
        {
            return SistranDAO.Cliente.RetornarListaClienteFaturamento();
        }


        public static DataTable GerarDiasClienteFaturamento()
        {
            return SistranDAO.Cliente.GerarDiasClienteFaturamento();
        }

        public static DataTable GerarMesesClienteFaturamento()
        {
            return SistranDAO.Cliente.GerarMesesClienteFaturamento();
        }



        public static DataTable RetornarListaClienteFaturamento(bool considerarFilial,string con)
        {
            return SistranDAO.Cliente.RetornarListaClienteFaturamento(considerarFilial, con);
        }


        public static DataTable GerarDiasClienteFaturamento(string con)
        {
            return SistranDAO.Cliente.GerarDiasClienteFaturamento(con);
        }

        public static DataTable GerarMesesClienteFaturamento(string con)
        {
            return SistranDAO.Cliente.GerarMesesClienteFaturamento(con);
        }

        public static SistranMODEL.Cliente RetornaCliente(int clienteIdCadastro, string Conn)
        {
            SistranMODEL.Cliente Cliente = new SistranMODEL.Cliente();
            Cliente = SistranDAO.Cliente.RetornaCliente(clienteIdCadastro, Conn);
            return Cliente;
        }

        public static DataTable ReadCadastro(int idCliente)
        {
            return SistranDAO.Cliente.ReadCadastro(idCliente);
        }

        public static string RetornaDivisoesClientes(string idUsuario, string idCliente)
        {
            return SistranDAO.Cliente.RetornaDivisoesClientes(idUsuario, idCliente);
        }

        public static DataTable DivisoesCompleta(string idCliente)
        {
            return SistranDAO.Cliente.DivisoesCompleta(idCliente);
        }

        public static DataTable Read(int idCliente)
        {
            return SistranDAO.Cliente.Read(idCliente);
        }

        public static string ReadCNPJByIdCliente(int IdCliente)
        {
            return SistranDAO.Cliente.ReadCNPJByIdCliente(IdCliente);
        }

        public DataTable RetornarClientesIntranet(string iniciais, string codigos)
        {
            return new SistranDAO.Cliente().RetornarClientesIntranet(iniciais, codigos);
        }

        public DataTable RetornarClientasRelacionados(string CodigoDoCliente)
        {
            return new SistranDAO.Cliente().RetornarClientesRelacionados(CodigoDoCliente);
        }

        public DataTable RetornarClientesUsuariosPelasIniciais(string Iniciais)
        {
            return new SistranDAO.Cliente().RetornarClientesUsuariosPelasIniciais(Iniciais);

        }

        public sealed class Divisao
        {
            public DataTable RetornarPais(int idCliente)
            {
                return new SistranDAO.Cliente.Divisao().RetornarPais(idCliente);
            }

            public DataTable RetornarFlihos(int idCliente, int IdParente)
            {
                return new SistranDAO.Cliente.Divisao().RetornarFlihos(idCliente, IdParente);
            }

            public void InserirClienteDivisao(string idCliente, string nome, string IdParente)
            {
                SistranDAO.Cliente.Divisao cliDiv = new SistranDAO.Cliente.Divisao();
                cliDiv.InserirClienteDivisao(idCliente, nome, IdParente);
            }

            public void AlterarNomeClienteDivisao(string IDCLIENTEDIVISAO, string nome)
            {
                SistranDAO.Cliente.Divisao cliDiv = new SistranDAO.Cliente.Divisao();
                cliDiv.AlterarNomeClienteDivisao(IDCLIENTEDIVISAO, nome);
            }

            public void DeletarClienteDivisao(string IDCLIENTEDIVISAO)
            {
                SistranDAO.Cliente.Divisao cliDiv = new SistranDAO.Cliente.Divisao();
                cliDiv.DeletarClienteDivisao(IDCLIENTEDIVISAO);
            }

            public DataTable RetornarListaDivisoesProduto(string CodigoProduto)
            {
                return new SistranDAO.Cliente.Divisao().RetornarListaDivisoesProduto(CodigoProduto);
            }

            public void ApagarEstoqueDivisaoByCodigoProdutoAndIDClienteDivisao(string CdProduto, string IDClienteDivisao)
            {
                SistranDAO.Cliente.Divisao cliDiv = new SistranDAO.Cliente.Divisao();
                cliDiv.ApagarEstoqueDivisaoByCodigoProdutoAndIDClienteDivisao(CdProduto, IDClienteDivisao);
            }

            public void DesabilitarEstoqueDivisao(string CdProduto)
            {
                new SistranDAO.Cliente.Divisao().DesabilitarEstoqueDivisao(CdProduto);
            }

            public void InserirEstoqueDivisao(string CdProduto, string IDClienteDivisao)
            {
                SistranDAO.Cliente.Divisao cliDiv = new SistranDAO.Cliente.Divisao();
                cliDiv.InserirEstoqueDivisao(CdProduto, IDClienteDivisao);
            }

            public DataTable ListarDivisoesCadastradasUser(string IdUsuario, string IdCliente)
            {
                return new SistranDAO.Cliente.Divisao().ListarDivisoesCadastradasUser(IdUsuario, IdCliente);
            }

        }
    }
}