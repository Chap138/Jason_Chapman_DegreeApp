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
            //addTermBtnForTest(); //FOR TESTING ///////////////////////
            //OnAppearing(); Don't need this in Constructor
            addTermFromDB();

        }
        protected override void OnAppearing()//Creates all Term buttons from DB info
        {
            //addTermFromDB();//Creates Buttons for all Terms in DB
        }//end OnAppearing

        private void addTermFromDB()//Creates Buttons for all Terms in DB
        {
            //Term termTest = new Term()
            //{
            //    TermTitle = "Term 1", //Needs to get this from DB because it tests ability to change 'TermTitle'
            //    CreateDate = System.DateTime.Now
            //};
            //int termID = 0;
            string btnTitle;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                //conn.Table<Term>
                //crrntTerm = termTest.TermID.ToString();
                termList = conn.Table<Term>().ToList();
                
                for (int i = 0; i < termList.Count; i++)
                {
                    int termID = termList[i].TermID;
                    btnTitle = termList[i].TermTitle;

                    Button testBtn = new Button()
                    {
                        //Text = "KJHKg",
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        Margin = 30,
                        BackgroundColor = Color.White
                    };

                    testBtn.Clicked += (sender, args) => GoToTermButton_Clicked(sender, args, termID);
                    testBtn.BindingContext = termList;
                    testBtn.SetBinding(Button.TextProperty, "TermTitle");

                    layout.Children.Add(testBtn);
                }
            }

            //Button testBtn = new Button()
            //{
            //    Text = "KJHKg",
            //    TextColor = Color.Black,
            //    FontAttributes = FontAttributes.Bold,
            //    FontSize = 20,
            //    Margin = 30,
            //    BackgroundColor = Color.White
            //};

            //testBtn.Clicked += (sender, args) => GoToTermButton_Clicked(sender, args, termID);
            //testBtn.BindingContext = termList;
            //testBtn.SetBinding(Button.TextProperty, "TermTitle");

            //layout.Children.Add(testBtn);
        }//end addTermBtnFromDB


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

            Button testBtn = new Button()
            {
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                Margin = 30,
                BackgroundColor = Color.White
            };

            int termID = term.TermID;
            testBtn.Clicked += (s, a) => GoToTermButton_Clicked(s, a, termID);
            testBtn.BindingContext = term;
            testBtn.SetBinding(Button.TextProperty, "TermTitle");

            layout.Children.Add(testBtn);

        }//end AddTermSaveBtn_Clicked

        private async void GoToTermButton_Clicked(object sender, EventArgs e, int id)//Navigate to appropriate term
        {
            //await Navigation.PushAsync(new TermPage());
            
            await DisplayAlert(id.ToString(), "Werks!!!", "Git!!!");

        }//end GoToTermButton_Clicked






        private void addTermBtnForTest()//FOR TESTING/////////////////////////
        {
            Term termTest = new Term()
            {
                TermTitle = "Term 1", //Needs to get this from DB because it tests ability to change 'TermTitle'
                CreateDate = System.DateTime.Now
            };

            int termID = termTest.TermID;

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
                BackgroundColor = Color.White,
                //CommandParameter = termTest.TermID
                ClassId = termTest.TermID.ToString()
            };
            testBtn.Clicked += (sender, args) => GoToTermButton_Clicked(sender, args, termID);
            testBtn.BindingContext = termTest;
            testBtn.SetBinding(Button.TextProperty, "TermTitle");
            //testBtn.SetValue(Button.ClassIdProperty, termTest.TermID);
            //int termID = termTest.TermID;
            layout.Children.Add(testBtn);

        }//end addTermBtnForTest FOR TESTING///////////////

        private void AddTermBtn_Clicked(object sender, EventArgs e)//Initiates Term creation 
        {
            AddTermEntry.IsVisible = true;
            AddTermEntry.Focus();
            AddTermSaveBtn.IsVisible = true;
        }//end AddTermBtn_Clicked
        
    }
}