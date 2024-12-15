using Forun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Content = "Confira meu novo software para controle de férias dos funcionários",
                Author = "Patrício",
                CreatedAt = DateTime.Now,
                Replies = new List<Comment>
                {
                    new Comment
                    {
                        Id = 2,
                        Content = "Eu gostei da forma como você estruturou os componentes do sistema de " +
                        "controle de férias, mas acho que seria interessante adicionar uma funcionalidade" +
                        "notificação para lembrar os usuários sobre o prazo das férias solicitadas.",
                        Author = "João Silva",
                        CreatedAt = DateTime.Now
                    }
                }
            },
            new Comment
            {
                Id = 3,
                Content = "Já experimentaram fazer trilhas em montanhas? Recentemente fiz uma trilha no " +
                "Pico da Bandeira e foi uma experiência incrível! O contato com a natureza renova as energias.",
                Author = "Ana Costa",
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

        public ActionResult CommentDetails(int id, string author = null)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return HttpNotFound();

            return View(comment);
        }
    }
}