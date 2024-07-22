using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    static public class clsTestTypesData
    {
        static public bool GetTestTypeInfoByID(int TestTypeID, ref string TestTypeTitle,
                                 ref string TestTypeDescription, ref float TestTypeFees)
        {

            {
                bool isFound = false;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = " SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;
                        TestTypeTitle = (string)reader["TestTypeTitle"];
                        TestTypeFees = (float)reader["TestTypeFees"];
                        TestTypeDescription = (string)reader["TestTypeDescription"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in clsTestTypesData: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return isFound;
            }




        }
        static public DataTable GetAllTestTypes()
        {
            {

                DataTable dataTable = new DataTable();

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = " SELECT * FROM TestTypes order by TestTypeID";

                SqlCommand command = new SqlCommand(query, connection);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dataTable.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in clsTestTypesData: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return dataTable;
            }
        }

        static public bool EditTestTypeTitle(int TestTypeID, string TestTypeTitle)
        {
            bool UpdatedSuccessfuly = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[TestTypes] " +
                           "SET [TestTypeTitle] = @TestTypeTitle" +
                           "WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
                Console.WriteLine("Error in clsTestTypesData: " + ex.Message);
                UpdatedSuccessfuly = false;
            }
            finally
            {
                connection.Close();
            }
            return UpdatedSuccessfuly;


        }
        static public bool EditTestTypeFees(int TestTypeID, double TestTypeFees)
        {
            bool UpdatedSuccessfuly = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[TestTypes] " +
                           "SET [TestTypeFees] = @TestTypeFees" +
                           "WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
                Console.WriteLine("Error in clsTestTypesData: " + ex.Message);
                UpdatedSuccessfuly = false;
            }
            finally
            {
                connection.Close();
            }
            return UpdatedSuccessfuly;


        }
        static public bool EditTestTypeDescription(int TestTypeID, string TestTypeDescription)
        {
            bool UpdatedSuccessfuly = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE [dbo].[TestTypes] " +
                           "SET [TestTypeDescription] = @TestTypeDescription" +
                           "WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
                Console.WriteLine("Error in clsTestTypesData: " + ex.Message);
                UpdatedSuccessfuly = false;
            }
            finally
            {
                connection.Close();
            }
            return UpdatedSuccessfuly;


        }



        public static bool UpdateTestType(int TestTypeID, string Title, string Description, float Fees)
        {
            bool UpdatedSuccessfuly = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Update  TestTypes  
                            set TestTypeTitle = @Title,
                                TestTypeDescription=@Description,
                                TestTypeFees = @Fees
                                where TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);

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
                Console.WriteLine("Error in clsTestTypesData: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return UpdatedSuccessfuly;
        }





        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int TestTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeTitle,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@ApplicationFees)
                            where TestTypeID = @TestTypeID;
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
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


            return TestTypeID;

        }




    }
}
