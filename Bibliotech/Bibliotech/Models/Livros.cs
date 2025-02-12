namespace Bibliotech.Models
{
    public class Livros
    {
        public Guid LivroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateOnly DataPublicacao { get; set; }
        public Guid? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
