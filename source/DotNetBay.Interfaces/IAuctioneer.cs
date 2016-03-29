using System;
using DotNetBay.Model.EventArgs;

namespace DotNetBay.Interfaces {
    public interface IAuctioneer
    {
        event EventHandler<ProcessedBidEventArgs> BidDeclined;

        event EventHandler<ProcessedBidEventArgs> BidAccepted;

        event EventHandler<AuctionEventArgs> AuctionEnded;

        event EventHandler<AuctionEventArgs> AuctionStarted;
    }
}