using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Employee
{
    [DataContract]
    public class Department : BaseClass
    {
        
        string name;
        string shortName;
        decimal code;
        decimal shortCode;
        [DataMember]
        public decimal ShortCode
        {
            get { return shortCode; }
            set { shortCode = value; }
        }
        [DataMember]
        public decimal Code
        {
            get { return code; }
            set { code = value; }
        }
        [DataMember]
        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

     
    }
}
