using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.HandPunch;
using ATSCommon.Entites.HandPunch;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.HandPunch;
using Oracle.DataAccess.Client;

namespace ATSDataAccess.SQLImlementation.HandPunch
{
    public class LogDetailRepository : RepositoryBaseClass<LogDetails>
    {
        public override void Delete(LogDetails entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Insert(LogDetails entity, ATSCommon.ActionState actionState)
        {

            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogDetailRepositoryConstants.Insert, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter mainIDParameter = new OracleParameter();
                mainIDParameter.OracleDbType = OracleDbType.Int32;
                mainIDParameter.Direction = ParameterDirection.Input;
                mainIDParameter.Value = entity.LogMainID;
                com.Parameters.Add(mainIDParameter);

                OracleParameter operationDateParameter = new OracleParameter();
                operationDateParameter.OracleDbType = OracleDbType.Date;
                operationDateParameter.Direction = ParameterDirection.Input;
                operationDateParameter.Value = entity.OperationDate;
                com.Parameters.Add(operationDateParameter);

                OracleParameter operationTypeParameter = new OracleParameter();
                operationTypeParameter.OracleDbType = OracleDbType.Int32;
                operationTypeParameter.Direction = ParameterDirection.Input;
                operationTypeParameter.Value = entity.OperationType;
                com.Parameters.Add(operationTypeParameter);

                OracleParameter isWrongParameter = new OracleParameter();
                isWrongParameter.OracleDbType = OracleDbType.Int32;
                isWrongParameter.Direction = ParameterDirection.Input;
                isWrongParameter.Value = entity.IsWrong;
                com.Parameters.Add(isWrongParameter);

                OracleParameter deviceIDParameter = new OracleParameter();
                deviceIDParameter.OracleDbType = OracleDbType.Int32;
                deviceIDParameter.Direction = ParameterDirection.Input;
                deviceIDParameter.Value = entity.DeviceID;
                com.Parameters.Add(deviceIDParameter);

                com.ExecuteNonQuery();
                actionState.SetSuccess();

            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);

            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();

            }
        }

