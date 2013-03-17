using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Entites.Employee;

namespace ATSCommon.Entites.Attendance
{
    class AttendanceRecord : BaseClass
    {
        
        DateTime inDate;
        DateTime outDate;
        DateTime attendanceDate;
        string status;
        string systemStatus;
        string notes;
        int iD;
        List<AttendanceLog> attendanceLog;
        bool isClosed;
        Employees employee;

        public Employees Employee
        {
            get { return employee; }
            set { employee = value; }
        }


        public bool IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }

        public List<AttendanceLog> AttendanceLog
        {
            get { return attendanceLog; }
            set { attendanceLog = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }


        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string SystemStatus
        {
            get { return systemStatus; }
            set { systemStatus = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime AttendanceDate
        {
            get { return attendanceDate; }
            set { attendanceDate = value; }
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
    }
}
