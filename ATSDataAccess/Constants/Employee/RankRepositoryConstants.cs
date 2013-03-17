using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
    public static class RankRepositoryConstants
    {
        public const string ID = CommonConstants.Post_SP + RankConstants.ID;
        public const string Name = CommonConstants.Post_SP + RankConstants.Name;

        public const string FindByID = "RANK_FIND_BY_ID";
        public const string FindAll = "RANK_FIND_ALL";

    }
}
