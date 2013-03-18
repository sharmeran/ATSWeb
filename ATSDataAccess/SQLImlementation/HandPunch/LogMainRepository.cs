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
    public class LogMainRepository : RepositoryBaseClass<LogMain>
    {
        public override void Delete(LogMain entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Insert(LogMain entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogMainRepositoryConstants.Insert, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter userParameter = new OracleParameter();
                userParameter.Direction = ParameterDirection.Input;
                userParameter.OracleDbType = OracleDbType.Int32;
                userParameter.Value = entity.UserID;
                com.Parameters.Add(userParameter);

                OracleParameter attendanceDateParameter = new OracleParameter();
                attendanceDateParameter.Direction = ParameterDirection.Input;
                attendanceDateParameter.OracleDbType = OracleDbType.Date;
                attendanceDateParameter.Value = entity.AttendanceDate;
                com.Parameters.Add(attendanceDateParameter);

                OracleParameter inDateParameter = new OracleParameter();
                inDateParameter.Direction = ParameterDirection.Input;
                inDateParameter.OracleDbType = OracleDbType.Date;
                inDateParameter.Value = entity.InDate;
                com.Parameters.Add(inDateParameter);

                OracleParameter outDateParameter = new OracleParameter();
                outDateParameter.Direction = ParameterDirection.Input;
                outDateParameter.OracleDbType = OracleDbType.Date;
                outDateParameter.Value = entity.OutDate;
                com.Parameters.Add(outDateParameter);

                OracleParameter isClosedParameter = new OracleParameter();
                isClosedParameter.Direction = ParameterDirection.Input;
                isClosedParameter.OracleDbType = OracleDbType.Int32;
                isClosedParameter.Value = entity.IsClosed;
                com.Parameters.Add(isClosedParameter);

                OracleParameter missInParameter = new OracleParameter();
                missInParameter.Direction = ParameterDirection.Input;
                missInParameter.OracleDbType = OracleDbType.Int32;
                missInParameter.Value = entity.MissIN;
                com.Parameters.Add(missInParameter);

                OracleParameter missOutParameter = new OracleParameter();
                missOutParameter.Direction = ParameterDirection.Input;
                missOutParameter.OracleDbType = OracleDbType.Int32;
                missOutParameter.Value = entity.MissOut;
                com.Parameters.Add(missOutParameter);

                OracleParameter shiftIDParameter = new OracleParameter();
                shiftIDParameter.Direction = ParameterDirection.Input;
                shiftIDParameter.OracleDbType = OracleDbType.Int32;
                shiftIDParameter.Value = entity.ShiftID;
                com.Parameters.Add(shiftIDParameter);

                OracleParameter plusInParameter = new OracleParameter();
                plusInParameter.Direction = ParameterDirection.Input;
                plusInParameter.OracleDbType = OracleDbType.Int32;
                plusInParameter.Value = entity.PlusIN;
                com.Parameters.Add(plusInParameter);


                OracleParameter plusOutParameter = new OracleParameter();
                plusOutParameter.Direction = ParameterDirection.Input;
                plusOutParameter.OracleDbType = OracleDbType.Int32;
                plusOutParameter.Value = entity.PlusOut;
                com.Parameters.Add(plusOutParameter);

                OracleParameter idParameter = new OracleParameter();                
                idParameter.Direction = ParameterDirection.Output;
                idParameter.OracleDbType = OracleDbType.Int32;                
                com.Parameters.Add(idParameter);

                com.ExecuteNonQuery();
                entity.ID = Convert.ToInt32(com.Parameters[10].Value);
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

        public override void Update(LogMain entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogMainRepositoryConstants.Update, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter idParameter = new OracleParameter();
                idParameter.Direction = ParameterDirection.Input;
                idParameter.OracleDbType = OracleDbType.Int32;
                idParameter.Value = entity.ID;
                com.Parameters.Add(idParameter);

                OracleParameter userParameter = new OracleParameter();
                userParameter.Direction = ParameterDirection.Input;
                userParameter.OracleDbType = OracleDbType.Int32;
                userParameter.Value = entity.UserID;
                com.Parameters.Add(userParameter);

                OracleParameter attendanceDateParameter = new OracleParameter();
                attendanceDateParameter.Direction = ParameterDirection.Input;
                attendanceDateParameter.OracleDbType = OracleDbType.Date;
                attendanceDateParameter.Value = entity.AttendanceDate;
                com.Parameters.Add(attendanceDateParameter);

                OracleParameter inDateParameter = new OracleParameter();
                inDateParameter.Direction = ParameterDirection.Input;
                inDateParameter.OracleDbType = OracleDbType.Date;
                inDateParameter.Value = entity.InDate;
                com.Parameters.Add(inDateParameter);

                OracleParameter outDateParameter = new OracleParameter();
                outDateParameter.Direction = ParameterDirection.Input;
                outDateParameter.OracleDbType = OracleDbType.Date;
                outDateParameter.Value = entity.OutDate;
                com.Parameters.Add(outDateParameter);

                OracleParameter isClosedParameter = new OracleParameter();
                isClosedParameter.Direction = ParameterDirection.Input;
                isClosedParameter.OracleDbType = OracleDbType.Int32;
                isClosedParameter.Value = entity.IsClosed;
                com.Parameters.Add(isClosedParameter);

                OracleParameter missInParameter = new OracleParameter();
                missInParameter.Direction = ParameterDirection.Input;
                missInParameter.OracleDbType = OracleDbType.Int32;
                missInParameter.Value = entity.MissIN;
                com.Parameters.Add(missInParameter);

                OracleParameter missOutParameter = new OracleParameter();
                missOutParameter.Direction = ParameterDirection.Input;
                missOutParameter.OracleDbType = OracleDbType.Int32;
                missOutParameter.Value = entity.MissOut;
                com.Parameters.Add(missOutParameter);

                OracleParameter shiftIDParameter = new OracleParameter();
                shiftIDParameter.Direction = ParameterDirection.Input;
                shiftIDParameter.OracleDbType = OracleDbType.Int32;
                shiftIDParameter.Value = entity.ShiftID;
                com.Parameters.Add(shiftIDParameter);

                OracleParameter plusInParameter = new OracleParameter();
                plusInParameter.Direction = ParameterDirection.Input;
                plusInParameter.OracleDbType = OracleDbType.Int32;
                plusInParameter.Value = entity.PlusIN;
                com.Parameters.Add(plusInParameter);


                OracleParameter plusOutParameter = new OracleParameter();
                plusOutParameter.Direction = ParameterDirection.Input;
                plusOutParameter.OracleDbType = OracleDbType.Int32;
                plusOutParameter.Value = entity.PlusOut;
                com.Parameters.Add(plusOutParameter);

               
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
       
        public override List<LogMain> FindAll(ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override LogMain FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            LogMain logMain = new LogMain();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogMainRepositoryConstants.FindByID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Int32;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (OracleDataReader reader = ((OracleDataReader)(com.ExecuteReader())))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        logMain = LogMainHelper(reader);
                    }
                    else
                    {
                        logMain = null;
                    }


                }
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
            return logMain;
        }

        public  List<LogMain> FindByDateAndUserID(DateTime date, int userID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<LogMain> logMainList = new List<LogMain>();
            LogMain logMain = new LogMain();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogMainRepositoryConstants.FindByUserIDAndDate, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter userIDParameter = new OracleParameter();
                userIDParameter.OracleDbType = OracleDbType.Int32;
                userIDParameter.Direction = ParameterDirection.Input; ;
                userIDParameter.Value =userID;
                com.Parameters.Add(userIDParameter);

                OracleParameter attendanceDateParameter = new OracleParameter();
                attendanceDateParameter.OracleDbType = OracleDbType.Date;
                attendanceDateParameter.Direction = ParameterDirection.Input; ;
                attendanceDateParameter.Value = date;
                com.Parameters.Add(attendanceDateParameter);


                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        logMain = LogMainHelper(reader);
                        if (logMain != null)
                        {
                            logMainList.Add(logMain);
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
            return logMainList;
        }

        public List<LogMain> FindByDateAndDeptCode(string deptCode, DateTime date, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<LogMain> logMainList = new List<LogMain>();
            LogMain logMain = new LogMain();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(LogMainRepositoryConstants.FindByDateAndDeptCode, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter deptCodeParameter = new OracleParameter();
                deptCodeParameter.OracleDbType = OracleDbType.Long;
                deptCodeParameter.Direction = ParameterDirection.Input; ;
                deptCodeParameter.Value = deptCode;
                com.Parameters.Add(deptCodeParameter);

                OracleParameter attendanceDateParameter = new OracleParameter();
                attendanceDateParameter.OracleDbType = OracleDbType.Date;
                attendanceDateParameter.Direction = ParameterDirection.Input; ;
                attendanceDateParameter.Value = date;
                com.Parameters.Add(attendanceDateParameter);


                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        logMain = LogMainHelper(reader);
                        if (logMain != null)
                        {
                            logMainList.Add(logMain);
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
            return logMainList;
        }

        private LogMain LogMainHelper(OracleDataReader reader)
        {
            LogMain logMain = new LogMain();
            logMain.AttendanceDate = Convert.ToDateTime(reader[LogMainConstants.AttendanceDate]);
            logMain.ID = Convert.ToInt32(reader[LogMainConstants.ID]);
            logMain.InDate = Convert.ToDateTime(reader[LogMainConstants.InDate]);
            logMain.IsClosed = Convert.ToBoolean(reader[LogMainConstants.IsClosed]);
            logMain.MissIN = Convert.ToInt32(reader[LogMainConstants.MissIn]);
            logMain.MissOut = Convert.ToInt32(reader[LogMainConstants.MissOut]);
            logMain.OutDate = Convert.ToDateTime(reader[LogMainConstants.OutDate]);
            logMain.PlusIN = Convert.ToInt32(reader[LogMainConstants.PlusIn]);
            logMain.PlusOut = Convert.ToInt32(reader[LogMainConstants.PlusOut]);
            logMain.ShiftID = Convert.ToInt32(reader[LogMainConstants.ShiftID]);
            logMain.UserID = Convert.ToInt32(reader[LogMainConstants.UserID]);
            return logMain;
        }
    }
}
