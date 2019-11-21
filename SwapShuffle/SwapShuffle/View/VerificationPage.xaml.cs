using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationPage : ContentPage
    {
        public VerificationPage(long id, int code)
        {
            InitializeComponent();
            Lb_ID.Text = id.ToString();
        }

        private void Bt_VerifyCode_Clicked(object sender, EventArgs e)
        {

        }
    }
}