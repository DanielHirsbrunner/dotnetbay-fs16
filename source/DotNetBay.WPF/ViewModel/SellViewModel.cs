using System;
using System.Windows;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Model.BO;
using Microsoft.Win32;
using MvvmBasic;

namespace DotNetBay.WPF.ViewModel {
    public class SellViewModel : ViewModelBase {
        
        public SellViewModel(Auction auction) {
            this.Auction = auction;

        }

        #region Public Properties

        public Auction Auction { get; set; }

        public string ImportNewImagePath { get; set; }

        public RelayCommand<Window> CloseWindowCommand => new RelayCommand<Window>(this.closeWindow);

        public RelayCommand<Window> SaveAuctionCommand => new RelayCommand<Window>(this.saveAuction);

        public ICommand LoadPictureCommand => new RelayCommand(this.loadPicture);

        public ICommand OpenPictureCommand => new RelayCommand(this.openPicture);

        #endregion

        #region Private Methoden

        private void openPicture() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                this.ImportNewImagePath = openFileDialog.FileName;
                this.RaisePropertyChanged(nameof(this.ImportNewImagePath));
            }
        }

        private void loadPicture() {
            this.Auction.Image = UIHelper.GetImageAsByteArray(this.ImportNewImagePath);
            this.RaisePropertyChanged(nameof(this.Auction));
        }

        private void closeWindow(Window window) {
            window?.Close();
        }

        private void saveAuction(Window window) {
            var me = ServiceLocator.GetInstance.GetMemberService.GetCurrentMember();
            this.Auction.Seller = me;
            if (Math.Abs(this.Auction.CurrentPrice) < 0.01) {
                this.Auction.CurrentPrice = this.Auction.StartPrice;
            }
            ServiceLocator.GetInstance.GetAuctionService.Save(this.Auction);
            this.closeWindow(window);
        }

        #endregion
    }
}
