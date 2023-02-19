using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class BugsController : ApiController
    {
        private BugTrackerEntities dbContext = new BugTrackerEntities();
        
        [HttpGet, Route("Bugs/GetBugs")]
        public List<Bugs> GetBugs()
        {
            List<Bugs> BugList = new List<Bugs>();

            try
            {
                foreach (var bug in dbContext.Bugs)
                {
                    BugList.Add(new Bugs(bug.BugID, bug.BugTitle, bug.BugDescription, bug.BugOpened, bug.BugAssigned, bug.BugClosed));
                }
            }
            catch
            {

            }

            return BugList;
        }

        [HttpGet, Route("Bugs/GetBugs/")]
        public Bugs GetBugs(int BugID)
        {
            Bugs Bug = new Bugs();

            var Bg = dbContext.Bugs.Where(B => B.BugID == BugID).FirstOrDefault();

            Bug.ID = Bg.BugID;
            Bug.BugTitle = Bg.BugTitle;
            Bug.BugDescription = Bg.BugDescription;
            Bug.BugOpened = Bg.BugOpened;            

            return Bug;
        }

        [HttpPost, Route("Bugs/AddBug/")]
        public int AddBug(string BugTitle, string BugAssigned, string BugDescription)
        {
            //var Bug = new Bugs();

            try
            {
                var NewBug = new Bug { BugTitle = BugTitle, BugAssigned = BugAssigned, BugDescription = BugDescription, BugOpened = DateTime.Now };
               
                using (var DataContext = dbContext)
                {
                    DataContext.Bugs.Add(NewBug);
                    
                    DataContext.SaveChanges();                    
                }
                
                return NewBug.BugID;
            }
            catch (Exception ex) 
            {
                return 0;
            }
        }

        [HttpPost, Route("Bugs/EditBug/")]
        public bool EditBug(int BugID, string BugTitle, string BugAssigned, string BugDescription)
        {
            try
            {
                using (var DataContext = dbContext)
                {
                    var EditBug = dbContext.Bugs.Where(B => B.BugID == BugID).First();
                    EditBug.BugTitle = BugTitle;
                    EditBug.BugAssigned = BugAssigned;
                    EditBug.BugDescription = BugDescription;

                    DataContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }           
        }

        [HttpPut, Route("Bugs/CloseBug/")]
        public bool CloseBug(int BugID)
        {
            try
            {
                using (var DataContext = dbContext)
                {
                    var EditBug = dbContext.Bugs.Where(B => B.BugID == BugID).First();
                    EditBug.BugClosed = true;

                    DataContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //[HttpPost, Route("Bugs/AssignBug/")]
        //public bool AssignBug(int BugID)
        //{
        //    Bugs Bug = new Bugs();

        //    var Bg = dbContext.Bugs.Where(B => B.BugID == BugID).FirstOrDefault();

        //    try
        //    {

        //        //Bg.BugID = Bg.BugID;
        //        //Bg.BugTitle = Bg.BugTitle;
        //        //Bg.BugDescription = Bg.BugDescription;
        //        //Bug.BugOpened = Bg.BugOpened;
        //        //Bug.BugClosed = Bg.BugClosed;                       

        //        //db.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        [HttpGet, Route("Bugs/GetUsers/")]
        public List<Users> GetUsers()
        {
            List<Users> UserList = new List<Users>();

            try
            {
                foreach (var user in dbContext.Users)
                {
                    UserList.Add(new Users(user.UserID, user.Title, user.FirstName, user.LastName, user.DOB));
                }                
            }
            catch (Exception ex)
            {

            }

            return UserList;
        }

        [HttpPost, Route("Bugs/AddUser/")]
        public int AddUser(string title, string firstName, string lastName, DateTime dob)
        {
            try
            {
                var NewUser = new User { Title = title, FirstName = firstName, LastName = lastName, DOB = dob };

                using (var DataContext = dbContext)
                {
                    DataContext.Users.Add(NewUser);

                    DataContext.SaveChanges();
                }

                return NewUser.UserID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }             

        [HttpPost, Route("Bugs/AmendUser/")]
        public bool AmendUser(int userID, string title, string firstName, string lastName, DateTime dob)
        {
            try
            {
                using (var DataContext = dbContext)
                {
                    var EditUser = dbContext.Users.Where(U => U.UserID == userID).First();
                    EditUser.Title = title;
                    EditUser.FirstName = firstName;
                    EditUser.LastName = lastName;
                    EditUser.DOB = dob;

                    DataContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
