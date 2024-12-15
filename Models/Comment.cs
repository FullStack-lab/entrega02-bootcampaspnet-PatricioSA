using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forun.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Display(Name = "Mensagem")]
        public string Content { get; set; }

        [Display(Name = "Nome")]
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relacionamento com respostas
        public List<Comment> Replies { get; set; } = new List<Comment>();

        // Identificador para diferenciar respostas de comentários principais
        public int? ParentId { get; set; }
    }
}