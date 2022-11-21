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
                        users.Add(new NewUserBaseModelData(reader["UserID"].ToString(), reader["Email"].ToString(), reader["PhoneNo"].ToString(), reader["Password"].ToString(), reader["Name"].ToString(), reader["Vorname"].ToString(), (bool)reader["IsDriver"]));
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
        public List<NewUserBaseModelData> ListUserByEmailDataService(string email)
        {

            var users = new List<NewUserBaseModelData>();
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
                        var user = new NewUserBaseModelData();
                        user.Email = email;
                        users.Add(user);
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
        /// This method adds a new User to the Database
        /// </summary>
        public void AddUserDataService(NewUserBaseModelData user)
        {
            //var newUser = new List<NewUserBaseModelData>();
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

        }


        /// <summary>
        /// This method deletes/removes an existing driver from the drivers file
        /// </summary>
        public void DeleteUserDataService(NewUserBaseModelData user)
        {

        }
    }
}