using Bug_Tracker.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Bug_Tracker.Windows
{
    /// <summary>
    /// Interaction logic for Bugs.xaml
    /// </summary>
    public partial class BugsWindow : Window
    {
        ObservableCollection<BugsClass> BugsList = new ObservableCollection<BugsClass>();
        //public static BugsClass EditBug1 = new BugsClass();

        public BugsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBugsGrid();
        }

        private void LoadBugsGrid()
        { 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/api/Bugs/GetBugs");
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                BugsList.Clear();
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    var jsonData = JsonConvert.DeserializeObject<dynamic>(responseReader.ReadToEnd());

                    foreach (var Bug in jsonData)
                    {
                        BugsList.Add(new BugsClass(Convert.ToInt16(Bug.ID), Convert.ToString(Bug.BugTitle), Convert.ToString(Bug.BugDescription), Bug.BugOpened.ToString("dd/MM/yyyy HH:mm:ss"), Convert.ToString(Bug.BugAssigned), Bug.BugClosed != null ? Convert.ToBoolean(Bug.BugClosed) : null));
                    }
                }
            }
            catch (Exception ex)
            {

            }

            cmbFilter.SelectedIndex = 0;
        }

        private void grdBugsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdBugsList.SelectedIndex > -1 && cmbFilter.SelectedIndex == 0)
            {
                btnEdit.IsEnabled= true;
            }
            else
            {
                btnEdit.IsEnabled = false;
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            WindowRef.MainMenuRef.Show();

            this.Hide();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditBug addEditBug = new AddEditBug("Add");

            addEditBug.ShowDialog();
                        

            //if (addEditBug.DialogResult == true)
            //{

            //}
            //else
            //{

            //}

            LoadBugsGrid();
            FilterOpenCalls();
            grdBugsList.SelectedIndex = -1;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {               
            AddEditBug.EditBug = new BugsClass((BugsClass)grdBugsList.SelectedItems[0]);

            AddEditBug addEditBug = new AddEditBug("Edit");

            addEditBug.ShowDialog();

            //if (addEditBug.DialogResult == true)
            //{

            //}
            //else
            //{

            //}

            LoadBugsGrid();
            FilterOpenCalls();
            grdBugsList.SelectedIndex = -1;
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbFilter.SelectedIndex)
            {
                case 0:
                    FilterOpenCalls();
                    lblTitle.Content = "Open Calls";

                    break;
                case 1:
                    FilterClosedCalls();
                    lblTitle.Content = "Closed Calls";

                    break;
            }
        }

        private void FilterOpenCalls()
        {
            grdBugsList.ItemsSource = BugsList.Where(Bg => Bg.BugClosed != true);

            if (grdBugsList.Items.Count > 0)
            {
                grdBugsList.Columns[0].Header = "Bug ID";
                grdBugsList.Columns[1].Header = "Bug Title";
                grdBugsList.Columns[2].Header = "Bug Description";
                grdBugsList.Columns[3].Header = "Bug Opened";
                grdBugsList.Columns[4].Header = "Bug Assigned To";

                grdBugsList.Columns[5].Visibility = Visibility.Hidden;
            }
        }

        private void FilterClosedCalls()
        {
            grdBugsList.ItemsSource = BugsList.Where(Bg => Bg.BugClosed == true);

            if (grdBugsList.Items.Count > 0)
            {
                grdBugsList.Columns[0].Header = "Bug ID";
                grdBugsList.Columns[1].Header = "Bug Title";
                grdBugsList.Columns[2].Header = "Bug Description";
                grdBugsList.Columns[3].Header = "Bug Opened";
                grdBugsList.Columns[4].Header = "Bug Assigned To";

                grdBugsList.Columns[5].Visibility = Visibility.Hidden;
            }            
        }
    }
}
