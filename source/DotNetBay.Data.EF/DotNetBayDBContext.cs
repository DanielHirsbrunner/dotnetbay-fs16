using System.Data.Entity.ModelConfiguration.Conventions;
using DotNetBay.Model.BO;

namespace DotNetBay.Data.EF {
    using System.Data.Entity;

    public class DotNetBayDBContext : DbContext {
        // Der Kontext wurde für die Verwendung einer DotNetBayDBContext-Verbindungszeichenfolge aus der 
        // Konfigurationsdatei ('App.config' oder 'Web.config') der Anwendung konfiguriert. Diese Verbindungszeichenfolge hat standardmäßig die 
        // Datenbank 'DotNetBay.Data.EF.DotNetBayDBContext' auf der LocalDb-Instanz als Ziel. 
        // 
        // Wenn Sie eine andere Datenbank und/oder einen anderen Anbieter als Ziel verwenden möchten, ändern Sie die DotNetBayDBContext-Zeichenfolge 
        // in der Anwendungskonfigurationsdatei.
        public DotNetBayDBContext()
            : base("DatabaseConnection") {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Configure the primary Key for the OfficeAssignment 
            //modelBuilder.Entity<Auction>()
            //    .HasKey(t => t.Seller);

            //modelBuilder.Entity<Auction>()
            //    .HasRequired(w => w.ActiveBid)
            //    .WithMany()
            //    .Map(m => m.MapKey("Id"));

            //modelBuilder.Entity<Auction>()
            //    .HasRequired(w => w.Bids)
            //    .WithMany()
            //    .Map(m => m.MapKey("Id"));

            modelBuilder.Entity<Bid>()
            .HasRequired(b => b.Auction)
            .WithMany(a => a.Bids);

            modelBuilder.Entity<Bid>()
            .HasOptional(b => b.Bidder)
            .WithMany(a => a.Bids);

            modelBuilder.Entity<Auction>()
            .HasRequired(b => b.Seller)
            .WithMany(a => a.Auctions);

            //modelBuilder.Entity<Auction>()
            //.HasRequired(b => b.Winner)
            //.WithMany(a => a.Auctions);


            //modelBuilder.Entity<Auction>()
            //.HasRequired(b => b.Seller)
            //.WithMany(a => a.Auctions);
        }

        // Fügen Sie ein 'DbSet' für jeden Entitätstyp hinzu, den Sie in das Modell einschließen möchten. Weitere Informationen 
        // zum Konfigurieren und Verwenden eines Code First-Modells finden Sie unter 'http://go.microsoft.com/fwlink/?LinkId=390109'.
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
    }
}