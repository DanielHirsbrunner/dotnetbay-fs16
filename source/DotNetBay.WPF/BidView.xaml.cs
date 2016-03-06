using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class BidView : Window {
        
        public BidView(Auction auction) {
            this.InitializeComponent();
            this.NewBid = new Bid() {Auction = auction, Bidder = ServiceDirectory.GetInstance.GetMemberService.GetCurrentMember()};
            this.DataContext = this;
        }

        public Bid NewBid { get; set; }

        private void BtnGebotAbgeben_Click(object sender, RoutedEventArgs e) {
            ServiceDirectory.GetInstance.GetAuctionService.PlaceBid(this.NewBid.Auction, this.NewBid.Amount);
            this.Close();
        }

        private void BtnAbbrechen_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
