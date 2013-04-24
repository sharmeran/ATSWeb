using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSDataAccess.Constants.HandPunch
{
  public static  class LogDetailRepositoryConstants
    {
      public const string Insert = "LOG_DETAILS_INSERT";
      public const string Update = "LOG_DETAILS_UPDATE";
      public const string FindByMainDetailID = "LOG_DETAILS_FIND_BY_LOG_MAIN";           
      public const string FindByUserIDAndDate = "LOG_DETAILS_FIND_BY_USER_DATE";
      public const string FindByUserID = "LOG_DETAILS_FIND_BY_USER_ID";
      public const string FindByDate = "LOG_DETAILS_FIND_BY_DATE";
    }
}
