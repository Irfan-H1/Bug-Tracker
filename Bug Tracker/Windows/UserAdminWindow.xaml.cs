using Bug_Tracker.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Bug_Tracker.Windows
{
    /// <summary>
    /// Interaction logic for UserAdminWindow.xaml
    /// </summary>
    public partial class UserAdminWindow : Window
    {
        ObservableCollection<UsersClass> UsersList = new ObservableCollection<UsersClass>();

        public UserAdminWindow()
        {
            InitializeComponent();
        }

        private void grdUsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdUsersList.SelectedIndex > -1)
            {
                btnEdit.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsersGrid();
        }

        private void LoadUsersGrid()
        {
            UsersList.Clear();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/api/Bugs/GetUsers");
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    var jsonData = JsonConvert.DeserializeObject<dynamic>(responseReader.ReadToEnd());
                    
                    foreach (var User in jsonData)
                    {
                        string DOBdate = User.DOB.ToString("dd-MM-yyyy");
                        UsersList.Add(new UsersClass(Convert.ToInt16(User.UserID), Convert.ToString(User.Title), Convert.ToString(User.FirstName), Convert.ToString(User.LastName), DOBdate));
                    }
                }
            }
            catch (Exception ex)
            {

            }

            ConfigureGrid();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            WindowRef.MainMenuRef.Show();

            this.Hide();
        }

        private void ConfigureGrid()
        {
            grdUsersList.ItemsSource = UsersList.Where(Ul => Ul.UserID != null);
           
            if (grdUsersList.Items.Count > 0)
            {
                grdUsersList.Columns[0].Header = "User ID";
                grdUsersList.Columns[1].Header = "Title";
                grdUsersList.Columns[2].Header = "First Name";
                grdUsersList.Columns[3].Header = "Last Name";
                grdUsersList.Columns[4].Header = "DOB";

                //grdUsersList.Columns[5].Visibility = Visibility.Hidden;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser("Add");

            addEditUser.ShowDialog();

            //if (addEditUser.DialogResult == true)
            //{

            //}
            //else
            //{

            //}

            LoadUsersGrid();
            grdUsersList.SelectedIndex = -1;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditUser.EditUser = new UsersClass((UsersClass)grdUsersList.SelectedItems[0]);

            AddEditUser addEditUser = new AddEditUser("Edit");

            addEditUser.ShowDialog();

            //if (addEditUser.DialogResult == true)
            //{

            //}
            //else
            //{

            //}

            LoadUsersGrid();
            grdUsersList.SelectedIndex = -1;
        }
    }
}
