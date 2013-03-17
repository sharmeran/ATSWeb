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
    /// Summary description for CompanyWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompanyWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Company> Add(Company company)
        {
            EntityResult<Company> entityResult = new EntityResult<Company>();
            try
            {
                CompanyDomain companyDomain = new CompanyDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
                companyDomain.Add(company);
                entityResult.ReturnedEntity = company;
                if (companyDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = companyDomain.ActionState.Result;
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
        public EntityResult<Company> Delete(Company company)
        {
            EntityResult<Company> entityResult = new EntityResult<Company>();
            try
            {
                CompanyDomain companyDomain = new CompanyDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
                companyDomain.Delete(company);                
                if (companyDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = companyDomain.ActionState.Result;
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
        public EntityResult<Company> Update(Company company)
        {
            EntityResult<Company> entityResult = new EntityResult<Company>();
            try
            {
                CompanyDomain companyDomain = new CompanyDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);                
                entityResult.ReturnedEntity = company;
                if (companyDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = companyDomain.ActionState.Result;
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
        public EntityResult<Company> FindByID(int entityID)
        {
            EntityResult<Company> entityResult = new EntityResult<Company>();
            try
            {
                CompanyDomain companyDomain = new CompanyDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
                entityResult.ReturnedEntity = companyDomain.FindByID(entityID);
                if (companyDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = companyDomain.ActionState.Result;
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
        public EntityListResult<Company> FindAll()
        {
            CompanyDomain companyDomain = new CompanyDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Company> entityListResult = new EntityListResult<Company>();
            try
            {
                entityListResult.ReturnedEntities = companyDomain.FindAll();
                if (companyDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = companyDomain.ActionState.Result;
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
