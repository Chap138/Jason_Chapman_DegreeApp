﻿using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        private string CurrentCourseProgress;
        private string CurrentCourseNotes;
        private DateTime CurrentCourseStart;
        private string CurrentCourseInstructorName;
        private string CurrentCourseInstructorPhone;
        private string CurrentCourseInstructorEmail;
        private DateTime CurrentCourseEnd;
        //private string title = "Course Title";
        //private bool courseSaveValid;
        int numAssmts;

        public CoursePage(int courseID)
        {
            InitializeComponent();
            CurrentCourseID = courseID;
            //DropAddAssmtTable();
            //AddAssessmentFromDB();
        }
        protected override void OnAppearing()
        {
            GetCourse();
            DeleteButtons();
            AddAssmtFromDB();
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
                    CurrentCourseProgress = row.CourseStatus;
                    CurrentCourseStart = row.StartDate;
                    CurrentCourseEnd = row.EndDate;
                    CurrentCourseInstructorName = row.InstructorName;
                    CurrentCourseInstructorPhone = row.InstructorPhone;
                    CurrentCourseInstructorEmail = row.InstructorEmail;
                    CurrentCourseNotes = row.CourseNotes;
                }
            }
            CourseLabel.Text = CurrentCourseTitle;
            ProgressLabel.Text = "Status: " + CurrentCourseProgress;
            StartDatePicker.Date = CurrentCourseStart;
            EndDatePicker.Date = CurrentCourseEnd;
            InstructorName.Text = "Name: " + CurrentCourseInstructorName;
            InstructorPhone.Text = "Phone: " + CurrentCourseInstructorPhone;
            InstructorEmail.Text = "Email: " + CurrentCourseInstructorEmail;
            CourseNotes.Text = CurrentCourseNotes;
        }//end GetCourse()

        private void EditCourse_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new EditTermPage(CurrentTermID));
            EditCourseTitleEntry.IsVisible = true;
            EditCourseTitleEntry.Focus();
            EditCourseInstructorName.IsVisible = true;
            EditCourseInstructorPhone.IsVisible = true;
            EditCourseInstructorEmail.IsVisible = true;
            EditCourseNotesEditor.IsVisible = true;
            EditCourseProgressPicker.IsVisible = true;
            EditCourseSaveBtn.IsVisible = true;
            EditCourseCancelBtn.IsVisible = true;
        }//end EditCourse_Clicked

        private void EditCourseSaveBtn_Clicked(object sender, EventArgs e)
        {
            foreach (Course row in courseList)
            {
                if (row.CourseID == CurrentCourseID)
                {
                    if (EditCourseTitleEntry.Text == null)
                    { row.CourseTitle = CurrentCourseTitle; }
                    else
                    {
                        row.CourseTitle = EditCourseTitleEntry.Text;
                        CourseLabel.Text = EditCourseTitleEntry.Text;
                    }

                    if (EditCourseProgressPicker.SelectedItem == null)
                    { row.CourseStatus = CurrentCourseProgress; }
                    else
                    {
                        row.CourseStatus = EditCourseProgressPicker.SelectedItem.ToString();
                        ProgressLabel.Text = EditCourseProgressPicker.SelectedItem.ToString();
                    }

                    if (EditCourseInstructorName.Text == null)
                    { row.InstructorName = CurrentCourseInstructorName; }
                    else
                    {
                        row.InstructorName = EditCourseInstructorName.Text;
                        InstructorName.Text = EditCourseInstructorName.Text;
                    }

                    if (EditCourseInstructorPhone.Text == null)
                    { row.InstructorPhone = CurrentCourseInstructorPhone; }
                    else
                    {
                        row.InstructorPhone = EditCourseInstructorPhone.Text;
                        InstructorPhone.Text = EditCourseInstructorPhone.Text;
                    }

                    if (EditCourseInstructorEmail.Text == null)
                    { row.InstructorEmail = CurrentCourseInstructorEmail; }
                    else
                    {
                        row.InstructorEmail = EditCourseInstructorEmail.Text;
                        InstructorEmail.Text = EditCourseInstructorEmail.Text;
                    }

                    if (EditCourseNotesEditor.Text == null)
                    { row.CourseNotes = CurrentCourseNotes; }
                    else
                    {
                        row.CourseNotes = EditCourseNotesEditor.Text;
                        CourseNotes.Text = EditCourseNotesEditor.Text;
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
            EditCourseTitleEntry.IsVisible = false;
            EditCourseInstructorName.IsVisible = false;
            EditCourseInstructorPhone.IsVisible = false;
            EditCourseInstructorEmail.IsVisible = false;
            EditCourseNotesEditor.IsVisible = false;
            EditCourseProgressPicker.IsVisible = false;
            EditCourseSaveBtn.IsVisible = false;
            EditCourseCancelBtn.IsVisible = false;
        }//end EditCourseSaveBtn_Clicked

        private void EditCourseCancelBtn_Clicked(object sender, EventArgs e)
        {
            EditCourseTitleEntry.IsVisible = false;
            EditCourseInstructorName.IsVisible = false;
            EditCourseInstructorPhone.IsVisible = false;
            EditCourseInstructorEmail.IsVisible = false;
            EditCourseNotesEditor.IsVisible = false;
            EditCourseProgressPicker.IsVisible = false;
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
            AssmtDueDatePicker.IsVisible = false;
            AssmtDueDateLabel.IsVisible = false;
            AddAssmtTypePicker.IsVisible = false;
        }//end AddAssessmentCancelBtn_Clicked
        private void AddAssmt_Clicked(object sender, EventArgs e)//ADD ASSESSMENTS
        {
            numAssmts = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                assmtList = conn.Table<Assessment>().ToList();
                for (int i = 0; i < assmtList.Count; i++)
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
                AddAssmtSaveBtn.IsVisible = true;
                AddAssmtCancelBtn.IsVisible = true;
                AddAssmtTypePicker.IsVisible = true;
                AddAssmtEntry.Text = null;
            }
            else DisplayAlert(" ", "Can not add more than 2 assessments to this course. (One performance and one objective)", "OK");
        }//end AddAssmt_Clicked

        private void AddAssmtSaveBtn_Clicked(object sender, EventArgs e)
        {
            if (AddAssmtEntry.Text == null 
               /* || AssmtNotesEditor.Text == null*/)
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
                AddAssmtTypePicker.IsVisible = false;

                Assessment assmt = new Assessment()
                {
                    CourseID = CurrentCourseID,
                    AssessmentTitle = AddAssmtEntry.Text,
                    AssessmentType = AddAssmtTypePicker.SelectedItem.ToString(),
                    DueDate = AssmtDueDatePicker.Date
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

                AddAssmtEntry.Text = null;
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

                    assmtBtn.Clicked += (sender, args) => GoToAssmtBtn_Clicked(sender, args, assmtID);
                    assmtBtn.BindingContext = assmtList[i];
                    assmtBtn.SetBinding(Button.TextProperty, "AssessmentTitle");

                    layout.Children.Add(assmtBtn);
                }
            }
        }//end AddAssmtFromDB
        private void DeleteButtons()//Delete buttons to replace refreshed
        {
            for (int i = 28; i < layout.Children.Count;)
            {
                layout.Children.RemoveAt(i);
            }
        }//end DeleteButtons
        private async void GoToAssmtBtn_Clicked(object sender, EventArgs e, int id)
        {
            await Navigation.PushAsync(new AssessmentPage(id));//USE WHEN READY TO ADD ASSMTS

        }//end GoToAssmtBtn_Clicked
        private void SetCourseNotification_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();
            }

            for (int i = 0; i < courseList.Count; i++)
            {
                Debug.WriteLine(courseList[i].NotificationID);
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

            DisplayAlert(" ", "Notification set!", "OK");
            DisplayAlert("Alert ID: " + alertID.ToString(), "Notification set!\n" + "Start date: " + start.ToString() + "\n" + "End date: " + end.ToString(), "OK");//DELETE THIS except for 'Notification set!'
            CrossLocalNotifications.Current.Show(" ", "Start date: " + start.ToString() + "\n" + "End date: " + end.ToString(), alertID, start);
            //CrossLocalNotifications.Current.Show("Alert!!!", "End date: + " + end.ToString(), alertID, end);
        }//end SetCourseNotification_Clicked
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

        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Alert!", "Are you sure you want to delete this course?", "Yes", "No");
            int currentTermID;

            if (answer)
            {
                foreach (Course row in courseList)
                {
                    if (row.CourseID == CurrentCourseID)
                    {
                        currentTermID = row.TermID;
                        using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                        {
                            conn.Delete(row);
                        }
                        await DisplayAlert(" ", "Course deleted.", "OK");
                        await Navigation.PopAsync();//Goes back to previous page

                        break;
                    }
                }
            }
            else return;
        }//end DeleteCourse_Clicked

        private async void ShareCourseNotes_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(CurrentCourseNotes);
            //await Share.RequestAsync(new ShareTextRequest
            //{
            //    Text = text,
            //    Title = "Share Text"
            //});
        }
    }
}
