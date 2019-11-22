using SwapShuffle.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPassPage : ContentPage
    {
        EmailServices emailServices;
        public ForgetPassPage()
        {
            InitializeComponent();
            var LoginPAge_Tap = new TapGestureRecognizer();
            LoginPAge_Tap.Tapped += (s, e) =>
            {

                Navigation.PushModalAsync(new LogInPage());
            };
            Lb_Login.GestureRecognizers.Add(LoginPAge_Tap);
        }

        public async void GetUser()
        {
            //using (var client = new HttpClient())
            //{
            //    // send a GET request  
            //    var uri = "http://192.168.0.5/api/Masters/GetEmployees";
            //    //client.GetStringAsync()
            //    var result = await client.GetStringAsync(uri);

            //    //handling the answer  
            //    var ProductList = JsonConvert.DeserializeObject<List<Product>>(result);

            //    //Obj= new ObservableCollection<User>(ProductList);
            //    //IsRefreshing = false;
            //}

        }

        private void Bt_next_Clicked(object sender, EventArgs e)
        {
            long id = long.Parse(Et_id.Text);
            string Subject = "Please Verify code";
            string Message = "" + "1234";
            int id_len = (int)Math.Floor(Math.Log10(id) + 1);
            if (id_len == 9)
            {
                emailServices = new EmailServices();
                bool flag = emailServices.SendVerifyCode(long.Parse(Et_id.Text),Subject,Message);
                if (flag)
                {
                    Navigation.PushModalAsync(new VerificationPage(long.Parse(Et_id.Text), 1122));
                }
                else
                {
                    DisplayAlert("App NAme", "Email Wrong", "Ok");
                }
            }
            else
            {
                DisplayAlert("App NAme", "student is not valid", "Ok");
            }

        }
    }
}