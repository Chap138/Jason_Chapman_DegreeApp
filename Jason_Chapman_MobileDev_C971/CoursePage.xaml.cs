using Plugin.LocalNotifications;
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
        int numAssmts;

        public CoursePage(int courseID)
        {
            InitializeComponent();
            CurrentCourseID = courseID;
            //DropAddAssmtTable();
            //AddAssessmentFromDB();
            //DeleteAssmtRows();
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
            CourseStartDatePicker.Date = CurrentCourseStart;
            CourseEndDatePicker.Date = CurrentCourseEnd;
            InstructorName.Text = "Name: " + CurrentCourseInstructorName;
            InstructorPhone.Text = "Phone: " + CurrentCourseInstructorPhone;
            InstructorEmail.Text = "Email: " + CurrentCourseInstructorEmail;
            CourseNotes.Text = CurrentCourseNotes;
        }//end GetCourse()

        private void EditCourse_Clicked(object sender, EventArgs e)
        {
            Instructor.IsVisible = false;
            InstructorName.IsVisible = false;
            InstructorPhone.IsVisible = false;
            InstructorEmail.IsVisible = false;
            Notes.IsVisible = false;
            CourseNotes.IsVisible = false;

            AddAssmtEntry.IsVisible = false;
            AssmtDueDateLabel.IsVisible = false;
            AssmtDueDatePicker.IsVisible = false;
            AddAssmtTypePicker.IsVisible = false;
            AddAssmtSaveBtn.IsVisible = false;
            AddAssmtCancelBtn.IsVisible = false;
            Assessments.IsVisible = false;

            ShareCourseNotes.IsVisible = false;
            DeleteCourse.IsVisible = false;

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
            bool containsSpec = EditCourseTitleEntry.Text.Any(char.IsPunctuation) || EditCourseTitleEntry.Text.Any(char.IsSymbol);//Check for special characters

            foreach (Course row in courseList)
            {
                if (row.CourseID == CurrentCourseID)
                {
                    if (EditCourseTitleEntry.Text == null)
                    {
                        row.CourseTitle = CurrentCourseTitle;
                    }
                    else if (containsSpec)
                    {
                        DisplayAlert(" ", "Please use letters and numbers only.", "OK");
                    }
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

                    if (CourseStartDatePicker.Date < CourseEndDatePicker.Date)
                    {
                        row.StartDate = CourseStartDatePicker.Date;
                        row.EndDate = CourseEndDatePicker.Date;
                    }
                    else
                    {
                        DisplayAlert(" ", "The start date can not occur on or after the end date.", "OK");
                    }

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        int rows = conn.Update(row);
                    }
                    break;
                }
            }
            Instructor.IsVisible = true;
            InstructorName.IsVisible = true;
            InstructorPhone.IsVisible = true;
            InstructorEmail.IsVisible = true;
            Notes.IsVisible = true;
            CourseNotes.IsVisible = true;

            ShareCourseNotes.IsVisible = true;
            DeleteCourse.IsVisible = true;

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
            Instructor.IsVisible = true;
            InstructorName.IsVisible = true;
            InstructorPhone.IsVisible = true;
            InstructorEmail.IsVisible = true;
            Notes.IsVisible = true;
            CourseNotes.IsVisible = true;

            ShareCourseNotes.IsVisible = true;
            DeleteCourse.IsVisible = true;

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
            CourseStartDatePicker.IsVisible = true;
            CourseEndDatePicker.IsVisible = true;
            CoursePageEndDateLabel.IsVisible = true;
            Instructor.IsVisible = true;
            InstructorName.IsVisible = true;
            InstructorPhone.IsVisible = true;
            InstructorEmail.IsVisible = true;
            Notes.IsVisible = true;
            CourseNotes.IsVisible = true;
            Assessments.IsVisible = true;

            ShareCourseNotes.IsVisible = true;
            DeleteCourse.IsVisible = true;

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
                CourseStartDatePicker.IsVisible = false;
                CourseEndDatePicker.IsVisible = false;
                CoursePageEndDateLabel.IsVisible = false;
                Instructor.IsVisible = false;
                InstructorName.IsVisible = false;
                InstructorPhone.IsVisible = false;
                InstructorEmail.IsVisible = false;
                Notes.IsVisible = false;
                CourseNotes.IsVisible = false;

                EditCourseTitleEntry.IsVisible = false;
                EditCourseInstructorName.IsVisible = false;
                EditCourseInstructorPhone.IsVisible = false;
                EditCourseInstructorEmail.IsVisible = false;
                EditCourseNotesEditor.IsVisible = false;
                EditCourseProgressPicker.IsVisible = false;
                EditCourseSaveBtn.IsVisible = false;
                EditCourseCancelBtn.IsVisible = false;
                Assessments.IsVisible = false;

                ShareCourseNotes.IsVisible = false;
                DeleteCourse.IsVisible = false;

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
            bool containsSpec = AddAssmtEntry.Text.Any(char.IsPunctuation) || AddAssmtEntry.Text.Any(char.IsSymbol);//Check for special characters

            bool saveOkay = true;
            if (AddAssmtEntry.Text != null &&
                AddAssmtTypePicker.SelectedItem != null &&
                AssmtDueDatePicker.ToString() != null)
            {
                foreach (Assessment row in assmtList)
                {
                    if (row.CourseID == CurrentCourseID)
                    {
                        if (row.AssessmentType != AddAssmtTypePicker.SelectedItem.ToString())
                        {
                            saveOkay = true;
                        }
                        else if (containsSpec)
                        {
                            DisplayAlert(" ", "Please use letters and numbers only.", "OK");
                        }
                        else
                        {
                            saveOkay = false;
                            DisplayAlert(" ", "Only one PA and OA allowed per course.", "OK");
                            break;
                        }
                    }
                }
            }
            else
            {
                DisplayAlert(" ", "Please enter all fields.", "OK");
                saveOkay = false;
            }
            if (saveOkay)
            {
                CoursePageStartDateLabel.IsVisible = true;
                CourseStartDatePicker.IsVisible = true;
                CourseEndDatePicker.IsVisible = true;
                CoursePageEndDateLabel.IsVisible = true;
                Instructor.IsVisible = true;
                InstructorName.IsVisible = true;
                InstructorPhone.IsVisible = true;
                InstructorEmail.IsVisible = true;
                Notes.IsVisible = true;
                CourseNotes.IsVisible = true;
                Assessments.IsVisible = true;

                ShareCourseNotes.IsVisible = true;
                DeleteCourse.IsVisible = true;

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
                    CornerRadius = 10,
                    BorderColor = Color.LightSkyBlue,
                    BorderWidth = 2
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
                    if (assmtList[i].CourseID == CurrentCourseID)
                    {
                        int assmtID = assmtList[i].AssessmentID;
                        string btnTitle = assmtList[i].AssessmentTitle;

                        Button assmtBtn = new Button()
                        {
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = 20,
                            Margin = 20,
                            BackgroundColor = Color.White,
                            CornerRadius = 10,
                            BorderColor = Color.LightSkyBlue,
                            BorderWidth = 2
                        };

                        assmtBtn.Clicked += (sender, args) => GoToAssmtBtn_Clicked(sender, args, assmtID);
                        assmtBtn.BindingContext = assmtList[i];
                        assmtBtn.SetBinding(Button.TextProperty, "AssessmentTitle");

                        layout.Children.Add(assmtBtn);
                    }
                }
            }
        }//end AddAssmtFromDB
        private void DeleteButtons()//Delete buttons to replace refreshed
        {
            for (int i = 29; i < layout.Children.Count;)
            {
                layout.Children.RemoveAt(i);
            }
        }//end DeleteButtons
        private async void GoToAssmtBtn_Clicked(object sender, EventArgs e, int id)
        {
            await Navigation.PushAsync(new AssessmentPage(id));

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
            CrossLocalNotifications.Current.Show(" ", "Start date: " + start.ToString() + "\n" + "End date: " + end.ToString(), alertID, start);
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

            if (answer)
            {
                foreach (Assessment row in assmtList)
                {
                    if (row.CourseID == CurrentCourseID)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                        {
                            conn.Delete(row);
                        }
                    }
                }//delete assessments associated with this course

                foreach (Course row in courseList)
                {
                    if (row.CourseID == CurrentCourseID)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                        {
                            conn.Delete(row);
                        }
                        await DisplayAlert(" ", "Course deleted.", "OK");
                        await Navigation.PopAsync();//Goes back to previous page

                        break;
                    }//course row
                }//course foreach
            }
            else return;
        }//end DeleteCourse_Clicked

        private async void ShareCourseNotes_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(CurrentCourseNotes);

        }
    }
}
