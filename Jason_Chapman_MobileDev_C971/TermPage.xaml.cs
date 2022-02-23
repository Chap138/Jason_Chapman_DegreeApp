﻿using SQLite;
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
        private List<Term> termList;//From the DB
        private List<Course> courseList;//From the DB
        private int CurrentTermID;
        private string CurrentTermTitle;
        private DateTime CurrentTermStart;
        private DateTime CurrentTermEnd;

        private int currentCourse;//CourseID to pass into GoToCourseBtn_Clicked() for navigating to appropriate coursePage
        private string title = "Term Title";
        public string TrmTitle
        {
            get { return title; }
            set
            {
                //title1 = value;
                {
                    if (value == title)
                        return;
                    title = value;
                    OnPropertyChanged();//Handles (nameof(Title1)) automatically
                }
            }
        }//end TrmTitle


        public TermPage(int termID)
        {
            InitializeComponent();
            CurrentTermID = termID;
            BindingContext = this;
            //Allows user to change title by clicking on it
            TitleEntry.Completed += (sender, e) => TitleEntry_Completed(sender, e);

            GetTerm();

        }//end constructor

        public void GetTerm()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                termList = conn.Table<Term>().ToList();
                //conn.Table<Term>().Select(termList ,CurrentTermID)
            }

            foreach(Term row in termList)
            {
                if(row.TermID == CurrentTermID)
                {
                    CurrentTermTitle = row.TermTitle;
                    CurrentTermStart = row.Start;
                    CurrentTermEnd = row.End;

    }
            }
            //CurrentTermTitle = termList[1].TermID();
            DisplayAlert(CurrentTermTitle, CurrentTermStart.ToString(), CurrentTermEnd.ToString());//Test to display CurrentTerm properties 
        }

        //protected override void OnAppearing()
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
        //    {
        //        courseList = conn.Table<Course>().ToList(); //courses = new List<Course>();
        //    }
        //}//end OnAppearing

        //Change Title by tapping on it//////////////////////
        public ICommand TermLabel_Clicked => new Command(ChangeTermTitle);
        private void ChangeTermTitle()
        {
            TitleEntry.IsVisible = true;
            TermLabel.TextColor = Color.Black;
            TitleEntry.Focus();
        }//end ChangeTermTitle
        private void TitleEntry_Completed(object sender, EventArgs e)//Use to update TermTitle in DB
        {
            TitleEntry.IsVisible = false;
            TermLabel.TextColor = Color.White;

        }//end TitleEntry_Completed/////////////////////////





        private async void GoToCourseBtn_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CoursePage(currentCourse));//USE WHEN READY TO ADD COURSES

            //await Navigation.PushAsync(new Course1Page());//TEST TEST TEST delete when ready to add courses

        }//end GoToCourseBtn_Clicked

    }


}
