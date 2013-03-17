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
    /// Summary description for RankWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RankWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Rank> FindByID(int entityID)
        {
            EntityResult<Rank> entityResult = new EntityResult<Rank>();
            RankDomain rankDomain = new RankDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = rankDomain.FindByID(entityID);
                if (rankDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = rankDomain.ActionState.Result;
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
        public EntityListResult<Rank> FindAll()
        {
            RankDomain rankDomain = new RankDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Rank> entityListResult = new EntityListResult<Rank>();
            try
            {
                entityListResult.ReturnedEntities = rankDomain.FindAll();
                if (rankDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = rankDomain.ActionState.Result;
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
