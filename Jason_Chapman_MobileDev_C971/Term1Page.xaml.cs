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
        private string title1;
        public string Title1
        {
            get { return title1; }
            set { value = title1; }
        }
        public Term1Page()
        {
            InitializeComponent();
            //var title1 = new Label { Text = "Term 1", TextDecorations = TextDecorations.Underline };
            //var editor = new Editor { Text = "Term 1" };

        }

        private static string course1BtnText = "Math201";
        public static string Course1Title //property for course1BtnText
        {
            get { return course1BtnText; }
            set { course1BtnText = value; }
            //{
            //    if (value == course1BtnText)
            //        return;
            //    course1BtnText = value;
            //    //OnPropertyChanged();
            //}
        }

        public List<Course> Term1List = new List<Course>()
        {
            new Course (Course1Title, DateTime.Today, DateTime.Today, "Completed", "Mr. Mackey", "5555555", "name@hotmail.com" )
        };

        private async void Course1Btn_Clicked(object sender, EventArgs e)
        {
            //Course1Button.Text = Course1Title;
            await Navigation.PushAsync(new Course1Page());
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