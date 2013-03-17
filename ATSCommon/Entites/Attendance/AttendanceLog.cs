using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Entites.Devices;

namespace ATSCommon.Entites.Attendance
{
   public class AttendanceLog : BaseClass
    {
        
        int operationType;
        int userID;        
        DateTime operationDate;
        bool isWrong;
        int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public bool IsWrong
        {
            get { return isWrong; }
            set { isWrong = value; }
        }
        

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public int OperationType
        {
            get { return operationType; }
            set { operationType = value; }
        }

        public DateTime OperationDate
        {
            get { return operationDate; }
            set { operationDate = value; }
        }

    }
}
