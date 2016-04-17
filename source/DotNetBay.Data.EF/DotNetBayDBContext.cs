using System.Data.Entity.ModelConfiguration.Conventions;
using DotNetBay.Model.BO;

namespace DotNetBay.Data.EF {
    using System.Data.Entity;

    public class DotNetBayDBContext : DbContext {
        // Der Kontext wurde für die Verwendung einer DotNetBayDBContext-Verbindungszeichenfolge aus der 
        // Konfigurationsdatei ('App.config' oder 'Web.config') der Anwendung konfiguriert. Diese Verbindungszeichenfolge hat standardmäßig die 
        // Datenbank 'DotNetBay.Data.EF.DotNetBayDBContext' auf der LocalDb-Instanz als Ziel. Wenn Sie eine andere Datenbank und/oder einen anderen Anbieter als Ziel verwenden möchten, ändern Sie die DotNetBayDBContext-Zeichenfolge 
        // in der Anwendungskonfigurationsdatei.
        public DotNetBayDBContext()
            : base("DatabaseConnection") {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add(new DateTime2Convention());

            //-------------------------------
            //modelBuilder.Entity<Bid>().HasRequired(b => b.Auction).WithMany(a => a.Bids);
            //modelBuilder.Entity<Bid>().HasOptional(b => b.Bidder).WithMany(a => a.Bids);
            //modelBuilder.Entity<Auction>().HasRequired(b => b.Seller).WithMany(a => a.Auctions);
            //-------------------------------

            modelBuilder.Entity<Auction>().HasRequired(a => a.Seller).WithMany(member => member.Auctions);
            modelBuilder.Entity<Auction>().HasMany(a => a.Bids).WithRequired(b => b.Auction);
            modelBuilder.Entity<Bid>().HasRequired(b => b.Bidder).WithMany(member => member.Bids);
        }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
    }
}