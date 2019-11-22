using SwapShuffle.Helper;
using SwapShuffle.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewProductDetailPage : ContentPage
    {
        public ViewProductDetailPage(Product product)
        {
            InitializeComponent();
            if (product != null)
            {
                if(product.Uid !=UserSettings.Uid)
                    this.BindingContext = product;
                else
                {
                    Btn_Buy.Text = "Request Processing";
                    Btn_Buy.IsEnabled = false;
                }
            }
        }

        private async void Btn_Buy_Clicked(object sender, System.EventArgs e)
        {
            //Request Add To interestedTable 
            bool res = await DisplayAlert("Message", "Do you want to Buy Product?", "Ok", "Cancel");
            if(res)
                await Navigation.PopToRootAsync();
            else
            {

            }
        }
    }
}