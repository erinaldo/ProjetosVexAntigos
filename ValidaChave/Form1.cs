using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValidaChave
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GerarDigitoVerificadorNFe(textBox1.Text).ToString());
            //MessageBox.Show(geraDigMod11(textBox1.Text).ToString());
        }


        private int GerarDigitoVerificadorNFe(string chave)
        {
            int soma = 0; // Vai guardar a Soma
            int mod = -1; // Vai guardar o Resto da divisão
            int dv = -1;  // Vai guardar o DigitoVerificador
            int pesso = 2; // vai guardar o pesso de multiplicacao

            //percorrendo cada caracter da chave da direita para esquerda para fazer os calculos com o pesso
            for (int i = chave.Length - 1; i != -1; i--)
            {
                int ch = Convert.ToInt32(chave[i].ToString());
                soma += ch * pesso;
                //sempre que for 9 voltamos o pesso a 2
                if (pesso < 9)
                    pesso += 1;
                else
                    pesso = 2;
            }

            //Agora que tenho a soma vamos pegar o resto da divisão por 11
            mod = soma % 11;
            //Aqui temos uma regrinha, se o resto da divisão for 0 ou 1 então o dv vai ser 0
            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            return dv;
        }

        private int geraDigMod11(string strText)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };

            int intSoma = 0;
            int intIdx = 0;

            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];
                if (intIdx == 9)
                {
                    intIdx = 2;
                }
                else
                {
                    intIdx++;
                }
            }

            int intResto = (intSoma * 10) % 11;
            int intDigito = intResto;
            if (intDigito >= 10)
                intDigito = 0;

            return intDigito;
        }


        int digitoRetorno = 0;
        public  string digito(string chave)
        {
            int soma = 0;
            int resto = 0;
            int[] peso = { 4, 3, 2, 9, 8, 7, 6, 5 };

            for (int i = 0; i < chave.Length; i++)
            {
                soma += peso[i % 8] * (int.Parse(chave.Substring(i, 1)));
            }

            resto = soma % 11;
            if (resto == 0 || resto == 1)
            {
                digitoRetorno = 0;
            }
            else
            {
                digitoRetorno = 11 - resto;
            }

            return chave + digitoRetorno.ToString();
        }
    }
}
