using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSDataAccess.Constants.Common
{
   public static class CommonConstants
    {
       public const string Post_SP = "P_";
       public const string IdentityID = "P_IN_ID";
       public const string Err_CannotDelete = "لا يمكن الحذف";
       public const string Err_CannotUpdate = "لا يمكن التعديل";
       public const string Err_CannotInsert = "لا يمكن الحفظ";
       public const string Err_CannotFound = "لايمكن الايجاد";

       public const string SP_Parameter_Post = "@";
       public const string PramOutIsExist = "P_OUT_STATUS";
       public const string ParamOutCarsor = "P_REF_INBOX";
       public const string ConnectionString = "Connection String";
       public const string OracleConnectionstring = "OracleConnectionstring";
       public const string Param_Founded = "@Founded";
    }
}
