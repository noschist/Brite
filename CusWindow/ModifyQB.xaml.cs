using Brite.CusClasses;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace Brite.CusWindow
{
    /// <summary>
    /// Interaction logic for ModifyQB.xaml
    /// </summary>
    public partial class ModifyQB : Window
    {
        public ModifyQB()
        {
            InitializeComponent();
        }

        private void GetQid_Click(object sender, RoutedEventArgs e)
        {
            string dest = "ques.pdf";
            FileInfo file = new FileInfo(dest);
            file.Directory.Create();
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            iText.Layout.Element.Table table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

            sql.LoadTable($"select qid, ques, sub from Questions", ref dt);
            table.AddCell("QID");
            table.AddCell("Question");
            table.AddCell("Subject");


            foreach (DataRow row in dt.Rows)
            {
                table.AddCell(row[0].ToString());
                table.AddCell(row[1].ToString());
                table.AddCell(row[2].ToString());
            }

            doc.Add(table);
            doc.Close();
            System.Diagnostics.Process.Start(dest);
        }

        DataTable dt = new DataTable();
        SQLSetup sql = new SQLSetup();

        private void AddQuesBtn_Click(object sender, RoutedEventArgs e)
        {
            if(QpBranch.SelectedIndex != -1 && QpSem.SelectedIndex != -1 && QpDiff.SelectedIndex != -1 && QpSubject.SelectedIndex != -1 && QuesBox.Text != "")
            {
                string branch = ((ComboBoxItem)QpBranch.SelectedItem).Content.ToString();
                string sem = ((ComboBoxItem)QpSem.SelectedItem).Content.ToString();
                string diff = ((ComboBoxItem)QpDiff.SelectedItem).Content.ToString();
                DataRowView subject = (DataRowView)QpSubject.SelectedItem;
                string sub = subject.Row[0].ToString();
                string query = $"insert into Questions ('ques', 'branch', 'sem', 'diff', 'sub') values('{QuesBox.Text.Trim()}', '{branch}', {sem}, {diff}, '{sub}')";
                sql.ExecuteQuery(query);
                MsgBoxLogin msgBox = new MsgBoxLogin("suc", "Question added successfully", "Cool")
                {
                    Owner = this,
                    Width = ActualWidth,
                    Height = ActualHeight
                };
                msgBox.ShowDialog();
                QpBranch.SelectedItem = -1;
                QpSem.SelectedItem = -1;
                QpSubject.SelectedItem = -1;
                QpDiff.SelectedItem = -1;
                QuesBox.Text = "";
            }
            else
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Please fill in all fields", "Okay")
                {
                    Owner = this,
                    Width = ActualWidth,
                    Height = ActualHeight
                };
                msgBox.ShowDialog();
            }
        }

        private void QuesDelBtn_Click(object sender, RoutedEventArgs e)
        {
            sql.LoadTable($"select qid from Questions where qid = '{QuesID.Text.Trim()}'", ref dt);
            if(dt.Rows.Count > 0)
            {
                string query = $"delete from Questions where qid = {QuesID.Text.Trim()}";
                sql.ExecuteQuery(query);
                MsgBoxLogin msgBox = new MsgBoxLogin("suc", "Question deleted successfully", "Cool")
                {
                    Owner = this,
                    Width = ActualWidth,
                    Height = ActualHeight
                };
                msgBox.ShowDialog();
                QuesID.Text = "";
            }
            else
            {
                MsgBoxLogin msgBox = new MsgBoxLogin("error", "Invalid QID", "Okay")
                {
                    Owner = this,
                    Width = ActualWidth,
                    Height = ActualHeight
                };
                msgBox.ShowDialog();
            }
        }

        private void QpSem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QpBranch.SelectedIndex != -1 && QpSem.SelectedIndex != -1)
                UpdateSubjectList();
        }

        private void QpBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QpSem.SelectedIndex != -1 && QpBranch.SelectedIndex != -1)
                UpdateSubjectList();
        }

        private void UpdateSubjectList()
        {
            string branch = ((ComboBoxItem)QpBranch.SelectedItem).Content.ToString();
            string sem = ((ComboBoxItem)QpSem.SelectedItem).Content.ToString();
            sql.LoadTable($"select ccode from {branch.ToLower()}{sem}", ref dt);
            QpSubject.ItemsSource = dt.DefaultView;
            QpSubject.DisplayMemberPath = "ccode";
        }
    }
}
