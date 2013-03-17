using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Employee
{
    public class Title : BaseClass
    {
        decimal iD;
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
