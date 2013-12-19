using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Employee
{
    [DataContract]
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
        


       [DataMember]
        public Specialist Specialist
        {
            get { return specialist; }
            set { specialist = value; }
        }
        [DataMember]
        public Company Company
        {
            get { return company; }
            set { company = value; }
        }

       [DataMember]
        public Qualifications Qualifications
        {
            get { return qualifications; }
            set { qualifications = value; }
        }
        [DataMember]
        public Title Title
        {
            get { return title; }
            set { title = value; }
        }
        [DataMember]
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        [DataMember]
        public Nationality Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        [DataMember]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        [DataMember]
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }
        [DataMember]
        public Rank Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public Department Department
        {
            get { return department; }
            set { department = value; }
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
