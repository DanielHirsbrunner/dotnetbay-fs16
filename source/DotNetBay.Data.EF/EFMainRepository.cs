using System;
using System.Linq;
using DotNetBay.Interfaces;
using DotNetBay.Model.BO;

namespace DotNetBay.Data.EF
{
    public class EFMainRepository : IMainRepository {
        private readonly object syncRoot = new object();
        private readonly DotNetBayDBContext db;

        public EFMainRepository() {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            this.db = new DotNetBayDBContext();
        }

        public IQueryable<Auction> GetAuctions() {
            return this.db.Auctions;
        }

        public IQueryable<Member> GetMembers() {
            return this.db.Members;
        }
        public Auction Add(Auction auction) {
            if (auction == null) {
                throw new ArgumentNullException("auction");
            }

            if (auction.Seller == null) {
                throw new ArgumentException("Its required to set a seller");
            }

            lock (this.syncRoot) {
                // Add Member (from Seller) if not yet exists
                var seller = this.db.Members.FirstOrDefault(m => m.UniqueId == auction.Seller.UniqueId);

                // Create member as seller if not exists
                if (seller == null) {
                    // The seller does not yet exist in store
                    seller = auction.Seller;
                    this.db.Members.Add(seller);
                }

                //this.ThrowForInvalidReferences(auction);

                if (this.db.Auctions.Any(a => a.Id == auction.Id)) {
                    return auction;
                }

                this.db.Auctions.Add(auction);

                // Add auction to sellers list of auctions
                if (seller.Auctions.All(a => a.Id != auction.Id)) {
                    seller.Auctions.Add(auction);
                }

                return auction;
            }
        }

        public Member Add(Member member) {
            lock (this.syncRoot) {

                if (this.db.Members.Any(m => m.UniqueId == member.UniqueId)) {
                    return member;
                }

                //this.ThrowForInvalidReferences(member);

                this.db.Members.Add(member);

                if (member.Auctions != null && member.Auctions.Any()) {
                    foreach (var auction in member.Auctions) {
                        this.Add(auction);
                    }
                }

                return member;
            }
        }

        public Auction Update(Auction auction) {
            if (auction == null) {
                throw new ArgumentNullException("auction");
            }

            if (auction.Seller == null) {
                throw new ArgumentException("Its required to set a seller");
            }

            lock (this.syncRoot) {
                if (this.db.Auctions.All(a => a.Id != auction.Id)) {
                    throw new Exception("This auction does not exist and cannot be updated!");
                }

                //this.ThrowForInvalidReferences(auction);

                foreach (var bid in auction.Bids) {
                    bid.Auction = auction;

                    if (!this.db.Bids.Contains(bid)) {
                        this.db.Bids.Add(bid);
                    }
                }

                return auction;
            }
        }

        public Bid Add(Bid bid) {
            if (bid == null) {
                throw new ArgumentNullException("bid");
            }

            if (bid.Bidder == null) {
                throw new ArgumentException("Its required to set a bidder");
            }

            if (bid.Auction == null) {
                throw new ArgumentException("Its required to set an auction");
            }

            lock (this.syncRoot) {
                // Does the auction exist?
                if (this.db.Auctions.All(a => a.Id != bid.Auction.Id)) {
                    throw new Exception("This auction does not exist an cannot be added this way!");
                }

                // Does the member exist?
                if (this.db.Members.All(a => a.UniqueId != bid.Bidder.UniqueId)) {
                    throw new Exception("the bidder does not exist and cannot be added this way!");
                }

               // this.ThrowForInvalidReferences(bid);
               
                bid.Accepted = null;
                bid.TransactionId = Guid.NewGuid();

                this.db.Bids.Add(bid);

                // Reference back from auction
                var auction = this.db.Auctions.FirstOrDefault(a => a.Id == bid.Auction.Id);
                auction.Bids.Add(bid);

                // Reference back from bidder
                var bidder = this.db.Members.FirstOrDefault(b => b.UniqueId == bid.Bidder.UniqueId);
                if (bidder.Bids != null && !bidder.Bids.Contains(bid)) {
                    bidder.Bids.Add(bid);
                }

                return bid;
            }
        }

        public Bid GetBidByTransactionId(Guid transactionId) {
            lock (this.syncRoot) {
                return this.db.Bids.FirstOrDefault(b => b.TransactionId == transactionId);
            }
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }
    }
}
