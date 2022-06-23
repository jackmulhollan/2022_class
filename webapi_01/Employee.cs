using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace webapi_01
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public decimal? Salary { get; set; }
        public string ImagePath { get; set; }
        public int EmployeeCount { get; set; }

        public static List<Employee> SearchEmployees(string search, int pageSize, int pageNumber, SqlConnection sqlConnection)
        {
            List<Employee> employees = new List<Employee>();

            string sql = "select p.EmployeeID, e.FirstName, e.LastName, e.Salary, e.ImagePath, p.[Count] from (select EmployeeID, count(*) over () AS [Count] from Employee where LastName like '%' + @Search + '%' or FirstName like '%' + @Search + '%' order by EmployeeId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p join Employee e on p.EmployeeId = e.EmployeeId order by 1;";

            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.Text;

                sqlCommand.Parameters.Add("@PageSize", System.Data.SqlDbType.Int);
                sqlCommand.Parameters.Add("@PageNumber", System.Data.SqlDbType.Int);
                sqlCommand.Parameters.Add("@Search", System.Data.SqlDbType.VarChar);

                sqlCommand.Parameters["@PageSize"].Value = pageSize;
                sqlCommand.Parameters["@PageNumber"].Value = pageNumber;
                sqlCommand.Parameters["@Search"].Value = search;

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]);
                        employee.LastName = sqlDataReader["LastName"].ToString();
                        employee.FirstName = sqlDataReader["FirstName"].ToString();
                        employee.Salary = (sqlDataReader["Salary"].ToString().Length == 0) ? null : Convert.ToDecimal(sqlDataReader["Salary"]);
                        employee.ImagePath = sqlDataReader["ImagePath"].ToString();
                        employee.EmployeeCount = Convert.ToInt32(sqlDataReader["Count"]);
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }
        public static int InsertEmployee(string lastName, string firstName, decimal salary, SqlConnection sqlConnection)
        {
            string sql = "insert into Employee (LastName, FirstName, Salary) values (@LastName, @FirstName, @Salary);";

            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.Text;

                sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@Salary", System.Data.SqlDbType.Decimal);

                sqlCommand.Parameters["@LastName"].Value = lastName;
                sqlCommand.Parameters["@FirstName"].Value = firstName;
                sqlCommand.Parameters["@Salary"].Value = salary;

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        public static int UpdateEmployee(int employeeId, string lastName, string firstName, decimal salary, SqlConnection sqlConnection)
        {
            string sql = "update Employee set LastName = @LastName, FirstName = @FirstName, Salary = @Salary where EmployeeId = @EmployeeID;";

            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.Text;

                sqlCommand.Parameters.Add("@EmployeeId", System.Data.SqlDbType.Int);
                sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@Salary", System.Data.SqlDbType.Decimal);

                sqlCommand.Parameters["@EmployeeId"].Value = employeeId;
                sqlCommand.Parameters["@LastName"].Value = lastName;
                sqlCommand.Parameters["@FirstName"].Value = firstName;
                sqlCommand.Parameters["@Salary"].Value = salary;

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        public static int DeleteEmployee(int employeeId, SqlConnection sqlConnection)
        {
            string sql = "delete from Employee where EmployeeId = @EmployeeId;";

            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.Text;

                sqlCommand.Parameters.Add("@EmployeeId", System.Data.SqlDbType.Int);

                sqlCommand.Parameters["@EmployeeId"].Value = employeeId;

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                return rowsAffected;
            }
        }

    }
}