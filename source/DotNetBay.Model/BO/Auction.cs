using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DotNetBay.Model.BO {
    public class Auction {
        public Auction() {
            this.Bids = new List<Bid>();
        }

        public int Id { get; set; }

        [Display(Name = "Start Preis")]
        public double StartPrice { get; set; }

        [Display(Name = "Titel")]
        public string Title { get; set; }

        [Display(Name = "Beschreibung")]
        public string Description { get; set; }

        [Display(Name = "Akt. Preis")]
        public double CurrentPrice { get; set; }

        /// <summary> Gets or sets the UTC DateTime values to avoid wrong data when serializing the values </summary>
        [Display(Name = "Start Zeit")]
        public DateTime StartDateTimeUtc { get; set; } = DateTime.Today.AddDays(1);

        /// <summary>Gets or sets the UTC DateTime values to avoid wrong data when serializing the values</summary>
        [Display(Name = "End Zeit")]
        public DateTime EndDateTimeUtc { get; set; } = DateTime.Today.AddDays(15);

        /// <summary> Gets or sets the UTC DateTime values to avoid wrong data when serializing the values</summary>
        public DateTime CloseDateTimeUtc { get; set; }

        public bool IsClosed { get; set; }

        public bool IsRunning { get; set; }

        [Display(Name = "Bild")]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays",Justification = "Keep it as byte-array for compatibility reasons")]
        public byte[] Image { get; set; }

        public int SellerId { get; set; }

        [ForeignKey("SellerId")]
        public Member Seller { get; set; }

        public int? WinnerId { get; set; }

        [ForeignKey("WinnerId")]
        public Member Winner { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",Justification = "Cannot reomve setter, because needs to be accessible by ORM")]
        public ICollection<Bid> Bids { get; set; }

        public int? ActiveBidId { get; set; }

        [ForeignKey("ActiveBidId")]
        public Bid ActiveBid { get; set; }
    }
}