
using SwapShuffle.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
       

        public UserProfilePage(long uid)
        {
            if(uid ==0)
            {
                Navigation.PushModalAsync(new LogInPage());
            }

            InitializeComponent();

            Et_UName.Text = "Kartik Lakhani";
            Et_UNo.Text = Convert.ToString(8866404551);
            Et_PD.Text = "test";
            Et_CPD.Text = "test";

            this.Title = "Profile";
        }

        private void EditUser(object sender, EventArgs e)
        {
            //get 

            User user = new User();

            user.Name = Et_UName.Text;

            user.PhNo = Convert.ToInt64(Et_UNo.Text);

            user.Pass = Et_PD.Text;
            

            //edit 
        }
    }
}