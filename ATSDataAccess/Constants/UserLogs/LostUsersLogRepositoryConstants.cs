using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.UserLogs;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.UserLogs
{
    public static class LostUsersLogRepositoryConstants
    {
        public const string Delete = "LostUsersDelete";
        public const string Insert = "LostUsersInsert";
        public const string FindAll = "LostUsersFindAll";
        public const string FindByID = "LostUsersFindByID";
        public const string Update = "LostUsersUpdate";
        public const string IsExist = "LostUsersLogIsExist";

        public const string ID = CommonConstants.SP_Parameter_Post + LostUsersLogConstants.ID;
        public const string UserID = CommonConstants.SP_Parameter_Post + LostUsersLogConstants.UserID;
        public const string LogingType = CommonConstants.SP_Parameter_Post + LostUsersLogConstants.LoggingType;
        public const string LogingDateTime = CommonConstants.SP_Parameter_Post + LostUsersLogConstants.LogingDate;
        public const string Device = CommonConstants.SP_Parameter_Post + LostUsersLogConstants.Device;
        public const string StartDate = CommonConstants.SP_Parameter_Post + "LogingStartDate";
        public const string EndDate = CommonConstants.SP_Parameter_Post + "LogingEndate";


    }
}
