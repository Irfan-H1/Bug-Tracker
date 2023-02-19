using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Classes
{
    public class BugsClass
    {
        public int BugID { get; set; }
        public string BugTitle { get; set; }
        public string BugDescription { get; set; }
        public string BugOpened { get; set; }
        public string BugAssigned { get; set; }
        public bool? BugClosed { get; set; }

        public BugsClass(int bugID, string bugTitle, string bugDescription, string bugOpened, string bugAssigned, bool? bugClosed)
        {
            BugID = bugID;
            BugTitle = bugTitle;
            BugDescription = bugDescription;
            BugOpened = bugOpened;
            BugAssigned = bugAssigned;
            BugClosed = bugClosed;
        }

        public BugsClass(BugsClass BG)
        {
            BugID = BG.BugID;
            BugTitle = BG.BugTitle;
            BugDescription = BG.BugDescription;
            BugOpened = BG.BugOpened;
            BugAssigned = BG.BugAssigned;
            BugClosed = BG.BugClosed;

        }

        public BugsClass()
        {
        }
    }
}
