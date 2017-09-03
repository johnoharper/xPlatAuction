using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace xPlatAuction.ViewModels
{
    public class AuctionsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public AuctionsViewModel(INavigation navigation) :base(navigation)
        {}


        private ObservableCollection<MyAuctionItem> myItems;

        public ObservableCollection<MyAuctionItem> MyAuctionItems
        {
            get { return myItems; }
            set { 
                myItems = value;
                NotifyPropertyChanged("MyAuctionItems");
            }
        }

        private ObservableCollection<Auction> auctionList;

        public ObservableCollection<Auction> Auctions {
            get { return auctionList; }
            set
            {
                auctionList = value;
                NotifyPropertyChanged("Auctions");
            }
        }

        public void Load()
        {
            //escape if already loaded
            if (Auctions != null)
                return;
				
            IsLoading = true;
            App.GetAuctionService().GetAuctions().ContinueWith((ta => {
                if(ta.Exception == null)
                {
                    var auctionResults = ta.Result;
                    Auctions = new ObservableCollection<Auction>(auctionResults);
                }
                else
                {
                    //alert to exception
                }
                IsLoading = false;
            }));

            App.GetAuctionService().GetMyItems().ContinueWith(gmit => {
                if(gmit.Exception == null)
                {
                    MyAuctionItems = new ObservableCollection<MyAuctionItem>(gmit.Result);
                }
                else
                {
                    //Notify of error
                }
            });
        }

    }
}
