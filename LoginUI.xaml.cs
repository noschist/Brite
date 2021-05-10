using Brite.CusClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LoginUI.xaml
    /// </summary>
    public partial class LoginUI : Window
    {
        public LoginUI()
        {
            InitializeComponent();
            CheckForRem();
        }

        private void CheckForRem()
        {
            if(Properties.Settings.Default.Email != "" && Properties.Settings.Default.Password != "")
            {
                LoginEmail.Text = Properties.Settings.Default.Email;
                LoginPass.Password = Properties.Settings.Default.Password;
                UserRem.IsChecked = true;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CreatAccBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private bool Validator()
        {
            if (LoginEmail.Text.Trim() == "" || LoginPass.Password.Trim() == "")
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please fill in all necessary fields", "Try again")
                {
                    Owner = this,
                    Width = ActualWidth,
                    Height = ActualHeight
                };
                msgBox.ShowDialog();
            }
            else if (!Regex.IsMatch(LoginEmail.Text.Trim(), @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please enter a valid mail-id", "Okay")
                {
                    Owner = this,
                    Width = ActualWidth,
                    Height = ActualHeight
                };
                msgBox.ShowDialog();
            }
            else
            {
                return true;
            }

            return false;
        }

        DataTable dt = new DataTable();
        SQLSetup sql = new SQLSetup();

        private bool IsStudent()
        {
            sql.LoadTable($"select * from Student where email = '{LoginEmail.Text.Trim()}'", ref dt);
            if (dt.Rows.Count == 0)
                return false;
            return true;
        }

        private bool IsTeacher()
        {
            sql.LoadTable($"select * from Teacher where email = '{LoginEmail.Text.Trim()}'", ref dt);
            if (dt.Rows.Count == 0)
                return false;
            return true;
        }


        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Validator())
            {
                if(!IsStudent() && !IsTeacher())
                {
                    MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please check your username or password", "Okay")
                    {
                        Owner = this,
                        Width = ActualWidth,
                        Height = ActualHeight
                    };
                    msgBox.ShowDialog();
                }
                else if(IsTeacher())
                {
                    sql.LoadTable($"select email, pass from Teacher where email = '{LoginEmail.Text.Trim()}'", ref dt);
                    if (LoginEmail.Text.Trim() == dt.Rows[0][0].ToString() && LoginPass.Password.Trim() == dt.Rows[0][1].ToString())
                    {

                        //login success
                        if (UserRem.IsChecked == true)
                        {
                            Properties.Settings.Default.Email = LoginEmail.Text.Trim();
                            Properties.Settings.Default.Password = LoginPass.Password.Trim();
                            Properties.Settings.Default.Save();
                        }
                        else
                            Properties.Settings.Default.Reset();

                        Properties.Settings.Default.isStu = false;
                        Properties.Settings.Default.currentUser = LoginEmail.Text.Trim();
                        Properties.Settings.Default.Save();

                        MainWindow mainWin = new MainWindow();
                        mainWin.Show();
                        Close();

                    }
                    else
                    {
                        MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please check your username or password", "Okay")
                        {
                            Owner = this,
                            Width = ActualWidth,
                            Height = ActualHeight
                        };
                        msgBox.ShowDialog();
                    }
                }
                else if (IsStudent())
                {
                    sql.LoadTable($"select email, pass from Student where email = '{LoginEmail.Text.Trim()}'", ref dt);
                    if (LoginEmail.Text.Trim() == dt.Rows[0][0].ToString() && LoginPass.Password.Trim() == dt.Rows[0][1].ToString())
                    {
                        //login success
                        if (UserRem.IsChecked == true)
                        {
                            Properties.Settings.Default.Email = LoginEmail.Text.Trim();
                            Properties.Settings.Default.Password = LoginPass.Password.Trim();
                            Properties.Settings.Default.Save();
                        }
                        else
                            Properties.Settings.Default.Reset();

                        Properties.Settings.Default.isStu = true;
                        Properties.Settings.Default.currentUser = LoginEmail.Text.Trim();
                        Properties.Settings.Default.Save();

                        MainWindow mainWin = new MainWindow();
                        mainWin.Show();
                        Close();
                    }
                    else
                    {
                        MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please check your username or password", "Okay")
                        {
                            Owner = this,
                            Width = ActualWidth,
                            Height = ActualHeight
                        };
                        msgBox.ShowDialog();
                    }
                }
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
