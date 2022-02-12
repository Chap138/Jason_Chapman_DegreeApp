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
    public partial class Term1Page : ContentPage
    {
        private string Title1 { get; set; }
        
        public List<Course> term1List = new List<Course>
        {
            new Course (1, "Math 101", DateTime.Today, DateTime.Today, "In Progress", "Mr. Mackey",  "555-3508", "mackey@hotmail.com" ),
            new Course (1, "English 201", DateTime.Today, DateTime.Today, "Completed", "Mrs. Streibel", "555-0241", "streibel@hotmail.com" ),
            new Course (1, "History", DateTime.Today, DateTime.Today, "Plan to take", "Mr. Adler", "555-8824", "adler@hotmail.com" ),
            new Course (1, "Sociology", DateTime.Today, DateTime.Today, "Completed", "Mr. Derp", "555-7600", "derp@hotmail.com" ),
            new Course (1, "Basketweaving", DateTime.Today, DateTime.Today, "Dropped", "Ms. Bronski", "555-2855", "bronski@hotmail.com" ),
            new Course (1, "Astronomy", DateTime.Today, DateTime.Today, "In Progress", "Mr. Garrison", "555-7637", "garrison@hotmail.com" )
        };

        public Term1Page()
        {
            InitializeComponent();
            //var title1 = new Label { Text = "Term 1", TextDecorations = TextDecorations.Underline };
            //var editor = new Editor { Text = "Term 1" ;

            Course1Button.Text = term1List[0].CourseTitle;
            Course2Button.Text = term1List[1].CourseTitle;
            Course3Button.Text = term1List[2].CourseTitle;
            Course4Button.Text = term1List[3].CourseTitle;
            Course5Button.Text = term1List[4].CourseTitle;
            Course6Button.Text = term1List[5].CourseTitle;

        }

        //private static string course1BtnText = "Math201";
        //public static string Course1Title //property for course1BtnText
        //{
        //    get { return course1BtnText; }
        //    set { course1BtnText = value; }
        //    //{
        //    //    if (value == course1BtnText)
        //    //        return;
        //    //    course1BtnText = value;
        //    //    //OnPropertyChanged();
        //    //}
        //}


        private async void Course1Btn_Clicked(object sender, EventArgs e)
        {
            //Course1Button.Text = Course1Title;
            await Navigation.PushAsync(new Course1Page());
            //((Button)sender).Text = "Math 101";
            //this.TitleLabel.Text = "Term 87978";


        }

        private async void Course2Btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Course2Page());
        }
        private async void Course3Btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Course3Page());
        }
        private async void Course4Btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Course4Page());
        }
        private async void Course5Btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Course5Page());
        }
        private async void Course6Btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Course6Page());
        }

    }
}