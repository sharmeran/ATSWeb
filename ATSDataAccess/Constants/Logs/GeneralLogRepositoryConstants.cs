using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Logs;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Logs
{
    public static class GeneralLogRepositoryConstants
    {
        public const string Delete = "GeneralErrorsLogDelete";
        public const string FindAll = "GeneralErrorsLogFindAll";
        public const string FindByID = "GeneralErrorsLogFindByID";
        public const string Insert = "GeneralErrorsLogInsert";
        public const string Update = "GeneralErrorsLogUpdate";

        public const string ID = CommonConstants.SP_Parameter_Post + GeneralLogConstants.ID;
        public const string ErrorName = CommonConstants.SP_Parameter_Post + GeneralLogConstants.ErrorName;
        public const string ErrorDescription = CommonConstants.SP_Parameter_Post + GeneralLogConstants.ErrorDescription;
        public const string ErrorType = CommonConstants.SP_Parameter_Post + GeneralLogConstants.ErrorType;
        public const string ErrorDate = CommonConstants.SP_Parameter_Post + GeneralLogConstants.ErrorDate;
        public const string ErrorTarget = CommonConstants.SP_Parameter_Post + GeneralLogConstants.ErrorTarget;

    }
}
