using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class UsersDataServiceSQL : IUsersDataServiceSQL
    {

        string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolDB;Integrated Security=True; TrustServerCertificate=True;";

        /// <summary>
        /// This method lists all the users in the Database
        /// </summary>
        public List<UserBaseModelData> ListAllUsersDataService()
        {

            var users = new List<UserBaseModelData>();
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
                        users.Add(new UserBaseModelData((int)reader["UserID"], reader["Email"].ToString(), reader["PhoneNo"].ToString(), reader["Password"].ToString(), reader["Name"].ToString(), reader["Vorname"].ToString(), (bool)reader["IsDriver"]));
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
        public UserBaseModelData ListUserByEmailDataService(string email)
        {

            var users = new UserBaseModelData();
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
                        return new UserBaseModelData
                        (
                            (int)reader["UserID"],
                            email.ToLower(),
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
        public void AddUserDataService(UserBaseModelData user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Users(Email,PhoneNo,Password,Name,Vorname,IsDriver) VALUES('{user.Email.ToLower()}','{user.PhoneNo}','{user.Password}','{user.LastName}','{user.FirstName}',{Convert.ToInt32(user.IsDriver)})";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method replaces saved infos with new infos for a defined Driver ID
        /// </summary>
        public void EditUserDataService(UserBaseModelData user)
        {
            if (user != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = $"Update Users SET Email ='{user.Email.ToLower()}'," +
                        $"PhoneNo = '{user.PhoneNo}'," +
                        $"Password = '{user.Password}'," +
                        $"Name = '{user.LastName}'," +
                        $"Vorname = '{user.FirstName}'," +
                        $"IsDriver = {Convert.ToInt32(user.IsDriver)}" +
                        $"WHERE Email = '{user.Email}'";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// This method deletes/removes an existing driver from the drivers file
        /// </summary>
        public void DeleteUserDataService(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM Users WHERE Email = '{email.ToLower()}'";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}