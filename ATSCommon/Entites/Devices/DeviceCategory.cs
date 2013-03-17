﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Devices
{
   public class DeviceCategory :BaseClass
    {
        int iD;
        string name;
        string description;
        List<Device> deviceList;

        public List<Device> DeviceList
        {
            get { return deviceList; }
            set { deviceList = value; }
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
