using SwapShuffle.Services;
using System;

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
                        Navigation.PushModalAsync(new VerificationPage(long.Parse(Et_Id.Text), 1122));
                    }
                    else
                    {

                        DisplayAlert("App NAme", "Email Wrong", "Ok");
                    }
                }
                else
                {
                    DisplayAlert("App NAme", "NAme or Password is Empty", "Ok");
                }
            }
            else
            {
                DisplayAlert("App NAme", "Input Numeric Data invalid", "Ok");
            }

        }
    }
}