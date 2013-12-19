using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ATSCommon.Entites.Employee;
using ATSDomain.Domains.Employee;
using ATSWebServices.Common;

namespace ATSWebServices.Services.Employee
{
    /// <summary>
    /// Summary description for DepartmentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DepartmentWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Department> FindByID(int entityID)
        {
            EntityResult<Department> entityResult = new EntityResult<Department>();
            DepartmentDomain departmentDomain = new DepartmentDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = departmentDomain.FindByID(entityID);
                if (departmentDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = departmentDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityResult.Message = ex.Message;
            }
            return entityResult;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityListResult<Department> FindAll()
        {
            DepartmentDomain departmentDomain = new DepartmentDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Department> entityListResult = new EntityListResult<Department>();
            try
            {
                entityListResult.ReturnedEntities = departmentDomain.FindAll();
                entityListResult.Message = string.Empty;
                if (departmentDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = departmentDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityListResult.Message = ex.Message;
            }
            return entityListResult;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityListResult<Department> FindStartWith(string deptCode)
        {
            DepartmentDomain departmentDomain = new DepartmentDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Department> entityListResult = new EntityListResult<Department>();
            try
            {
                entityListResult.ReturnedEntities = departmentDomain.FindStartWith(deptCode);
                if (departmentDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = departmentDomain.ActionState.Result;
                }

            }
            catch (Exception ex)
            {
                entityListResult.Message = ex.Message;
            }
            return entityListResult;
        }
    }
}
