
namespace SistranMODEL
{
    public sealed class NotasDocumentosRelacionados
    {
         public int Documento { get; set; }
      

        public NotasDocumentosRelacionados()
        { }


        public NotasDocumentosRelacionados(int documento)
        {
            Documento = documento;
        }
    }
}
