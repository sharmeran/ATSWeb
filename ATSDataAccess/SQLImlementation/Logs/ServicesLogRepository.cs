using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Constants.Logs;
using ATSCommon.Entites.Logs;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.Logs;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ATSDataAccess.SQLImlementation.Logs
{
    public class ServicesLogRepository : RepositoryBaseClass<ServicesLog>
    {
        public override void Delete(ServicesLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(ServicesLogRepositoryConstants.Delete);

                database.AddInParameter(cmd, ServicesLogRepositoryConstants.ID, DbType.Int64, entity.ID);


                spResult = database.ExecuteNonQuery(cmd);
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotInsert, CommonConstants.Err_CannotDelete);
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.CannotInsert, ex.Message);
            }
            finally
            {
                cmd = null;
            }
        }

        public override void Insert(ServicesLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(ServicesLogRepositoryConstants.Insert);

                database.AddInParameter(cmd, ServicesLogRepositoryConstants.Action, DbType.Int32, Convert.ToInt32(entity.Action));
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.Description, DbType.String, entity.Description);
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.LoggingDate, DbType.DateTime, entity.LoggingDate);
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.Service, DbType.Int32, Convert.ToInt32(entity.Service));


                spResult = database.ExecuteNonQuery(cmd);
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotInsert, CommonConstants.Err_CannotInsert);
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.CannotInsert, ex.Message);
            }
            finally
            {
                cmd = null;
            }
        }

        public override void Update(ServicesLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(ServicesLogRepositoryConstants.Update);
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.ID, DbType.Int32, entity.ID);
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.Action, DbType.Int32, Convert.ToInt32(entity.Action));
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.Description, DbType.String, entity.Description);
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.LoggingDate, DbType.DateTime, entity.LoggingDate);
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.Service, DbType.Int32, Convert.ToInt32(entity.Service));

                spResult = database.ExecuteNonQuery(cmd);
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotInsert, CommonConstants.Err_CannotUpdate);
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.CannotInsert, ex.Message);
            }
            finally
            {
                cmd = null;
            }
        }

        public override List<ServicesLog> FindAll(ActionState actionState)
        {
            List<ServicesLog> servicesLogList;
            ServicesLog servicesLog;
            DbCommand cmd;

            servicesLogList = new List<ServicesLog>();
            servicesLog = null;

            try
            {
                cmd = database.GetStoredProcCommand(ServicesLogRepositoryConstants.FindAll);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        servicesLog = ServicesLogHelper(reader);
                        if (servicesLog != null)
                        {
                            servicesLogList.Add(servicesLog);
                        }
                    }
                    actionState.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return servicesLogList;
        }

        public override ServicesLog FindByID(int entityID, ActionState actionState)
        {
            ServicesLog servicesLog;
            DbCommand cmd;

            // Initialization
            servicesLog = null;
            cmd = null;

            // Implementation 
            try
            {
                cmd = database.GetStoredProcCommand(ServicesLogRepositoryConstants.FindByID);
                database.AddInParameter(cmd, ServicesLogRepositoryConstants.ID, DbType.Int32, entityID);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    if (!reader.HasRows)
                    {
                        actionState.SetFail(ActionStatusEnum.NotFound, CommonConstants.Err_CannotFound);
                    }
                    else
                    {
                        actionState.SetSuccess();
                        reader.Read();
                        servicesLog = ServicesLogHelper(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return servicesLog;
        }

     

        private ServicesLog ServicesLogHelper(SqlDataReader reader)
        {
            ServicesLog servicesLog = new ServicesLog();
            servicesLog.ID = Convert.ToInt64(reader[ServicesLogConstants.ID]);
            servicesLog.Action = (ServicesActionsEnum)Convert.ToInt32(reader[ServicesLogConstants.Action]);
            servicesLog.Description = reader[ServicesLogConstants.Description].ToString();
            servicesLog.LoggingDate = Convert.ToDateTime(reader[ServicesLogConstants.LoggingDate]);
            servicesLog.Service = (ServicesEnum)Convert.ToInt32(reader[ServicesLogConstants.Service]);
            return servicesLog;
        }
    }
}
