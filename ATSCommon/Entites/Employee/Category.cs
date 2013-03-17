using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Employee
{
    [DataContract]
   public class Category :BaseClass
    {
        float iD;
        string name;
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public float ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
