using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Attendance
{
   public class UserDetails :BaseClass
    {
        int userID;


        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
    }
}
