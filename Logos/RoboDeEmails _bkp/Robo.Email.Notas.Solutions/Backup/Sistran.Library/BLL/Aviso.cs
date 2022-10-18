using System.Data;

namespace SistranBLL
{
    public sealed class Aviso
    {
        public void ApagarAviso(string IdAviso)
        {
            new SistranDAO.Aviso().ApagarAviso(IdAviso);
        }

        public DataTable Listar(string Nome, string Login, string Operacao, string IdAviso)
        {
            return new SistranDAO.Aviso().Listar(Nome, Login, Operacao, IdAviso);
        }

        public int VerificarDuplicidade(string idUsuario, string idClienteDivisao, string Operacao, string IdAviso)
        {
            return new SistranDAO.Aviso().VerificarDuplicidade(idUsuario, idClienteDivisao, Operacao, IdAviso);
        }

        public int Inserir(string Operacao, string IdClienteDivisao, string IdCanalDeVenda, string IdUsuario)
        {            
            return new SistranDAO.Aviso().Inserir(Operacao, IdClienteDivisao, IdCanalDeVenda, IdUsuario);
        }

        public void Alterar(string Operacao, string IdClienteDivisao, string IdCanalDeVenda, string IdUsuario, string idAviso)
        {
            new SistranDAO.Aviso().Alterar(Operacao, IdClienteDivisao, IdCanalDeVenda, IdUsuario, idAviso);
        }

        public sealed class CanalDeVenda
        {
            public DataTable Listar()
            {
                return new SistranDAO.Aviso.CanalDeVenda().Listar();
            }
        }
    }
}