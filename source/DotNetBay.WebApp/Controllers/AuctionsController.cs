using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Model.BO;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionsController : Controller {

        // GET: Auctions
        public ActionResult Index() {
            return View(ServiceLocator.GetInstance.GetMainRepository.GetAuctions());
        }

        //// GET: Auctions/Details/5
        //public ActionResult Details(int id) {
        //    return View(ServiceLocator.GetInstance.GetMainRepository.GetAuctions().FirstOrDefault(x => x.Id == id));
        //}

        // GET: Auctions/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(Auction auction) {
            auction.Seller = ServiceLocator.GetInstance.GetMemberService.GetCurrentMember();
            ServiceLocator.GetInstance.GetAuctionService.Save(auction);
            return this.RedirectToAction("Index");
        }
    }
}
