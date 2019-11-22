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
    public partial class InterestedUser : ContentPage
    {
        public ObservableCollection<InterestedTable> interesteds;
        private ObservableCollection<Product> products;
        private List<User> users;

        public InterestedUser(Product p)
        {
            InitializeComponent();
            if(p!= null)
            {
                this.BindingContext = p;
                //var re = interesteds.Where(s => s.Pid == p.Pid);
                //Interested through get Product and User who are intereseted 

            }

            users = new List<User>()
            {
                new User{ Uid = 201812043, Name ="test", Pass = "test" },

                new User{ Uid = 201812046, Name ="admin", Pass = "admin" },
            };

            this.Title = "Interested User";

            UserList.ItemsSource = users;
        }

        private void UserList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //User Detail
        }

        public void Sell(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            long ID = Convert.ToInt64(button.CommandParameter.ToString());

            //Get User


            //Entry in Histroy 

            DisplayAlert("Message", "Do you want to Sell Product To "+ID+"?", "Ok");

            Navigation.PopToRootAsync();
        }
    }
}