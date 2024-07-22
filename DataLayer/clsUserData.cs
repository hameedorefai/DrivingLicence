using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;


namespace DataLayer
{
    public class clsUserData
    {
        static public DataTable initializeUserDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UserID", typeof(int));
            dataTable.Columns.Add("PersonID", typeof(int));
            dataTable.Columns.Add("UserName", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("IsActive", typeof(byte));
            return dataTable;
        }

        static public bool GetUserInfoByUserID(int UserID,ref int PersonID,
            ref string username, ref string Password, ref bool isActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                   // UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    username = (string)reader["UserName"];
                    isActive = (bool)reader["IsActive"];
                    Password = (string)reader["Password"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message) ;
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool GetUserInfoByPersonID(int PersonID, ref int userID,
                ref string username, ref string Password, ref bool isActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from Users where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    userID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    username = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool GetUserInfoByUsernameAndPassword(ref string username, 
            ref string password,ref int PersonID, ref int userID,ref bool isActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from Users where UserName = @username and Password = @password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    userID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    username = (string)reader["UserName"];
                    isActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool isActive(string username)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select IsActive from Users where username = @username ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = (bool)reader["IsActive"];


                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool isActive(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select IsActive from Users where UserID = @UserID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = (bool)reader["IsActive"];


                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool Activate(string username)
        {
            bool ActivationStatus = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[Users] " +
                           "SET [IsActive] = 1" +
                           "WHERE UserName = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {

                    ActivationStatus = true;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                ActivationStatus = false;
            }
            finally
            {
                connection.Close();
            }
            return ActivationStatus;
        }
        static public bool Activate(int UserID)
        {
            bool ActivationStatus = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[Users] " +
                           "SET [IsActive] = 1 " +
                           "WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {

                    ActivationStatus = true;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                ActivationStatus = false;
            }
            finally
            {
                connection.Close();
            }
            return ActivationStatus;
        }
        static public bool DeActivate(string username)
        {
            bool ActivationStatus = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[Users] " +
                           "SET [IsActive] = 0 " +
                           "WHERE UserName = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {

                    ActivationStatus = true;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                ActivationStatus = false;
            }
            finally
            {
                connection.Close();
            }
            return ActivationStatus;
        }
        static public bool DeActivate(int UserID)
        {
            bool ActivationStatus = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[Users] " +
                           "SET [IsActive] = 0 " +
                           "WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {

                    ActivationStatus = true;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                ActivationStatus = false;
            }
            finally
            {
                connection.Close();
            }
            return ActivationStatus;
        }
        static public bool UpdatePassword(int UserID,string Password)
        {
            bool UpdatedSuccessfuly = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[Users] " +
                           "SET [Password] = @Password" +
                           "WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {

                    UpdatedSuccessfuly = true;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                UpdatedSuccessfuly = false;
            }
            finally
            {
                connection.Close();
            }
            return UpdatedSuccessfuly;
        }
        static public bool DeleteUser(int UserID)
        {
            bool DeletedSuccessfuly = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM [dbo].[Users] " +
                           "WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {

                    DeletedSuccessfuly = true;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                DeletedSuccessfuly = false;
            }
            finally
            {
                connection.Close();
            }
            return DeletedSuccessfuly;
        }
        static public bool CheckUsernameAndPassword(string username, string Password)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select top(1) from Users where username = @username and Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool CheckUserIDAndPassword(int userID, string Password)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT COUNT(*) from Users where userID = @userID and Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public int AddNewUser(DataRow User)
        {
            int newUserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string qurey = @"INSERT INTO [dbo].[Users]
                                   ([PersonID]
                                   ,[UserName]
                                   ,[Password]
                                   ,[IsActive])
                                   VALUES
                                   (@PersonID, @UserName, @Password, @IsActive)";

            SqlCommand command = new SqlCommand(qurey, connection);

            command.Parameters.AddWithValue("@PersonID", (int)User["PersonID"]);
            command.Parameters.AddWithValue("@UserName", (string)User["UserName"]);
            command.Parameters.AddWithValue("@Password", (string)User["Password"]);
            command.Parameters.AddWithValue("@IsActive", (bool)User["IsActive"]);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null)
                {
                    newUserID = (int)result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUser: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return newUserID;
        }
        static public int GetUserIdByPersonID(int PersonID)
        {
            int userID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select UserID from Users where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    userID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return userID;
        }

        static public DataTable GetAllUsers()
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dtUsers = initializeUserDataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int Counter = 0;
                while (reader.Read())
                {
                    DataRow dataRow = dtUsers.NewRow();

                    dataRow["UserID"] = (string)reader["UserID"];
                    dataRow["PersonID"] = (string)reader["PersonID"];
                    dataRow["UserName"] = (string)reader["UserName"];
                    dataRow["Password"] = (string)reader["Password"];
                    dataRow["IsActive"] = (string)reader["IsActive"];

                    Counter++;
                    dtUsers.Rows.Add(dataRow);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return dtUsers;
        }



        static public bool IsUserExist(string username)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select Found = 1 from Users where username = @username ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                int RowsAffectted = (int)command.ExecuteScalar();

                if (RowsAffectted > 0)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select Found = 1 from Users where UserID = @UserID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                int RowsAffectted = (int)command.ExecuteScalar();

                if (RowsAffectted > 0)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select Found = 1 from Users where PersonID = @PersonID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                int RowsAffectted = (int)command.ExecuteScalar();

                if (RowsAffectted > 0)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUserData: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public int AddNewUser(int PersonID, string Username, string Password, bool IsActive)
        {
            int newUserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string qurey = @"INSERT INTO [dbo].[Users]
                                   ([PersonID]
                                   ,[UserName]
                                   ,[Password]
                                   ,[IsActive])
                                   VALUES
                                   (@PersonID, @UserName, @Password, @IsActive)";

            SqlCommand command = new SqlCommand(qurey, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", Username);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    newUserID = (int)result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUser: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return newUserID;
        }
        static public bool UpdateUser(int UserID,int PersonID, string UserName, string Password, bool IsActive)
        {
            bool UdpatedSuccefully = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Users
                        SET [Password] = @Password,
                            [UserName] = @UserName
                            [IsActive] = @IsActive
                            [PersonID] = @PersonID
                        WHERE [UserID] = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    UdpatedSuccefully = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in clsUser: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return UdpatedSuccefully;
        }

    }
}
