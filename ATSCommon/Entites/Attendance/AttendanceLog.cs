using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Entites.HandPunch;


namespace ATSCommon.Entites.Attendance
{
    [DataContract]
    public class AttendanceLog : BaseClass
    {
        int userID;
        List<LogMain> logMainList;
        [DataMember]
        public List<LogMain> LogMainList
        {
            get { return logMainList; }
            set { logMainList = value; }
        }
        [DataMember]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
    }
}
