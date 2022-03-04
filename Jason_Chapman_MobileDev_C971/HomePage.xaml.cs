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
        //private int crrntTerm;
        private List<Term> termList;
        
        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
            //DeleteTermRows(); //DELETES EVERYTHING FROM THE TERM TABLE
            //CreateTermTable();
            AddTermFromDb();
        }

        protected override void OnAppearing()//Creates all Term buttons from DB info
        {
            DeleteButtons();//Delete buttons already on page
            AddTermFromDb();//Create updated buttons to replace

        }//end OnAppearing

        private void DeleteButtons()//Delete buttons to replace refreshed
        {
            for (int i = 8; i < layout.Children.Count;)
            {
                layout.Children.RemoveAt(i);
            }
        }

        private void AddTermSaveBtn_Clicked(object sender, EventArgs e)
        {
            AddTermEntry.IsVisible = false;
            AddTermSaveBtn.IsVisible = false;
            AddTermCancelBtn.IsVisible = false;
            StartDatePicker.IsVisible = false;
            StartDateLabel.IsVisible = false;
            EndDatePicker.IsVisible = false;
            EndDateLabel.IsVisible = false;

            Term term = new Term()
            {
                TermTitle = AddTermEntry.Text,
                Start = StartDatePicker.Date,
                End = EndDatePicker.Date
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
                Margin = 20,
                BackgroundColor = Color.White,
                CornerRadius = 10,
                BorderColor = Color.LightSkyBlue,
                BorderWidth = 5
            };

            int termID = term.ID;
            testBtn.Clicked += (s, a) => GoToTermButton_Clicked(s, a, termID);
            testBtn.BindingContext = term;
            testBtn.SetBinding(Button.TextProperty, "TermTitle");
            layout.Children.Add(testBtn);
            AddTermEntry.Placeholder = "Enter Term Title";

        }//end AddTermSaveBtn_Clicked

        private async void GoToTermButton_Clicked(object sender, EventArgs e, int id)//Navigate to appropriate term
        {
            await Navigation.PushAsync(new TermPage(id));
            //await DisplayAlert(id.ToString(), "Werks", "Git!!!");//DELETE For testing 
        }//end GoToTermButton_Clicked

        private void AddTermBtn_Clicked(object sender, EventArgs e)//Initiates Term creation 
        {
            AddTermEntry.IsVisible = true;
            AddTermEntry.Focus();
            StartDateLabel.IsVisible = true;
            StartDatePicker.IsVisible = true;
            EndDatePicker.IsVisible = true;
            EndDateLabel.IsVisible = true;
            AddTermSaveBtn.IsVisible = true;
            AddTermCancelBtn.IsVisible = true;
            AddTermEntry.Text = null;

        }//end AddTermBtn_Clicked

        public void DeleteTermRows()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DeleteAll<Term>();
            }
        }
        public void DropTermTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DropTable<Term>();
            }
        }
        private void AddTermBtnForTest()//FOR TESTING/////////////////////////
        {
            Term termTest = new Term()
            {
                TermTitle = "Term 1", //Needs to get this from DB because it tests ability to change 'TermTitle'
                //CreateDate = System.DateTime.Now
            };

            int termID = termTest.ID;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                conn.Insert(termTest);
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
                ClassId = termTest.ID.ToString()
            };
            //testBtn.Clicked += (sender, args) => GoToTermButton_Clicked(sender, args, termID);
            testBtn.BindingContext = termTest;
            testBtn.SetBinding(Button.TextProperty, "TermTitle");
            //testBtn.SetValue(Button.ClassIdProperty, termTest.TermID);
            //int termID = termTest.TermID;
            layout.Children.Add(testBtn);

        }//end AddTermBtnForTest FOR TESTING

        private void AddTermCancelBtn_Clicked(object sender, EventArgs e)
        {
            AddTermEntry.IsVisible = false;
            StartDateLabel.IsVisible = false;
            StartDatePicker.IsVisible = false;
            EndDatePicker.IsVisible = false;
            EndDateLabel.IsVisible = false;
            AddTermSaveBtn.IsVisible = false;
            AddTermCancelBtn.IsVisible = false;
            AddTermEntry.Text = null;
        }//end AddTermCancelBtn_Clicked

        private void CreateTermTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
            }
        }//end CreateTermTable
        private void AddTermFromDb()//Creates Buttons for all Terms in DB
        {
            
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                termList = conn.Table<Term>().ToList();

                for (int i = 0; i < termList.Count; i++)
                {
                    int termID = termList[i].ID;
                    string btnTitle = termList[i].TermTitle;

                    Button testBtn = new Button()
                    {
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        Margin = 20,
                        BackgroundColor = Color.White,
                        CornerRadius = 10,
                        BorderColor = Color.LightSkyBlue,
                        BorderWidth = 5
                    };

                    testBtn.Clicked += (sender, args) => GoToTermButton_Clicked(sender, args, termID);
                    testBtn.BindingContext = termList[i];
                    testBtn.SetBinding(Button.TextProperty, "TermTitle");

                    layout.Children.Add(testBtn);
                }
            }
        }//end AddTermFromDB

    }
}