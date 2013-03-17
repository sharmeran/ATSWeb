using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;

namespace ATSCommon.Entites.Devices
{
   public class Device :BaseClass
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

        public Server Server
        {
            get { return server; }
            set { server = value; }
        }

        public DeviceCategory Category
        {
            get { return category; }
            set { category = value; }
        }
        
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public int ReaderID
        {
            get { return readerID; }
            set { readerID = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public string IP
        {
            get { return iP; }
            set { iP = value; }
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
