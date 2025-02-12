namespace Bibliotech.Models
{
    public class Resenha
    {
        public int LivroId { get; set; }
        public int UsuarioId { get; set; }
        public string Texto { get; set; }
        public int Avaliação { get; set; } = 5;
        public int DataResenha { get; set; }= 0;
    }
}
