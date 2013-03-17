using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
    public static class NationalityRepositoryConstants
    {
        public const string ID = CommonConstants.Post_SP + NationalityConstants.ID;
        public const string Name = CommonConstants.Post_SP + NationalityConstants.Name;

        public const string FindAll = "NATIONALITY_FIND_ALL";
        public const string FindByID = "NATIONALITY_FIND_BY_ID";
    }
}
