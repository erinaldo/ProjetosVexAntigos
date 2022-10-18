namespace SistranMODEL
{
    public class Menu
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Link { get; set; }


        public Menu()
        { }

        public Menu(int id, string titulo, string link)
        {
            Id = id;
            Titulo = titulo;
            Link = link;
        }
    }


    public class Modulo
    {
        public int idModulo{get;set;}
        public string Texto {get;set;}


        public Modulo(int id, string texto)
        {
            idModulo = id;
            Texto = texto;

        }

    }
}
