using SwapShuffle.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistroyPage : ContentPage
    {
        public ObservableCollection<order> orders;
        private ObservableCollection<Product> products;
        public HistroyPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            PopulateProductList();
        }
        public void PopulateProductList()
        {
            HistoryList.ItemsSource = null;
            products = new ObservableCollection<Product>()
            {
                new Product{Name="Science Book",
                    p_images= "book.png",
                    p_description="Book is Good",p_datetime = DateTime.Now,
                    p_status =false,Pid=102,Cid=11,Uid=1001,price=Convert.ToDecimal("1250.23")},
                new Product{Name="CC Book",
                    p_images="book.png",
                    p_description="CC Book is Good",p_datetime = DateTime.Now,
                    p_status =false,Pid=103,Cid=11,Uid=1001,price=Convert.ToDecimal("1450.23")},
            };
            HistoryList.ItemsSource = products;
            //here from order if bid he is in puid than purchase lb_status else sell 
            //lb_Status.Text = "Status : Purchased";
        }
        private void HistoryList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}