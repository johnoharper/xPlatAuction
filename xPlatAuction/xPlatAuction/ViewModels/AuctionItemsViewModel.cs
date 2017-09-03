using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xPlatAuction.ViewModels
{
    public class AuctionItemsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private Auction auction;
        private ObservableCollection<AuctionItem> items;

        public AuctionItemsViewModel(Auction selectedAuction, INavigation navigation) : base(navigation)
        {
            auction = selectedAuction;
        }

      
        public ObservableCollection<AuctionItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyPropertyChanged("Items");
            }
        }

        public void Load()
        {
            //escape if already loaded
            if (Items != null)
                return;

            IsLoading = true;
            
            App.GetAuctionService().GetItems(auction.Id).ContinueWith(
                (ait) =>
                {
                    if(ait.Exception == null)
                    {
                        Items = new ObservableCollection<AuctionItem>(ait.Result);
                    }
                    else
                    {
                        //handle exception
                    }

                    IsLoading = false;
                });
        }

    }
}
