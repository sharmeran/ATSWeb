using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Entites.Devices;
using ATSCommon.Enums;

namespace ATSCommon.Entites.UserLogs
{
    public class CheatUsers : BaseClass
    {
        Int64 iD;
        Int64 userID;
        UserLogTypeEnum loggingType;
        DateTime loggingDate;
        Device device;
        DateTime catchDate;

        public DateTime CatchDate
        {
            get { return catchDate; }
            set { catchDate = value; }
        }

        public Device Device
        {
            get { return device; }
            set { device = value; }
        }

        public DateTime LoggingDate
        {
            get { return loggingDate; }
            set { loggingDate = value; }
        }

        public UserLogTypeEnum LoggingType
        {
            get { return loggingType; }
            set { loggingType = value; }
        }

        public Int64 UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
