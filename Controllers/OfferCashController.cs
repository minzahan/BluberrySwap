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
    public class OfferCashController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OfferCash
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var units = db.Units.ToList();
            var cashOffers = db.CashOffers.Include(o => o.Offer).
                Where(o => o.Offer.OfferedByAuthorId == user.Id).ToList();

            var offeredByIds = cashOffers.Select(c => c.Offer.OfferedByAuthorId).ToList();
            var offerMakers = db.Users.Where(u => offeredByIds.Contains(u.Id)).ToList();

            foreach (var cashOffer in cashOffers)
            {
                cashOffer.Offer.OfferedByName = offerMakers.
                First(m => m.Id == cashOffer.Offer.OfferedByAuthorId).FirstName;
                cashOffer.UnitName = units.First(u => u.id == cashOffer.Unit_id.GetValueOrDefault()).Name;
            }
            return View(cashOffers);
        }


        public ActionResult Accept(int id)
        {
            var acceptedOfferTransaction = new Transaction
            {
                OfferId = id,
                Accepted = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                RejectionReason="NA"
            };

            db.Transactions.Add(acceptedOfferTransaction);
            db.SaveChanges();
            return RedirectToAction("Index", "Transactions");
        }

        public ActionResult Reject(int id)
        {
            var acceptedOfferTransaction = new Transaction
            {
                OfferId = id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                RejectionReason="Rejected-new feature"
            };

            db.Transactions.Add(acceptedOfferTransaction);
            db.SaveChanges();
            return RedirectToAction("Index", "Transactions");
        }

        public ActionResult CashOffersToMe()
        {
            var completedTransactionOfferIds = db.Transactions.Select(t => t.OfferId).ToList();
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var units = db.Units.ToList();

            var cashOffers = db.CashOffers.Include(o => o.Offer).
                Where(o => o.Offer.OfferedByAuthorId != user.Id).
                ToList().Where(o => !completedTransactionOfferIds.Contains(o.OfferId)).ToList();

            var offeredByIds = cashOffers.Select(c => c.Offer.OfferedByAuthorId).ToList();
            var offerMakers = db.Users.Where(u => offeredByIds.Contains(u.Id)).ToList();

            foreach (var cashOffer in cashOffers)
            {
                cashOffer.Offer.OfferedByName = offerMakers.
                First(m => m.Id == cashOffer.Offer.OfferedByAuthorId).FirstName;
                cashOffer.UnitName = units.First(u => u.id == cashOffer.Unit_id.GetValueOrDefault()).Name;
            }
            return View("Index",cashOffers);
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
        public ActionResult Create(int itemId, string description)
        {
            ViewBag.UnitID = new SelectList(db.Units, "id", "name");
            ViewBag.ItemId = itemId;
            
            var model = new OfferCashViewModel
            {
                ItemDescription = description
            };

            return View(model);
        }

        // POST: OfferCash/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "OfferCashId,CashValue,CreatedAt,UpdatedAt,Unit_id,OfferId")]
        public ActionResult Create(OfferCashViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                var offer = new Offer
                {
                    ItemId = model.ItemId,
                    Qty = model.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    OfferedByAuthorId=user.Id
                };


                db.Offers.Add(offer);
                db.SaveChanges();
                var currentOffer = db.Offers.FirstOrDefault(o => o.OfferId == offer.OfferId);
                var offerCash = new Offer_Cash
                {
                    Offer = currentOffer,
                    CashValue = model.CashValue,
                    Unit_id = model.UnitID,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    OfferCashId = currentOffer.OfferId,
                    OfferId = currentOffer.OfferId
                };
                db.CashOffers.Add(offerCash);
                db.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }

           // ViewBag.OfferCashId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", offer_Cash.OfferCashId);
            return View();
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
