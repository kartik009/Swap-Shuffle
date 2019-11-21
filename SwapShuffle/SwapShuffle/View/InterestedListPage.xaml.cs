
using SwapShuffle.Helper;
using SwapShuffle.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InterestedListPage : ContentPage
    {
        private ObservableCollection<Product> products;

        public ObservableCollection<InterestedTable> interesteds;

        int count;

        public InterestedListPage()
        {
            InitializeComponent();

            
        }

        protected override void OnAppearing()
        {
            PopulateProductList();
        }
        public void PopulateProductList()
        {
            InterestedProductList.ItemsSource = null;
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

            //var p = products.Where(s => s.Uid == UserSettings.Uid);
            InterestedProductList.ItemsSource = products;

            interesteds = new ObservableCollection<InterestedTable>()
            {
                new InterestedTable{Iid = 10001, Uid=201812046,Pid=102},
                new InterestedTable{Iid = 10002, Uid=201812046,Pid=103},
                new InterestedTable{Iid = 10003, Uid=201812044,Pid=102}

            };

            var re = from p in products
                     join i in interesteds
                     on p.Pid equals i.Pid
                     group i by i.Uid into newi
                     select newi;

            //Problem still getting
            re.Count();
        }

        private void ViewUSerList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Product details = e.Item as Product;

            if (details != null)
            {
                Navigation.PushAsync(new InterestedUser(details));
            }
        }

    }
}