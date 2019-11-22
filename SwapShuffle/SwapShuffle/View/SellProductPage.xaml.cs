
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
    public partial class SellProductPage : ContentPage
    {
        public ObservableCollection<Product> products;
        public SellProductPage()
        {
            InitializeComponent();
         
        }


        protected override void OnAppearing()
        {
            PopulateProductList();
        }
        public async void PopulateProductList()
        {
            SellProductList.ItemsSource = null;

            using (var client = new HttpClient())
            {
                // send a GET request  
                var uri = "http://192.168.0.5/ss/Product/GetAll";
                //client.GetStringAsync()
                var result = await client.GetStringAsync(uri);

                //handling the answer  
                var ProductList = JsonConvert.DeserializeObject<List<Product>>(result);

                products = new ObservableCollection<Product>(ProductList);
                
                //IsRefreshing = false;
            }

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


            //MessagingCenter.Subscribe<Product>(this, "EditProduct", (value) => {
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
            //    MessagingCenter.Unsubscribe<Product>(this, "EditProduct");
            //});

            //MessagingCenter.Subscribe<Product>(this, "AddProduct", (value) => {
            //    products.Add((Product)value);
            //    MessagingCenter.Unsubscribe<Product>(this, "AddProduct");
            //});


            SellProductList.ItemsSource = products;

            //ProductList.ItemsSource = DependencyService.Get<ISQLite>().GetProduct();
        }

        public async Task<string> DelData(Product product)
        {
            var client = new HttpClient();
            //client.BaseAddress = new Uri("192.168.0.5:8080");

            string jsonData = JsonConvert.SerializeObject(product);

            //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri("192.168.0.5:8080/ss/Product/delete")
            };
            HttpResponseMessage response = await client.SendAsync(request);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }


        private void AddProduct(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProduct());
        }

        private void EditSellProduct_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Product details = e.Item as Product;
            if (details != null)
            {
                Navigation.PushAsync(new EditProduct(details));
            }
        }

        private async void DeleteProduct(object sender, EventArgs e)
        {
            bool res = await DisplayAlert("Message", "Do you want to delete Product?", "Ok", "Cancel");
            if (res)
            {
                var menu = sender as MenuItem;
                Product details = menu.CommandParameter as Product;
                //DependencyService.Get<ISQLite>().DeleteProduct(details.Id);
                //products.Remove(details);

                var f =DelData(details);
                if (f != null)
                {
                    //MessagingCenter.Send<Product>(details, "DeleteProduct1");
                    PopulateProductList();
                }
            }
        }
    }
}