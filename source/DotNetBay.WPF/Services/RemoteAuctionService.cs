using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Interfaces;
using DotNetBay.Model.BO;

namespace DotNetBay.WPF.Services {
    public class RemoteAuctionService : IAuctionService {
        private HttpClient client;

        public RemoteAuctionService() {
            this.client = new HttpClient();    
            //client.req
        }

        public IQueryable<Auction> GetAll() {
            throw new NotImplementedException();
        }

        public Auction GetById(int id) {
            throw new NotImplementedException();
        }

        public Auction Save(Auction auction) {
            throw new NotImplementedException();
        }

        public Bid PlaceBid(Auction auction, double amount) {
            throw new NotImplementedException();
        }
    }
}
