using Bug_Tracker.Classes;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Xml.Linq;
using System.Windows.Markup;
using System.Text;
using RestSharp;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Bug_Tracker.Windows
{
    /// <summary>
    /// Interaction logic for AddEditBug.xaml
    /// </summary>
    public partial class AddEditBug : Window
    {        
        public static BugsClass EditBug = new BugsClass();
        private string AddOrEdit = null;
        private List<string> UserList = new List<string>();

        public AddEditBug(string AddEdit)
        {
            InitializeComponent();
            AddOrEdit = AddEdit;

            LoadUsers();
            cmbBugAssigned.ItemsSource = UserList;

            if (AddOrEdit == "Edit")
            {
                this.DataContext = EditBug;

                lblTitle.Content = "Edit Bug";
                txtBugID.Text = EditBug.BugID.ToString();
                txtBugDescription.Text = EditBug.BugDescription;
                txtBugTitle.Text = EditBug.BugTitle;
                txtBugOpened.Text = EditBug.BugOpened;
                cmbBugAssigned.Text = EditBug.BugAssigned;
                btnClose.IsEnabled = true;
            }
            else
            {
                lblTitle.Content = "Add New Bug";
                EditBug = null;
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string url;

            if (AddOrEdit == "Edit")
            {
                url = "http://localhost/api/Bugs/EditBug?BugID=" + EditBug.BugID + "&BugTitle=" + txtBugTitle.Text + "&BugAssigned=" + cmbBugAssigned.Text + "&BugDescription=" + txtBugDescription.Text;

                try
                {
                    var client = new RestClient(url);
                    var request = new RestRequest("", Method.Post);
                    RestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        lblMessage.Content = "Record ID: " + EditBug.BugID + " Updated Successfully";
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
                url = "http://localhost/api/Bugs/AddBug?BugTitle=" + txtBugTitle.Text + "&BugAssigned=" + cmbBugAssigned.Text + "&BugDescription=" + txtBugDescription.Text;

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
                    //Console.WriteLine(response.Content);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            BugCloseConfirmation bugCloseConfirmation = new BugCloseConfirmation();

            this.Opacity = 0.2;

            bugCloseConfirmation.ShowDialog();

            if (bugCloseConfirmation.DialogResult == true)
            {
                string url;

                url = "http://localhost/api/Bugs/CloseBug?BugID=" + EditBug.BugID;

                try
                {
                    var client = new RestClient(url);
                    var request = new RestRequest("", Method.Put);
                    RestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        lblMessage.Content = "Record ID: " + EditBug.BugID + " has now been closed";
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
            else
            {

            }

            this.Opacity = 1;
        }

        private void ClearFields()
        {
            txtBugID.Text = null;
            txtBugTitle.Text = null;
            txtBugDescription.Text = null;
            txtBugOpened.Text = null;
            cmbBugAssigned.Text = null;
        }

        private void LoadUsers()
        {
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
                        //string DOBdate = User.DOB.ToString("dd-MM-yyyy");
                        UserList.Add(Convert.ToString(User.Title) + " " + Convert.ToString(User.FirstName) + " " + Convert.ToString(User.LastName));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
