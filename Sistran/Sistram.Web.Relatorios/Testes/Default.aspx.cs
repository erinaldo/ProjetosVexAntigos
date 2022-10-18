// -- Default.ASPX.CS (Inicio)
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    //using System.Drawing.Design;


    public partial class _Default : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Estou Criando um Objeto do tipo BITMAP - Uma Imagem
            Bitmap objBMP;
            
            // Criando Objeto do Tipo Imagem
            Graphics objGrafico;

            // Determinando qual sera a altura/largura da imagem BMP; estou criando uma area que ocupa a tela inteira
                objBMP      = new Bitmap(800, 640);
            
            // Adicionando a Imagem BMP ao objeto grafico, que sera usado para mostrar a imagem
                objGrafico  = Graphics.FromImage(objBMP);
            
            // Determinando a cor de fundo da imagem
                //Color representa ARGB (Alpha, Red, Green, Blue) o melhor e deixar como White
                objGrafico.Clear(Color.Violet );

            // Criando objeto usado para desenhar linhas e curvas
                Pen p = new Pen(Color.Yellow, 0);
            
            // Armazena um conjunto de quadro inteiros que representam a localizacao e o tamanho de um retangulo. 
            // Para a funcões Region mais avancadas, utilize um objeto Region ao inves de Retangle.
            // O tamanho do grafico esta utilizando quase todo o espaco da area 800 x 640

                int nLeft  = 100;   int nTop    = 50;
                int nWidth = 600;   int nHeight = 500;
                Rectangle rect = new Rectangle(nLeft, nTop, nWidth, nHeight);
                objGrafico.DrawEllipse(p, rect);

            // Quantidade de faixa do grafico o numero de faixas poderia por exemplo ser um vetor
                Brush b1 = new SolidBrush(Color.Silver);
                Brush b2 = new SolidBrush(Color.Tomato);
                Brush b3 = new SolidBrush(Color.Blue);
                Brush b4 = new SolidBrush(Color.Yellow);

            // Aqui estou anexando o numero de faixas ao objeto grafico, observe que inicio a peira faixa em 0,
            // na sequencia vem o largura da faixa preste atencao pois o TOTAL da largura da feixa deve obrigatória
            // mente resultar em 360, pois tota  circunferencia tem 300 graus. 
            // TESTE: Troque o 120 por 80 e execute.

                objGrafico.FillPie(b1, rect, 0f   , 80f);   // Silver
                objGrafico.FillPie(b2, rect, 80f  , 80f);   // Tomato
                objGrafico.FillPie(b3, rect, 160f , 80f);   // Blue
                objGrafico.FillPie(b4, rect, 240f , 120f);  // Yellow

            // Criando um objeto do tipo FONTE
                FontFamily objFonte      = new FontFamily(GenericFontFamilies.Monospace);
                 FontStyle objFonteStyle = FontStyle.Bold | FontStyle.Italic ;

            // Determinando o TAMANHO e ESTILO da fonte
                Font font = new Font(objFonte, 16, objFonteStyle);

            // Determinando a COR da fonte
                SolidBrush brush = new SolidBrush(Color.Black);

            // Utilizo o metodo DrawString, para anexar escrever no grafico
                objGrafico.DrawString("Criando Graficos em ASP.NET ", font, brush, nLeft+250, nTop-30);
                objGrafico.DrawString("HereClick Software Office ME", font, brush, nLeft-50, nTop+530);

            //Esta e a parte que salva o Bitmap na OutputStream.
            // A OutputStream e justamente um objeto que guarda tudo o que vai ser enviado ao usuario. 
            // No caso de requisicões feitas para este arquivo .aspx, o que sera retornado ao usuario sera uma 
            // imagem em formato jpeg em vez de um documento HTML. E esta linha que mostra o grafico na tela

                objBMP.Save(Response.OutputStream,   ImageFormat.Gif);

            // Armazeno a imagem em disco, para utiliza-la.
                objBMP.Save(Server.MapPath("x.jpg"), ImageFormat.Jpeg);
            
            // Limpo os objetos criado da memoria;
                objFonte.Dispose();
                objBMP.Dispose();
                objGrafico.Dispose();   
        }
    }
// -- Default.ASPX.CS (Fim)