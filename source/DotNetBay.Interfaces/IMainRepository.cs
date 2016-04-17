using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DotNetBay.Model.BO;

namespace DotNetBay.Interfaces
{
    /// <summary>
    /// Interface für den Storage Layer
    /// </summary>
    public interface IMainRepository
    {
        /// <summary>
        /// Liefert alle aktiven Auktionen zurück
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We would like this to keep this as is because calling this function actually queries the store")]
        IQueryable<Auction> GetAuctions();

        /// <summary>
        /// Liert alle registrierten Member zurück
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We would like this to keep this as is because calling this function actually queries the store")]
        IQueryable<Member> GetMembers();

        /// <summary>
        /// Fügt eine neue Auktion hinzu
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        Auction Add(Auction auction);

        /// <summary>
        /// Aktualisiert eine bestehende Auktion
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        Auction Update(Auction auction);

        /// <summary>
        /// Gibt ein Gebot für eine bestehende Auktion ab
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        Bid Add(Bid bid);

        Bid GetBidByTransactionId(Guid transactionId);

        Auction GetAuctionById(int id);

        Member Add(Member member);

        void SaveChanges();
    }
}