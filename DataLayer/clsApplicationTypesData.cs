using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    static public class clsApplicationTypesData
    {

        static public bool GetApplicationTypeInfoByID(int ApplicationTypeID, ref string Title, ref float Fees)
        {
                bool isFound = false;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = " SELECT * FROM ApplicationTypes WHERE " +
                               "ApplicationTypeID = @ApplicationTypeID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        Title = (string)reader["ApplicationTypeTitle"];
                        Fees = Convert.ToSingle((double)reader["ApplicationFees"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in clsApplicationTypes: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return isFound;
        }
        static public bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, double ApplicationTypeFees)
        {
            bool UpdatedSuccessfuly = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[ApplicationTypes] " +
                           "SET [ApplicationTypeTitle] = @ApplicationTypeTitle," +
                           "ApplicationTypeFees = @ApplicationTypeFees" +
                           "WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationTypeFees", ApplicationTypeFees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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
                Console.WriteLine("Error in clsApplicationTypes: " + ex.Message);
                UpdatedSuccessfuly = false;
            }
            finally
            {
                connection.Close();
            }
            return UpdatedSuccessfuly;


        }
        ////static public bool EditApplicationTypeFees(int ApplicationTypeID, double ApplicationTypeFees)
        ////{
        ////    bool UpdatedSuccessfuly = false;

        ////    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        ////    string query = "UPDATE [dbo].[ApplicationTypes] " +
        ////                   "SET [ApplicationTypeFees] = @ApplicationTypeFees" +
        ////                   "WHERE ApplicationTypeID = @ApplicationTypeID";
        ////    SqlCommand command = new SqlCommand(query, connection);
        ////    command.Parameters.AddWithValue("@ApplicationTypeFees", ApplicationTypeFees);
        ////    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

        ////    try
        ////    {
        ////        connection.Open();
        ////        int rowsAffected = command.ExecuteNonQuery();


        ////        if (rowsAffected > 0)
        ////        {
        ////            UpdatedSuccessfuly = true;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Console.WriteLine("Error in clsApplicationTypes: " + ex.Message);
        ////        UpdatedSuccessfuly = false;
        ////    }
        ////    finally
        ////    {
        ////        connection.Close();
        ////    }
        ////    return UpdatedSuccessfuly;


        ////}
        public static DataTable GetAllApplicationTypes()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes order by ApplicationTypeTitle";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        public static int AddNewApplicationType(string Title, float Fees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            Values (@Title,@Fees)
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationTypeID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return ApplicationTypeID;

        }


    }
}