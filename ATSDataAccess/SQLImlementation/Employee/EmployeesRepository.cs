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
    public class EmployeesRepository : RepositoryBaseClass<Employees>
    {
        public override void Delete(Employees entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Employees entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override void Update(Employees entity, ATSCommon.ActionState actionState)
        {
            throw new NotImplementedException();
        }

        public override List<Employees> FindAll(ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            List<Employees> employeesList = new List<Employees>();
            Employees employees = new Employees();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(EmployeesRepositoryConstants.FindAll, con);
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
                        employees = EmployeesHelper(reader);
                        if (employees != null)
                        {
                            employeesList.Add(employees);
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
            return employeesList;
        }

        public List<Employees> FindByManagerDirectCode(int managerID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Employees employees = new Employees();
            List<Employees> employeesList = new List<Employees>();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(EmployeesRepositoryConstants.FindByDirectManagerID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Int32;
                uniqueCodeParameter.Value = managerID;
                com.Parameters.Add(uniqueCodeParameter);


                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        employees = EmployeesHelper(reader);
                        if (employees != null)
                        {
                            employeesList.Add(employees);
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
            return employeesList;
        }

        public List<Employees> FindByDeptCode(string deptCode, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Employees employees = new Employees();
            List<Employees> employeesList = new List<Employees>();
            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(EmployeesRepositoryConstants.FindByManagerDeptCode, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Varchar2;
                uniqueCodeParameter.Value = deptCode;
                com.Parameters.Add(uniqueCodeParameter);


                using (Oracle.DataAccess.Client.OracleDataReader reader = ((Oracle.DataAccess.Client.OracleDataReader)(com.ExecuteReader())))
                {
                    while (reader.Read())
                    {
                        employees = EmployeesHelper(reader);
                        if (employees != null)
                        {
                            employeesList.Add(employees);
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
            return employeesList;
        }

        public override Employees FindByID(int entityID, ATSCommon.ActionState actionState)
        {
            OracleConnection con = null;
            OracleCommand com = null;
            Employees employees = new Employees();

            try
            {
                con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                com = new OracleCommand(EmployeesRepositoryConstants.FindByPersonnelID, con);
                con.Open();

                com.CommandType = CommandType.StoredProcedure;

                OracleParameter refCursorParameter = new OracleParameter();
                refCursorParameter.OracleDbType = OracleDbType.RefCursor;
                refCursorParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(refCursorParameter);

                OracleParameter uniqueCodeParameter = new OracleParameter();
                uniqueCodeParameter.Direction = ParameterDirection.Input;
                uniqueCodeParameter.OracleDbType = OracleDbType.Double;
                uniqueCodeParameter.Value = entityID;
                com.Parameters.Add(uniqueCodeParameter);


                using (OracleDataReader reader = ((OracleDataReader)(com.ExecuteReader())))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        employees = EmployeesHelper(reader);
                    }
                    else
                    {
                        employees = null;
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
            return employees;
        }

        private Employees EmployeesHelper(OracleDataReader reader)
        {
            Employees employees = new Employees();
            if (reader[EmployeesConstants.CategoryDescription] != DBNull.Value)
            {
                employees.Category = new Category();
                employees.Category.Name = reader[EmployeesConstants.CategoryDescription].ToString();
            }

            employees.Department = new Department();

            if (reader[EmployeesConstants.DepartmentDescription] != DBNull.Value)
                employees.Department.Name = reader[EmployeesConstants.DepartmentDescription].ToString();

            if (reader[EmployeesConstants.DepartmentCode] != DBNull.Value)
                employees.Department.Code = Convert.ToDecimal(reader[EmployeesConstants.DepartmentCode]);

            if (reader[EmployeesConstants.DepartmentShortCode] != DBNull.Value)
                employees.Department.ShortCode = Convert.ToDecimal(reader[EmployeesConstants.DepartmentShortCode]);

            if (reader[EmployeesConstants.DepartmentShortDescription] != DBNull.Value)
                employees.Department.ShortName = reader[EmployeesConstants.DepartmentShortDescription].ToString();

            if (reader[EmployeesConstants.UserID] != DBNull.Value)
                employees.UserID = Convert.ToInt32(reader[EmployeesConstants.UserID]);

            if (reader[EmployeesConstants.Name] != DBNull.Value)
                employees.Name = reader[EmployeesConstants.Name].ToString();

            if (reader[EmployeesConstants.QualificationDescription] != DBNull.Value)
            {
                employees.Qualifications = new Qualifications();
                employees.Qualifications.Name = reader[EmployeesConstants.QualificationDescription].ToString();
            }

            if (reader[EmployeesConstants.RankDescription] != DBNull.Value)
            {
                employees.Rank = new Rank();
                employees.Rank.Name = reader[EmployeesConstants.RankDescription].ToString();
            }

            if (reader[EmployeesConstants.SpecialistDescription] != DBNull.Value)
            {
                employees.Specialist = new Specialist();
                employees.Specialist.Name = reader[EmployeesConstants.SpecialistDescription].ToString();
            }

            if (reader[EmployeesConstants.NationalityDescription] != DBNull.Value)
            {
                employees.Nationality = new Nationality();
                employees.Nationality.Name = reader[EmployeesConstants.NationalityDescription].ToString();
            }

            if (reader[EmployeesConstants.TitleDescription] != DBNull.Value)
            {
                employees.Title = new Title();
                employees.Title.Name = reader[EmployeesConstants.TitleDescription].ToString();
            }



            return employees;
        }
    }
}
