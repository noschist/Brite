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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Brite.CusPages.Register
{
    /// <summary>
    /// Interaction logic for StudentPage2.xaml
    /// </summary>
    public partial class StudentPage2 : Page
    {

        LoginWindow loginWindow;

        public StudentPage2(LoginWindow window)
        {
            InitializeComponent();
            loginWindow = window;
        }

        private void PreventCopyPaste(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        DataTable dt = new DataTable();
        SQLSetup sql = new SQLSetup();

        public void SaveStudentDetails()
        {
            if(Validator())
            {
                sql.LoadTable($"select * from Student where Roll = '{StudentRoll.Text.Trim()}' or Email = '{StudentEmail.Text.Trim()}'", ref dt);
                if(dt.Rows.Count == 0)
                {
                    string branch = ((ComboBoxItem)StudentBranch.SelectedItem).Content.ToString();
                    string sem = ((ComboBoxItem)StudentSem.SelectedItem).Content.ToString();
                    string query = $"insert into Student values ('{StudentName.Text.Trim()}', '{StudentRoll.Text.Trim()}', '{branch}', '{sem}', '{StudentEmail.Text.Trim()}', '{StudentPass.Password.Trim()}')";
                    sql.ExecuteQuery(query);
                    MsgBoxLogin msgBox = new MsgBoxLogin("succ", "Account successfully created", "Cool")
                    {
                        Owner = loginWindow,
                        Width = loginWindow.ActualWidth,
                        Height = loginWindow.ActualHeight
                    };
                    msgBox.ShowDialog();
                    Properties.Settings.Default.isStu = true;
                    Properties.Settings.Default.currentUser = StudentEmail.Text.Trim();
                    Properties.Settings.Default.Save();

                }
                else
                {
                    MsgBoxLogin msgBox = new MsgBoxLogin("error", "Account already exists", "Login")
                    {
                        Owner = loginWindow,
                        Width = loginWindow.ActualWidth,
                        Height = loginWindow.ActualHeight
                    };
                    msgBox.ShowDialog();
                }
            }
        }

        private bool Validator()
        {
            if (StudentName.Text.Trim() == "" || StudentRoll.Text.Trim() == "" || StudentEmail.Text.Trim() == "" || StudentPass.Password.Trim() == "")
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please fill in all necessary fields", "Try again")
                {
                    Owner = loginWindow,
                    Width = loginWindow.ActualWidth,
                    Height = loginWindow.ActualHeight
                };
                msgBox.ShowDialog();
            }
            else if (!Regex.IsMatch(StudentEmail.Text.Trim(), @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please enter a valid mail-id", "Okay")
                {
                    Owner = loginWindow,
                    Width = loginWindow.ActualWidth,
                    Height = loginWindow.ActualHeight
                };
                msgBox.ShowDialog();
            }
            else if (!Regex.IsMatch(StudentRoll.Text.Trim(), @"^AM\.EN\.U4[A-Z]{0,3}[0-9]{0,5}$"))
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please enter a valid roll no", "Okay")
                {
                    Owner = loginWindow,
                    Width = loginWindow.ActualWidth,
                    Height = loginWindow.ActualHeight
                };
                msgBox.ShowDialog();
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}
