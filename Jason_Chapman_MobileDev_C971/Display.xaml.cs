using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jason_Chapman_MobileDev_C971
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Display : ContentPage
    {
        private List<Course> courseList;

        public Display()
        {
            InitializeComponent();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList(); //courses = new List<Course>();
            }


            OnAppearing();
           // displayStuff();
        }




        protected override void OnAppearing()
        {
            DisplayAlert("Title", "Werks", "Git!!!");
        }
        private void displayStuff()
        {

        //ListView view = new ListView
        //{
        //    // Source of data items.
        //    ItemsSource = courseList,

        //    // Define template for displaying each item.
        //    // (Argument of DataTemplate constructor is called for 
        //    //      each item; it must return a Cell derivative.)
        //    ItemTemplate = new DataTemplate(() =>
        //    {
        //        // Create views with bindings for displaying each property.
        //        Label nameLabel = new Label();
        //        nameLabel.SetBinding(Label.TextProperty, "Name");

        //        Label birthdayLabel = new Label();
        //        birthdayLabel.SetBinding(Label.TextProperty,
        //            new Binding("Birthday", BindingMode.OneWay,
        //                null, null, "Born {0:d}"));

        //        BoxView boxView = new BoxView();
        //        boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

        //        // Return an assembled ViewCell.
        //        return new ViewCell
        //        {
        //            View = new StackLayout
        //            {
        //                Padding = new Thickness(0, 5),
        //                Orientation = StackOrientation.Horizontal,
        //                Children =
        //                    {
        //                        boxView,
        //                        new StackLayout
        //                        {
        //                            VerticalOptions = LayoutOptions.Center,
        //                            Spacing = 0,
        //                            Children =
        //                            {
        //                                nameLabel,
        //                                birthdayLabel
        //                            }
        //                            }
        //                    }
        //            }
        //        };
        //    })
        //};//end ListView

        //this.Content = new StackLayout
        //{
        //    Children =
        //    {
        //        view
        //    }
        //};



    }//end displayStuff()
    }
}