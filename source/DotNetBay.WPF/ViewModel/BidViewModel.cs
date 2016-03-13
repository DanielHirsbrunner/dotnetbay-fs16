using System.Windows;
using DotNetBay.Model;
using MvvmBasic;

namespace DotNetBay.WPF.ViewModel {
    public class BidViewModel : ViewModelBase {
        
        public BidViewModel(Auction auction) {
            this.NewBid = new Bid() { Auction = auction, Bidder = ServiceDirectory.GetInstance.GetMemberService.GetCurrentMember() };
        }

        #region Public Properties

        public Bid NewBid { get; set; }

        public RelayCommand<Window> CloseWindowCommand => new RelayCommand<Window>(this.closeWindow);

        public RelayCommand<Window> SaveBidCommand => new RelayCommand<Window>(this.saveBid);

        #endregion

        #region Private Methoden

        private void closeWindow(Window window) {
            window?.Close();
        }

        private void saveBid(Window window) {
            ServiceDirectory.GetInstance.GetAuctionService.PlaceBid(this.NewBid.Auction, this.NewBid.Amount);
            this.closeWindow(window);
        }

        #endregion
    }
}
