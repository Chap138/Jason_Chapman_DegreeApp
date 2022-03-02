﻿using Plugin.LocalNotifications;
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
    public partial class CoursePage : ContentPage
    {
        int alertID;
        private int CurrentCourseID;
        List<Course> courseList;
        List<Assessment> assmtList;
        DateTime start;
        DateTime end;
        private string CurrentCourseTitle;
        private DateTime CurrentCourseStart;
        private DateTime CurrentCourseEnd;
        private string title = "Course Title";
        private bool courseSaveValid;
        int numAssmts;

        public CoursePage(int courseID)
        {
            InitializeComponent();
            CurrentCourseID = courseID;
            //DropAddAssmtTable();
            //DropAddAssmtTable();
            //AddAssessmentFromDB();
        }
        protected override void OnAppearing()
        {
            //GetCourse();
            //DeleteButtons();
            //AddCourseFromDB();
        }//end OnAppearing
        public void GetCourse()//Update course info when page appears
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();
                //conn.Table<Term>().Select(termList ,CurrentTermID)
            }

            foreach (Course row in courseList)
            {
                if (row.CourseID == CurrentCourseID)
                {
                    CurrentCourseTitle = row.CourseTitle;
                    CurrentCourseStart = row.StartDate;
                    CurrentCourseEnd = row.EndDate;
                }
            }
            CourseLabel.Text = CurrentCourseTitle;
            StartDatePicker.Date = CurrentCourseStart;
            EndDatePicker.Date = CurrentCourseEnd;
            //DisplayAlert(CurrentTermTitle, CurrentTermStart.ToString(), CurrentTermEnd.ToString());//Test to display CurrentTerm properties 
        }//end GetCourse()

        private void EditCourse_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new EditTermPage(CurrentTermID));
            TitleEntry.IsVisible = true;
            TitleEntry.Focus();
            EditCourseSaveBtn.IsVisible = true;
            EditCourseCancelBtn.IsVisible = true;
        }//end EditCourse_Clicked

        private void EditCourseSaveBtn_Clicked(object sender, EventArgs e)
        {
            TitleEntry.IsVisible = false;
            EditCourseSaveBtn.IsVisible = false;
            EditCourseCancelBtn.IsVisible = false;

            foreach (Course row in courseList)
            {
                if (row.CourseID == CurrentCourseID)
                {
                    if (TitleEntry.Text == null)
                    {
                        row.CourseTitle = CurrentCourseTitle;
                    }
                    else
                    {
                        row.CourseTitle = TitleEntry.Text;
                        CourseLabel.Text = TitleEntry.Text;
                    }

                    row.StartDate = StartDatePicker.Date;
                    row.EndDate = EndDatePicker.Date;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        int rows = conn.Update(row);
                    }
                    break;
                }
            }
        }//end EditCourseSaveBtn_Clicked

        private void EditCourseCancelBtn_Clicked(object sender, EventArgs e)
        {
            TitleEntry.IsVisible = false;
            EditCourseSaveBtn.IsVisible = false;
            EditCourseCancelBtn.IsVisible = false;
        }

        private void AddAssmtCancelBtn_Clicked(object sender, EventArgs e)
        {
            CoursePageStartDateLabel.IsVisible = true;
            StartDatePicker.IsVisible = true;
            EndDatePicker.IsVisible = true;
            CoursePageEndDateLabel.IsVisible = true;

            AddAssmtEntry.IsVisible = false;
            AddAssmtSaveBtn.IsVisible = false;
            AddAssmtCancelBtn.IsVisible = false;
            AssmtStartDatePicker.IsVisible = false;
            AssmtStartDateLabel.IsVisible = false;
            AssmtEndDatePicker.IsVisible = false;
            AssmtEndDateLabel.IsVisible = false;
            AssmtNotesEditor.IsVisible = false;
        }//end AddAssessmentCancelBtn_Clicked
        private void AddAssmt_Clicked(object sender, EventArgs e)//ADD ASSESSMENTS
        {
            numAssmts = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                assmtList = conn.Table<Assessment>().ToList();
                for (int i = 0; i < courseList.Count; i++)
                {
                    if (assmtList[i].CourseID == CurrentCourseID)
                    {
                        numAssmts++;
                    }
                }
            }

            if (numAssmts < 2)
            {

                CoursePageStartDateLabel.IsVisible = false;
                StartDatePicker.IsVisible = false;
                EndDatePicker.IsVisible = false;
                CoursePageEndDateLabel.IsVisible = false;

                AddAssmtEntry.IsVisible = true;
                AddAssmtEntry.Focus();
                AssmtDueDateLabel.IsVisible = true;
                AssmtDueDatePicker.IsVisible = true;
                AssmtSaveBtn.IsVisible = true;
                AssmtCancelBtn.IsVisible = true;
                AssmtNotesEditor.IsVisible = true;
                AddAssmtEntry.Text = null;
            }
            else DisplayAlert(" ", "Can not add more than 2 assessments to this course. (One performance and one objective)", "OK");
        }//end AddAssmt_Clicked


        private void AddAssmtSaveBtn_Clicked(object sender, EventArgs e)
        {
            if (AddAssmtEntry.Text == null ||
                AssmtNotesEditor.Text == null ||)
            {
                DisplayAlert(" ", "Please enter all fields.", "OK");
            }
            else
            {
                CoursePageStartDateLabel.IsVisible = true;
                StartDatePicker.IsVisible = true;
                EndDatePicker.IsVisible = true;
                CoursePageEndDateLabel.IsVisible = true;

                AddAssmtEntry.IsVisible = false;
                AddAssmtSaveBtn.IsVisible = false;
                AddAssmtCancelBtn.IsVisible = false;
                AssmtDueDatePicker.IsVisible = false;
                AssmtDueDateLabel.IsVisible = false;
                AssmtNotesEditor.IsVisible = false;

                Assessment assmt = new Assessment()
                {
                    CourseID = CurrentCourseID,
                    AssmtTitle = AddAssmtEntry.Text,
                    DueDate = AssmtDueDatePicker.Date,
                    AssmtNotes = AssmtNotesEditor.Text
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Assessment>();
                    conn.Insert(assmt);
                }

                Button testBtn = new Button()
                {
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 20,
                    Margin = 30,
                    BackgroundColor = Color.White,
                    CornerRadius = 10
                };

                int assmtID = assmt.AssessmentID;
                testBtn.Clicked += (s, a) => GoToAssmtBtn_Clicked(s, a, assmtID);
                testBtn.BindingContext = assmt;
                testBtn.SetBinding(Button.TextProperty, "AssessmentTitle");
                layout.Children.Add(testBtn);
                AddAssmtEntry.Placeholder = "Enter Assessment Title";

                AddAssessmentEntry.Text = null;
            }

        }//end AddCourseSaveBtn_Clicked

        private void AddAssmtFromDB()//Creates Buttons for all Terms in DB
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                assmtList = conn.Table<Assessment>().ToList();

                for (int i = 0; i < assmtList.Count; i++)
                {
                    int assmtID = assmtList[i].AssessmentID;
                    string btnTitle = assmtList[i].AssessmentTitle;

                    Button assmtBtn = new Button()
                    {
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        Margin = 30,
                        BackgroundColor = Color.White,
                        CornerRadius = 10
                    };

                    assmtBtn.Clicked += (sender, args) => GoToAssessmentBtn_Clicked(sender, args, assmtID);
                    assmtBtn.BindingContext = assmtList[i];
                    assmtBtn.SetBinding(Button.TextProperty, "AssessmentTitle");

                    layout.Children.Add(assmtBtn);
                }
            }
        }//end AddAssmtFromDB
        private void DeleteButtons()//Delete buttons to replace refreshed
        {
            for (int i = 20; i < layout.Children.Count;)
            {
                layout.Children.RemoveAt(i);
            }
        }//end DeleteButtons

        private async void GoToAssmtBtn_Clicked(object sender, EventArgs e, int id)
        {

            await Navigation.PushAsync(new AssessmentPage(id));//USE WHEN READY TO ADD ASSMTS


        }//end GoToAssessmentBtn_Clicked
        private void SetCourseNotification_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();
            }

            for (int i = 0; i < courseList.Count; i++)
            {
                if (courseList[i].CourseID == CurrentCourseID)
                {
                    start = courseList[i].StartDate;
                    end = courseList[i].EndDate;
                    if (courseList[i].NotificationID > 0)
                    {
                        CrossLocalNotifications.Current.Cancel(courseList[i].NotificationID);
                    }
                    for (int j = 0; j < courseList.Count; j++)
                    {
                        if (courseList[j].NotificationID > 0)
                        {
                            alertID++;
                        }
                    }//for j
                    break;
                }
            }//for i

            foreach (Course row in courseList)
            {
                if (row.CourseID == CurrentCourseID)
                {
                    row.NotificationID = alertID;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        int rows = conn.Update(row);
                    }
                    break;
                }
            }//foreach

            DisplayAlert(alertID.ToString(), "Notification set!\n" + "Start date: " + start.ToString() + "\n" + "End date: " + end.ToString(), "OK");//DELETE THIS except for 'Notification set!'
            CrossLocalNotifications.Current.Show(" ", "Start date: " + start.ToString() + "\n" + "End date: " + end.ToString(), alertID, start);
            //CrossLocalNotifications.Current.Show("Alert!!!", "End date: + " + end.ToString(), alertID, end);
        }//end AlertTest_Clicked
        private void DeleteAssmtRows()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DeleteAll<Assessment>();
            }
        }//end DeleteCourseRows
        private void DropAddAssmtTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DropTable<Assessment>();
                conn.CreateTable<Assessment>();
            }
        }//end DropCourseTableAddCourseTable

    }
}