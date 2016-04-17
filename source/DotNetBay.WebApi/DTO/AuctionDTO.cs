using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Core;
using DotNetBay.Model.BO;

namespace DotNetBay.WebApi.DTO {
    public class AuctionDTO {
        
        public int Id { get; set; }

        public double CurrentPrice { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime StartDateTimeUtc { get; set; }
        
        public DateTime EndDateTimeUtc { get; set; }
        
        public DateTime CloseDateTimeUtc { get; set; }

        public string WinnerName { get; set; }

        public string CurrentWinnerName { get; set; }

        public string SellerName { get; set; }

        public static AuctionDTO GetAuctionDtoByAuction(Auction auction) {

            return new AuctionDTO {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                CurrentPrice = auction.CurrentPrice,
                WinnerName = auction.Winner?.DisplayName,
                CurrentWinnerName = auction.ActiveBid?.Bidder.DisplayName,
                SellerName = auction.Seller.DisplayName
            };
        }

        public static Auction GetAuctionByAuctionDTO(AuctionDTO auction) {
            var sl = ServiceLocator.GetInstance;
            Member seller = sl.GetMemberService.GetAll().FirstOrDefault(s => s.DisplayName.Equals(auction.SellerName));
            if (seller == null) seller = sl.GetMemberService.GetCurrentMember();
            Member winner = sl.GetMemberService.GetAll().FirstOrDefault(s => s.DisplayName.Equals(auction.SellerName));
            return new Auction {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                CurrentPrice = auction.CurrentPrice,
                Winner = winner,
                Seller = seller
            };
        }
    }
}
