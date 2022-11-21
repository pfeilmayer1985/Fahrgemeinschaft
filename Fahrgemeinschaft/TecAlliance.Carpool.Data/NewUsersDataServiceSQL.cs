using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class NewUsersDataServiceSQL : INewUsersDataServiceSQL
    {

        string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolDB;Integrated Security=True; TrustServerCertificate=True;";


        /// <summary>
        /// This method lists all the users in the Database
        /// </summary>
        public List<NewUserBaseModelData> ListAllUsersDataService()
        {

            var users = new List<NewUserBaseModelData>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        users.Add(new NewUserBaseModelData((int)reader["UserID"], reader["Email"].ToString(), reader["PhoneNo"].ToString(), reader["Password"].ToString(), reader["Name"].ToString(), reader["Vorname"].ToString(), (bool)reader["IsDriver"]));
                    }

                }
                finally
                {
                    reader.Close();
                }
            }
            return users;
        }

        /// <summary>
        /// This method lists one selected user from the Database based on email address
        /// </summary>
        public NewUserBaseModelData ListUserByEmailDataService(string email)
        {

            var users = new NewUserBaseModelData();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Users WHERE Email = @Email";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters["@Email"].Value = email;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        return new NewUserBaseModelData
                        (
                            (int)reader["UserID"],
                            email,
                            reader["PhoneNo"].ToString(),
                            reader["Password"].ToString(),
                            reader["Vorname"].ToString(),
                            reader["Name"].ToString(),
                            (bool)reader["IsDriver"]
                        );
                    }

                }
                finally
                {
                    reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// This method adds a new User to the Database
        /// </summary>
        public void AddUserDataService(NewUserBaseModelData user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Users(Email,PhoneNo,Password,Name,Vorname,IsDriver) VALUES('{user.Email}','{user.PhoneNo}','{user.Password}','{user.LastName}','{user.FirstName}',{Convert.ToInt32(user.IsDriver)})";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method replaces saved infos with new infos for a defined Driver ID
        /// </summary>
        public void EditUserDataService(NewUserBaseModelData user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"Update Users SET(Email,PhoneNo,Password,Name,Vorname,IsDriver) VALUES('{user.Email}','{user.PhoneNo}','{user.Password}','{user.LastName}','{user.FirstName}',{Convert.ToInt32(user.IsDriver)}) WHERE Email = '{user.Email}'";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// This method deletes/removes an existing driver from the drivers file
        /// </summary>
        public void DeleteUserDataService(NewUserBaseModelData user)
        {

        }
    }
}