using Brite.CusClasses;
using Brite.CusControls;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Brite.CusPages.MainApp
{
    /// <summary>
    /// Interaction logic for StudentDash.xaml
    /// </summary>
    public partial class StudentDash : Page
    {
        public StudentDash()
        {
            InitializeComponent();
            Populate();
        }

        SQLSetup sql = new SQLSetup();
        DataTable dt = new DataTable();

        private void Populate()
        {
            sql.LoadTable($"select sem, branch from Student where email = '{Properties.Settings.Default.currentUser}';", ref dt);
            string sem = dt.Rows[0][0].ToString();
            string branch = dt.Rows[0][1].ToString();
            sql.LoadTable($"select cname, ccode from {branch.ToLower()}{sem} limit 4", ref dt);
            foreach (DataRow row in dt.Rows)
            {
                CourseBox courseBox = new CourseBox(row[0].ToString(), row[1].ToString());
                CourseHolder.Children.Add(courseBox);
            }
        }
    }
}
