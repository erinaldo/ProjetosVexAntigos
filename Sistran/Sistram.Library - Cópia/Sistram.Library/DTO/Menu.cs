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

    public class menuChildren
    { 
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Link { get; set; }

        public menuChildren()
        { }

        public menuChildren(int id, string titulo, string link)
        {
            Id = id;
            Titulo = titulo;
            Link = link;
        }
    }
}
