using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.HandPunch
{
   public class LogMain : BaseClass
    {
        int iD;
        int userID;
        DateTime attendanceDate;
        DateTime inDate;
        DateTime outDate;
        bool isClosed;
        int missIN;
        int missOut;
        int shiftID;
        int plusIN;
        int plusOut;

        public int PlusOut
        {
            get { return plusOut; }
            set { plusOut = value; }
        }

        public int PlusIN
        {
            get { return plusIN; }
            set { plusIN = value; }
        }

        public int ShiftID
        {
            get { return shiftID; }
            set { shiftID = value; }
        }

        public int MissOut
        {
            get { return missOut; }
            set { missOut = value; }
        }

        public int MissIN
        {
            get { return missIN; }
            set { missIN = value; }
        }

        public bool IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }

        public DateTime OutDate
        {
            get { return outDate; }
            set { outDate = value; }
        }

        public DateTime InDate
        {
            get { return inDate; }
            set { inDate = value; }
        }

        public DateTime AttendanceDate
        {
            get { return attendanceDate; }
            set { attendanceDate = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
