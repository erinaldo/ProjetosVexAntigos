using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace Sistecno.BLL.Helpers
{
    public static class ManipulacaoArquivos
    {

        public static void criarZIPXmlPdf(string CaminhoDosArquivosPdf, string CaminhoArquivosXML, List<string> NomeArquivosPDF, List<string> NomeArquivosXML, string NomeZip, string caminhoDoZip)
        {
            string nomeArquivo = NomeZip;
            caminhoDoZip = caminhoDoZip + NomeZip;
            ZipOutputStream zipOutPut = new ZipOutputStream(File.Create(caminhoDoZip));
            //Compactação level 9
            zipOutPut.SetLevel(9);
            zipOutPut.Finish();
            zipOutPut.Close();


            ZipFile zip = new ZipFile(caminhoDoZip);
            //Inicia a criação do ZIP
            zip.BeginUpdate();


            for (int i = 0; i < NomeArquivosPDF.Count; i++)
            {
                //Adicionando arquivos previamente criados ao zipFile
                string cam = CaminhoDosArquivosPdf + NomeArquivosPDF[i];
                string nomeZIP = cam;
                zip.NameTransform = new ZipNameTransform(nomeZIP.Substring(0, nomeZIP.LastIndexOf("\\")));
                zip.Add(nomeZIP);

            }


            for (int i = 0; i < NomeArquivosXML.Count; i++)
            {
                //Adicionando arquivos previamente criados ao zipFile
                string cam = CaminhoArquivosXML + NomeArquivosXML[i];
                string nomeZIP = cam;
                zip.NameTransform = new ZipNameTransform(nomeZIP.Substring(0, nomeZIP.LastIndexOf("\\")));
                zip.Add(nomeZIP);

            }

            zip.CommitUpdate();
            zip.Close();
        }
    }
}