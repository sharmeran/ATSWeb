using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.HandPunch
{
   public class LogDetails :BaseClass
    {
        int iD;
        int logMainID;
        DateTime operationDate;
        int operationType;
        bool isWrong;
        int deviceID;

        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        public bool IsWrong
        {
            get { return isWrong; }
            set { isWrong = value; }
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

        public int LogMainID
        {
            get { return logMainID; }
            set { logMainID = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
