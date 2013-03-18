using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.UserLogs;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.UserLogs
{
    public static class CheatUsersRepositoryConstants
    {
        public const string Delete = "CheatUsersDelete";
        public const string FindAll = "CheatUsersFindAll";
        public const string FindByID = "CheatUsersFindByID";
        public const string Insert = "CheatUsersInsert";
        public const string Update = "CheatUsersUpdate";


        public const string ID = CommonConstants.SP_Parameter_Post + CheatUsersConstants.ID;
        public const string UserID = CommonConstants.SP_Parameter_Post + CheatUsersConstants.UserID;
        public const string LogingType = CommonConstants.SP_Parameter_Post + CheatUsersConstants.LoggingType;
        public const string LogingDateTime = CommonConstants.SP_Parameter_Post + CheatUsersConstants.LogingDate;
        public const string DeviceID = CommonConstants.SP_Parameter_Post + CheatUsersConstants.DeviceID;
        public const string CatchDate = CommonConstants.SP_Parameter_Post + CheatUsersConstants.CatchDate;

    }
}
