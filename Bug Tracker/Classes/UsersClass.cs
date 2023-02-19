using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Classes
{
    public class UsersClass
    {
        public int UserID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        //public DateTime DOB { get; set; }

    public UsersClass(int userID, string title, string firstName, string lastName, string dOB)  //DateTime dOB
        {
            UserID = userID;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            DOB = dOB;
        }
             
        public UsersClass(UsersClass UL)
        {
            UserID = UL.UserID;
            Title = UL.Title;
            FirstName = UL.FirstName;
            LastName = UL.LastName;
            DOB = UL.DOB;
        }

        public UsersClass()
        {
        }
    }
}
