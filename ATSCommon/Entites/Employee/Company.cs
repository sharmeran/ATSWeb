using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Employee
{
    public class Company : BaseClass
    {
        int iD;
        string name;
        string description;
        string address;
        string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
