using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Windows
{
    internal class WindowRef
    {
        public static MainMenu MainMenuRef { get; set; }
        public static BugsWindow BugsWindowRef { get; set; }
        public static UserAdminWindow UserAdminWindowRef { get; set; }
}
}