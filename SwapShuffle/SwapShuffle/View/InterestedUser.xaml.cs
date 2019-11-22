using Newtonsoft.Json;
using SwapShuffle.Helper;
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
    public partial class InterestedUser : ContentPage
    {
        public ObservableCollection<InterestedTable> interesteds;
        private ObservableCollection<Product> products;
        private ObservableCollection<User> users;

        public Product pt;

        public async Task<ObservableCollection<User>> Getuser()
        {
            using (var client = new HttpClient())
            {
                // send a GET request  
                var uri = "http://192.168.0.5/ss/User/All";
                //client.GetStringAsync()
                var result = await client.GetStringAsync(uri);

                //handling the answer  
                var UserList = JsonConvert.DeserializeObject<List<User>>(result);

                users = new ObservableCollection<User>(UserList);
                //IsRefreshing = false;
                return users;
            }
        }

        public InterestedUser(Product p)
        {
            InitializeComponent();
            if(p!= null)
            {
                this.BindingContext = p;
                //var re = interesteds.Where(s => s.Pid == p.Pid);
                //Interested through get Product and User who are intereseted 
                pt = p;
            }

            //users = new List<User>()
            //{
            //    new User{ Uid = 201812043, Name ="Kartik Lakhani", Pass = "test" },

            //    new User{ Uid = 201812046, Name ="Alvis", Pass = "test" },

            //    new User{ Uid = 201812044, Name ="Brij", Pass = "test" },

            //    new User{ Uid = 201812042, Name ="Jayswee", Pass = "test" },

            //     new User{ Uid = 201812109, Name ="Bhautik", Pass = "test" }
            //};

            users = Getuser();
            this.Title = "Interested User";

            UserList.ItemsSource = users;
        }

        public async Task<string> SoldItem(order order)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("192.168.0.5:8080");

            string jsonData = JsonConvert.SerializeObject(order);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/ss/order/add", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        private void UserList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //User Detail

        }

        public async void Sell(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            long ID = Convert.ToInt64(button.CommandParameter.ToString());

            //Get User


            //Entry in Histroy 

            bool res = await DisplayAlert("Message", "Do you want to Sell Product To "+ID+"?", "Ok","cancel");
            if(res)
            {
                order order = new order();
                order.Pid = pt.Pid;
                order.Sid = UserSettings.Uid;
                order.Puid = ID;
                order.datetime = DateTime.Now;

                var f = SoldItem(order);
                if(f!=null)
                    await Navigation.PopToRootAsync();
            }

           
        }
    }
}