using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ATSCommon.Entites.Devices;
using ATSDomain.Domains.Devices;
using ATSWebServices.Common;

namespace ATSWebServices.Services.Devices
{
    /// <summary>
    /// Summary description for ServerWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ServerWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public EntityResult<Server> Add(Server entity)
        {
            EntityResult<Server> entityResult = new EntityResult<Server>();
            ServerDomain serverDomain = new ServerDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                serverDomain.Add(entity);
                entityResult.ReturnedEntity = entity;
                if (serverDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = serverDomain.ActionState.Result;
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
        public EntityResult<Server> Delete(Server entity)
        {
            EntityResult<Server> entityResult = new EntityResult<Server>();
            ServerDomain serverDomain = new ServerDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                serverDomain.Delete(entity);
                entityResult.ReturnedEntity = entity;
                if (serverDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = serverDomain.ActionState.Result;
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
        public EntityResult<Server> Update(Server entity)
        {
            EntityResult<Server> entityResult = new EntityResult<Server>();
            ServerDomain serverDomain = new ServerDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                serverDomain.Update(entity);
                entityResult.ReturnedEntity = entity;
                if (serverDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = serverDomain.ActionState.Result;
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
        public EntityResult<Server> FindByID(int entityID)
        {
            EntityResult<Server> entityResult = new EntityResult<Server>();
            ServerDomain serverDomain = new ServerDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);

            try
            {
                entityResult.ReturnedEntity = serverDomain.FindByID(entityID);
                if (serverDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityResult.Message = serverDomain.ActionState.Result;
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
        public EntityListResult<Server> FindAll()
        {
            ServerDomain serverDomain = new ServerDomain(1, ATSCommon.Enums.LanguagesEnum.Arabic);
            EntityListResult<Server> entityListResult = new EntityListResult<Server>();
            try
            {
                entityListResult.ReturnedEntities = serverDomain.FindAll();
                if (serverDomain.ActionState.Status != ATSCommon.Enums.ActionStatusEnum.NoError)
                {
                    entityListResult.Message = serverDomain.ActionState.Result;
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
