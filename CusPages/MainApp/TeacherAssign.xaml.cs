using Brite.CusClasses;
using Brite.CusWindow;
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
    /// Interaction logic for TeacherAssign.xaml
    /// </summary>
    public partial class TeacherAssign : Page
    {
        public TeacherAssign()
        {
            InitializeComponent();
        }

        private void ModifyQpBtn_Click(object sender, RoutedEventArgs e)
        {
            ModifyQB modifyQB = new ModifyQB();
            modifyQB.Owner = Application.Current.MainWindow;
            modifyQB.ShowDialog();
        }

        DataTable dt = new DataTable();
        SQLSetup sql = new SQLSetup();

        private void CreateQpBtn_Click(object sender, RoutedEventArgs e)
        {
            sql.LoadTable($";", ref dt);
        }
    }
}
