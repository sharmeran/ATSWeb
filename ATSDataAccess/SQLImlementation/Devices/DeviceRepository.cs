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
    public class DeviceRepository : RepositoryBaseClass<Device>
    {
        public override void Delete(Device entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;


            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceRepositoryConstants.Delete, con);
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

        public override void Insert(Device entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceRepositoryConstants.Insert, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.OracleDbType = OracleDbType.Varchar2;
                nameParameter.Value = entity.Name;
                com.Parameters.Add(nameParameter);

                OracleParameter descriptionParamete = new OracleParameter();
                descriptionParamete.Direction = ParameterDirection.Input;
                descriptionParamete.OracleDbType = OracleDbType.Varchar2;
                descriptionParamete.Value = entity.Description;
                com.Parameters.Add(descriptionParamete);

                OracleParameter ipParameter = new OracleParameter();
                ipParameter.Direction = ParameterDirection.Input;
                ipParameter.OracleDbType = OracleDbType.Varchar2;
                ipParameter.Value = entity.IP;
                com.Parameters.Add(ipParameter);

                OracleParameter statusParameter = new OracleParameter();
                statusParameter.Direction = ParameterDirection.Input;
                statusParameter.OracleDbType = OracleDbType.Int32;
                statusParameter.Value = entity.Status;
                com.Parameters.Add(statusParameter);

                OracleParameter readerIDParameter = new OracleParameter();
                readerIDParameter.Direction = ParameterDirection.Input;
                readerIDParameter.OracleDbType = OracleDbType.Int32;
                readerIDParameter.Value = entity.ReaderID;
                com.Parameters.Add(readerIDParameter);

                OracleParameter portParameter = new OracleParameter();
                portParameter.Direction = ParameterDirection.Input;
                portParameter.OracleDbType = OracleDbType.Int32;
                portParameter.Value = entity.Port;
                com.Parameters.Add(portParameter);

                OracleParameter categoryParameter = new OracleParameter();
                categoryParameter.Direction = ParameterDirection.Input;
                categoryParameter.OracleDbType = OracleDbType.Int32;
                categoryParameter.Value = entity.Category.ID;
                com.Parameters.Add(categoryParameter);

                OracleParameter serverParameter = new OracleParameter();
                serverParameter.Direction = ParameterDirection.Input;
                serverParameter.OracleDbType = OracleDbType.Int32;
                serverParameter.Value = entity.Server.ID;
                com.Parameters.Add(serverParameter);

                OracleParameter internalDBDeviceID = new OracleParameter();
                internalDBDeviceID.Direction = ParameterDirection.Input;
                internalDBDeviceID.OracleDbType = OracleDbType.Int32;
                internalDBDeviceID.Value = entity.InternalDBDeviceID;
                com.Parameters.Add(internalDBDeviceID);

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

        public override void Update(Device entity, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceRepositoryConstants.Update, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter iDParameter = new OracleParameter();
                iDParameter.Direction = ParameterDirection.Input;
                iDParameter.OracleDbType = OracleDbType.Int32;
                iDParameter.Value = entity.ID;
                com.Parameters.Add(iDParameter);

                OracleParameter nameParameter = new OracleParameter();
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.OracleDbType = OracleDbType.Varchar2;
                nameParameter.Value = entity.Name;
                com.Parameters.Add(nameParameter);

                OracleParameter descriptionParamete = new OracleParameter();
                descriptionParamete.Direction = ParameterDirection.Input;
                descriptionParamete.OracleDbType = OracleDbType.Varchar2;
                descriptionParamete.Value = entity.Description;
                com.Parameters.Add(descriptionParamete);

                OracleParameter ipParameter = new OracleParameter();
                ipParameter.Direction = ParameterDirection.Input;
                ipParameter.OracleDbType = OracleDbType.Varchar2;
                ipParameter.Value = entity.IP;
                com.Parameters.Add(ipParameter);

                OracleParameter statusParameter = new OracleParameter();
                statusParameter.Direction = ParameterDirection.Input;
                statusParameter.OracleDbType = OracleDbType.Int32;
                statusParameter.Value = entity.Status;
                com.Parameters.Add(statusParameter);

                OracleParameter readerIDParameter = new OracleParameter();
                readerIDParameter.Direction = ParameterDirection.Input;
                readerIDParameter.OracleDbType = OracleDbType.Int32;
                readerIDParameter.Value = entity.ReaderID;
                com.Parameters.Add(readerIDParameter);

                OracleParameter portParameter = new OracleParameter();
                portParameter.Direction = ParameterDirection.Input;
                portParameter.OracleDbType = OracleDbType.Int32;
                portParameter.Value = entity.Port;
                com.Parameters.Add(portParameter);

                OracleParameter categoryParameter = new OracleParameter();
                categoryParameter.Direction = ParameterDirection.Input;
                categoryParameter.OracleDbType = OracleDbType.Int32;
                categoryParameter.Value = entity.Category.ID;
                com.Parameters.Add(categoryParameter);

                OracleParameter serverParameter = new OracleParameter();
                serverParameter.Direction = ParameterDirection.Input;
                serverParameter.OracleDbType = OracleDbType.Int32;
                serverParameter.Value = entity.Server.ID;
                com.Parameters.Add(serverParameter);

                OracleParameter internalDBDeviceID = new OracleParameter();
                internalDBDeviceID.Direction = ParameterDirection.Input;
                internalDBDeviceID.OracleDbType = OracleDbType.Int32;
                internalDBDeviceID.Value = entity.InternalDBDeviceID;
                com.Parameters.Add(internalDBDeviceID);

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

        public override List<Device> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Device> deviceList = new List<Device>();
            Device device = new Device();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceRepositoryConstants.FindAll, con);
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
                        device = DeviceHelper(reader);
                        if (device != null)
                        {
                            deviceList.Add(device);
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
            return deviceList;
        }

        public override Device FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Device device = new Device();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceRepositoryConstants.FindByID, con);
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
                        device = DeviceHelper(reader);
                    }
                    else
                    {
                        device = null;
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
            return device;
        }

        public List<Device> FindByDeviceCategory(int categoryID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Device> deviceList = new List<Device>();
            Device device = new Device();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceRepositoryConstants.FindByCategoryID, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter iDParameter = new OracleParameter();
                iDParameter.Direction = ParameterDirection.Input;
                iDParameter.OracleDbType = OracleDbType.Int32;
                iDParameter.Value = categoryID;
                com.Parameters.Add(iDParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        device = DeviceHelper(reader);
                        if (device != null)
                        {
                            deviceList.Add(device);
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
            return deviceList;
        }

        public List<Device> FindByServer(int serverID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Device> deviceList = new List<Device>();
            Device device = new Device();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DeviceRepositoryConstants.FindByServerID, con);
                con.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter iDParameter = new OracleParameter();
                iDParameter.Direction = ParameterDirection.Input;
                iDParameter.OracleDbType = OracleDbType.Int32;
                iDParameter.Value = serverID;
                com.Parameters.Add(iDParameter);

                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        device = DeviceHelper(reader);
                        if (device != null)
                        {
                            deviceList.Add(device);
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
            return deviceList;
        }

        private Device DeviceHelper(OracleDataReader reader)
        {
            Device device = new Device();
            device.ID = Convert.ToInt32(reader[DeviceConstants.ID]);
            device.Description = reader[DeviceConstants.Description].ToString();
            device.IP = reader[DeviceConstants.IP].ToString();
            device.Name = reader[DeviceConstants.Name].ToString();
            device.Port = Convert.ToInt32(reader[DeviceConstants.Port]);
            device.ReaderID = Convert.ToInt32(reader[DeviceConstants.ReaderID]);
            device.Status = Convert.ToInt32(reader[DeviceConstants.Status]);
            DeviceCategoryRepository deviceCategoryRepository = new DeviceCategoryRepository();
            device.Category = deviceCategoryRepository.FindByID(Convert.ToInt32(reader[DeviceConstants.CategoryID]), new ATSCommon.ActionState());
            ServerRepository serverRepository = new ServerRepository();
            device.Server = serverRepository.FindByID(Convert.ToInt32(reader[DeviceConstants.ServerID]), new ATSCommon.ActionState());
            device.InternalDBDeviceID = Convert.ToInt32(reader[DeviceConstants.InternalDBDeviceID]);
            return device;
        }
    }
}
