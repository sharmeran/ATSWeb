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
    public class Device : BaseClass
    {
        int iD;
        string name;
        string description;
        string iP;
        int status;
        int readerID;
        int port;
        DeviceCategory category;
        Server server;
        int internalDBDeviceID;
        [DataMember]
        public int InternalDBDeviceID
        {
            get { return internalDBDeviceID; }
            set { internalDBDeviceID = value; }
        }
        [DataMember]
        public Server Server
        {
            get { return server; }
            set { server = value; }
        }
        [DataMember]
        public DeviceCategory Category
        {
            get { return category; }
            set { category = value; }
        }
        [DataMember]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        [DataMember]
        public int ReaderID
        {
            get { return readerID; }
            set { readerID = value; }
        }
        [DataMember]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        [DataMember]
        public string IP
        {
            get { return iP; }
            set { iP = value; }
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
