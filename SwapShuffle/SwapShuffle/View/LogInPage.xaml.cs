using SwapShuffle.Helper;
using SwapShuffle.Model;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        List<User> users;
        public LogInPage()
        {
            InitializeComponent();
            users = new List<User>()
            {
                new User{ Uid = 201812043, Name ="test", Pass = "test" },

                new User{ Uid = 201812046, Name ="admin", Pass = "admin" },
            };

            var ForgetPass_Tap = new TapGestureRecognizer();
            ForgetPass_Tap.Tapped += (s, e) =>
            {
                Navigation.PushModalAsync(new ForgetPassPage());
            };
            sp_forgetPass.GestureRecognizers.Add(ForgetPass_Tap);



            var NewUSer_Tap = new TapGestureRecognizer();
            NewUSer_Tap.Tapped += (s, e) =>
            {

                Navigation.PushModalAsync(new NewUSerPage());
            };
            sp_NewUser.GestureRecognizers.Add(NewUSer_Tap);

        }

        

        private void Btn_Login_Clicked(object sender, EventArgs e)
        {
            if (Et_userid.Text == null && Et_pass.Text == null)
            {
                DisplayAlert("App Name", "Empty Field", "Ok");
            }
            else
            {
                long id = long.Parse(Et_userid.Text);
                int id_len = (int)Math.Floor(Math.Log10(id) + 1);
                if (id_len == 9)
                {
                    var temp = users.FirstOrDefault(c => c.Uid == long.Parse(Et_userid.Text) && c.Pass == Et_pass.Text) as User;

                    if (temp != null)
                    {
                        //App.Current.Properties["TUser"] = temp.username;
                        //App.Current.Properties["TType"] = temp.type;
                        //App.Current.SavePropertiesAsync();

                        UserSettings.Uid = temp.Uid;

                        //Navigation.PushModalAsync(new MasterDetailPage());
                        Navigation.PopModalAsync();
                        //DisplayAlert("App Name", "Userd " + temp.Uid.ToString(), "Ok");

                    }
                    else
                    {
                        DisplayAlert("App Name", "User Id and Password are incorrect", "Ok");
                    }
                }
                else
                {
                    DisplayAlert("App Name", "User Id Is not valid", "Ok");
                }

            }
        }

    }
}