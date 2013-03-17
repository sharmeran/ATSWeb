using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSCommon.Entites.Employee;
using ATSCommon.Enums;
using ATSDataAccess.BaseClasses;
using ATSDataAccess.Constants.Employee;
using Oracle.DataAccess.Client;

namespace ATSDataAccess.SQLImlementation.Employee
{
    public class DepartmentRepository : RepositoryBaseClass<Department>
    {
        public override void Delete(Department entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Department entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Update(Department entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override List<Department> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Department> departmentList = new List<Department>();
            Department department = new Department();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DepartmentRepositoryConstants.FindAll, con);
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
                        department = DepartmentHelper(reader);
                        if (department != null)
                        {
                            departmentList.Add(department);
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
            return departmentList;
        }

        public override Department FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public List<Department> FindStartWith(string entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Department department = new Department();
            List<Department> departmentList = new List<Department>();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(DepartmentRepositoryConstants.FindByID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();                
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.NVarchar2;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        department = DepartmentHelper(reader);
                        if (department != null)
                        {
                            departmentList.Add(department);
                        }
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
            return departmentList;
        }

        private Department DepartmentHelper(OracleDataReader reader)
        {
            Department department = new Department();
            if(reader[DepartmentConstants.Code]!= DBNull.Value)
            department.Code = Convert.ToDecimal(reader[DepartmentConstants.Code]);
            if (reader[DepartmentConstants.Name] != DBNull.Value)
            department.Name = reader[DepartmentConstants.Name].ToString();
            if (reader[DepartmentConstants.ShortCode] != DBNull.Value)
            department.ShortCode = Convert.ToDecimal(reader[DepartmentConstants.ShortCode]);
            if (reader[DepartmentConstants.ShortName] != DBNull.Value)
            department.ShortName = reader[DepartmentConstants.ShortName].ToString();
            return department;
        }
    }
}
