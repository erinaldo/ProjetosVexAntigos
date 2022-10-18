using System;
using System.Data;
using System.Linq;
using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class Veiculo
    {
        public DataTable RetornarVeicPropMotorista(string placa,  string cnx)
        {
            string strsql = " SELECT V.IDVEICULO,V.PLACA,MOT.CNPJCPF CPFMOTORISTA,MOT.RAZAOSOCIALNOME MOTORISTA,PROP.RAZAOSOCIALNOME PROPRIETARIO ";
            strsql += " FROM VEICULO V ";
            strsql += " INNER JOIN CADASTRO MOT ON MOT.IDCADASTRO = V.IDMOTORISTA ";
            strsql += " INNER JOIN CADASTRO PROP ON PROP.IDCADASTRO = V.IDPROPRIETARIO  ";
            strsql += " WHERE V.PLACA='"+placa+"' ";

            return DAL.BD.cDb.RetornarDataTable(strsql, cnx);

        }

        public DataTable Retornar(int? idVeiculo, string Placa, string NomeMotorista, string NomeProprietario, string cnx)
        {
            string strsql = "  SELECT TOP 50  V.IDVEICULO [ID], V.PLACA, V.RENAVAM,   CADPROP.CNPJCPF [CNPJ/CPF PROP.],  CADPROP.RAZAOSOCIALNOME [PROPRIETARIO], CADMOTO.CNPJCPF [CNPJ/CPF MOT.], CADMOTO.RAZAOSOCIALNOME [MOTORISTA]  ";
            strsql += " FROM VEICULO V LEFT JOIN CADASTRO CADPROP ON CADPROP.IDCADASTRO = V.IDPROPRIETARIO  LEFT JOIN CADASTRO CADMOTO ON CADMOTO.IDCADASTRO = V.IDMOTORISTA ";
            strsql += " WHERE 0=0 ";

            if (Placa != "")
                strsql += " AND PLACA LIKE '%" + Placa + "%' ";

            if (NomeMotorista != "")
                strsql += " AND CADMOTO.RAZAOSOCIALNOME LIKE '%" + NomeMotorista + "%' ";


            if (NomeProprietario != "")
                strsql += " AND CADPROP.RAZAOSOCIALNOME LIKE '%" + NomeProprietario + "%' ";

            strsql += " AND IDVEICULO = ISNULL (" + (idVeiculo == null ? "NULL" : idVeiculo.ToString()) + ",IDVEICULO) ";
            strsql += " ORDER BY V.IDVEICULO DESC " ;
            return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
        }

        public int GravarVeiculo(DAL.Models.Veiculo veic, string cnx)
        {
            var context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();

            try
            {
                if (veic.IDVeiculo == 0)
                {
                    veic.IDVeiculo = DAL.BD.cDb.RetornarIDTabela(cnx, "Veiculo");
                    context.Veiculoes.Add(veic);
                    context.SaveChanges();
                }
                else
                {
                    var o = context.Veiculoes.First(i => i.IDVeiculo == veic.IDVeiculo);
                    o.AnoModelo = veic.AnoModelo;
                    o.Ano = veic.Ano;
                    o.IDVeiculoModelo = veic.IDVeiculoModelo;
                    o.Placa = veic.Placa;
                    o.Cor = veic.Cor;
                    o.IDVeiculoTipo = veic.IDVeiculoTipo;
                    o.IDMotorista = veic.IDMotorista;
                    o.IDProprietario = veic.IDProprietario;
                    o.CapacidadeDeCargaKG = veic.CapacidadeDeCargaKG;
                    o.CapacidadeDeCargaM3 = veic.CapacidadeDeCargaM3;
                    o.CategoriasDeCNHPermitidas = veic.CategoriasDeCNHPermitidas;
                    o.Chassi = veic.Chassi;
                    o.Renavam = veic.Renavam;
                    o.IDVeiculoRastreador = veic.IDVeiculoRastreador;
                    o.NumeroSerieEquipamento = veic.NumeroSerieEquipamento;
                    o.Antt = veic.Antt;
                    o.AnttVencimento = veic.AnttVencimento;
                    o.DataDeLicenciamento = veic.DataDeLicenciamento;
                    context.SaveChanges();
                }

                return veic.IDVeiculo;

            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }

        }

        public DAL.Models.Veiculo RetornarAllFields(int idVeiculo, string cnx)
        {
            try
            {
                SistecnoContext context = new SistecnoContext();
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                var oC = context.Veiculoes.FirstOrDefault(i => i.IDVeiculo == idVeiculo);
                context.Database.Connection.Close();
                return oC;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DAL.Models.Veiculo RetornarbyPlaca(string placa, string cnx)
        {
            try
            {
                SistecnoContext context = new SistecnoContext();
                context.Database.Connection.ConnectionString = cnx;
                context.Database.Connection.Open();
                var oC = context.Veiculoes.FirstOrDefault(i => i.Placa == placa);
                context.Database.Connection.Close();
                return oC;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public class Modelo
        {

            public DataTable Retornar(string cnx)
            {
                string strsql = "SELECT * FROM VEICULOMODELO ORDER BY 3";
                return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
            }
        }


        public class Tipo
        {

            public DataTable Retornar(string cnx)
            {
                string strsql = "SELECT * FROM VEICULOTIPO ORDER BY 2";
                return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
            }
        }

        public class Rastreador
        {

            public DataTable Retornar(string cnx)
            {
                string strsql = "SELECT * FROM VEICULORASTREADOR ORDER BY 2";
                return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
            }
        }
    }
}