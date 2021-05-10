using Brite.CusClasses;
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
    /// Interaction logic for SettingsUI.xaml
    /// </summary>
    public partial class SettingsUI : Page
    {
        public SettingsUI()
        {
            InitializeComponent();
        }

        SQLSetup sql = new SQLSetup();
        DataTable dt = new DataTable();

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.isStu)
            {
                if(UserName.Text != "")
                {
                    string query = $"update Student set Name = '{UserName.Text}' where email = '{Properties.Settings.Default.currentUser}'";
                    sql.ExecuteQuery(query);
                }

                sql.LoadTable($"select Pass from Student where email = '{Properties.Settings.Default.currentUser}'", ref dt);

                if(UserOldPass.Password == dt.Rows[0][0].ToString() && (UserNewPass.Password == UserRepNewPass.Password))
                {
                    string query = $"update Student set Pass = '{UserRepNewPass.Password}' where email = '{Properties.Settings.Default.currentUser}'";
                    sql.ExecuteQuery(query);
                }

            }
            else
            {
                if (UserName.Text != "")
                {
                    string query = $"update Teacher set Name = '{UserName.Text}' where email = '{Properties.Settings.Default.currentUser}'";
                    sql.ExecuteQuery(query);
                }

                sql.LoadTable($"select Pass from Teacher where email = '{Properties.Settings.Default.currentUser}'", ref dt);

                if (UserOldPass.Password == dt.Rows[0][0].ToString() && (UserNewPass.Password == UserRepNewPass.Password))
                {
                    string query = $"update Teacher set Pass = '{UserRepNewPass.Password}' where email = '{Properties.Settings.Default.currentUser}'";
                    sql.ExecuteQuery(query);
                }
            }
        }
    }
}
