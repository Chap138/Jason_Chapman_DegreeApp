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
    public partial class Course2Page : ContentPage
    {
        public Course2Page()
        {
            InitializeComponent();
            //Term1Page term1 = new Term1Page();
            //Course2.Text = term1.term1List[1].CourseTitle;
        }
    }
}