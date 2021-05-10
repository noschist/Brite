using Brite.CusClasses;
using Brite.CusPages.MainApp;
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
using System.Data;

namespace Brite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            if(Properties.Settings.Default.isStu)
            {
                TeacherSideBar.Visibility = Visibility.Collapsed;
                StudentSideBar.Visibility = Visibility.Visible;
                StudentDash studentDash = new StudentDash();
                MainFrame.Navigate(studentDash);
            }
            else
            {
                TeacherSideBar.Visibility = Visibility.Visible;
                StudentSideBar.Visibility = Visibility.Collapsed;
                TeacherDash teacherDash = new TeacherDash();
                MainFrame.Navigate(teacherDash);
            }

            GetNameOfUser();

        }

        SQLSetup sql = new SQLSetup();
        DataTable dt = new DataTable();

        private void GetNameOfUser()
        {

            if (Properties.Settings.Default.isStu)
                sql.LoadTable($"select name from Student where email = '{Properties.Settings.Default.currentUser}';", ref dt);
            else
                sql.LoadTable($"select name from Teacher where email = '{Properties.Settings.Default.currentUser}';", ref dt);

            UserName.Text = "Hi, " + dt.Rows[0][0].ToString();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginUI loginUI = new LoginUI();
            loginUI.Show();
            Close();
        }

        private void StuHome_Selected(object sender, RoutedEventArgs e)
        {
            StudentDash studentDash = new StudentDash();
            MainFrame.Navigate(studentDash);
        }

        private void StuCourse_Selected(object sender, RoutedEventArgs e)
        {
            StudentCourse studentCourse = new StudentCourse();
            MainFrame.Navigate(studentCourse);
        }

        private void TchrAssign_Selected(object sender, RoutedEventArgs e)
        {
            TeacherAssign teacherAssign = new TeacherAssign();
            MainFrame.Navigate(teacherAssign);
        }

        private void TchrDash_Selected(object sender, RoutedEventArgs e)
        {
            TeacherDash teacherDash = new TeacherDash();
            MainFrame.Navigate(teacherDash);
        }

        private void TchrStude_Selected(object sender, RoutedEventArgs e)
        {
            TeacherStuList teacherStuList = new TeacherStuList();
            MainFrame.Navigate(teacherStuList);
        }

        private void StuSetting_Selected(object sender, RoutedEventArgs e)
        {
            SettingsUI settingsUI = new SettingsUI();
            MainFrame.Navigate(settingsUI);
        }

        private void TchrSetting_Selected(object sender, RoutedEventArgs e)
        {
            SettingsUI settingsUI = new SettingsUI();
            MainFrame.Navigate(settingsUI);
        }
    }
}
