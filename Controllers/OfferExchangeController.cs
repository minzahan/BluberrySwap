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
    public class OfferExchangeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OfferExchange
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var units = db.Units.ToList();
            var exchangeOffers = db.ExchangeOffers.Include(o => o.Offer).
                Where(o => o.Offer.OfferedByAuthorId == user.Id).ToList();

            var offeredByIds = exchangeOffers.Select(c => c.Offer.OfferedByAuthorId).ToList();
            var offerMakers = db.Users.Where(u => offeredByIds.Contains(u.Id)).ToList();

            foreach (var exchangeOffer in exchangeOffers)
            {
                exchangeOffer.Offer.OfferedByName = offerMakers.
                First(m => m.Id == exchangeOffer.Offer.OfferedByAuthorId).FirstName;
                exchangeOffer.ExchangeItemUnitName = units.
                    First(u => u.id == exchangeOffer.ExchangeItemUnitId).Name;
            }

            return View(exchangeOffers);
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
                RejectionReason="Rejected-comingsoon"
            };

            db.Transactions.Add(acceptedOfferTransaction);
            db.SaveChanges();
            return RedirectToAction("Index", "Transactions");
        }

        public ActionResult ExchangeOffersToMe()
        {
            var completedTransactionOfferIds = db.Transactions.Select(t => t.OfferId).ToList();
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var units = db.Units.ToList();

            var exchangeOffers = db.ExchangeOffers.Include(o => o.Offer).
                Where(o => o.Offer.OfferedByAuthorId != user.Id).
                ToList().Where(o => !completedTransactionOfferIds.Contains(o.OfferId)).ToList();

            var offeredByIds = exchangeOffers.Select(c => c.Offer.OfferedByAuthorId).ToList();
            var offerMakers = db.Users.Where(u => offeredByIds.Contains(u.Id)).ToList();

            foreach (var exchangeOffer in exchangeOffers)
            {
                exchangeOffer.Offer.OfferedByName = offerMakers.
                First(m => m.Id == exchangeOffer.Offer.OfferedByAuthorId).FirstName;
                exchangeOffer.ExchangeItemUnitName = units.
                    First(u => u.id == exchangeOffer.ExchangeItemUnitId).Name;
            }

            return View("Index",exchangeOffers);
        }



        // GET: OfferExchange/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer_Exchange offer_Exchange = db.ExchangeOffers.Find(id);
            if (offer_Exchange == null)
            {
                return HttpNotFound();
            }
            return View(offer_Exchange);
        }

        // GET: OfferExchange/Create
        public ActionResult Create(int itemId, string description)
        {
            var model = new OfferExchangeViewModel
            {
                OfferedItemId = itemId,
                OfferedItemDescription = description
            };

            ViewBag.ExchangeItemUnitId = new SelectList(db.Units, "id", "name");
            // ViewBag.OfferedItemId = itemId; 
            //ViewBag.OfferedItemDescription = description;

            var loggedInUser = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var myItems = db.Items.Where(i => i.Author_Id == loggedInUser.Id).ToList();
            ViewBag.ExchangeItemId = new SelectList(myItems, "id", "Name");

            return View(model);
        }

        // POST: OfferExchange/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferExchangeViewModel model)
        {
            Offer_Exchange exchangeOffer = new Offer_Exchange();
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (ModelState.IsValid)
            {

                var offer = new Offer
                {
                    ItemId = model.OfferedItemId,
                    Qty = model.ExchangeItemQty,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    OfferedByAuthorId = user.Id
                };

                db.Offers.Add(offer);
                db.SaveChanges();
                var currentOffer = db.Offers.FirstOrDefault(o => o.OfferId == offer.OfferId);
                exchangeOffer = new Offer_Exchange
                {
                    Offer = currentOffer,
                    ExchangeItemId = model.ExchangeItemId,
                    ExchangeItemUnitId = model.ExchangeItemUnitId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    OfferExchangeId = currentOffer.OfferId,
                    OfferId = currentOffer.OfferId
                };
                
                db.ExchangeOffers.Add(exchangeOffer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.OfferExchangeId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", exchangeOffer.OfferExchangeId);
            return View(exchangeOffer);
        }

        // GET: OfferExchange/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer_Exchange offer_Exchange = db.ExchangeOffers.Find(id);
            if (offer_Exchange == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferExchangeId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", offer_Exchange.OfferExchangeId);
            return View(offer_Exchange);
        }

        // POST: OfferExchange/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OfferExchangeId,CreatedAt,UpdatedAt,ExchangeItemId,ExchangeItemQty,ExchangeItemUnitId,OfferId")] Offer_Exchange offer_Exchange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer_Exchange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfferExchangeId = new SelectList(db.Offers, "OfferId", "OfferedByAuthorId", offer_Exchange.OfferExchangeId);
            return View(offer_Exchange);
        }

        // GET: OfferExchange/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer_Exchange offer_Exchange = db.ExchangeOffers.Find(id);
            if (offer_Exchange == null)
            {
                return HttpNotFound();
            }
            return View(offer_Exchange);
        }

        // POST: OfferExchange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer_Exchange offer_Exchange = db.ExchangeOffers.Find(id);
            db.ExchangeOffers.Remove(offer_Exchange);
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
