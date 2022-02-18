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
    public partial class HomePage : ContentPage
    {
        private List<Term> termList;

        //StackLayout parent = new StackLayout();
        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
            //Layout.BindingContext = this;
            addTermBtnForTest();

        }

        private void addTermBtnForTest()
        {
            Term termTest = new Term()
            {
                TermTitle = "Term 52", //Make it so you get this from DB
                CreateDate = System.DateTime.Now
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                conn.Insert(termTest);
                termList = conn.Table<Term>().ToList();
                //parent = new StackLayout();
            }

            Button testBtn = new Button()
            {
                //Text = "Term 1" , //Make it so you get this from DB
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                Margin = 30,
                BackgroundColor = Color.White
            };
            testBtn.BindingContext = termTest;
            testBtn.SetBinding(Button.TextProperty, "TermTitle");// termList[30].TermTitle
            /*testBtn.SetBinding(Button.TextProperty, "TermTitle");*///termTest.TermTitle
            //testBtn.SetBinding(Button.TextColorProperty, Color.Red);//termTest.TermTitle

            Layout.Children.Add(testBtn);
        }//end addTermBtnForTest

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

            Layout.Children.Add(newTab);

        }//end AddTermSaveBtn_Clicked

       
    }
}