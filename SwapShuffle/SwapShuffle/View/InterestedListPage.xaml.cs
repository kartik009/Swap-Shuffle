using Newtonsoft.Json;
using SwapShuffle.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
        private ObservableCollection<InterestedTable> Interests;

        public InterestedListPage()
        {
            InitializeComponent();

            //lb_count.Text = " ";
            //Label label = (Label)InterestedProductList.FindByName("cnt");
            //label.Text = "1";
        }

        protected override void OnAppearing()
        {
            PopulateProductList();
        }
        public async void PopulateProductList()
        {
            using (var client = new HttpClient())
            {
                // send a GET request  
                var uri = "http://192.168.0.5/ss/product/GetAll";

                //client.GetStringAsync()
                var result = await client.GetStringAsync(uri);

                //handling the answer  
                var ProductList = JsonConvert.DeserializeObject<List<Product>>(result);

                products = new ObservableCollection<Product>(ProductList);
                //IsRefreshing = false;

                // send a GET request  
                var uri1 = "http://192.168.0.5/ss/request/GetAll";

                //client.GetStringAsync()
                var result1 = await client.GetStringAsync(uri1);

                //handling the answer  
                var InterestList = JsonConvert.DeserializeObject<List<InterestedTable>>(result1);

                Interests = new ObservableCollection<InterestedTable>(InterestList);
            }

            InterestedProductList.ItemsSource = null;
            //products = new ObservableCollection<Product>()
            //{
            //    new Product{Name="Cloud Computing Book",
            //        p_images= "cc1book.jpg",
            //        p_description="Cloud computing is all the rage, " +
            //        "allowing for the delivery of computing and storage capacity to a diverse community of end-recipients. However, before you can decide on a cloud model, you need to determine what the ideal cloud service model is for your business."
            //        ,p_datetime = DateTime.Now,                    
            //        p_status =false,Pid=102,Cid=11,Uid=1001,price=Convert.ToDecimal("250.00")},
            //    new Product{Name="Postgres Databsse Notes",
            //        p_images="notes2.jpg",
            //        p_description="This notes is help to you explain every details of lecturee and help you in exam"
            //        ,p_datetime = DateTime.Now,
            //        p_status =false,Pid=103,Cid=11,Uid=1001,price=Convert.ToDecimal("80.00")},
            //    new Product{Name="Itmp Class Notes",
            //        p_images="notes3.jpg",
            //        p_description="This notes is help to you explain every details of lecturee and help you in exam"
            //        ,p_datetime = DateTime.Now,
            //        p_status =false,Pid=103,Cid=11,Uid=1001,price=Convert.ToDecimal("68.00")},
            //    new Product{Name="The Great India",
            //        p_images="notes2.jpg",
            //        p_description="The Great Indian Novel is a satirical novel by Shashi Tharoor, first published by Viking Press in 1989. It is a fictional work that takes the story of the Mahabharata, the Indian epic, and recasts and resets it in the context of the Indian Independence Movement and the first three decades post-independence"
            //        ,p_datetime = DateTime.Now,
            //        p_status =false,Pid=103,Cid=11,Uid=1001,price=Convert.ToDecimal("126.00")}
            //};

            //var p = products.Where(s => s.Uid == UserSettings.Uid);
            InterestedProductList.ItemsSource = products;

            //interesteds = new ObservableCollection<InterestedTable>()
            //{
            //    new InterestedTable{Iid = 10001, Uid=201812046,Pid=102},
            //    new InterestedTable{Iid = 10002, Uid=201812046,Pid=103},
            //    new InterestedTable{Iid = 10003, Uid=201812044,Pid=102}

            //};

            var re = from p in products
                     join i in interesteds
                     on p.Pid equals i.Pid
                     group i by i.Uid into newi
                     select newi;

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