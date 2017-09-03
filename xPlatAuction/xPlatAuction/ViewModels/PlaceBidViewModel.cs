using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace xPlatAuction.ViewModels
{
    public class PlaceBidViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private AuctionItem targetItem;

        public PlaceBidViewModel(AuctionItem item, INavigation navigation) : base(navigation)
        {
            targetItem = item;
			BidAmount = item.CurrentBid == 0 ? item.StartingBid : item.CurrentBid + 5;
            PlaceBidCommand = new DelegateCommand(ExecutePlaceBid, CanExecutePlaceBid);
        }
        

        private double amount;
        public double BidAmount { 
            get{return amount;}
            set {
                if (amount != value)
                {
                    amount = value;
                    NotifyPropertyChanged("BidAmount");
                }
            } 
        }


        public AuctionItem Item
        {
            get { return targetItem; }
            set
            {
                targetItem = value;
                NotifyPropertyChanged("Item");
            }
        }

        public ICommand PlaceBidCommand { get; set; }

        public async void ExecutePlaceBid(object parameter)
        {

            var newBid = await App.GetAuctionService().PlaceBid(
                new Bid { AuctionItemId = targetItem.Id, BidAmount = BidAmount });

            Item.CurrentBid = newBid.BidAmount;
            
            //move back to the page they were on before bidding
            await Navigation.PopAsync();

        }
        public bool CanExecutePlaceBid(object parameter)
        {
            return Item.CurrentBid < BidAmount;
        }
    }
}
