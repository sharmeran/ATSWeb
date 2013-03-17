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
    /// Summary description for SpecialistWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SpecialistWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Specialist> FindByID(int entityID)
        {
            EntityResult<Specialist> entityResult = new EntityResult<Specialist>();
            SpecialistDomain specialistDomain = new SpecialistDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = specialistDomain.FindByID(entityID);
                if (specialistDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = specialistDomain.ActionState.Result;
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
        public EntityListResult<Specialist> FindAll()
        {
            SpecialistDomain specialistDomain = new SpecialistDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Specialist> entityListResult = new EntityListResult<Specialist>();
            try
            {
                entityListResult.ReturnedEntities = specialistDomain.FindAll();
                if (specialistDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = specialistDomain.ActionState.Result;
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
