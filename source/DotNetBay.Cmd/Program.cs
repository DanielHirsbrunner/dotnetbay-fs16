using System;
using System.Linq;

using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Interfaces;

namespace DotNetBay.Cmd
{
    /// <summary>
    /// Main Entry for program
    /// </summary>
    public static class Program {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA2204:Literals should be spelled correctly", MessageId = "AuctionRunner",
            Justification = "Reviewed. This is fine here.")]
        public static void Main() {
            Console.WriteLine("DotNetBay Commandline");

            IAuctionRunner auctionRunner = null;

            try {
                var store = ServiceLocator.GetInstance.GetMainRepository;
                var auctionService = ServiceLocator.GetInstance.GetAuctionService;

                auctionRunner = AuctionRunner.GetInstance(store);

                Console.WriteLine("Started AuctionRunner");
                auctionRunner.Start();

                var allAuctions = auctionService.GetAll();

                Console.WriteLine("Found {0} auctions returned by the service.", allAuctions.Count());

                Console.Write("Press enter to quit");
                Console.ReadLine();
            } finally {
                auctionRunner?.Dispose();

            }

            Environment.Exit(0);
        }
    }
}
