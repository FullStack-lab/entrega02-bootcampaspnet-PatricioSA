using Forun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forun.Controllers
{
    public class ForumController : Controller
    {
        // Lista estática para armazenar os dados
        private static List<Comment> _comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                Content = "Este é o primeiro comentário.",
                Author = "Usuário1",
                CreatedAt = DateTime.Now,
                Replies = new List<Comment>
                {
            new Comment
            {
                Id = 2,
                Content = "Esta é uma resposta ao primeiro comentário.",
                Author = "Usuário2",
                CreatedAt = DateTime.Now
            }
        }
            },
            new Comment
            {
                Id = 3,
                Content = "Este é o segundo comentário.",
                Author = "Usuário3",
                CreatedAt = DateTime.Now,
                Replies = new List<Comment>()
            }
        };

        // GET: Forum
        public ActionResult Index()
        {
            var mainComments = _comments.Where(c => c.ParentId == null).ToList();
            return View(mainComments);
        }

        public ActionResult CreateComment(int? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            comment.Id = _comments.Count > 0 ? _comments.Max(c => c.Id) + 1 : 1;

            if (comment.ParentId.HasValue)
            {
                // Encontrar o comentário pai na lista de comentários
                var parentComment = _comments.FirstOrDefault(c => c.Id == comment.ParentId.Value);

                parentComment?.Replies.Add(comment);
            }
            else
            {
                // Se não tiver ParentId, é um comentário principal
                _comments.Add(comment);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditComment(int id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return HttpNotFound();

            return View(comment);
        }

        [HttpPost]
        public ActionResult EditComment(Comment updatedComment)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == updatedComment.Id);
            if (comment == null) return HttpNotFound();

            comment.Content = updatedComment.Content;
            return RedirectToAction("Index");
        }

        public ActionResult DeleteComment(int id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);

            return View(comment);
        }

        [HttpPost, ActionName("DeleteComment")]
        public ActionResult ConfirmDelete(int id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            if (comment != null)
            {
                _comments.Remove(comment);
            }
            return RedirectToAction("Index");
        }
    }
}