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
            
        }
        private void displayStuff()
        {

      


    }//end displayStuff()
    }
}