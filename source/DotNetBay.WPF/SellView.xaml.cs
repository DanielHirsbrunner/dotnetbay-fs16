using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetBay.Model;
using Microsoft.Win32;

namespace DotNetBay.WPF {
    /// <summary>
    /// Interaktionslogik für BidView.xaml
    /// </summary>
    public partial class SellView : Window, INotifyPropertyChanged {

        private string importNewImagePath = string.Empty;

        public SellView(Auction auction) {
            this.InitializeComponent();
            this.DataContext = this;
            this.Auction = auction;
        }

        public Auction Auction { get; set; }

        public String ImportNewImagePath {
            get { return this.importNewImagePath; }
            set {
                this.importNewImagePath = value;
                this.NotifyPropertyChanged();
            }
        } 

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                this.ImportNewImagePath = openFileDialog.FileName;
            }
        }

        private void BtnImportFile_Click(object sender, RoutedEventArgs e) {
            this.Auction.Image = UIHelper.GetImageAsByteArray(this.importNewImagePath);
            this.NotifyPropertyChanged(nameof(this.Auction));
            this.NotifyPropertyChanged(nameof(this.Auction.Image));
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String info = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e) {
            var me = ServiceDirectory.GetInstance.GetMemberService.GetCurrentMember();
            this.Auction.Seller = me;
            if (Math.Abs(this.Auction.CurrentPrice) < 0.01) {
                this.Auction.CurrentPrice = this.Auction.StartPrice;
            }
            ServiceDirectory.GetInstance.GetAuctionService.Save(this.Auction);
            this.Close();
        }
    }
}
