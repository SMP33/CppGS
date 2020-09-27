using CppGS.Source;
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
using Microsoft.VisualStudio.Shell;

namespace CppGS
{
    /// <summary>
    /// Interaction logic for GSWindowControl.xaml
    /// </summary>
    public partial class GSWindowControl : UserControl
    {
        public Action CloseWindow { get; set; }
        public GSWindowControl(Action closeWindow)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            InitializeComponent();
            Menu.DataContext = new GSWindowViewModel(GSWindowCommand.Dte);
            CloseWindow = closeWindow;
        }

        public static void OnClosed(object sender, EventArgs e)
        {
            GSWindowViewModel.DebugClear();
        }
    }
}
