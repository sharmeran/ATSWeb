using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Devices
{
    [DataContract]
    public class DeviceCategory : BaseClass
    {
        int iD;
        string name;
        string description;
        List<Device> deviceList;
        [DataMember]
        public List<Device> DeviceList
        {
            get { return deviceList; }
            set { deviceList = value; }
        }
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
