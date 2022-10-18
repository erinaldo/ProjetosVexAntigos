using System.Collections.Generic;
using System.Data;

namespace Sistecno.BLL
{
    public class DT
    {

        public DataTable RetornarInfoEletronico(int IdDt, string cnx)
        {
            return new Sistecno.DAL.DT().RetornarInfoEletronico(IdDt, cnx);
        }

        public int  CriarDt(Sistecno.DAL.Models.Romaneio rom, List<Sistecno.DAL.Models.RomaneioDocumento> ldocs, Sistecno.DAL.Models.DT dt, int idEmpresa, List<string> CtesExcluidos, string cnx)
        {
            return new Sistecno.DAL.DT().CriarDt(rom, ldocs, dt, idEmpresa, CtesExcluidos,cnx);
        }

        public DataTable RetornarDocumentosDoRomaneioByDT(int IdDt, string cnx)
        {
           return new Sistecno.DAL.DT().RetornarDocumentosDoRomaneioByDT(IdDt, cnx);
        }

        public DataTable Retornar(string numero, int idFilial, string Andamento, string cnx)
        {
            return new Sistecno.DAL.DT().Retornar(numero, idFilial, Andamento, cnx);
        }

        public DataTable RetornarByIdDt(int IdDt, string cnx)
        {
            return new Sistecno.DAL.DT().RetornarByIdDt(IdDt, cnx);
        }


        public class Tipo
        {
            public DataTable Retornar(string cnx)
            {
                return new Sistecno.DAL.DT.Tipo().Retornar(cnx);
            }

        }
    }
}
