using System;
using System.Data;
using System.IO;

namespace ServicosWEB.Util
{
    public static class CidadeBairro
    {
        public static byte[] ReduzTamanhoDaImagem(this byte[] imageData, int width, int height, System.Drawing.Imaging.ImageFormat type)
        {
            if (imageData == null)
                return null;
            if (imageData.Length == 0)
                return imageData;
            using (MemoryStream myMemStream = new MemoryStream(imageData))
            {
                System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
                if (width <= 0 || width > fullsizeImage.Width)
                    width = fullsizeImage.Width;
                if (height <= 0 || height > fullsizeImage.Height)
                    height = fullsizeImage.Height;
                System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
                using (MemoryStream myResult = new MemoryStream())
                {
                    newImage.Save(myResult, type);
                    return myResult.ToArray();
                }
            }
        }
        public static int RetornarCidade(string CEP, string cnx)
        {
           string sql = "";
            sql = "Select c.IdCidade from CidadeFaixaDeCep cfc  ";
            sql += " inner join Cidade c on c.IdCidade = cfc.IdCidade ";
            sql += " where " + CEP + " between CepInicial and CepFinal ";

           DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

           if (dt.Rows.Count > 0)
               return Convert.ToInt32(dt.Rows[0]["IdCidade"]);
           else
           {

               sql = "select IdCidade from Cidade where cep='" + CEP + "' and IdEstado=26";
               dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

               if (dt.Rows.Count > 0)
                   return Convert.ToInt32(dt.Rows[0]["IdCidade"]);
               else
                   return 0;
           
           }

        }

        public static int RetornarBairro(string Nome, string IdClidade, string cnx)
        {
            int Id = 0;
            Nome = Validacao.removerAcentos(Nome);

            string sql = "select IdBairro from Bairro where Nome = '" + Nome + "' and IdCidade=" + IdClidade;
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["IdBairro"]);
            else
            {
                Id = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("Bairro", cnx));
                sql = "Insert into Bairro (IdBairro, Nome, IdCidade) values (" + Id + ", '" + Nome + "', " + IdClidade + ")";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, cnx);
                return Id;
            }

            return Id;
        }
    }
}