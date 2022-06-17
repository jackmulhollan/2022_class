using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace webapi_01
{
    public enum Result { success, failure }
    public class Response
    {
        public string result { get; set; }
        public int rowsAffected { get; set; }
        public string message { get; set; }
        public List<Employee> employees { get; set; }
    }
}