        public override void Update(LogDetails entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogDetailRepositoryConstants.Update, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter idParameter = new OracleParameter();
                idParameter.OracleDbType = OracleDbType.Int32;
                idParameter.Direction = ParameterDirection.Input;
                idParameter.Value = entity.ID;
                com.Parameters.Add(idParameter);

                OracleParameter mainIDParameter = new OracleParameter();
                mainIDParameter.OracleDbType = OracleDbType.Int32;
                mainIDParameter.Direction = ParameterDirection.Input;
                mainIDParameter.Value = entity.LogMainID;
                com.Parameters.Add(mainIDParameter);

                OracleParameter operationDateParameter = new OracleParameter();
                operationDateParameter.OracleDbType = OracleDbType.Date;
                operationDateParameter.Direction = ParameterDirection.Input;
                operationDateParameter.Value = entity.OperationDate;
                com.Parameters.Add(operationDateParameter);

                OracleParameter operationTypeParameter = new OracleParameter();
                operationTypeParameter.OracleDbType = OracleDbType.Int32;
                operationTypeParameter.Direction = ParameterDirection.Input;
                operationTypeParameter.Value = entity.OperationType;
                com.Parameters.Add(operationTypeParameter);

                OracleParameter isWrongParameter = new OracleParameter();
                isWrongParameter.OracleDbType = OracleDbType.Int32;
                isWrongParameter.Direction = ParameterDirection.Input;
                isWrongParameter.Value = entity.IsWrong;
                com.Parameters.Add(isWrongParameter);

                OracleParameter deviceIDParameter = new OracleParameter();
                deviceIDParameter.OracleDbType = OracleDbType.Int32;
                deviceIDParameter.Direction = ParameterDirection.Input;
                deviceIDParameter.Value = entity.DeviceID;
                com.Parameters.Add(deviceIDParameter);

                com.ExecuteNonQuery();
                actionState.SetSuccess();

            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStatusEnum.Exception, ex.Message);

            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();

            }
        }

        public override List<LogDetails> FindAll(ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public List<LogDetails> FindByDate(DateTime entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<LogDetails> logDetailsList = new List<LogDetails>();
            LogDetails logDetails = new LogDetails();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogDetailRepositoryConstants.FindByDate, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter operationDateParameter = new OracleParameter();
                operationDateParameter.OracleDbType = OracleDbType.Date;
                operationDateParameter.Direction = ParameterDirection.Input;
                operationDateParameter.Value = entity;
                com.Parameters.Add(operationDateParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        logDetails = LogDetailsHelper(reader);
                        if (logDetails != null)
                        {
                            logDetailsList.Add(logDetails);
                        }
                    }
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ATSCommon.Enums.ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();
            }
            return logDetailsList;
        }

        public List<LogDetails> FindByLogMainID(int logMainID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<LogDetails> logDetailsList = new List<LogDetails>();
            LogDetails logDetails = new LogDetails();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogDetailRepositoryConstants.FindByMainDetailID, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter operationDateParameter = new OracleParameter();
                operationDateParameter.OracleDbType = OracleDbType.Int32;
                operationDateParameter.Direction = ParameterDirection.Input;
                operationDateParameter.Value = logMainID;
                com.Parameters.Add(operationDateParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        logDetails = LogDetailsHelper(reader);
                        if (logDetails != null)
                        {
                            logDetailsList.Add(logDetails);
                        }
                    }
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ATSCommon.Enums.ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();
            }
            return logDetailsList;
        }

        public List<LogDetails> FindByUserID(int userID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<LogDetails> logDetailsList = new List<LogDetails>();
            LogDetails logDetails = new LogDetails();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogDetailRepositoryConstants.FindByUserID, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter operationDateParameter = new OracleParameter();
                operationDateParameter.OracleDbType = OracleDbType.Int32;
                operationDateParameter.Direction = ParameterDirection.Input;
                operationDateParameter.Value = userID;
                com.Parameters.Add(operationDateParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        logDetails = LogDetailsHelper(reader);
                        if (logDetails != null)
                        {
                            logDetailsList.Add(logDetails);
                        }
                    }
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ATSCommon.Enums.ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();
            }
            return logDetailsList;
        }

        public List<LogDetails> FindByUserIDAndDate(int userID , DateTime date, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<LogDetails> logDetailsList = new List<LogDetails>();
            LogDetails logDetails = new LogDetails();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogDetailRepositoryConstants.FindByUserIDAndDate, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter userIDParameter = new OracleParameter();
                userIDParameter.OracleDbType = OracleDbType.Int32;
                userIDParameter.Direction = ParameterDirection.Input;
                userIDParameter.Value = userID;
                com.Parameters.Add(userIDParameter);

                OracleParameter operationDateParameter = new OracleParameter();
                operationDateParameter.OracleDbType = OracleDbType.Date;
                operationDateParameter.Direction = ParameterDirection.Input;
                operationDateParameter.Value = date;
                com.Parameters.Add(operationDateParameter);


                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        logDetails = LogDetailsHelper(reader);
                        if (logDetails != null)
                        {
                            logDetailsList.Add(logDetails);
                        }
                    }
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ATSCommon.Enums.ActionStatusEnum.Exception, ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                com.Dispose();
            }
            return logDetailsList;
        }

        public override LogDetails FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        private LogDetails LogDetailsHelper(OracleDataReader reader)
        {
            LogDetails logDetails = new LogDetails();
            logDetails.DeviceID = Convert.ToInt32(reader[LogDetailsConstants.DeviceID]);
            logDetails.ID = Convert.ToInt32(reader[LogDetailsConstants.ID]);
            logDetails.IsWrong = Convert.ToBoolean(reader[LogDetailsConstants.IsWrong]);
            logDetails.LogMainID = Convert.ToInt32(reader[LogDetailsConstants.LogMainID]);
            logDetails.OperationDate = Convert.ToDateTime(reader[LogDetailsConstants.OperationDate]);
            logDetails.OperationType = Convert.ToInt32(reader[LogDetailsConstants.OperationType]);
            return logDetails;
        }
    }
}
