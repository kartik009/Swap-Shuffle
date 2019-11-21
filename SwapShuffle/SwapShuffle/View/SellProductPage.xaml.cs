
using SwapShuffle.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        public void PopulateProductList()
        {
            SellProductList.ItemsSource = null;
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

            MessagingCenter.Subscribe<Product>(this, "EditProduct", (value) => {
                //Product p = value;

                if (value != null)
                {
                    
                    Product s = products.FirstOrDefault(k => k.Pid == value.Pid);
                    int i = products.IndexOf(s);
                    //products[i]
                    products[i].Name = value.Name;
                    products[i].Cid = value.Cid;
                    products[i].p_description = value.p_description;
                    products[i].price = value.price;
                    products[i].p_datetime = value.p_datetime;
                    //products.Add(s);
                }
                MessagingCenter.Unsubscribe<Product>(this, "EditProduct");
            });

            MessagingCenter.Subscribe<Product>(this, "AddProduct", (value) => {
                products.Add((Product)value);
                MessagingCenter.Unsubscribe<Product>(this, "AddProduct");
            });


            SellProductList.ItemsSource = products;

            //ProductList.ItemsSource = DependencyService.Get<ISQLite>().GetProduct();
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
            bool res = await DisplayAlert("Message", "Do you want to delete employee?", "Ok", "Cancel");
            if (res)
            {
                var menu = sender as MenuItem;
                Product details = menu.CommandParameter as Product;
                //DependencyService.Get<ISQLite>().DeleteProduct(details.Id);
                products.Remove(details);
                MessagingCenter.Send<Product>(details, "DeleteProduct1");
                PopulateProductList();
            }
        }
    }
}