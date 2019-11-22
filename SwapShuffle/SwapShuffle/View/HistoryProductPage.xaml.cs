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
    public partial class HistoryProductPage : ContentPage
    {
        public HistoryProductPage(Product p)
        {
            InitializeComponent();
            if(p!=null)
                this.BindingContext = p;
            lb_uname.Text = "Kartik Lakhani";
            lb_userid.Text = "201812043";
            lb_date.Text = "22-11-2019";

        }
    }
}