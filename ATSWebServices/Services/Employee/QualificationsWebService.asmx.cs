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
    /// Summary description for QualificationsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class QualificationsWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Qualifications> FindByID(int entityID)
        {
            EntityResult<Qualifications> entityResult = new EntityResult<Qualifications>();
            QualificationsDomain qualificationsDomain = new QualificationsDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = qualificationsDomain.FindByID(entityID);
                if (qualificationsDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = qualificationsDomain.ActionState.Result;
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
        public EntityListResult<Qualifications> FindAll()
        {
            QualificationsDomain qualificationsDomain = new QualificationsDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Qualifications> entityListResult = new EntityListResult<Qualifications>();
            try
            {
                entityListResult.ReturnedEntities = qualificationsDomain.FindAll();
                if (qualificationsDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = qualificationsDomain.ActionState.Result;
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
