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
    public class OfferCashController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OfferCash
        public ActionResult Index()
        {
            var cashOffers = db.CashOffers.Include(o => o.Offer);
            return View(cashOffers.ToList());
        }

        // GET: OfferCash/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer_Cash offer_Cash = db.CashOffers.Find(id);
            if (offer_Cash == null)
            {
                return HttpNotFound();
            }
            return View(offer_Cash);
        }

        // GET: OfferCash/Create
        public ActionResult Create()
        {
            ViewBag.OfferCashId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId");
            return View();
        }

        // POST: OfferCash/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OfferCashId,CashValue,CreatedAt,UpdatedAt,Unit_id,OfferId")] Offer_Cash offer_Cash)
        {
            if (ModelState.IsValid)
            {
                db.CashOffers.Add(offer_Cash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OfferCashId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", offer_Cash.OfferCashId);
            return View(offer_Cash);
        }

        // GET: OfferCash/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer_Cash offer_Cash = db.CashOffers.Find(id);
            if (offer_Cash == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferCashId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", offer_Cash.OfferCashId);
            return View(offer_Cash);
        }

        // POST: OfferCash/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OfferCashId,CashValue,CreatedAt,UpdatedAt,Unit_id,OfferId")] Offer_Cash offer_Cash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer_Cash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfferCashId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", offer_Cash.OfferCashId);
            return View(offer_Cash);
        }

        // GET: OfferCash/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer_Cash offer_Cash = db.CashOffers.Find(id);
            if (offer_Cash == null)
            {
                return HttpNotFound();
            }
            return View(offer_Cash);
        }

        // POST: OfferCash/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer_Cash offer_Cash = db.CashOffers.Find(id);
            db.CashOffers.Remove(offer_Cash);
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
