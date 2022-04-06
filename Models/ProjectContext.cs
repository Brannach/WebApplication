using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using MySql.Data.MySqlClient;
namespace WebApplication.Models
{
    public class ProjectContext
    {
        public string ConnectionString { get; set; }

        public ProjectContext (string aConnectionString)
        {
            this.ConnectionString = aConnectionString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Project> GetAllProjects()
        {
            List<Project> ProjectList = new List<Project>();

            using (MySqlConnection MySqlConnection = GetConnection())
            {
                MySqlConnection.Open();
                MySqlCommand MySqlCommand = new MySqlCommand("select * from project;", MySqlConnection);

                using (var reader = MySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProjectList.Add(new Project()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                        });
                    }
                }
            }
            return ProjectList;
        }

        public Project GetProjectById(int id)
        {
            Project AProject = new Project();

            using (MySqlConnection MySqlConnection = GetConnection())
            {
                MySqlConnection.Open();
                MySqlCommand MySqlCommand = new MySqlCommand("select * from project where id=@id;", MySqlConnection);
                MySqlCommand.Parameters.AddWithValue("@id", id.ToString());
                using (var reader = MySqlCommand.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        AProject = new Project()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return AProject;
        }

        public Project UpdateProject(Project NewValues)
        {
            Project AProjectList = new Project();

            using (MySqlConnection MySqlConnection = GetConnection())
            {
                MySqlConnection.Open();
                MySqlCommand MySqlCommand = new MySqlCommand("update project set name = @name where id=@id;", MySqlConnection);
                MySqlCommand.Parameters.AddWithValue("@id", NewValues.id);
                MySqlCommand.Parameters.AddWithValue("@name", NewValues.name);
                using (var reader = MySqlCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        AProjectList = new Project()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return AProjectList;
        }

        public void InsertNewProject(StringValues id, StringValues name)
        {
            using (MySqlConnection MySqlConnection = GetConnection())
            {
                var InsertStr = "insert into project (id, name) values(@id,@name);";
                
                using (MySqlCommand InsertCommand = new MySqlCommand(InsertStr, MySqlConnection))
                {
                    InsertCommand.Parameters.AddWithValue("@id", id.ToString());
                    InsertCommand.Parameters.AddWithValue("@name", name.ToString());

                    MySqlConnection.Open();
                    InsertCommand.ExecuteNonQuery();
                    MySqlConnection.Close();
                }
                
            }
        }
    }
}
