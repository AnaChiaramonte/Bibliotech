namespace Bibliotech.Models
{
    public class Livros
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public Guid CategoriaId { get; set; }
        public int DataPublicacao { get; set; }
    }
}
