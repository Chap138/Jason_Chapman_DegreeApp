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
    public partial class TermPage : ContentPage
    {
        // private int termId;

        private List<Course> courseList;//From the DB
        private List<Term> termList;//From the DB
        private int CurrentTermID;
        private string CurrentTermTitle;
        private DateTime CurrentTermStart;
        private DateTime CurrentTermEnd;

        private int currentCourse;//CourseID to pass into GoToCourseBtn_Clicked() for navigating to appropriate coursePage
        private string title = "Term Title";

        public TermPage(int termID)
        {
            InitializeComponent();
            CurrentTermID = termID;
            BindingContext = this;
            //TermLabel.Text = CurrentTermTitle;

        }//end constructor

        protected override void OnAppearing()
        {
            GetTerm();
        }//end OnAppearing

        public void GetTerm()//Update term info when page appears
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                termList = conn.Table<Term>().ToList();
                //conn.Table<Term>().Select(termList ,CurrentTermID)
            }

            foreach (Term row in termList)
            {
                if (row.ID == CurrentTermID)
                {
                    CurrentTermTitle = row.TermTitle;
                    CurrentTermStart = row.Start;
                    CurrentTermEnd = row.End;
                }
            }
            TermLabel.Text = CurrentTermTitle;
            StartDatePicker.Date = CurrentTermStart;
            EndDatePicker.Date = CurrentTermEnd;
            //DisplayAlert(CurrentTermTitle, CurrentTermStart.ToString(), CurrentTermEnd.ToString());//Test to display CurrentTerm properties 
        }//end GetTerm()

        private async void GoToCourseBtn_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CoursePage(currentCourse));//USE WHEN READY TO ADD COURSES

            //await Navigation.PushAsync(new Course1Page());//TEST TEST TEST delete when ready to add courses

        }//end GoToCourseBtn_Clicked

        private void EditTerm_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new EditTermPage(CurrentTermID));
            TitleEntry.IsVisible = true;
            TitleEntry.Focus();
            EditTermSaveBtn.IsVisible = true;
            EditTermCancelBtn.IsVisible = true;
        }//end EditTerm_Clicked

        private void EditTermSaveBtn_Clicked(object sender, EventArgs e)
        {
            TitleEntry.IsVisible = false;
            EditTermSaveBtn.IsVisible = false;
            EditTermCancelBtn.IsVisible = false;

            foreach (Term row in termList)
            {
                if (row.ID == CurrentTermID)
                {
                    if (TitleEntry.Text == null)
                    {
                        row.TermTitle = CurrentTermTitle;
                    }
                    else
                    {
                        row.TermTitle = TitleEntry.Text;
                        TermLabel.Text = TitleEntry.Text;
                    }

                    row.Start = StartDatePicker.Date;
                    row.End = EndDatePicker.Date;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        int rows = conn.Update(row);
                    }
                    break;
                }
            }

        }//end EditTermSaveBtn_Clicked

        private void EditTermCancelBtn_Clicked(object sender, EventArgs e)
        {
            TitleEntry.IsVisible = false;
            EditTermSaveBtn.IsVisible = false;
            EditTermCancelBtn.IsVisible = false;
        }//end EditTermCancelBtn_Clicked
    }

    //PART OF TAPPING TITLE TO CHANGE
    //TitleEntry.Completed += (sender, e) => TitleEntry_Completed(sender, e);//THIS LINE GOES IN CONSTRUCTOR

    //TrmTitle property goes up top
    //public string TrmTitle
    //{
    //    get { return title; }
    //    set
    //    {
    //        //title1 = value;
    //        {
    //            if (value == title)
    //                return;
    //            title = value;
    //            OnPropertyChanged();//Handles (nameof(Title1)) automatically
    //        }
    //    }
    //}//end TrmTitle

    //public ICommand TermLabel_Clicked => new Command(ChangeTermTitle);
    //private void ChangeTermTitle()
    //{
    //    TitleEntry.IsVisible = true;
    //    TermLabel.TextColor = Color.White;
    //    TitleEntry.Focus();
    //}//end ChangeTermTitle

    //PART OF TAPPING TITLE TO CHANGE
    //private void TitleEntry_Completed(object sender, EventArgs e)//Use to update TermTitle in DB
    //{
    //    TitleEntry.IsVisible = false;
    //    TermLabel.TextColor = Color.White;
    //    TermLabel.Text = TitleEntry.Text;

    //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
    //    {
    //        conn.CreateTable<Term>();
    //        int rows = conn.Update(termList);


    //        ////int rows = conn.Update(termList);
    //        ////conn.Table<Term>().Select(termList,CurrentTermID)
    //        ///
    //        //var query = termList.Where(t => t.TermID == CurrentTermID);
    //        //foreach (Term row in query)
    //        //{
    //        //    row.TermTitle = TermLabel.Text;
    //        //    conn.Update(termList);
    //        //}

    //    }

    //}//end TitleEntry_Completed/////////////////////////

}
