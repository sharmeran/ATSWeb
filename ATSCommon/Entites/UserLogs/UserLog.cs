using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Entites.Devices;
using ATSCommon.Enums;

namespace ATSCommon.Entites.UserLogs
{
    [DataContract]
    public class UserLog : BaseClass
    {
        Int64 iD;
        Int64 userID;
        UserLogTypeEnum loggingType;
        DateTime logingDateTime;
        Device deviceID;
       
        [DataMember]
        public Device DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }


        [DataMember]
        public DateTime LogingDateTime
        {
            get { return logingDateTime; }
            set { logingDateTime = value; }
        }
        [DataMember]
        public UserLogTypeEnum LoggingType
        {
            get { return loggingType; }
            set { loggingType = value; }
        }
        [DataMember]
        public Int64 UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        [DataMember]
        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
