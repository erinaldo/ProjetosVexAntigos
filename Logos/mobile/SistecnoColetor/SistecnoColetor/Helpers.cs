using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using SistecnoColetor.Classes;

namespace SistecnoColetor
{
    public static class Helpers
    {
        public static void AceitarDecimais(object sender, KeyPressEventArgs e, char cSymbol)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != cSymbol)
            {
                e.Handled = true;
            }

            if (e.KeyChar == cSymbol && (sender as TextBox).Text.IndexOf(cSymbol) > -1)
            {
                e.Handled = true;
            }
        }
        
        public static void AceitarNumerosInteiros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
            }            
        }

        public static string Formatar3CasasDecimais(decimal valor)
        {
            return String.Format("{0:0.000}", valor).Replace(".", ","); 
        }

        public static string RetirarDigitoUA(string Ua)
        {
            if (Ua.Length < 2)
                return Ua;
            else
            return Ua.Substring(0, Ua.Length - 1);
        }



        public static bool EnderecoVazio(string IdDepositoPlantaLocalizacao)
        {
            string sql = "SELECT   SUM(UAL.SALDO) FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " WHERE  ";
            sql += " UA.IDDEPOSITOPLANTALOCALIZACAO =" + IdDepositoPlantaLocalizacao + " and dpl.MultiplasUa='NAO'";

            DataTable dt = BdExterno.RetornarDT(sql, VarGlobal.Conexao);

            if (dt.Rows.Count == 0)
                return true;


            if (dt.Rows[0][0].ToString() == "")
                return true;


            if (int.Parse(float.Parse(dt.Rows[0][0].ToString()).ToString()) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidaNumeto(string v)
        {
            try
            {
                float x = float.Parse(v);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsData(string v)
        {
            try
            {
                if (v == "")
                    return true;

        //10/10/2010


                DateTime x = new DateTime(int.Parse(v.Substring(6, 4)), int.Parse(v.Substring(3, 2)), int.Parse(v.Substring(0, 2))); 
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
    }
}
