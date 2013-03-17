using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
   public static class QualificationsRepositoryConstants
    {
       public const string ID = CommonConstants.Post_SP + QualificationsConstants.ID;
       public const string Name = CommonConstants.Post_SP + QualificationsConstants.Name;


       public const string FindByID = "QUALIFICATIONS_FIND_BY_ID";
       public const string FindAll = "QUALIFICATIONS_FIND_All";

    }
}
