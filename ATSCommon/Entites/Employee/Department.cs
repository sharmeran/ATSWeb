using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Employee
{
    public class Department : BaseClass
    {
        
        string name;
        string shortName;
        decimal code;
        decimal shortCode;

        public decimal ShortCode
        {
            get { return shortCode; }
            set { shortCode = value; }
        }

        public decimal Code
        {
            get { return code; }
            set { code = value; }
        }

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

     
    }
}
