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
    /// Summary description for EmployeesWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmployeesWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Employees> FindByID(int entityID)
        {
            EntityResult<Employees> entityResult = new EntityResult<Employees>();
            EmployeesDomain employeesDomain = new EmployeesDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = employeesDomain.FindByID(entityID);
                if (employeesDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = employeesDomain.ActionState.Result;
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
        public EntityListResult<Employees> FindAll()
        {
            EmployeesDomain employeesDomain = new EmployeesDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Employees> entityListResult = new EntityListResult<Employees>();
            try
            {
                entityListResult.ReturnedEntities = employeesDomain.FindAll();
                entityListResult.Message = string.Empty;
                if (employeesDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = employeesDomain.ActionState.Result;
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
        public EntityListResult<Employees> FindByManagerID(int managerID)
        {
            EmployeesDomain employeesDomain = new EmployeesDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Employees> entityListResult = new EntityListResult<Employees>();
            try
            {
                entityListResult.ReturnedEntities = employeesDomain.FindByManagerID(managerID);
                if (employeesDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = employeesDomain.ActionState.Result;
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
        public EntityListResult<Employees> FindByDeptCode(string deptCode)
        {
            EmployeesDomain employeesDomain = new EmployeesDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Employees> entityListResult = new EntityListResult<Employees>();
            try
            {
                entityListResult.ReturnedEntities = employeesDomain.FindByDeptCode(deptCode);
                if (employeesDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = employeesDomain.ActionState.Result;
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
