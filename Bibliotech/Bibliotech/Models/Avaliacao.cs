﻿using Bibliotech.Models;

namespace Biblioteca.Models
{
    public class Avaliacao
    {
        public int AvaliacaoId { get; set; }
        public int Nota { get; set; }
        public string? Comentario { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public Guid LivroId { get; set; }
        public Livros? Livro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}