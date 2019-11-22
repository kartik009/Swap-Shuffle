using Newtonsoft.Json;
using SwapShuffle.Model;
using SwapShuffle.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUSerPage : ContentPage
    {
        EmailServices emailServices;
        public NewUSerPage()
        {
            InitializeComponent();

            var LoginPAge_Tap = new TapGestureRecognizer();
            LoginPAge_Tap.Tapped += (s, e) =>
            {

                Navigation.PushModalAsync(new LogInPage());
            };
            Lb_Login.GestureRecognizers.Add(LoginPAge_Tap);
        }

        public async Task<string> PostData(User user)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("192.168.0.5:8080");

            string jsonData = JsonConvert.SerializeObject(user);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/ss/User/add", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();

            return result;

        }
        private void Btn_NewUser_Clicked(object sender, EventArgs e)
        {
            string Subject = "Please Verify code";
            string Message = "" + "1234";
            string error = String.Empty;
            long id = long.Parse(Et_Id.Text);
            int id_len = (int)Math.Floor(Math.Log10(id) + 1);
            long ph = long.Parse(Et_Phno.Text);
            int ph_len = (int)Math.Floor(Math.Log10(ph) + 1);
            if (id_len == 9 && ph_len >= 10)
            {
                if (Et_name.Text != String.Empty && Et_pass.Text != String.Empty)
                {
                    DisplayAlert("App Name", "Ph No :" + ph, "Ok");
                    emailServices = new EmailServices();
                    bool flag = emailServices.SendVerifyCode(long.Parse(Et_Id.Text),Subject,Message);
                    //bool flag = true;
                    if (flag)
                    {
                        User user = new User();
                        user.Name = Et_name.Text;
                        user.Uid = Convert.ToInt64(Et_Id.Text);
                        user.PhNo = ph;
                        user.Pass = Et_pass.Text;

                        var f = PostData(user);
                        if(f!= null)
                            Navigation.PushModalAsync(new VerificationPage(long.Parse(Et_Id.Text), 1122));
                    }
                    else
                    {

                        DisplayAlert("S&S", "Email Wrong", "Ok");
                    }
                }
                else
                {
                    DisplayAlert("S&S", "NAme or Password is Empty", "Ok");
                }
            }
            else
            {
                DisplayAlert("S&S", "Input Numeric Data invalid", "Ok");
            }

        }
    }
}