using BlogTriple.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogTriple.Controllers
{
    public class ArticleController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var database = new BlogDbContext();

            var articles = database.Articles.Include(a => a.Author).ToList();

            return View(articles);


        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var database = new BlogDbContext();
            var article = database.Articles.Where(a => a.Id == id).Include(a => a.Author).First();

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                var database = new BlogDbContext();

                var authorID = database.Users
                    .Where(u => u.UserName == this.User.Identity.Name)
                    .First()
                    .Id;

                article.AuthorId = authorID;

                database.Articles.Add(article);
                database.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(article);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var database = new BlogDbContext();

            var article = database.Articles
                .Where(a => a.Id == id)
                .Include(a => a.Author)
                .First();

            if (! IsUserAuthorizedToEdit(article))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var database = new BlogDbContext();

            var article = database.Articles.Where(a => a.Id == id).FirstOrDefault();

            if (article == null)
            {
                return HttpNotFound();
            }

            database.Articles.Remove(article);
            database.SaveChanges();

            return RedirectToAction("Index");


        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var database = new BlogDbContext();

            var article = database.Articles.Where(a => a.Id == id).First();

            if (!IsUserAuthorizedToEdit(article))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (article == null)
            {
                return HttpNotFound();
            }

            var model = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit (ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var database = new BlogDbContext();

                var articles = database.Articles.FirstOrDefault(a => a.Id == model.Id);

                articles.Title = model.Title;
                articles.Content = model.Content;

                database.Entry(articles).State = EntityState.Modified;
                database.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(model);
        }

        public bool IsUserAuthorizedToEdit(Article article)
        {
            bool isAdmin = this.User.IsInRole("Admin");
            bool isAuthor = article.IsAuthor(this.User.Identity.Name);

            return isAdmin || isAuthor;
        }

    }
}