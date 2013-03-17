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
    /// Summary description for TitleWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TitleWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Title> FindByID(int entityID)
        {
            EntityResult<Title> entityResult = new EntityResult<Title>();
            TitleDomain titleDomain = new TitleDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = titleDomain.FindByID(entityID);
                if (titleDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = titleDomain.ActionState.Result;
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
        public EntityListResult<Title> FindAll()
        {
            TitleDomain titleDomain = new TitleDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Title> entityListResult = new EntityListResult<Title>();
            try
            {
                entityListResult.ReturnedEntities = titleDomain.FindAll();
                if (titleDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = titleDomain.ActionState.Result;
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
