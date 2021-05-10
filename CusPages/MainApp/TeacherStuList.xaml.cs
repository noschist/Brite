using Brite.CusClasses;
using System;
using System.Data;
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
using Brite.CusControls;

namespace Brite.CusPages.MainApp
{
    /// <summary>
    /// Interaction logic for TeacherStuList.xaml
    /// </summary>
    public partial class TeacherStuList : Page
    {
        public TeacherStuList()
        {
            InitializeComponent();
            Populate();
        }

        SQLSetup sql = new SQLSetup();
        DataTable dt = new DataTable();

        private void Populate()
        {
            sql.LoadTable("select * from Student", ref dt);
            foreach(DataRow row in dt.Rows)
            {
                StudentBox stuList = new StudentBox();
                stuList.StuName.Text = row[0].ToString();
                stuList.StuRoll.Text = row[1].ToString();
                stuList.StuBranch.Text = row[2].ToString();
                stuList.StuSem.Text = row[3].ToString();
                StudentContainer.Children.Add(stuList);
            }
        }
    }
}
