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
    public partial class Course5Page : ContentPage
    {
        public Course5Page()
        {
            InitializeComponent();
            Term1Page term1 = new Term1Page();
            Course5.Text = term1.term1List[4].CourseTitle;
        }
    }
}