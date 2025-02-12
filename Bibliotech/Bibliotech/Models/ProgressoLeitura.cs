namespace Bibliotech.Models
{
    public class ProgressoLeitura
    {
        public Guid ProgressoLeituraId { get; set; }
        public int livroId { get; set; }
        public int usuarioId { get; set; }
        public int paginaLidas { get; set; }
        public int totalPaginas { get; set; }
        public int DataAtualização { get; set; }
    }
}
