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
                Replies = new List<Comment>()
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

        [HttpGet]
        public ActionResult CreateComment(int? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            comment.Id = _comments.Count > 0 ? _comments.Max(c => c.Id) + 1 : 1;
            _comments.Add(comment);
            return RedirectToAction("Index");
        }
    }
}