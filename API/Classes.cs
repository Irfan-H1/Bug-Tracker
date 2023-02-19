using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public class Bugs
    {
        //public static List<Bugs> bugsList = new List<Bugs>();

        public int ID { get; set; }
        public string BugTitle { get; set; }
        public string BugDescription { get; set; }
        public DateTime BugOpened { get; set; }
        public string BugAssigned { get; set; }
        public bool? BugClosed { get; set; }

        public Bugs(int iD, string bugTitle, string bugDescription, DateTime bugOpened, string bugAssigned, bool? bugClosed)
        {
            ID = iD;
            BugTitle = bugTitle;
            BugDescription = bugDescription;
            BugOpened = bugOpened;
            BugAssigned = bugAssigned;
            BugClosed = bugClosed;
        }

        public Bugs()
        {
        }
    }

    public class Users
    {
        public int UserID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }

        public Users(int userID, string title, string firstName, string lastName, DateTime dOB)
        {
            UserID = userID;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            DOB = dOB;
        }       

        public Users()
        {
        }
    }
}