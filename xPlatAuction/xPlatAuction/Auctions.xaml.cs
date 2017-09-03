using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;
using xPlatAuction.ViewModels;
using Xamarin.Forms;

namespace xPlatAuction
{
    public partial class Auctions
    {
        public Auctions()
        {
            InitializeComponent();
            this.BindingContext = new AuctionsViewModel(this.Navigation);
        }

       
        protected override void OnAppearing()
        {
           base.OnAppearing();
           ((AuctionsViewModel)BindingContext).Load();
        }

        protected void Auction_Tapped(object sender, ItemTappedEventArgs e)
        {
            Auction auction = e.Item as Auction;
            Navigation.PushAsync(
                new AuctionItems(auction));
        }

        protected void MyItem_Tapped(object sender, ItemTappedEventArgs e)
        {
            MyAuctionItem item = e.Item as MyAuctionItem;

            var targetItem = new AuctionItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CurrentBid = item.CurrentBid
            };

            Navigation.PushAsync(new PlaceBid(targetItem));
        }
         
    }
}
