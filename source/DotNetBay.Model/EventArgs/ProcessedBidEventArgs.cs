using DotNetBay.Model.BO;

namespace DotNetBay.Model.EventArgs
{
    public class ProcessedBidEventArgs : System.EventArgs
    {
        public Bid Bid { get; set; }

        public Auction Auction { get; set; }
    }
}