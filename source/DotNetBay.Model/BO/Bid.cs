using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBay.Model.BO
{
    public class Bid
    {
        public Bid()
        {
            this.TransactionId = Guid.NewGuid();
        }

        public int Id { get; set; }

        public DateTime ReceivedOnUtc { get; set; }

        public Guid TransactionId { get; set; }

        public double Amount { get; set; }

        public bool? Accepted { get; set; }

        public int AuctionId { get; set; }

        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }

        public int BidderId { get; set; }

        [ForeignKey("BidderId")]
        public Member Bidder { get; set; }
    }
}
