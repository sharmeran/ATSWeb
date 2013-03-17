using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Devices;
using ATSCommon.Entites.Devices;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.Constants.Devices;
using Oracle.DataAccess.Client;

namespace ATSDataAccess.SQLImlementation.Devices
{
    public class ServerRepository : RepositoryBaseClass<Server>
    {
        public override void Delete(Server entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;


            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ServerRepositoryConstants.Delete, con);
                con.Open();
                com.CommandType = CommandType.StoredProcedure;

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Int32;
                uniqueCodeParameter.Value = entity.ID;
                com.Parameters.Add(uniqueCodeParameter);
                int spResult = 0;

                spResult = com.ExecuteNonQuery();
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotDelete, CommonConstants.Err_CannotDelete);
                }


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

        public override void Insert(Server entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ServerRepositoryConstants.Insert, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter connectionStringParameter = new OracleParameter();
                connectionStringParameter.Direction = ParameterDirection.Input;
                connectionStringParameter.OracleDbType = OracleDbType.Varchar2;
                connectionStringParameter.Value = entity.ConnectionSring;
                com.Parameters.Add(connectionStringParameter);

                OracleParameter descriptionParamete = new OracleParameter();
                descriptionParamete.Direction = ParameterDirection.Input;
                descriptionParamete.OracleDbType = OracleDbType.Varchar2;
                descriptionParamete.Value = entity.Description;
                com.Parameters.Add(descriptionParamete);

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.OracleDbType = OracleDbType.Varchar2;
                nameParameter.Value = entity.Name;
                com.Parameters.Add(nameParameter);

                OracleParameter serverIPParameter = new OracleParameter();
                serverIPParameter.Direction = ParameterDirection.Input;
                serverIPParameter.OracleDbType = OracleDbType.Varchar2;
                serverIPParameter.Value = entity.IP;
                com.Parameters.Add(serverIPParameter);

                int spResult = 0;

                spResult = com.ExecuteNonQuery();
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotDelete, CommonConstants.Err_CannotInsert);
                }


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

        public override void Update(Server entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ServerRepositoryConstants.Update, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter iDParameter = new OracleParameter();
                iDParameter.Direction = ParameterDirection.Input;
                iDParameter.OracleDbType = OracleDbType.Int32;
                iDParameter.Value = entity.ID;
                com.Parameters.Add(iDParameter);

                OracleParameter connectionStringParameter = new OracleParameter();
                connectionStringParameter.Direction = ParameterDirection.Input;
                connectionStringParameter.OracleDbType = OracleDbType.Varchar2;
                connectionStringParameter.Value = entity.ConnectionSring;
                com.Parameters.Add(connectionStringParameter);

                OracleParameter descriptionParamete = new OracleParameter();
                descriptionParamete.Direction = ParameterDirection.Input;
                descriptionParamete.OracleDbType = OracleDbType.Varchar2;
                descriptionParamete.Value = entity.Description;
                com.Parameters.Add(descriptionParamete);

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.OracleDbType = OracleDbType.Varchar2;
                nameParameter.Value = entity.Name;
                com.Parameters.Add(nameParameter);

                OracleParameter serverIPParameter = new OracleParameter();
                serverIPParameter.Direction = ParameterDirection.Input;
                serverIPParameter.OracleDbType = OracleDbType.Varchar2;
                serverIPParameter.Value = entity.IP;
                com.Parameters.Add(serverIPParameter);

                int spResult = 0;

                spResult = com.ExecuteNonQuery();
                if (spResult > 0)
                {
                    actionState.SetSuccess();
                }
                else
                {
                    actionState.SetFail(ActionStatusEnum.CannotDelete, CommonConstants.Err_CannotUpdate);
                }


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

        public override List<Server> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Server> serverList = new List<Server>();
            Server server = new Server();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ServerRepositoryConstants.FindAll, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        server = ServerHelper(reader, true);
                        if (server != null)
                        {
                            serverList.Add(server);
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
            return serverList;
        }

        public override Server FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Server server = new Server();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(ServerRepositoryConstants.FindByID, con);
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
                        server = ServerHelper(reader, false);
                    }
                    else
                    {
                        server = null;
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
            return server;
        }

        private Server ServerHelper(OracleDataReader reader, bool isFull)
        {
            Server server = new Server();
            server.ConnectionSring = reader[ServerConstants.ConnectionString].ToString();
            server.Description = reader[ServerConstants.Description].ToString();            
            server.ID = Convert.ToInt32(reader[ServerConstants.ID]);
            server.IP = reader[ServerConstants.ServerIP].ToString();
            server.Name = reader[ServerConstants.Name].ToString();
            if (isFull)
            {
                DeviceRepository deviceRepository = new DeviceRepository();
                server.DeviceList = deviceRepository.FindByServer(server.ID, new ATSCommon.ActionState());
            }
            return server;
        }
    }
}
