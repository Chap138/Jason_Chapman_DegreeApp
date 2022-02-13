using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jason_Chapman_MobileDev_C971
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Term1Page : ContentPage
    {
        private string title1 = "Term 1";
        public string Title1 //{ get; set; } = "Term 1";
        {
            get { return title1; }
            set
            {
                //title1 = value;
                {
                    if (value == title1)
                        return;
                    title1 = value;
                    OnPropertyChanged();//Handles (nameof(Title1)) automatically
                }
            }
        }




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

            BindingContext = this;
            Title1Entry.Completed += (sender, e) => Title1Entry_Completed(sender, e);
        }
        private void Title1Entry_Completed(object sender, EventArgs e)
        {
            Title1Entry.IsVisible = false;
            Term1Label.TextColor = Color.White;
        }

        //Change Title
        public ICommand Term1Label_Clicked => new Command(ChangeTerm1Title);
        private void ChangeTerm1Title()
        {
            Title1Entry.IsVisible = true;
            Term1Label.TextColor = Color.Black;
            Title1Entry.Focus();
        }//end ChangeTerm1Title
        

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


        //        SfButton button = new SfButton();
        //        button.Text = "Button";
        //button.CornerRadius = 3;


        //private void Title1EditButton_Clicked(object sender, EventArgs e)
        //{
        //    Title1Entry.IsVisible = true;
        //    Title1Entry.Focus();
        //}

    }
}