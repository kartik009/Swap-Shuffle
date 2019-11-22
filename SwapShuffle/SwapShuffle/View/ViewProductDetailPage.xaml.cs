using Newtonsoft.Json;
using SwapShuffle.Helper;
using SwapShuffle.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewProductDetailPage : ContentPage
    {

        Product product;
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
                this.product = product;
            }
        }

        public async Task<string> PostData(InterestedTable interested)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("192.168.0.5:8080");

            string jsonData = JsonConvert.SerializeObject(interested);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/ss/request/add", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        private async void Btn_Buy_Clicked(object sender, System.EventArgs e)
        {
            //Request Add To interestedTable 
            bool res = await DisplayAlert("Message", "Do you want to Buy Product?", "Ok", "Cancel");
            if (res)
            {
                InterestedTable interested = new InterestedTable();
                interested.Pid = product.Pid;
                interested.Uid = UserSettings.Uid;
                interested.datetime = DateTime.Now;
                var f = PostData(interested);

                if (f != null)
                {
                    await Navigation.PopToRootAsync();
                }
            }
        }
    }
}