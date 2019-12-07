using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlueberrySwap.Models;

namespace BlueberrySwap.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var items = db.Items.ToList();
            var units = db.Units.ToList();
            var users = db.Users.ToList();
            var transactions = db.Transactions.Include(t => t.Offer).ToList();

            foreach (var transaction in transactions)
            {
                var remark = transaction.Offer.Offer_Cash != null ?
                    string.Format("Cash Value: {0}, Qty: {1}",
                    transaction.Offer.Offer_Cash.CashValue.ToString("c"),
                    transaction.Offer.Qty) :
                    string.Format("Exchange Item :{0} {1} {2} ",
                    items.FirstOrDefault(i => i.id == transaction.Offer.Offer_Exchange.ExchangeItemId).Name,
                    transaction.Offer.Qty, 
                   units.FirstOrDefault(u => u.id == transaction.Offer.Offer_Exchange.ExchangeItemUnitId).Name);

                var offeredItem = items.FirstOrDefault(i => i.id == transaction.Offer.ItemId).Name;

                transaction.Remarks = string.Format("Offered Item: {0}, {1}", offeredItem, remark);
                transaction.Offer.OfferedByName = users.
                    FirstOrDefault(u => u.Id == transaction.Offer.OfferedByAuthorId).Email;
            }
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.OfferId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionId,OfferId,Accepted,RejectionReason,CreatedAt,UpdatedAt")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OfferId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", transaction.OfferId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", transaction.OfferId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,OfferId,Accepted,RejectionReason,CreatedAt,UpdatedAt")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfferId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", transaction.OfferId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
