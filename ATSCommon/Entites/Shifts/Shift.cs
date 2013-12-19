using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Shifts
{
    [DataContract]
    public class Shift : BaseClass
    {
        int iD;
        string description;
        string name;
        DateTime inTime;
        DateTime outTime;
        int inAllowTime;
        int outAllowTime;
        string deptCode;
        int dayID;
        bool isFormalVacations;
        bool isNormalShift;
        int maxAllowWorkTime;
        int minAllowWorkTime;
        bool isOffDay;
        [DataMember]
        public bool IsOffDay
        {
            get { return isOffDay; }
            set { isOffDay = value; }
        }
        [DataMember]
        public int MinAllowWorkTime
        {
            get { return minAllowWorkTime; }
            set { minAllowWorkTime = value; }
        }
        [DataMember]
        public int MaxAllowWorkTime
        {
            get { return maxAllowWorkTime; }
            set { maxAllowWorkTime = value; }
        }
        [DataMember]
        public bool IsNormalShift
        {
            get { return isNormalShift; }
            set { isNormalShift = value; }
        }
        [DataMember]
        public bool IsFormalVacations
        {
            get { return isFormalVacations; }
            set { isFormalVacations = value; }
        }
        [DataMember]
        public int DayID
        {
            get { return dayID; }
            set { dayID = value; }
        }
        [DataMember]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        [DataMember]
        public int OutAllowTime
        {
            get { return outAllowTime; }
            set { outAllowTime = value; }
        }
        [DataMember]
        public int InAllowTime
        {
            get { return inAllowTime; }
            set { inAllowTime = value; }
        }
        [DataMember]
        public DateTime OutTime
        {
            get { return outTime; }
            set { outTime = value; }
        }
        [DataMember]
        public DateTime InTime
        {
            get { return inTime; }
            set { inTime = value; }
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        [DataMember]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
