using System.Data;

namespace SistranBLL
{
    public class Localizacao
    {
        public class Cidade
        {
            public DataTable Read(int IdCidade)
            {
                return new SistranDAO.Localizacao.Cidade().Read(IdCidade);
            }

            public DataTable ReadbyIdEstado(int IdEstado)
            {
                return new SistranDAO.Localizacao.Cidade().ReadbyIdEstado(IdEstado);
            }
        }

        public class Estado
        {
            public DataTable Read(int IdEstado)
            {
                return new SistranDAO.Localizacao.Estado().Read(IdEstado);
            }

            public DataTable Listar()
            {
                return new SistranDAO.Localizacao.Estado().Listar();
            }
        }

        public class Bairro
        {
            public DataTable Read(int idCidade)
            {
                return new SistranDAO.Localizacao.Bairro().Read(idCidade);
            }

        }
    }
}
