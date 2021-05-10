using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Brite.CusControls
{
    /// <summary>
    /// Interaction logic for CourseBox.xaml
    /// </summary>
    public partial class CourseBox : UserControl
    {
        public CourseBox(string course, string ccode)
        {
            InitializeComponent();
            SetUp(course, ccode);
        }

        List<string> colorList = new List<string>(new string[] { "#16a085", "#27ae60", "#2980b9", "#8e44ad", "#2c3e50", "#c0392b", "#f1c40f" });

        private static Random random;

        private void SetUp(string course, string ccode)
        {
            random = new Random();
            int index = random.Next(random.Next(colorList.Count));
            SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorList[index]));
            CourseInitial.Text = course[0].ToString().ToUpper();
            CourseName.Text = course;
            CoursContainer.ToolTip = ccode + " - " + course;
            CoursContainer.Background = brush;
        }
    }
}
