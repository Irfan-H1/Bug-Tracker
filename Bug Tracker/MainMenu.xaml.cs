using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.IO;
using System.Security.Policy;
using System.Windows.Markup;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Bug_Tracker.Windows;
using System.Collections.ObjectModel;

namespace Bug_Tracker
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private ObservableCollection<string> person = new ObservableCollection<string>();
        public MainMenu()
        {
            InitializeComponent();

            WindowRef.MainMenuRef = this;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnBugs_Click(object sender, RoutedEventArgs e)
        {
            BugsWindow bugsWindow = new BugsWindow();
            bugsWindow.Show();

            this.Hide();
            WindowRef.BugsWindowRef = bugsWindow;
        }

        private void btnUserAdmin_Click(object sender, RoutedEventArgs e)
        {
            UserAdminWindow userAdminWindow = new UserAdminWindow();
            userAdminWindow.Show();

            this.Hide();
            WindowRef.UserAdminWindowRef = userAdminWindow;
        }
    }
}
