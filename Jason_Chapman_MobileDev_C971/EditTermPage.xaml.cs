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
    public partial class EditTermPage : ContentPage
    {
        private List<Term> termList;

        public int CurrentTermID { get; }
        public EditTermPage(int currentTermID)
        {
            InitializeComponent();
            CurrentTermID = currentTermID;
        }

        //private void EditTermSaveBtn_Clicked(object sender, EventArgs e)
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
        //    {
        //        termList = conn.Table<Term>().ToList();
        //        //conn.Table<Term>().Select(termList ,CurrentTermID)
        //    }
        //    Navigation.PushAsync(new TermPage(CurrentTermID));


        //    //foreach (Term row in termList)
        //    //{
        //    //    if (row.ID == CurrentTermID)
        //    //    {
        //    //        row.TermTitle = TitleEntry.Text;
        //    //    }
        //    //}
        //}//end EditTermSaveBtn_Clicked

        //public void GetTerm()//Update term info when page appears
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
        //    {
        //        termList = conn.Table<Term>().ToList();
        //        //conn.Table<Term>().Select(termList ,CurrentTermID)
        //    }

        //    foreach (Term row in termList)
        //    {
        //        if (row.ID == CurrentTermID)
        //        {
        //            CurrentTermTitle = row.TermTitle;
        //            CurrentTermStart = row.Start;
        //            CurrentTermEnd = row.End;
        //        }
        //    }
        //    TermLabel.Text = CurrentTermTitle;
        //    StartDate.Date = CurrentTermStart;
        //    EndDate.Date = CurrentTermEnd;
        //    //DisplayAlert(CurrentTermTitle, CurrentTermStart.ToString(), CurrentTermEnd.ToString());//Test to display CurrentTerm properties 
        //}//end GetTerm()

    }
}

