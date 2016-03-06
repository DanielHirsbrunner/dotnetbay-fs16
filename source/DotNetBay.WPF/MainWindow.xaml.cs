using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Controls.Ribbon;
using DotNetBay.Core;
using DotNetBay.Model;

namespace DotNetBay.WPF {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow {
        public MainWindow() {
            this.InitializeComponent();
            this.DataContext = this;
            this.CurrentMember = ServiceDirectory.GetInstance.GetMemberService.GetCurrentMember();
            this.Auctions = new ObservableCollection<Auction>(ServiceDirectory.GetInstance.GetAuctionService.GetAll());
        }

        public ObservableCollection<Auction> Auctions { get; }
        public Auction CurrentAuction { get; set; }
        public Member CurrentMember { get; set; }

        private void BtnBieten_Click(object sender, RoutedEventArgs e) {
            // View Expense Report
            BidView bid = new BidView(this.CurrentAuction);
            bid.ShowDialog();
        }

        private void BtnBearbeiten_Click(object sender, RoutedEventArgs e) {
            // View Expense Report
            SellView edit = new SellView(this.CurrentAuction);
            edit.ShowDialog();
        }
        private void NeueAuktion_Click(object sender, RoutedEventArgs e) {
            // View Expense Report
            SellView sell = new SellView(new Auction());
            sell.ShowDialog();
        }
    }
}
