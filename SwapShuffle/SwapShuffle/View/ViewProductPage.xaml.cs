using Newtonsoft.Json;
using SwapShuffle.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewProductPage : ContentPage
    {
        public ObservableCollection<Product> products;

        public ObservableCollection<Category> categories;

        //public ObservableCollection<Product> Obj;
        public ViewProductPage()
        {

            InitializeComponent();


            //ViewProductList.ItemsSource = products;

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
                var uri = "http://192.168.0.5/ss/Product/GetAll";
                //client.GetStringAsync()
                var result = await client.GetStringAsync(uri);

                //handling the answer  
                var ProductList = JsonConvert.DeserializeObject<List<Product>>(result);

                products = new ObservableCollection<Product>(ProductList);

                var uri1 = "http://192.168.0.5/ss/Category/GetAll";
                //client.GetStringAsync()
                var result1 = await client.GetStringAsync(uri1);
                //handling the answer  
                var CatList = JsonConvert.DeserializeObject<List<Category>>(result1);

                categories = new ObservableCollection<Category>(CatList);

                //query
                var list = from p in products
                           join
                            c in categories on p.Cid equals c.Cid into product
                           select product;

                //IsRefreshing = false;
            }

            ViewProductList.ItemsSource = null;
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

            ViewProductList.ItemsSource = products;

            //MessagingCenter.Subscribe<Product>(this, "EditProduct1", (value) => {
            //    //Product p = value;

            //    if (value != null)
            //    {

            //        Product s = products.FirstOrDefault(k => k.Pid == value.Pid);
            //        int i = products.IndexOf(s);
            //        //products[i]
            //        products[i].Name = value.Name;
            //        products[i].Cid = value.Cid;
            //        products[i].p_description = value.p_description;
            //        products[i].price = value.price;
            //        products[i].p_datetime = value.p_datetime;
            //        //products.Add(s);
            //    }
            //    MessagingCenter.Unsubscribe<Product>(this, "EditProduct1");
            //});

            //MessagingCenter.Subscribe<Product>(this, "AddProduct1", (value) => {
            //    products.Add((Product)value);
            //    MessagingCenter.Unsubscribe<Product>(this, "AddProduct1");
            //});

            //MessagingCenter.Subscribe<Product>(this, "DeleteProduct1", (value) => {
            //    products.Remove((Product)value);
            //    MessagingCenter.Unsubscribe<Product>(this, "DeleteProduct1");
            //});


            //ProductList.ItemsSource = DependencyService.Get<ISQLite>().GetProduct();
        }

        private void ViewProductList_ItemTapped(object sender, ItemTappedEventArgs e)
        {            
            Product details = e.Item as Product;
            if (details != null)
            {
                Navigation.PushAsync(new ViewProductDetailPage(details));
            }
        }
        
    }
}