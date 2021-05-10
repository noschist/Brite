using Brite.CusPages.Register;
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
using System.Windows.Shapes;

namespace Brite
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginFrame.Navigate(pageCommon);
            studentPage2 = new StudentPage2(this);
            teacherPage2 = new TeacherPage2(this);
        }

        PageCommon pageCommon = new PageCommon();
        StudentPage2 studentPage2;
        TeacherPage2 teacherPage2;

        private void Step1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!pageCommon.isStudent && !pageCommon.isTeacher)
            {
                //Select an option
            }
            else
            {
                page = 2;
                if (pageCommon.isStudent)
                {
                    NavigatorStudent("backward");
                }
                else if (pageCommon.isTeacher)
                {
                    NavigatorTeacher("backward");
                }
            }
        }

        private void Step2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!pageCommon.isStudent && !pageCommon.isTeacher)
            {
                //Select an option
            }
            else
            {
                page = 1;
                if (pageCommon.isStudent)
                {
                    NavigatorStudent("forward");
                }
                else if (pageCommon.isTeacher)
                {
                    NavigatorTeacher("forward");
                }
            }
        }

        int page = 1;

        private void NavigatorStudent(string waytogo)
        {
            if (waytogo == "forward")
            {
                switch (page)
                {
                    case 1:
                        PrevBtn.Visibility = Visibility.Visible;
                        NextBtn.Content = "Finish";
                        Step2Desc.Visibility = Visibility.Visible;
                        Step1Desc.Visibility = Visibility.Collapsed;
                        LoginFrame.Navigate(studentPage2);
                        page++;
                        break;
                }
            }
            else
            {
                switch (page)
                {
                    case 2:
                        PrevBtn.Visibility = Visibility.Hidden;
                        NextBtn.Visibility = Visibility.Visible;
                        NextBtn.Content = "Next Step";
                        Step1Desc.Visibility = Visibility.Visible;
                        Step2Desc.Visibility = Visibility.Collapsed;
                        LoginFrame.Navigate(pageCommon);
                        page--;
                        break;
                }
            }
        }

        private void NavigatorTeacher(string waytogo)
        {
            if (waytogo == "forward")
            {
                switch (page)
                {
                    case 1:
                        PrevBtn.Visibility = Visibility.Visible;
                        NextBtn.Content = "Finish";
                        Step2Desc.Visibility = Visibility.Visible;
                        Step1Desc.Visibility = Visibility.Collapsed;
                        LoginFrame.Navigate(teacherPage2);
                        page++;
                        break;
                }
            }
            else
            {
                switch (page)
                {
                    case 2:
                        PrevBtn.Visibility = Visibility.Hidden;
                        NextBtn.Visibility = Visibility.Visible;
                        NextBtn.Content = "Next Step";
                        Step1Desc.Visibility = Visibility.Visible;
                        Step2Desc.Visibility = Visibility.Collapsed;
                        LoginFrame.Navigate(pageCommon);
                        page--;
                        break;
                }
            }
        }


        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!pageCommon.isStudent && !pageCommon.isTeacher)
            {
                //Select an option
            }
            else if((string)NextBtn.Content == "Finish")
            {
                //SaveDetails();
                if (pageCommon.isStudent)
                    studentPage2.SaveStudentDetails();
                else if (pageCommon.isTeacher)
                    teacherPage2.SaveTeacherDetails();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();

            }
            else if(pageCommon.isStudent)
            {
                NavigatorStudent("forward");
            }
            else if (pageCommon.isTeacher)
            {
                NavigatorTeacher("forward");
            }
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!pageCommon.isStudent && !pageCommon.isTeacher)
            {
                //Select an option
            }
            else if (pageCommon.isStudent)
            {
                NavigatorStudent("backward");
            }
            else if (pageCommon.isTeacher)
            {
                NavigatorTeacher("backward");
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoginBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            LoginUI loginUI = new LoginUI();
            loginUI.Show();
            Close();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginUI loginUI = new LoginUI();
            loginUI.Show();
            Close();
        }
    }
}
