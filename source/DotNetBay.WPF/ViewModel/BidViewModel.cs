using System.Windows;
using DotNetBay.Core;
using DotNetBay.Model.BO;
using MvvmBasic;

namespace DotNetBay.WPF.ViewModel {
    public class BidViewModel : ViewModelBase {
        
        public BidViewModel(Auction auction) {
            this.NewBid = new Bid() { Auction = auction, Bidder = ServiceLocator.GetInstance.GetMemberService.GetCurrentMember() };
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
            ServiceLocator.GetInstance.GetAuctionService.PlaceBid(this.NewBid.Auction, this.NewBid.Amount);
            this.closeWindow(window);
        }

        #endregion
    }
}
