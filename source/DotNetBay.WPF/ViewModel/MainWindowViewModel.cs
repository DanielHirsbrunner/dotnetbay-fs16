using System.Collections.ObjectModel;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Model.BO;
using DotNetBay.Model.EventArgs;
using DotNetBay.WPF.View;
using MvvmBasic;

namespace DotNetBay.WPF.ViewModel {

    public class MainWindowViewModel : ViewModelBase {
        
        public MainWindowViewModel() {
            this.CurrentMember = ServiceLocator.GetInstance.GetMemberService.GetCurrentMember();
            ServiceLocator.GetInstance.GetAuctionRunner.Auctioneer.AuctionStarted += this.auctioneer_AuctionStarted;
            ServiceLocator.GetInstance.GetAuctionRunner.Auctioneer.AuctionEnded += this.auctioneer_AuctionEnded;
            this.loadAuctions();
        }

        #region Public Properties

        public ObservableCollection<AuctionViewModel> Auctions { get; private set; }

        public Member CurrentMember { get; set; }

        public ICommand NewAuctionCommand => new RelayCommand(this.newAuction);

        #endregion

        #region Private Methoden

        private void newAuction() {
            SellView sell = new SellView { DataContext = new SellViewModel(new Auction()) };
            sell.ShowDialog();
            this.loadAuctions();
        }

        private void loadAuctions() {
            this.Auctions = new ObservableCollection<AuctionViewModel>();
            var allAuctions = ServiceLocator.GetInstance.GetAuctionService.GetAll();
            foreach (var auction in allAuctions) {
                var auctionVm = new AuctionViewModel(auction);
                this.Auctions.Add(auctionVm);
            }
            //var auctionViewModels = from auction in ServiceLocator.GetInstance.GetAuctionService.GetAll()
            //                        select new AuctionViewModel(auction);
            //this.Auctions = new ObservableCollection<AuctionViewModel>(auctionViewModels);
            this.RaisePropertyChanged(nameof(this.Auctions));
        }

        #endregion

        #region EventMethoden

        private void auctioneer_AuctionStarted(object sender, AuctionEventArgs auctionEventArgs) {
            this.loadAuctions();
        }

        private void auctioneer_AuctionEnded(object sender, AuctionEventArgs auctionEventArgs) {
            this.loadAuctions();
        }

        #endregion
    }
}
