using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Entites.HandPunch;
using ATSCommon.Entites.Shifts;
using ATSCommon.Entites.UserLogs;
using ATSDataAccess.SQLImlementation.HandPunch;
using ATSDataAccess.SQLImlementation.Shifts;
using ATSDataAccess.SQLImlementation.UserLogs;
using ATSDomain.Domains.Logs;

namespace ATSWebTest
{
    class Program
    {
        static void Main(string[] args)
        {
           
            UserLogAddDomain userLogAddDomain = new UserLogAddDomain();
            Shift shift= new Shift();
            LogMain logMain= new LogMain();
            DateTime dateTime = new DateTime(2013, 4, 22, 23, 00, 00);
            userLogAddDomain.FindUserShift(990298, 1, 1, dateTime, shift, logMain);

        }
    }
}
