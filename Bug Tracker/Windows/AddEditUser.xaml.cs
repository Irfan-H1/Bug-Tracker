using Bug_Tracker.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        public static UsersClass EditUser = new UsersClass();
        private string AddOrEdit = null;

        public AddEditUser(string AddEdit)
        {
            InitializeComponent();

            AddOrEdit = AddEdit;

            cmbUserTitle.ItemsSource = new List<string> { "Mr", "Mrs", "Ms", "Dr" };

            if (AddOrEdit == "Edit")
            {
                this.DataContext = EditUser;

                lblTitle.Content = "Edit User";
                txtUserID.Text = EditUser.UserID.ToString();
                cmbUserTitle.Text = EditUser.Title;
                txtFirstName.Text = EditUser.FirstName;
                txtLastName.Text = EditUser.LastName;
                dtDOB.Text = EditUser.DOB;
            }
            else
            {
                lblTitle.Content = "Add New User";
                EditUser = null;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string url;

            DateTime DOBdate = (DateTime)dtDOB.SelectedDate;
            string DOBFormatted = DOBdate.ToString("yyyy-MM-dd");

            if (AddOrEdit == "Edit")
            {
                url = "http://localhost/api/Bugs/AmendUser?userID=" + EditUser.UserID + "&title=" + cmbUserTitle.Text + "&firstName=" + txtFirstName.Text + "&lastName=" + txtLastName.Text + "&dob=" + DOBFormatted;

                try
                {
                    var client = new RestClient(url);
                    var request = new RestRequest("", Method.Post);
                    RestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        lblMessage.Content = "Record ID: " + EditUser.UserID + " Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Content = "Record Save Unsuccessful";
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                url = "http://localhost/api/Bugs/AddUser?title=" + cmbUserTitle.Text + "&firstName=" + txtFirstName.Text + "&lastName=" + txtLastName.Text + "&dob=" + DOBFormatted;

                try
                {
                    var client = new RestClient(url);
                    var request = new RestRequest("", Method.Post);
                    RestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        lblMessage.Content = "Record ID: " + response.Content + " Saved Successfully";
                    }
                    else
                    {
                        lblMessage.Content = "Record Save Unsuccessful";
                    }

                    ClearFields();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void ClearFields()
        {
            txtFirstName.Text = null;
            txtLastName.Text = null;
            cmbUserTitle.Text = null;
            txtUserID.Text = null;
            dtDOB.Text = null;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
    