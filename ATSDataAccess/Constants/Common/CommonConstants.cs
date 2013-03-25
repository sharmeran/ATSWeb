using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSDataAccess.Constants.Common
{
   public static class CommonConstants
   {
       #region DataAccessErrors
       public const string Post_SP = "P_";
       public const string IdentityID = "P_IN_ID";
       public const string Err_CannotDelete = "لا يمكن الحذف";
       public const string Err_CannotUpdate = "لا يمكن التعديل";
       public const string Err_CannotInsert = "لا يمكن الحفظ";
       public const string Err_CannotFound = "لايمكن الايجاد";
       #endregion

       #region DataAccessParameterUsage
       public const string SP_Parameter_Post = "@";
       public const string PramOutIsExist = "P_OUT_STATUS";
       public const string ParamOutCarsor = "P_REF_INBOX";
       public const string ConnectionString = "Connection String";
       public const string OracleConnectionstring = "OracleConnectionstring";
       public const string Param_Founded = "@Founded";
       #endregion

       #region ErrorMessages
       public const string Err_CnnotInsertUserLog = "اضافة سجل للبصمة";
       public const string Err_CnnotGetUserShift = "لايمكن ايجاد معلومات الدوام";
       public const string Err_CannotFindUserDefaultShift = "لا يمكن ايجاد معلومات الدوام الافتراضية";
       public const string Err_ThereIsNoSHift = "لا يوجد معلومات دوام منسوبة للمستخدم";
       public const string Err_ShiftsMustEqualToSeven = "يجب اسناد دوريات ايام الاسبوع اجمع او ان نوع الدوام ليس بدوام منتظم";
       public const string Err_NoShiftInThisDate = "لاتوجد دورية بمثل هذا اليوم";
       #endregion

       #region DomainParameterUsage
       public const string XmlLogDirectoryLocation = "XmlLogDirectoryLocation";
       public const string XmlErrorLogDirectoryLocation = "XmlErrorLogDirectoryLocation";
       public const string XmlLostLogDirectoryLocation = "XmlLostLogDirectoryLocation";
       #endregion
   }
}
