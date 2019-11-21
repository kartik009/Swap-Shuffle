using SwapShuffle.Helper;
using SwapShuffle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProduct : ContentPage
    {

        Product productDetails;

        public EditProduct()
        {
            InitializeComponent();
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
            this.Title = "Edit Employee";
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
                details.p_images = "icons8trial100.png";
                details.price  = Convert.ToDecimal(Et_price.Text);
                details.p_datetime = DateTime.Now;

                MessagingCenter.Send<Product>(details, "AddProduct");
                MessagingCenter.Send<Product>(details, "AddProduct1");

                Navigation.PopAsync();
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

                MessagingCenter.Send<Product>(productDetails, "EditProduct");
                MessagingCenter.Send<Product>(productDetails, "EditProduct1");

                Navigation.PopAsync();

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
    }
}