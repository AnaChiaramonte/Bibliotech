namespace Bibliotech.Models
{
    public class ProgressoLeitura
    {
        public Guid ProgressoLeituraId { get; set; }
        public Guid livroId { get; set; }
        public Livros? Livro { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int paginaLidas { get; set; }
        public int totalPaginas { get; set; }
        public DateTime DataAtualizacao { get; set; }


    }
}
