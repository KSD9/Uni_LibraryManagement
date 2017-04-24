using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uni_LibraryManagement.Data;
using Uni_LibraryManagement.Data.Entities;

namespace Uni_LibraryManagement.Controllers
{
    public class BookIssuesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: BookIssues
        public ActionResult Index()
        {
            var bookIssues = db.BookIssues.Include(b => b.Book).Include(b => b.User);

            if (Authentication.AuthenticationManager.LoggedUser.IsAdmin)
            {
                return View(bookIssues.ToList());
            }

            return View(bookIssues.Where(bi => bi.UserId == Authentication.AuthenticationManager.LoggedUser.Id).ToList());
        }

        // GET: BookIssues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookIssue bookIssue = db.BookIssues.Find(id);
            if (bookIssue == null)
            {
                return HttpNotFound();
            }
            return View(bookIssue);
        }

        // GET: BookIssues/Create
        public ActionResult Create(int? id)
        {
            Book book = db.Books.Find(id.Value);

            BookIssue bookisu = new BookIssue();
            bookisu.Issued = DateTime.UtcNow;
            bookisu.DeadLine = DateTime.UtcNow.AddDays(30);
            bookisu.Book = book;
            bookisu.BookId = id.Value;
            bookisu.UserId = Uni_LibraryManagement.Authentication.AuthenticationManager.LoggedUser.Id;
            return View(bookisu);
        }

        // POST: BookIssues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Issued,Returned,BookId,UserId,QuantityIssued,QuantityReturned")] BookIssue bookIssue)
        {


            if (ModelState.IsValid)
            {
                db.BookIssues.Add(bookIssue);
                db.SaveChanges();

                Book book = db.Books.Where(b => b.Id == bookIssue.BookId).SingleOrDefault();
                book.QuantityAvailable -= bookIssue.QuantityIssued;

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bookIssue);
        }

        // GET: BookIssues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookIssue bookIssue = db.BookIssues.Find(id);
            if (bookIssue == null)
            {
                return HttpNotFound();
            }

            bookIssue.Returned = DateTime.UtcNow;
            ViewBag.BookId = new SelectList(db.Books, "Id", "ISBN", bookIssue.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", bookIssue.UserId);
            return View(bookIssue);
        }

        // POST: BookIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Issued,Returned,BookId,UserId,QuantityIssued,QuantityReturned")] BookIssue bookIssue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookIssue).State = EntityState.Modified;
                db.SaveChanges();

                Book book = db.Books.Where(b => b.Id == bookIssue.BookId).SingleOrDefault();

                book.QuantityAvailable += bookIssue.QuantityReturned;

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books, "Id", "ISBN", bookIssue.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", bookIssue.UserId);
            return View(bookIssue);
        }

        // GET: BookIssues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookIssue bookIssue = db.BookIssues.Find(id);
            if (bookIssue == null)
            {
                return HttpNotFound();
            }
            return View(bookIssue);
        }

        // POST: BookIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookIssue bookIssue = db.BookIssues.Find(id);
            db.BookIssues.Remove(bookIssue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
