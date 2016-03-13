using System.Linq;
using System.Windows.Input;
using DotNetBay.Core.Execution;
using DotNetBay.Model;
using DotNetBay.WPF.View;
using MvvmBasic;

namespace DotNetBay.WPF.ViewModel {
    public class AuctionViewModel : ViewModelBase {
        
        public AuctionViewModel(Auction auction) {
            this.CurrentAuction = auction;
            ServiceDirectory.GetInstance.GetAuctionRunner.Auctioneer.BidAccepted += this.auctioneer_BidAccepted;
            ServiceDirectory.GetInstance.GetAuctionRunner.Auctioneer.BidDeclined += this.auctioneer_BidDeclined;
        }

        #region Public Properties

        public Auction CurrentAuction { get; set; }

        public ICommand BidAuctionCommand => new RelayCommand(this.bidAuction);

        public ICommand EditAuctionCommand => new RelayCommand(this.editAuction);

        #endregion

        #region Private Methoden

        private void bidAuction() {
            BidView bid = new BidView { DataContext = new BidViewModel(this.CurrentAuction) };
            bid.ShowDialog();
            this.refreshCurrAuction();
        }

        private void editAuction() {
            SellView edit = new SellView { DataContext = new SellViewModel(this.CurrentAuction) };
            edit.ShowDialog();
            this.refreshCurrAuction();
        }

        private void refreshCurrAuction() {
            this.CurrentAuction = ServiceDirectory.GetInstance.GetAuctionService.GetAll().FirstOrDefault(x => x.Id == this.CurrentAuction.Id);
            this.RaisePropertyChanged(nameof(this.CurrentAuction));
        }

        #endregion

        #region EventMethoden

        private void auctioneer_BidDeclined(object sender, ProcessedBidEventArgs processedBidEventArgs) {
            this.refreshCurrAuction();
        }

        private void auctioneer_BidAccepted(object sender, ProcessedBidEventArgs processedBidEventArgs) {
            this.refreshCurrAuction();
        }

        #endregion
    }
}
 