using SQLite;
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
    public partial class HomePage : ContentPage
    {
        private string crrntTerm;
        private List<Term> termList;
        //public event EventHandler Clicked;

        //StackLayout parent = new StackLayout();
        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
            //Layout.BindingContext = this;
            // addTermBtnForTest();
            //OnAppearing();

        }
        protected override void OnAppearing()//Creates all Term buttons from DB info
        {
            addTermBtnForTest();
        }//end OnAppearing

        private void addTermBtnForTest()
        {
            Term termTest = new Term()
            {
                TermTitle = "Term 1", //Needs to get this from DB because it tests ability to change 'TermTitle'
                CreateDate = System.DateTime.Now
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                conn.Insert(termTest);
                crrntTerm = termTest.TermID.ToString();
                termList = conn.Table<Term>().ToList();
            }

            Button testBtn = new Button()
            {
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                Margin = 30,
                BackgroundColor = Color.White
            };
            testBtn.Clicked += (sender, args) => GoToTermButton_Clicked(sender, args);
            testBtn.BindingContext = termTest;
            testBtn.SetBinding(Button.TextProperty, "TermTitle");

            layout.Children.Add(testBtn);
        }//end addTermBtnForTest

        private async void GoToTermButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new TermPage());
            await DisplayAlert("Title", "Werks!!!", "Git!!!");
           
        }

        private void AddTermBtn_Clicked(object sender, EventArgs e)
        {
            AddTermEntry.IsVisible = true;
            AddTermEntry.Focus();
            AddTermSaveBtn.IsVisible = true;
        }//end AddTermBtn_Clicked

        private void AddTermSaveBtn_Clicked(object sender, EventArgs e)
        {
            AddTermEntry.IsVisible = false;
            AddTermSaveBtn.IsVisible = false;

            Term term = new Term()
            {
                TermTitle = AddTermEntry.Text,
                CreateDate = System.DateTime.Now
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                conn.Insert(term);
            }

            Button newTab = new Button()
            {
                Text = AddTermEntry.Text,
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                Margin = 30,
                BackgroundColor = Color.White
            };

            layout.Children.Add(newTab);

        }//end AddTermSaveBtn_Clicked

        
    }
}