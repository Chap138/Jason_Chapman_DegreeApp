using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;

namespace Jason_Chapman_MobileDev_C971
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        private List<Assessment> assmtList;
        private List<Course> courseList;
        private List<Term> termList;
        private int CurrentTermID;
        private string CurrentTermTitle;
        private DateTime CurrentTermStart;
        private DateTime CurrentTermEnd;
        int numCourses;
        private int currentTermID;
        private int currentCourseID;

        public TermPage(int termID)
        {
            InitializeComponent();
            CurrentTermID = termID;
            BindingContext = this;
            //DeleteCourseRows();
            //DropCourseTableAddCourseTable();
            AddCourseFromDB();

        }//end constructor

        protected override void OnAppearing()
        {
            GetTerm();
            DeleteButtons();
            AddCourseFromDB();
        }//end OnAppearing

        private void AddCourseSaveBtn_Clicked(object sender, EventArgs e)
        {
            if (AddCourseEntry.Text == null ||
                CourseInstructorName.Text == null ||
                CourseInstructorPhone.Text == null ||
                CourseInstructorEmail.Text == null ||
                CourseNotesEditor.Text == null ||
                CourseStartDatePicker.ToString() == null ||
                CourseEndDatePicker.ToString() == null||
                CourseProgressPicker.SelectedItem == null)
            {
                DisplayAlert(" ", "Please enter all fields.", "OK");
            }
            else if (CourseStartDatePicker.Date >= CourseEndDatePicker.Date)
            {
                DisplayAlert(" ", "The start date can not occur on or after the end date.", "OK");
            }
            else
            {
                TermPageStartDateLabel.IsVisible = true;
                TermStartDatePicker.IsVisible = true;
                TermEndDatePicker.IsVisible = true;
                TermPageEndDateLabel.IsVisible = true;

                AddCourseEntry.IsVisible = false;
                CourseInstructorName.IsVisible = false;
                CourseInstructorPhone.IsVisible = false;
                CourseInstructorEmail.IsVisible = false;
                AddCourseSaveBtn.IsVisible = false;
                AddCourseCancelBtn.IsVisible = false;
                CourseStartDatePicker.IsVisible = false;
                CourseStartDateLabel.IsVisible = false;
                CourseEndDatePicker.IsVisible = false;
                CourseEndDateLabel.IsVisible = false;
                CourseStatusLabel.IsVisible = false;
                CourseProgressPicker.IsVisible = false;
                CourseNotesEditor.IsVisible = false;

                Course course = new Course()
                {
                    TermID = CurrentTermID,
                    CourseTitle = AddCourseEntry.Text,
                    InstructorName = CourseInstructorName.Text,
                    InstructorPhone = CourseInstructorPhone.Text,
                    InstructorEmail = CourseInstructorEmail.Text,
                    CourseStatus = CourseProgressPicker.SelectedItem.ToString(),
                    StartDate = CourseStartDatePicker.Date,
                    EndDate = CourseEndDatePicker.Date,
                    CourseNotes = CourseNotesEditor.Text
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Course>();
                    conn.Insert(course);
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
                    BorderWidth = 2
                };

                int courseID = course.CourseID;
                testBtn.Clicked += (s, a) => GoToCourseBtn_Clicked(s, a, courseID);
                testBtn.BindingContext = course;
                testBtn.SetBinding(Button.TextProperty, "CourseTitle");
                layout.Children.Add(testBtn);
                AddCourseEntry.Placeholder = "Enter Course Title";

                AddCourseEntry.Text = null;
                CourseInstructorName.Text = null;
                CourseInstructorPhone.Text = null;
                CourseInstructorEmail.Text = null;
                CourseNotesEditor.Text = null;
            }

        }//end AddCourseSaveBtn_Clicked

        private void AddCourseFromDB()//Creates Buttons for all Terms in DB
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();

                for (int i = 0; i < courseList.Count; i++)
                {
                    if (courseList[i].TermID == CurrentTermID)
                    {
                        int courseID = courseList[i].CourseID;
                        string btnTitle = courseList[i].CourseTitle;

                        Button testBtn = new Button()
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

                        testBtn.Clicked += (sender, args) => GoToCourseBtn_Clicked(sender, args, courseID);
                        testBtn.BindingContext = courseList[i];
                        testBtn.SetBinding(Button.TextProperty, "CourseTitle");

                        layout.Children.Add(testBtn);
                    }//if
                }//for
            }
        }//end AddCourseFromDB

        private void DeleteButtons()//Delete buttons to replace refreshed
        {
            for (int i = 22; i < layout.Children.Count;)
            {
                layout.Children.RemoveAt(i);
            }
        }//end DeleteButtons

        public void GetTerm()//Update term info when page appears
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                termList = conn.Table<Term>().ToList();
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
            TermStartDatePicker.Date = CurrentTermStart;
            TermEndDatePicker.Date = CurrentTermEnd;
        }//end GetTerm()

        private async void GoToCourseBtn_Clicked(object sender, EventArgs e, int id)
        {
            await Navigation.PushAsync(new CoursePage(id));
        }//end GoToCourseBtn_Clicked

        private void EditTerm_Clicked(object sender, EventArgs e)
        {
            TermStartDatePicker.IsVisible = true;
            TermPageStartDateLabel.IsVisible = true;
            TermEndDatePicker.IsVisible = true;
            TermPageEndDateLabel.IsVisible = true;

            TitleEntry.IsVisible = true;
            TitleEntry.Focus();
            EditTermSaveBtn.IsVisible = true;
            EditTermCancelBtn.IsVisible = true;

            CourseStartDatePicker.IsVisible = false;
            CourseStartDateLabel.IsVisible = false;
            CourseEndDatePicker.IsVisible = false;
            CourseEndDateLabel.IsVisible = false;

            AddCourseEntry.IsVisible = false;
            CourseInstructorName.IsVisible = false;
            CourseInstructorPhone.IsVisible = false;
            CourseInstructorEmail.IsVisible = false;
            AddCourseSaveBtn.IsVisible = false;
            AddCourseCancelBtn.IsVisible = false;
            CourseStatusLabel.IsVisible = false;
            CourseProgressPicker.IsVisible = false;
            CourseNotesEditor.IsVisible = false;
            DeleteTerm.IsVisible = false;
        }//end EditTerm_Clicked

        private void EditTermSaveBtn_Clicked(object sender, EventArgs e)
        {
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

                    if (TermStartDatePicker.Date < TermEndDatePicker.Date)//Will save term edits if true
                    {
                        row.Start = TermStartDatePicker.Date;
                        row.End = TermEndDatePicker.Date;

                        TitleEntry.IsVisible = false;
                        EditTermSaveBtn.IsVisible = false;
                        EditTermCancelBtn.IsVisible = false;
                        DeleteTerm.IsVisible = true;
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

        }//end EditTermSaveBtn_Clicked

        private void EditTermCancelBtn_Clicked(object sender, EventArgs e)
        {
            TitleEntry.IsVisible = false;
            EditTermSaveBtn.IsVisible = false;
            EditTermCancelBtn.IsVisible = false;

            DeleteTerm.IsVisible = true;

            TermStartDatePicker.Date = CurrentTermStart;
            TermEndDatePicker.Date = CurrentTermEnd;
        }//end EditTermCancelBtn_Clicked
        private void AddCourseCancelBtn_Clicked(object sender, EventArgs e)
        {
            TermPageStartDateLabel.IsVisible = true;
            TermStartDatePicker.IsVisible = true;
            TermEndDatePicker.IsVisible = true;
            TermPageEndDateLabel.IsVisible = true;

            AddCourseEntry.IsVisible = false;
            CourseInstructorName.IsVisible = false;
            CourseInstructorPhone.IsVisible = false;
            CourseInstructorEmail.IsVisible = false;
            AddCourseSaveBtn.IsVisible = false;
            AddCourseCancelBtn.IsVisible = false;
            CourseStartDatePicker.IsVisible = false;
            CourseStartDateLabel.IsVisible = false;
            CourseEndDatePicker.IsVisible = false;
            CourseEndDateLabel.IsVisible = false;
            CourseStatusLabel.IsVisible = false;
            CourseProgressPicker.IsVisible = false;
            CourseNotesEditor.IsVisible = false;
            DeleteTerm.IsVisible = true;
        }
        private void AddCourse_Clicked(object sender, EventArgs e)//ADD COURSES
        {
            numCourses = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();
                for (int i = 0; i < courseList.Count; i++)
                {
                    if (courseList[i].TermID == CurrentTermID)
                    {
                        numCourses++;
                    }
                }
            }

            if (numCourses < 6)
            {
                TermPageStartDateLabel.IsVisible = false;
                TermStartDatePicker.IsVisible = false;
                TermEndDatePicker.IsVisible = false;
                TermPageEndDateLabel.IsVisible = false;
                DeleteTerm.IsVisible = false;

                TitleEntry.IsVisible = false;
                EditTermSaveBtn.IsVisible = false;
                EditTermCancelBtn.IsVisible = false;

                AddCourseEntry.IsVisible = true;
                AddCourseEntry.Focus();
                CourseInstructorName.IsVisible = true;
                CourseInstructorPhone.IsVisible = true;
                CourseInstructorEmail.IsVisible = true;
                CourseStatusLabel.IsVisible = true;
                CourseProgressPicker.IsVisible = true;
                CourseStartDateLabel.IsVisible = true;
                CourseStartDatePicker.IsVisible = true;
                CourseEndDatePicker.IsVisible = true;
                CourseEndDateLabel.IsVisible = true;
                AddCourseSaveBtn.IsVisible = true;
                AddCourseCancelBtn.IsVisible = true;
                CourseNotesEditor.IsVisible = true;
                AddCourseEntry.Text = null;
            }
            else DisplayAlert(" ", "Can not add more than 6 courses to this term.", "OK");
        }//end AddCourse_Clicked
        private void DeleteCourseRows()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DeleteAll<Course>();
            }
        }//end DeleteCourseRows
        private void DropCourseTableAddCourseTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DropTable<Course>();
                conn.CreateTable<Course>();
            }
        }//end DropCourseTableAddCourseTable

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Alert!", "Are you sure you want to delete this term?", "Yes", "No");

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                assmtList = conn.Table<Assessment>().ToList();
            }

            if (answer)
            {
                foreach (Course row in courseList)
                {
                    if (row.TermID == CurrentTermID)
                    {
                        currentCourseID = row.CourseID;
                        foreach (Assessment rowAssmt in assmtList)
                        {
                            if (rowAssmt.CourseID == currentCourseID)
                            {
                                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                                {
                                    conn.Delete(rowAssmt);
                                    conn.Update(rowAssmt);
                                }
                            }
                        }//delete assessments associated with this course

                        using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                        {
                            conn.Delete(row);
                            conn.Update(row);
                        }
                        break;
                    }//course row
                }//course foreach
                foreach (Term rowT in termList)
                {
                    if (rowT.ID == CurrentTermID)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                        {
                            conn.Delete(rowT);
                        }
                        await DisplayAlert(" ", "Term deleted.", "OK");
                        await Navigation.PopAsync();//Goes back to previous page

                        break;
                    }
                }
            }
            else return;
        }//end DeleteTerm_Clicked
    }
}
