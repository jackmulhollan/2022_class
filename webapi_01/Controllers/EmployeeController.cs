using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapi_01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        static string serverName = @"LAPTOP-T24FIB73\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
        static string databaseName = "db01"; //Change to the database where you created your Employee table.
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";


        [HttpGet]
        [Route("/SelectEmployees")]
        public Response SelectEmployees()
        {
            Response response = new Response();
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    employees = Employee.SelectEmployees(sqlConnection);
                    response.result = Result.success.ToString();
                    response.rowsAffected = 0;
                }
            }
            catch (Exception ex)
            {
                response.result = Result.failure.ToString();
                response.message = ex.Message;
            }

            response.employees = employees;

            return response;
        }

        [HttpGet]
        [Route("/InsertEmployee")]
        public Response InsertEmployee(string lastName, string firstName, decimal salary)
        {
            Response response = new Response();
            List<Employee> employees = new List<Employee>();
            int rowsAffected = 0;
            string result = Result.failure.ToString();
            string message = "";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    rowsAffected = Employee.InsertEmployee(lastName, firstName, salary, sqlConnection);
                    result = Result.success.ToString();
                    employees = Employee.SelectEmployees(sqlConnection);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            response.result = result;
            response.rowsAffected = rowsAffected;
            response.message = message;
            response.employees = employees;

            return response;
        }

        [HttpGet]
        [Route("/UpdateEmployee")]
        public Response UpdateEmployee(int employeeId, string lastName, string firstName, decimal salary)
        {
            Response response = new Response();
            List<Employee> employees = new List<Employee>();
            int rowsAffected = 0;
            string result = Result.failure.ToString();
            string message = "";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    rowsAffected = Employee.UpdateEmployee(employeeId, lastName, firstName, salary, sqlConnection);
                    result = Result.success.ToString();
                    employees = Employee.SelectEmployees(sqlConnection);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

            }

            response.result = result;
            response.rowsAffected = rowsAffected;
            response.message = message;
            response.employees = employees;

            return response;
        }

        [HttpGet]
        [Route("/DeleteEmployee")]
        public Response DeleteEmployee(int employeeId)
        {
            Response response = new Response();
            List<Employee> employees = new List<Employee>();
            int rowsAffected = 0;
            string result = Result.failure.ToString();
            string message = "";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    rowsAffected = Employee.DeleteEmployee(employeeId, sqlConnection);
                    result = Result.success.ToString();
                    employees = Employee.SelectEmployees(sqlConnection);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            response.result = result;
            response.rowsAffected = rowsAffected;
            response.message = message;
            response.employees = employees;

            return response;
        }

    }
}