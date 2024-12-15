using System;
using System.Collections.Generic;

namespace Forun.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relacionamento com respostas
        public List<Comment> Replies { get; set; } = new List<Comment>();

        // Identificador para diferenciar respostas de comentários principais
        public int? ParentId { get; set; }
    }
}