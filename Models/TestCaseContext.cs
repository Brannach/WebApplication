using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WebApplication.Models
{
    public class TestCaseContext
    {
        public string ConnectionString { get; set; }

        public TestCaseContext(string aConnectionString)
        {
            ConnectionString = aConnectionString;
        }

        private MySqlConnection GetConnection() => new MySqlConnection(ConnectionString);

        public List<TestCase> GetAllTestCases()
        {
            var list = new List<TestCase>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("select * from test_case;", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TestCase
                        {
                            id          = Convert.ToInt32(reader["id"]),
                            name        = reader["name"].ToString(),
                            description = reader["description"] == DBNull.Value ? null : reader["description"].ToString(),
                            project_id  = reader["project_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["project_id"])
                        });
                    }
                }
            }
            return list;
        }
    }
}
