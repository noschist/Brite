using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for PageCommon.xaml
    /// </summary>
    public partial class PageCommon : Page
    {
        public PageCommon()
        {
            InitializeComponent();
        }

        private void TchrT_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)TchrT.IsChecked)
            {
                isTeacher = true;
                isStudent = false;
                TchrT.Foreground = Brushes.White;
                StdntT.IsChecked = false;
                StdntT.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2196F3");
            }
            else
            {
                TchrT.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2196F3");
                isTeacher = false;
                isStudent = false;
            }
        }

        public bool isTeacher = false;
        public bool isStudent = false;

        private void StdntT_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)StdntT.IsChecked)
            {
                isTeacher = false;
                isStudent = true;
                StdntT.Foreground = Brushes.White;
                TchrT.IsChecked = false;
                TchrT.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2196F3");
            }
            else
            {
                StdntT.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2196F3");
                isTeacher = false;
                isStudent = false;
            }
        }
    }
}
