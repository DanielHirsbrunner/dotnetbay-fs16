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

        //[InverseProperty("Bids")]
        [ForeignKey("Id")]
        [Column("AuctionId")]
        public virtual Auction Auction { get; set; }

        //[InverseProperty("Bids")]
        [ForeignKey("Id")]
        [Column("BidderId")]
        public virtual Member Bidder { get; set; }
    }
}
