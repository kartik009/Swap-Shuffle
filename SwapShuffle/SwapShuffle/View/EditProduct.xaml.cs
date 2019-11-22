using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using SwapShuffle.Helper;
using SwapShuffle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProduct : ContentPage
    {
        private MediaFile _mediaFile;
        string path;

        Product productDetails;

        public EditProduct()
        {
            InitializeComponent();
            this.Title = "Add Product";
        }

        public EditProduct(Product product)
        {
            InitializeComponent();
            if(product!=null)
            {
                productDetails = product;
                PopulateDetails(productDetails); 
            }
        }

        private void PopulateDetails(Product details)
        {         

            //details.Pid = 108;
            Et_PName.Text = details.Name;
            Et_CId.Text = Convert.ToString(details.Cid);
            Et_PD.Text = details.p_description;
            Et_price.Text = Convert.ToString(details.price);
            details.p_datetime = DateTime.Now;

            saveBtn.Text = "Update";
            this.Title = "Edit Product";
        }

        public async Task<string> EditData(Product details)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("192.168.0.5:8080");

            string jsonData = JsonConvert.SerializeObject(details);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync("/ss/product/Edit", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> PostData(Product product)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("192.168.0.5:8080");

            string jsonData = JsonConvert.SerializeObject(product);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/ss/Product/delete", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }


        private void SaveProduct(object sender, EventArgs e)
        {
            if (saveBtn.Text == "Save")
            {
                Product details = new Product();
                details.Pid = 108;
                details.Name = Et_PName.Text;
                details.Cid = Convert.ToInt64(Et_CId.Text);
                details.Uid = UserSettings.Uid;
                details.p_description = Et_PD.Text;
                details.p_images = DEmo.path;
                details.price  = Convert.ToDecimal(Et_price.Text);
                details.p_datetime = DateTime.Now;
                
                //MessagingCenter.Send<Product>(details, "AddProduct");
                //MessagingCenter.Send<Product>(details, "AddProduct1");

                var f = PostData(details);
                if (f != null)
                {

                    Navigation.PopAsync();
                }
                //bool res = DependencyService.Get<ISQLite>().SaveProduct(details);
                //if (res)
                //{
                //    Navigation.PopAsync();
                //}
                //else
                //{
                //    DisplayAlert("Message", "Data Failed To Save", "Ok");
                //}
            }
            else
            {
                // update Product
                productDetails.Name = Et_PName.Text;
                productDetails.Cid = Convert.ToInt64(Et_CId.Text);
                productDetails.p_description = Et_PD.Text;
                productDetails.price = Convert.ToDecimal(Et_price.Text);
                productDetails.p_datetime = DateTime.Now;
                productDetails.p_images = DEmo.path;
                //MessagingCenter.Send<Product>(productDetails, "EditProduct");
                //MessagingCenter.Send<Product>(productDetails, "EditProduct1");

                var f = EditData(productDetails);

                if (f != null)
                {

                    Navigation.PopAsync();
                }
                //bool res = DependencyService.Get<ISQLite>().UpdateProduct(details);
                //if (res)
                //{
                //    Navigation.PopAsync();
                //}
                //else
                //{
                //    DisplayAlert("Message", "Data Failed To Update", "Ok");
                //}
            }
        }

        private async void Btn_pic_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Message", "Photo Pick Not Supported", "ok");
                return;
            }
            else
            {
                var storageStatus = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null)
                    return;
                path = _mediaFile.Path;

                DEmo.path = _mediaFile.Path;
                //Image image = new Image();
                //image.Source = ImageSource.FromStream(() =>
                //{
                //    return _mediaFile.GetStream();
                //});
            }
        }
    }
}