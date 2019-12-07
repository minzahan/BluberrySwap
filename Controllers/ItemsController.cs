using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlueberrySwap;
using BlueberrySwap.Models;

namespace BlueberrySwap.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        
        // GET: Items
        public ActionResult Index(int categoryId)
        {
            var loggedinUser = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var items = db.Items.Include(i => i.Category).
                Include(i => i.Unit).
                Where(i => i.CategoryID == categoryId ).ToList();

            var itemPosters = items.Select(i => i.Author_Id).ToList();
            var users = db.Users.Where(u => itemPosters.Contains(u.Id)).ToList();
            ApplicationUser itemPoster = null;
            List<Item> filteredItems = new List<Item>();
            foreach (var item in items)
            {
                itemPoster= users.FirstOrDefault(u => u.Id == item.Author_Id);
                if(itemPoster.Zipcode==loggedinUser.Zipcode)
                {
                    item.Author = itemPoster;
                    item.CanBeEdited = item.Author.Id == loggedinUser.Id;
                    filteredItems.Add(item);
                }
                
            }
            return View(filteredItems);
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "id", "name");
            ViewBag.UnitID = new SelectList(db.Units, "id", "name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,description,created_at,updated_at,CategoryID,UnitID,AuthorID")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedAt= DateTime.Now;
                item.UpdatedAt = DateTime.Now;

                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                item.Author_Id = user.Id;
                
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index", routeValues:new {categoryId = item.CategoryID});
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "id", "name", item.CategoryID);
            ViewBag.UnitID = new SelectList(db.Units, "id", "name", item.UnitID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "id", "name", item.CategoryID);
            ViewBag.UnitID = new SelectList(db.Units, "id", "name", item.UnitID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedAt = item.CreatedAt;
                item.UpdatedAt = DateTime.Now;

                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                item.Author_Id = user.Id;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { categoryId = item.CategoryID });
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "id", "name", item.CategoryID);
            ViewBag.UnitID = new SelectList(db.Units, "id", "name", item.UnitID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
