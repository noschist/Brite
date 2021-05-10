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
    /// Interaction logic for TeacherPage2.xaml
    /// </summary>
    public partial class TeacherPage2 : Page
    {

        LoginWindow loginWindow;

        public TeacherPage2(LoginWindow window)
        {
            InitializeComponent();
            loginWindow = window;
        }

        DataTable dt = new DataTable();
        SQLSetup sql = new SQLSetup();

        public void SaveTeacherDetails()
        {
            if (Validator())
            {
                sql.LoadTable($"select * from Teacher where Email = '{TeacherEmail.Text.Trim()}'", ref dt);
                if (dt.Rows.Count == 0)
                {
                    string dept = ((ComboBoxItem)TeacherDept.SelectedItem).Content.ToString();
                    string query = $"insert into Teacher (name, dept, email, pass) values ('{TeacherName.Text.Trim()}', '{dept}', '{TeacherEmail.Text.Trim()}', '{TeacherPass.Password.Trim()}')";
                    sql.ExecuteQuery(query);
                    MsgBoxLogin msgBox = new MsgBoxLogin("succ", "Account successfully created", "Cool")
                    {
                        Owner = loginWindow,
                        Width = loginWindow.ActualWidth,
                        Height = loginWindow.ActualHeight
                    };
                    msgBox.ShowDialog();
                    Properties.Settings.Default.isStu = false;
                    Properties.Settings.Default.currentUser = TeacherEmail.Text.Trim();
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
            if (TeacherName.Text.Trim() == "" || TeacherEmail.Text.Trim() == "" || TeacherPass.Password.Trim() == "")
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please fill in all necessary fields", "Try again")
                {
                    Owner = loginWindow,
                    Width = loginWindow.ActualWidth,
                    Height = loginWindow.ActualHeight
                };
                msgBox.ShowDialog();
            }
            else if (!Regex.IsMatch(TeacherEmail.Text.Trim(), @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please enter a valid mail-id", "Okay")
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

        private void PreventCopyPaste(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
