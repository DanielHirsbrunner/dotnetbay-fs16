using DotNetBay.Model.BO;

namespace DotNetBay.Model.EventArgs
{
    public class AuctionEventArgs : System.EventArgs
    {
        public Auction Auction { get; set; }

        public bool IsSuccessful { get; set; }
    }
}