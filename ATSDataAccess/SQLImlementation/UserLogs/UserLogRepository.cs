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
using ATSDataAccess.SQLImlementation.Devices;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace ATSDataAccess.SQLImlementation.UserLogs
{
    public class UserLogRepository : RepositoryBaseClass<UserLog>
    {
        public override void Delete(UserLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.Delete);

                database.AddInParameter(cmd, UserLogRepositoryConstants.ID, DbType.Int32, entity.ID);


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

        public override void Insert(UserLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.Insert);

                database.AddInParameter(cmd, UserLogRepositoryConstants.UserID, DbType.Int64, entity.UserID);
                database.AddInParameter(cmd, UserLogRepositoryConstants.LogingType, DbType.Int32, Convert.ToInt32(entity.LoggingType));
                database.AddInParameter(cmd, UserLogRepositoryConstants.LogingDateTime, DbType.DateTime, entity.LogingDateTime);
                database.AddInParameter(cmd, UserLogRepositoryConstants.DeviceID, DbType.Int32, entity.DeviceID.ID);

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

        public override void Update(UserLog entity, ActionState actionState)
        {
            int spResult;
            DbCommand cmd;
            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.Update);
                database.AddInParameter(cmd, UserLogRepositoryConstants.ID, DbType.Int32, entity.ID);
                database.AddInParameter(cmd, UserLogRepositoryConstants.UserID, DbType.Int64, entity.UserID);
                database.AddInParameter(cmd, UserLogRepositoryConstants.LogingType, DbType.Int32, Convert.ToInt32(entity.LoggingType));
                database.AddInParameter(cmd, UserLogRepositoryConstants.LogingDateTime, DbType.DateTime, entity.LogingDateTime);
                database.AddInParameter(cmd, UserLogRepositoryConstants.DeviceID, DbType.DateTime, entity.DeviceID.ID);

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

        public override List<UserLog> FindAll(ActionState actionState)
        {
            List<UserLog> userLogEntityList;
            UserLog userLogEntity;
            DbCommand cmd;

            userLogEntityList = new List<UserLog>();
            userLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.FindAll);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        userLogEntity = UserLogHelper(reader);
                        if (userLogEntity != null)
                        {
                            userLogEntityList.Add(userLogEntity);
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
            return userLogEntityList;
        }

        public override UserLog FindByID(int entityID, ActionState actionState)
        {
            throw new NotImplementedException();
        }     

        public List<UserLog> FindByUserID(int entityID, ActionState actionState)
        {
            List<UserLog> userLogEntityList;
            UserLog userLogEntity;
            DbCommand cmd;

            userLogEntityList = new List<UserLog>();
            userLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.UserLogFindByUserID);
                database.AddInParameter(cmd, UserLogRepositoryConstants.UserID, DbType.Int32, entityID);


                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        userLogEntity = UserLogHelper(reader);
                        if (userLogEntity != null)
                        {
                            userLogEntityList.Add(userLogEntity);
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
            return userLogEntityList;
        }

        public List<UserLog> FindByDateTime(DateTime startDate, DateTime endDate, ActionState actionState)
        {
            List<UserLog> userLogEntityList;
            UserLog userLogEntity;
            DbCommand cmd;

            userLogEntityList = new List<UserLog>();
            userLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.UserLogFindByDate);
                database.AddInParameter(cmd, UserLogRepositoryConstants.StartDateTime, DbType.DateTime, startDate);
                database.AddInParameter(cmd, UserLogRepositoryConstants.EndDateTime, DbType.DateTime, endDate);

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        userLogEntity = UserLogHelperLight(reader);
                        if (userLogEntity != null)
                        {
                            userLogEntityList.Add(userLogEntity);
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
            return userLogEntityList;
        }

        public List<UserLog> FindByUserIDAndDateTime(int userID, DateTime startDate, DateTime endDate, ActionState actionState)
        {
            List<UserLog> userLogEntityList;
            UserLog userLogEntity;
            DbCommand cmd;

            userLogEntityList = new List<UserLog>();
            userLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.UserLogFindByUserIDAndLoggingDate);
                database.AddInParameter(cmd, UserLogRepositoryConstants.UserID, DbType.Int32, userID);
                database.AddInParameter(cmd, UserLogRepositoryConstants.StartDateTime, DbType.DateTime, startDate);
                database.AddInParameter(cmd, UserLogRepositoryConstants.EndDateTime, DbType.DateTime, endDate);

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        userLogEntity = UserLogHelperLight(reader);
                        if (userLogEntity != null)
                        {
                            userLogEntityList.Add(userLogEntity);
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
            return userLogEntityList;
        }

        public List<UserLog> FindByDeviceCategoryID(DeviceCategory category, ActionState actionState)
        {
            List<UserLog> userLogEntityList;
            UserLog userLogEntity;
            DbCommand cmd;

            userLogEntityList = new List<UserLog>();
            userLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.UserLogByDeviceCategory);
                database.AddInParameter(cmd, UserLogRepositoryConstants.DeviceCategoryID, DbType.Int32, category.ID);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        userLogEntity = UserLogHelperLight(reader);
                        if (userLogEntity != null)
                        {
                            userLogEntityList.Add(userLogEntity);
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
            return userLogEntityList;
        }

        public List<UserLog> FindByDeviceIP(string ip, ActionState actionState)
        {
            List<UserLog> userLogEntityList;
            UserLog userLogEntity;
            DbCommand cmd;

            userLogEntityList = new List<UserLog>();
            userLogEntity = null;

            try
            {
                cmd = database.GetStoredProcCommand(UserLogRepositoryConstants.UserLogFindByDeviceIP);
                database.AddInParameter(cmd, UserLogRepositoryConstants.IPParameter, DbType.String, ip);
                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        userLogEntity = UserLogHelperLight(reader);
                        if (userLogEntity != null)
                        {
                            userLogEntityList.Add(userLogEntity);
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
            return userLogEntityList;
        }

        public Int64 FindRemoteCountByDate(DateTime startDate, DateTime endDate, ActionState actionState, string connictionString)
        {
            Int64 result = 0;
            int spResult;
            Database db;
            DbCommand cmd;
            try
            {
                db = new SqlDatabase(connictionString);


                cmd = db.GetStoredProcCommand(UserLogRepositoryConstants.UserLogFindCountByDate);
                db.AddInParameter(cmd, UserLogRepositoryConstants.StartDateTime, DbType.DateTime, startDate);
                db.AddInParameter(cmd, UserLogRepositoryConstants.EndDateTime, DbType.DateTime, endDate);
                db.AddOutParameter(cmd, CommonConstants.Param_Founded, DbType.Int64, 100);

                spResult = database.ExecuteNonQuery(cmd);

                result = Convert.ToInt64(cmd.Parameters[CommonConstants.Param_Founded].Value);
                actionState.SetSuccess();

            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                db = null;
                cmd = null;
            }
            return result;
        }

        public List<UserLog> FindRemoteUserLogByDate(DateTime startDate, DateTime endDate, ActionState actionState, string connictionString)
        {
            List<UserLog> userLogEntityList;
            UserLog userLogEntity;
            Database db;
            DbCommand cmd;

            userLogEntityList = new List<UserLog>();
            userLogEntity = null;

            try
            {
                db = new SqlDatabase(connictionString);
                cmd = db.GetStoredProcCommand(UserLogRepositoryConstants.UserLogFindByDate);
                db.AddInParameter(cmd, UserLogRepositoryConstants.StartDateTime, DbType.DateTime, startDate);
                db.AddInParameter(cmd, UserLogRepositoryConstants.EndDateTime, DbType.DateTime, endDate);

                using (SqlDataReader reader = ((SqlDataReader)((RefCountingDataReader)database.ExecuteReader(cmd)).InnerReader))
                {
                    while (reader.Read())
                    {
                        userLogEntity = UserLogRemotlyHelper(reader, connictionString);
                        if (userLogEntity != null)
                        {
                            userLogEntityList.Add(userLogEntity);
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
                db = null;
            }
            return userLogEntityList;
        }

        public bool IsExistRemotly(UserLog entity, ActionState actionState, string connectionString)
        {
            int spResult;
            SqlConnection con = null;
            SqlCommand com = null;
            bool result = false;

            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                com = new SqlCommand(UserLogRepositoryConstants.UserLogIsExist, con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add(UserLogRepositoryConstants.UserID, entity.UserID);
                com.Parameters.Add(UserLogRepositoryConstants.LogingDateTime, entity.LogingDateTime);
                com.Parameters.Add(CommonConstants.Param_Founded, SqlDbType.Int).Direction = ParameterDirection.Output;

                spResult = com.ExecuteNonQuery();
                actionState.SetSuccess();
                if (Convert.ToInt64(com.Parameters[CommonConstants.Param_Founded].Value) > 0)
                    result = true;
                else
                    result = false;


            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.CannotInsert, ex.Message);
            }
            finally
            {
                con.Close();
                con = null;
                com = null;
            }
            return result;
        }

        public void InsertRemotly(UserLog entity, ActionState actionState, string connectionString)
        {
            int spResult;
            SqlConnection con = null;
            SqlCommand com = null;

            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                com = new SqlCommand(UserLogRepositoryConstants.Insert, con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add(UserLogRepositoryConstants.UserID, entity.UserID);
                com.Parameters.Add(UserLogRepositoryConstants.LogingType, Convert.ToInt32(entity.LoggingType));
                com.Parameters.Add(UserLogRepositoryConstants.LogingDateTime, entity.LogingDateTime);
                com.Parameters.Add(UserLogRepositoryConstants.DeviceID, entity.DeviceID.ID);

                spResult = com.ExecuteNonQuery();
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
                con.Close();
                con = null;
                com = null;
            }
        }

        private UserLog UserLogHelper(SqlDataReader reader)
        {
            UserLog userLog = new UserLog();
            userLog.ID = Convert.ToInt64(reader[UserLogConstants.ID]);
            userLog.UserID = Convert.ToInt64(reader[UserLogConstants.UserID]);
            userLog.LoggingType = (UserLogTypeEnum)Convert.ToInt32(reader[UserLogConstants.LogingType]);
            userLog.LogingDateTime = Convert.ToDateTime(reader[UserLogConstants.LogingDateTime]);
            //DeviceRepository devicesRepository = new DeviceRepository();
            //userLog.DeviceID = devicesRepository.FindByIDNotFull(Convert.ToInt32(reader[UserLogConstants.DeviceID]), new ActionState());
            return userLog;
        }
        private UserLog UserLogHelperLight(SqlDataReader reader)
        {
            UserLog userLog = new UserLog();
            userLog.ID = Convert.ToInt64(reader[UserLogConstants.ID]);
            userLog.UserID = Convert.ToInt64(reader[UserLogConstants.UserID]);
            userLog.LoggingType = (UserLogTypeEnum)Convert.ToInt32(reader[UserLogConstants.LogingType]);
            userLog.LogingDateTime = Convert.ToDateTime(reader[UserLogConstants.LogingDateTime]);
            userLog.DeviceID = new Device();
            userLog.DeviceID.Name = reader[UserLogRepositoryConstants.Name].ToString();
            userLog.DeviceID.IP = reader[UserLogRepositoryConstants.IP].ToString();

            return userLog;
        }
        private UserLog UserLogRemotlyHelper(SqlDataReader reader, string connectionString)
        {
            UserLog userLog = new UserLog();
            userLog.ID = Convert.ToInt64(reader[UserLogConstants.ID]);
            userLog.UserID = Convert.ToInt64(reader[UserLogConstants.UserID]);
            userLog.LoggingType = (UserLogTypeEnum)Convert.ToInt32(reader[UserLogConstants.LogingType]);
            userLog.LogingDateTime = Convert.ToDateTime(reader[UserLogConstants.LogingDateTime]);
            //DeviceRepository devicesRepository = new DeviceRepository();
            //userLog.DeviceID = devicesRepository.FindByIDRemotlyLight(Convert.ToInt32(reader[UserLogConstants.DeviceID]), new ActionState(), connectionString);
            return userLog;
        }


    }
}
