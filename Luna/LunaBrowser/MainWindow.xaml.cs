using Luna.Core;
using MahApps.Metro.Controls;
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

namespace LunaBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Engine en;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {


            System.Windows.Forms.Application.EnableVisualStyles();
           // MainHost.Resources.Clear();// = false;

            System.Windows.Forms.Panel pn = new System.Windows.Forms.Panel();
            pn.Dock = System.Windows.Forms.DockStyle.Fill;

            MainHost.Child = pn;
            en = new Engine(pn);
            en.OpenApp("http://myvarhd.github.io/Luna/");
            en.InvokeMain();
        }

        private void MainHost_Loaded(object sender, RoutedEventArgs e)
        {
           // MainHost.
        }
    }
}
