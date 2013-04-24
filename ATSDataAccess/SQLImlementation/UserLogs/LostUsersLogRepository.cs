using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Constants.UserLogs;
using ATSCommon.Entites.Devices;
using ATSCommon.Entites.UserLogs;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.UserLogs;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ATSDataAccess.SQLImlementation.UserLogs
{
    public class LostUsersLogRepository : RepositoryBaseClass<LostUsersLog>
    {
        public override void Delete(LostUsersLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(LostUsersLogRepositoryConstants.Delete);

                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.ID, DbType.Int32, entity.ID);


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

        public override void Insert(LostUsersLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(LostUsersLogRepositoryConstants.Insert);

                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.UserID, DbType.Int64, entity.UserID);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.LogingType, DbType.Int32, Convert.ToInt32(entity.LoggingType));
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.LogingDateTime, DbType.DateTime, entity.LoggingDate);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.Device, DbType.Int32, entity.Device.ID);

                if (!IsExist(entity, actionState))
                {
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
                else
                {
                    actionState.SetSuccess();
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

        public override void Update(LostUsersLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(LostUsersLogRepositoryConstants.Update);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.ID, DbType.Int32, entity.ID);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.UserID, DbType.Int64, entity.UserID);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.LogingType, DbType.Int32, Convert.ToInt32(entity.LoggingType));
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.LogingDateTime, DbType.DateTime, entity.LoggingDate);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.Device, DbType.Int32, entity.Device.ID);


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

        public override List<LostUsersLog> FindAll(ActionState actionState)
        {
            List<LostUsersLog> LostUsersLogList;
            LostUsersLog LostUsersLog;
            DbCommand cmd;

            LostUsersLogList = new List<LostUsersLog>();
            LostUsersLog = null;

            try
            {
                cmd = database.GetStoredProcCommand(LostUsersLogRepositoryConstants.FindAll);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        LostUsersLog = LostUsersLogHelper(reader);
                        if (LostUsersLog != null)
                        {
                            LostUsersLogList.Add(LostUsersLog);
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
            return LostUsersLogList;
        }

        public override LostUsersLog FindByID(int entityID, ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(LostUsersLog entity, ActionState actionState)
        {
            DbCommand cmd;
            bool isExist = false;
            try
            {
                DateTime startDate=new DateTime(entity.LoggingDate.Year, entity.LoggingDate.Month, entity.LoggingDate.Day, 0,0,0);
                DateTime endDate=new DateTime(entity.LoggingDate.Year, entity.LoggingDate.Month, entity.LoggingDate.Day, 23,59,59);
                cmd = database.GetStoredProcCommand(LostUsersLogRepositoryConstants.IsExist);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.UserID, DbType.Int32, entity.UserID);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.LogingType, DbType.Int32, entity.LoggingType);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.StartDate, DbType.Date, startDate);
                database.AddInParameter(cmd, LostUsersLogRepositoryConstants.EndDate, DbType.Date, endDate);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result > 0) isExist = true;
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                cmd = null;
            }
            return isExist;
        }
        private LostUsersLog LostUsersLogHelper(SqlDataReader reader)
        {
            LostUsersLog lostUsersLog = new LostUsersLog();
            lostUsersLog.ID = Convert.ToInt64(reader[LostUsersLogConstants.ID]);
            lostUsersLog.UserID = Convert.ToInt64(reader[LostUsersLogConstants.UserID]);
            lostUsersLog.LoggingType = (UserLogTypeEnum)Convert.ToInt32(reader[LostUsersLogConstants.LoggingType]);
            lostUsersLog.LoggingDate = Convert.ToDateTime(reader[LostUsersLogConstants.LogingDate]);
            lostUsersLog.Device = new Device();
            lostUsersLog.Device.ID = Convert.ToInt32(reader[LostUsersLogConstants.Device]);
            return lostUsersLog;
        }
    }
}
