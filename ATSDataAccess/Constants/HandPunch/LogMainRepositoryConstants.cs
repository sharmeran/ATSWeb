using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSDataAccess.Constants.HandPunch
{
    public static class LogMainRepositoryConstants
    {
        public const string Insert = "LOG_MAIN_INSERT";
        public const string Update = "LOG_MAIN_UPDATE";
        public const string FindByDateAndDeptCode = "LOG_MAIN_FIND_BY_DEPT_AND_DATE";
        public const string FindByUserIDAndDate = "LOG_MAIN_FIND_BY_DATE_USER_ID";
        public const string FindByUserIDAndDateAndNotClosed = "LOGMAIN_FIND_DATE_USER_CLOSE";
        public const string FindByID = "LOG_MAIN_FIND_BY_ID";

    }
}
