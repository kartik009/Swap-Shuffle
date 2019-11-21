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
    public partial class ViewProductPage : ContentPage
    {
        public ObservableCollection<Product> products;
        public ViewProductPage()
        {

            InitializeComponent();


            //ViewProductList.ItemsSource = products;

        }

        protected override void OnAppearing()
        {
            PopulateProductList();
        }
        public void PopulateProductList()
        {
            ViewProductList.ItemsSource = null;
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

            ViewProductList.ItemsSource = products;

            MessagingCenter.Subscribe<Product>(this, "EditProduct1", (value) => {
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
                MessagingCenter.Unsubscribe<Product>(this, "EditProduct1");
            });

            MessagingCenter.Subscribe<Product>(this, "AddProduct1", (value) => {
                products.Add((Product)value);
                MessagingCenter.Unsubscribe<Product>(this, "AddProduct1");
            });

            MessagingCenter.Subscribe<Product>(this, "DeleteProduct1", (value) => {
                products.Remove((Product)value);
                MessagingCenter.Unsubscribe<Product>(this, "DeleteProduct1");
            });


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