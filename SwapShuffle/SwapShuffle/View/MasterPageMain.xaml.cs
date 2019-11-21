using SwapShuffle.Helper;
using SwapShuffle.Menu;
using SwapShuffle.Model;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwapShuffle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageMain : MasterDetailPage
    {
        User temp;

        Type t;
        public ObservableCollection<MasterPageItem> AppUsermenuList
        {
            get;
            set;
        }

        public MasterPageMain()
        {
            InitializeComponent();
            temp = new User();


            temp.Uid = UserSettings.Uid;

           
            AppUsermenuList = new ObservableCollection<MasterPageItem>();

            // Adding menu items to menuList and you can define title ,page and icon ==For App USer 


            //menuList.Add(new MasterPageItem()
            //{
            //    Title = "Features",
            //    //Icon = "contacticon.png",
            //    TargetType = typeof(FeaturePage)
            //});

            AppUsermenuList.Add(new MasterPageItem()
            {
                Title = "View Product",
                //Icon = "contacticon.png",
                TargetType = typeof(ViewProductPage)
            });


            AppUsermenuList.Add(new MasterPageItem()
            {
                Title = "Interested List",
                //Icon = "contacticon.png",
                TargetType = typeof(InterestedListPage)
            });

            AppUsermenuList.Add(new MasterPageItem()
            {
                Title = "Product List",
                //Icon = "contacticon.png",
                TargetType = typeof(SellProductPage)
            });

            AppUsermenuList.Add(new MasterPageItem()
            {
                Title = "Histroy",
                //Icon = "contacticon.png",
                TargetType = typeof(HistroyPage)
            });

            AppUsermenuList.Add(new MasterPageItem()
            {
                Title = "Log out"
            });



            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = AppUsermenuList;

            // Initial navigation, this can be used for our home page  
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ViewProductPage)));
        }
        // Event for Menu Item selection, here we are going to handle navigation based  
        // on user selection in menu ListView  

        public void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                //Code to execute on tapped event
                Detail = new NavigationPage(new UserProfilePage(UserSettings.Uid));
                IsPresented = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            if (item.Title == "Log out")
            {
                UserSettings.Uid = 0;

                App.Current.MainPage = new NavigationPage(new LogInPage());

                Navigation.PushModalAsync(new LogInPage());

                //await PopupNavigation.PopAllAsync();
                //var logpage = new LogInPage();//this could be content page
                //var rootPage = new NavigationPage(logpage);
                //App= rootPage.Navigation;
            }
            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            }
            IsPresented = false;
        }
    }
}