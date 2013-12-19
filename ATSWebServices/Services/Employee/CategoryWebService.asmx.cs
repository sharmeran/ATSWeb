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
    /// Summary description for CategoryWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CategoryWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Category> FindByID(int entityID)
        {
            EntityResult<Category> entityResult = new EntityResult<Category>();
            CategoryDomain categoryDomain = new CategoryDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            
            try
            {
                entityResult.ReturnedEntity = categoryDomain.FindByID(entityID);
                if (categoryDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = categoryDomain.ActionState.Result;
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
        public EntityListResult<Category> FindAll()
        {
            CategoryDomain categoryDomain = new CategoryDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Category> entityListResult = new EntityListResult<Category>();
            try
            {
                entityListResult.ReturnedEntities = categoryDomain.FindAll();
                entityListResult.Message = string.Empty;
                if (categoryDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = categoryDomain.ActionState.Result;
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
