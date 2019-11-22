
using Newtonsoft.Json;
using SwapShuffle.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        //private ObservableCollection<User> users;

        public async Task<User> Getuser(long uid)
        {
            using (var client = new HttpClient())
            {
                // send a GET request  
                var uri = "http://192.168.0.5/ss/User/Get"+uid;
                //client.GetStringAsync()
                var result = await client.GetStringAsync(uri);

                //handling the answer  
                var UserList = JsonConvert.DeserializeObject<User>(result);

                //users = new ObservableCollection<User>(UserList);
                //IsRefreshing = false;
                return (User)UserList;
            }
        }

        public UserProfilePage(long uid)
        {
            if(uid ==0)
            {
                Navigation.PushModalAsync(new LogInPage());
            }
             
            InitializeComponent();
            //User user = new User();
            User user = Getuser(uid);

            Et_UNo.Text = user.PhNo.ToString();
            Et_UName.Text = user.Name;
            //Et_UNo.Text = Convert.ToString(user.PhNo);
            Et_PD.Text = user.Pass.ToString();
            Et_CPD.Text = user.Pass;

            this.Title = "Profile";
        }

        public async Task<string> EditData(User user)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("192.168.0.5:8080");

            string jsonData = JsonConvert.SerializeObject(user);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync("/ss/User/add", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();

            return result;

        }

        private void EditUser(object sender, EventArgs e)
        {
            //get 

            User user = new User();

            user.Name = Et_UName.Text;

            user.PhNo = Convert.ToInt64(Et_UNo.Text);

            user.Pass = Et_PD.Text;


            //edit 

            var f = EditData(user);
            if(f!=null)
            {
                Navigation.PopAsync();
            }

        }
    }
}