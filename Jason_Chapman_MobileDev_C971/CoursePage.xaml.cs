using Plugin.LocalNotifications;
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
        DateTime Start;
        DateTime End;
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
            //DeleteAssessmentRows();
            //DropAssmtTableAddAssmtTable();
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

        private void AddAssessmentCancelBtn_Clicked(object sender, EventArgs e)
        {
            CoursePageStartDateLabel.IsVisible = true;
            StartDatePicker.IsVisible = true;
            EndDatePicker.IsVisible = true;
            CoursePageEndDateLabel.IsVisible = true;

            AddAssessmentEntry.IsVisible = false;
            AddAssessmentSaveBtn.IsVisible = false;
            AddAssessmentCancelBtn.IsVisible = false;
            AssessmentStartDatePicker.IsVisible = false;
            AssessmentStartDateLabel.IsVisible = false;
            AssessmentEndDatePicker.IsVisible = false;
            AssessmentEndDateLabel.IsVisible = false;
            AssessmentNotesEditor.IsVisible = false;
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

                AddAssessmentEntry.IsVisible = true;
                AddAssessmentEntry.Focus();
                AssessmentDueDateLabel.IsVisible = true;
                AssessmentDueDatePicker.IsVisible = true;
                AddAssessmentSaveBtn.IsVisible = true;
                AddAssessmentCancelBtn.IsVisible = true;
                AssessmentNotesEditor.IsVisible = true;
                AddAssessmentEntry.Text = null;
            }
            else DisplayAlert(" ", "Can not add more than 2 assessments to this course. (One performance and one objective)", "OK");
        }//end AddAssmt_Clicked


        private void AddAssessmentSaveBtn_Clicked(object sender, EventArgs e)
        {
            if (AddAssessmentEntry.Text == null ||
                AssessmentNotesEditor.Text == null ||)
            {
                DisplayAlert(" ", "Please enter all fields.", "OK");
            }
            else
            {
                CoursePageStartDateLabel.IsVisible = true;
                StartDatePicker.IsVisible = true;
                EndDatePicker.IsVisible = true;
                CoursePageEndDateLabel.IsVisible = true;

                AddAssessmentEntry.IsVisible = false;
                AddAssessmentSaveBtn.IsVisible = false;
                AddAssessmentCancelBtn.IsVisible = false;
                AssessmentDueDatePicker.IsVisible = false;
                AssessmentDueDateLabel.IsVisible = false;
                CourseNotesEditor.IsVisible = false;
                //CourseProgressPicker.SelectedItem = null;

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

                int AssessmentID = assmt.CourseID;
                testBtn.Clicked += (s, a) => GoToAssmtBtn_Clicked(s, a, ID);
                testBtn.BindingContext = course;
                testBtn.SetBinding(Button.TextProperty, "CourseTitle");
                layout.Children.Add(testBtn);
                AddCourseEntry.Placeholder = "Enter Course Title";

                AddCourseEntry.Text = null;
                CourseInstructorName.Text = null;
                CourseInstructorPhone.Text = null;
                CourseInstructorEmail.Text = null;
            }

        }//end AddCourseSaveBtn_Clicked

        private void AddAssessmentFromDB()//Creates Buttons for all Terms in DB
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();

                for (int i = 0; i < courseList.Count; i++)
                {
                    int courseID = courseList[i].CourseID;
                    string btnTitle = courseList[i].CourseTitle;

                    Button testBtn = new Button()
                    {
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        Margin = 30,
                        BackgroundColor = Color.White,
                        CornerRadius = 10
                    };

                    testBtn.Clicked += (sender, args) => GoToCourseBtn_Clicked(sender, args, courseID);
                    testBtn.BindingContext = courseList[i];
                    testBtn.SetBinding(Button.TextProperty, "CourseTitle");

                    layout.Children.Add(testBtn);
                }
            }
        }//end AddCourseFromDB
        private void DeleteButtons()//Delete buttons to replace refreshed
        {
            for (int i = 20; i < layout.Children.Count;)
            {
                layout.Children.RemoveAt(i);
            }
        }//end DeleteButtons

        private async void GoToAssessmentBtn_Clicked(object sender, EventArgs e, int id)
        {

            await Navigation.PushAsync(new CoursePage(id));//USE WHEN READY TO ADD COURSES

            //await Navigation.PushAsync(new Course1Page());//TEST TEST TEST delete when ready to add courses

        }//end GoToAssessmentBtn_Clicked
        private void SetNotification_Clicked(object sender, EventArgs e/*, DateTime start, DateTime end*/)
        {
            //alertID++;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();
            }

            for (int i = 0; i < courseList.Count; i++)
            {
                if (courseList[i].CourseID == CurrentCourseID)
                {
                    //alertID = courseList[i].NotificationID;
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
        private void DeleteAssessmentRows()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DeleteAll<Assessment>();
            }
        }//end DeleteCourseRows
        private void DropAssmtTableAddAssmtTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DropTable<Assessment>();
                conn.CreateTable<Assessment>();
            }
        }//end DropCourseTableAddCourseTable

    }
}