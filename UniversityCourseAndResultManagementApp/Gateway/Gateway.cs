using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityCourseAndResultManagementApp.Gateway
{
    public class Gateway
    {
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }
        public string SecondQuery { get; set; }
        public string SecondCommand { get; set; }

        private static string connectionString = WebConfigurationManager.ConnectionStrings["UniversityManagementDB"].ConnectionString;

        public Gateway()
        {
            Connection = new SqlConnection(connectionString);
        }
    }
}