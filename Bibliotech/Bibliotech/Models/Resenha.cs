namespace Bibliotech.Models
{
    public class Resenha
    {
        public Guid ResenhaId { get; set; }
        public Guid? LivroId { get; set; }
        public Livros? Livro { get; set; }
        public int UsuarioId { get; set; }
        public string Texto { get; set; }
        public int Avaliação { get; set; } = 5;
        public DateOnly DataResenha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
