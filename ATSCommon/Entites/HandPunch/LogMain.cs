using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.HandPunch
{
    [DataContract]
    public class LogMain : BaseClass
    {
        int iD;
        int userID;
        DateTime attendanceDate;
        DateTime? inDate;
        DateTime? outDate;
        bool isClosed;
        int? missIN;
        int? missOut;
        int? shiftID;
        int? plusIN;
        int? plusOut;
        List<LogDetails> logDetailList;

        [DataMember]
        public List<LogDetails> LogDetailList
        {
            get { return logDetailList; }
            set { logDetailList = value; }
        }
        [DataMember]
        public int? PlusOut
        {
            get { return plusOut; }
            set { plusOut = value; }
        }
        [DataMember]
        public int? PlusIN
        {
            get { return plusIN; }
            set { plusIN = value; }
        }
        [DataMember]
        public int? ShiftID
        {
            get { return shiftID; }
            set { shiftID = value; }
        }
        [DataMember]
        public int? MissOut
        {
            get { return missOut; }
            set { missOut = value; }
        }
        [DataMember]
        public int? MissIN
        {
            get { return missIN; }
            set { missIN = value; }
        }
        [DataMember]
        public bool IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }
        [DataMember]
        public DateTime? OutDate
        {
            get { return outDate; }
            set { outDate = value; }
        }
        [DataMember]
        public DateTime? InDate
        {
            get { return inDate; }
            set { inDate = value; }
        }
        [DataMember]
        public DateTime AttendanceDate
        {
            get { return attendanceDate; }
            set { attendanceDate = value; }
        }
        [DataMember]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        [DataMember]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
