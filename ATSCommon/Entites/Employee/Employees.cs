using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Employee
{
   public class Employees : BaseClass
    {
        int iD;
        int userID;
        Department department;
        string name;
        Rank rank;
        Category category;
        DateTime startDate;
        Nationality nationality;
        DateTime endDate;
        Title title;
        Qualifications qualifications;        
        Company company;
        Specialist specialist;

        public Specialist Specialist
        {
            get { return specialist; }
            set { specialist = value; }
        }

        public Company Company
        {
            get { return company; }
            set { company = value; }
        }

       
        public Qualifications Qualifications
        {
            get { return qualifications; }
            set { qualifications = value; }
        }

        public Title Title
        {
            get { return title; }
            set { title = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public Nationality Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public Category Category
        {
            get { return category; }
            set { category = value; }
        }

        public Rank Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Department Department
        {
            get { return department; }
            set { department = value; }
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
