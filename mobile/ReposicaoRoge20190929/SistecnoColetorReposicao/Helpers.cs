using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

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


        
    }
}
