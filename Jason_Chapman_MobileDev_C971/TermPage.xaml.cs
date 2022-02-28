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
        private bool courseSaveValid;
        int numCourses;

        public TermPage(int termID)
        {
            InitializeComponent();
            CurrentTermID = termID;
            BindingContext = this;
            //DeleteCourseRows();
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
                CourseProgressPicker.SelectedItem.ToString() == null)
            {
                DisplayAlert(" ", "Please enter all fields.", "OK");
            }
            else
            {
                TermPageStartDateLabel.IsVisible = true;
                StartDatePicker.IsVisible = true;
                EndDatePicker.IsVisible = true;
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
                //CourseProgressPicker.SelectedItem = null;

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
                    Margin = 30,
                    BackgroundColor = Color.White,
                    CornerRadius = 10
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
            }

        }//end AddCourseSaveBtn_Clicked

        private void AddCourseFromDB()//Creates Buttons for all Terms in DB
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

        private async void GoToCourseBtn_Clicked(object sender, EventArgs e, int id)
        {

            await Navigation.PushAsync(new CoursePage(id));//USE WHEN READY TO ADD COURSES

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
        private void AddCourseCancelBtn_Clicked(object sender, EventArgs e)
        {
            TermPageStartDateLabel.IsVisible = true;
            StartDatePicker.IsVisible = true;
            EndDatePicker.IsVisible = true;
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
                StartDatePicker.IsVisible = false;
                EndDatePicker.IsVisible = false;
                TermPageEndDateLabel.IsVisible = false;

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
