using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Core;
using DotNetBay.Model.BO;

namespace DotNetBay.WebApi.DTO {
    class BidDTO {
        public int Id { get; set; }

        public DateTime ReceivedOnUtc { get; set; }

        public double Amount { get; set; }

        public bool? Accepted { get; set; }

        public int AuctionId { get; set; }
        
        public string BidderName { get; set; }

        public static Bid GetBidByBidDTO(BidDTO bid) {
            ServiceLocator sl = ServiceLocator.GetInstance;
            Auction auction = sl.GetAuctionService.GetById(bid.AuctionId);
            Member bidder = sl.GetMemberService.GetAll().FirstOrDefault(s => s.DisplayName.Equals(bid.BidderName));
            if (bidder == null) bidder = sl.GetMemberService.GetCurrentMember();
            return new Bid {
                Auction = auction,
                AuctionId = auction.Id,
                Amount = bid.Amount,
                Accepted = bid.Accepted,
                Bidder = bidder,
                BidderId = bidder.Id,
                ReceivedOnUtc = bid.ReceivedOnUtc
            };
        }
    }
}
