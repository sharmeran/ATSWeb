using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Logs;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Logs
{
    public static class ServicesLogRepositoryConstants
    {
        public const string Insert = "ServicesLogInsert";
        public const string Update = "ServicesLogUpdate";
        public const string Delete = "ServicesLogDelete";
        public const string FindAll = "ServicesLogFindByID";
        public const string FindByID = "ServicesLogFindAll";


        public const string ID = CommonConstants.SP_Parameter_Post + ServicesLogConstants.ID;
        public const string Action = CommonConstants.SP_Parameter_Post + ServicesLogConstants.Action;
        public const string Description = CommonConstants.SP_Parameter_Post + ServicesLogConstants.Description;
        public const string LoggingDate = CommonConstants.SP_Parameter_Post + ServicesLogConstants.LoggingDate;
        public const string Service = CommonConstants.SP_Parameter_Post + ServicesLogConstants.Service;
    }
}
