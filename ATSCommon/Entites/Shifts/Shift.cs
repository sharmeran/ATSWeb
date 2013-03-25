using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Shifts
{
   public class Shift:BaseClass
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

        public bool IsOffDay
        {
            get { return isOffDay; }
            set { isOffDay = value; }
        }

        public int MinAllowWorkTime
        {
            get { return minAllowWorkTime; }
            set { minAllowWorkTime = value; }
        }

        public int MaxAllowWorkTime
        {
            get { return maxAllowWorkTime; }
            set { maxAllowWorkTime = value; }
        }

        public bool IsNormalShift
        {
            get { return isNormalShift; }
            set { isNormalShift = value; }
        }

        public bool IsFormalVacations
        {
            get { return isFormalVacations; }
            set { isFormalVacations = value; }
        }

        public int DayID
        {
            get { return dayID; }
            set { dayID = value; }
        }

        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }

        public int OutAllowTime
        {
            get { return outAllowTime; }
            set { outAllowTime = value; }
        }

        public int InAllowTime
        {
            get { return inAllowTime; }
            set { inAllowTime = value; }
        }

        public DateTime OutTime
        {
            get { return outTime; }
            set { outTime = value; }
        }

        public DateTime InTime
        {
            get { return inTime; }
            set { inTime = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
