using System.Data;

namespace Sistecno.BLL
{
    public class Veiculo
    {
        public DataTable RetornarVeicPropMotorista(string placa, string cnx)
        {
            return new Sistecno.DAL.Veiculo().RetornarVeicPropMotorista(placa, cnx);
        }

        public DataTable Retornar(int? idVeiculo, string Placa, string NomeMotorista, string NomeProprietario, string cnx)
        {
            return new Sistecno.DAL.Veiculo().Retornar(idVeiculo, Placa,NomeMotorista, NomeProprietario, cnx);
        }

        public void GravarVeiculo(Sistecno.DAL.Models.Veiculo veic, string cnx)
        {
            new Sistecno.DAL.Veiculo().GravarVeiculo(veic, cnx);
        }

        public Sistecno.DAL.Models.Veiculo RetornarbyPlaca(string placa, string cnx)
        {
            return new Sistecno.DAL.Veiculo().RetornarbyPlaca(placa, cnx);
        }

        public Sistecno.DAL.Models.Veiculo RetornarAllFields(int idVeiculo, string cnx)
        {
            return new Sistecno.DAL.Veiculo().RetornarAllFields(idVeiculo, cnx);
        }
        public class Modelo
        {

            public DataTable Retornar(string cnx)
            {
                return new Sistecno.DAL.Veiculo.Modelo().Retornar(cnx);
            }
        }

        public class Tipo
        {

            public DataTable Retornar(string cnx)
            {
                return new Sistecno.DAL.Veiculo.Tipo().Retornar(cnx);
            }
        }

        public class Rastreador
        {

            public DataTable Retornar(string cnx)
            {
                return new Sistecno.DAL.Veiculo.Rastreador().Retornar(cnx);
            }
        }
    }
}
