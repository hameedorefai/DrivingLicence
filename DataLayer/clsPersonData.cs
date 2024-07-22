using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;

namespace DataLayer
{
    static public class clsPersonData
    {
        static public DataTable initializePersonDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NationalNo", typeof(string));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("SecondName", typeof(string));
            dataTable.Columns.Add("ThirdName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("DateOfBirth", typeof(DateTime));
            dataTable.Columns.Add("Gendor", typeof(byte));
            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("Phone", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NationalityCountryID", typeof(int));
            dataTable.Columns.Add("ImagePath", typeof(string));
            return dataTable;
        }

        static public bool GetPersonInformationByID(int PersonID, ref string firstName, ref string secondName, ref string thirdName,
            ref string lastName, ref string nationalNo, ref short gendor, ref DateTime dateOfBirth,
            ref string email, ref int NationalityCountryID, ref string address, ref string phone, ref string imagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    firstName = (string)reader["FirstName"];
                    secondName = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        thirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        thirdName = "";
                    }

                    lastName = (string)reader["LastName"];
                    nationalNo = (string)reader["NationalNo"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gendor = (short)reader["Gendor"];
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];


                    // Handling Email null
                    if (reader["Email"] != DBNull.Value)
                    {
                        email = (string)reader["Email"];
                    }
                    else
                    {
                        email = "";
                    }



                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        imagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        imagePath = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        static public bool GetPersonInformationByNationalNo(string nationalNo, ref int PersonID,ref string firstName, ref string secondName, ref string thirdName,
                     ref string lastName,  ref short gendor, ref DateTime dateOfBirth,
                     ref string email, ref int NationalityCountryID, ref string address, ref string phone, ref string imagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " SELECT * FROM People WHERE NationalNo = @nationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    firstName = (string)reader["FirstName"];
                    secondName = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        thirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        thirdName = "";
                    }

                    lastName = (string)reader["LastName"];
                    nationalNo = (string)reader["NationalNo"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gendor = (short)reader["Gendor"];
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];


                    // Handling Email null
                    if (reader["Email"] != DBNull.Value)
                    {
                        email = (string)reader["Email"];
                    }
                    else
                    {
                        email = "";
                    }



                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        imagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        imagePath = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        static public int AddNewPerson(DataTable Person)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[People]
                                ([NationalNo]
                                ,[FirstName]
                                ,[SecondName]
                                ,[ThirdName]
                                ,[LastName]
                                ,[DateOfBirth]
                                ,[Gendor]
                                ,[Address]
                                ,[Phone]
                                ,[Email]
                                ,[NationalityCountryID]
                                ,[ImagePath])
                            VALUES
                                (@NationalNo
                                ,@FirstName
                                ,@SecondName
                                ,@ThirdName
                                ,@LastName
                                ,@DateOfBirth
                                ,@Gendor
                                ,@Address
                                ,@Phone
                                ,@Email
                                ,@NationalityCountryID
                                ,@ImagePath)
                    SELECT SCOPE_IDENTITY();"; // Retrieve the inserted ID


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", (string)Person.Rows[0]["NationalNo"]);
            command.Parameters.AddWithValue("@FirstName", (string)Person.Rows[0]["FirstName"]);
            command.Parameters.AddWithValue("@SecondName", (string)Person.Rows[0]["SecondName"]);
            command.Parameters.AddWithValue("@ThirdName", (string)Person.Rows[0]["ThirdName"]);
            command.Parameters.AddWithValue("@LastName", (string)Person.Rows[0]["LastName"]);
            command.Parameters.AddWithValue("@DateOfBirth", (DateTime)Person.Rows[0]["DateOfBirth"]);
            command.Parameters.AddWithValue("@Gendor", (int)Person.Rows[0]["Gendor"]);
            command.Parameters.AddWithValue("@Address", (string)Person.Rows[0]["Address"]);
            command.Parameters.AddWithValue("@Phone", (string)Person.Rows[0]["Phone"]);
            command.Parameters.AddWithValue("@Email", (string)Person.Rows[0]["Email"]);
            command.Parameters.AddWithValue("@NationalityCountryID", (int)Person.Rows[0]["NationalityCountryID"]);
            command.Parameters.AddWithValue("@ImagePath", (string)Person.Rows[0]["ImagePath"]);


            try
            {
                connection.Open();
                // Execute the query and retrieve the inserted ID
                int insertedID = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();

                return insertedID;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }
        static public bool UpdatePerson(int PersonID, DataTable Person)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[People]
                                    SET
                                        [NationalNo] = @NationalNo,
                                        [FirstName] = @FirstName,
                                        [SecondName] = @SecondName,
                                        [ThirdName] = @ThirdName,
                                        [LastName] = @LastName,


                                        [Address] = @Address,
                                        [Phone] = @Phone,
                                        [Email] = @Email,
                                        [ImagePath] = @ImagePath
                                    WHERE
                                        [PersonID] = @PersonID;
                                    ";




            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", (string)Person.Rows[0]["NationalNo"]);
            command.Parameters.AddWithValue("@FirstName", (string)Person.Rows[0]["FirstName"]);
            command.Parameters.AddWithValue("@SecondName", (string)Person.Rows[0]["SecondName"]);
            command.Parameters.AddWithValue("@ThirdName", (string)Person.Rows[0]["ThirdName"]);
            command.Parameters.AddWithValue("@LastName", (string)Person.Rows[0]["LastName"]);
            //command.Parameters.AddWithValue("@DateOfBirth", (DateTime)Person.Rows[0]["DateOfBirth"]);
            //command.Parameters.AddWithValue("@Gendor", (byte)Person.Rows[0]["Gendor"]);
            command.Parameters.AddWithValue("@Address", (string)Person.Rows[0]["Address"]);
            command.Parameters.AddWithValue("@Phone", (string)Person.Rows[0]["Phone"]);
            command.Parameters.AddWithValue("@Email", (string)Person.Rows[0]["Email"]);
            //         command.Parameters.AddWithValue("@NationalityCountryID", (int)Person.Rows[0]["NationalityCountryID"]);
            command.Parameters.AddWithValue("@ImagePath", (string)Person.Rows[0]["ImagePath"]);
            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        public static int AddNewPerson(string FirstName, string SecondName,
   string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
   short Gendor, string Address, string Phone, string Email,
    int NationalityCountryID, string ImagePath)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName,LastName,NationalNo,
                                                   DateOfBirth,Gendor,Address,Phone, Email, NationalityCountryID,ImagePath)
                             VALUES (@FirstName, @SecondName,@ThirdName, @LastName, @NationalNo,
                                     @DateOfBirth,@Gendor,@Address,@Phone, @Email,@NationalityCountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
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

            return PersonID;
        }



        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName,
           string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
           short Gendor, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  People  
                            set FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName, 
                                NationalNo = @NationalNo,
                                DateOfBirth = @DateOfBirth,
                                Gendor=@Gendor,
                                Address = @Address,  
                                Phone = @Phone,
                                Email = @Email, 
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath =@ImagePath
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        static public DataTable GetAllPeople()
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dtPeople = initializePersonDataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int Counter = 0;
                while (reader.Read())
                {
                    DataRow dataRow = dtPeople.NewRow();

                    dataRow["FirstName"] = (string)reader["FirstName"];
                    dataRow["SecondName"] = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        dataRow["ThirdName"] = (string)reader["ThirdName"];
                    }
                    else
                    {
                        dataRow[""] = "";
                    }

                    dataRow["LastName"] = (string)reader["LastName"];
                    dataRow["NationalNo"] = (string)reader["NationalNo"];
                    dataRow["DateOfBirth"] = (DateTime)reader["DateOfBirth"];
                    dataRow["Gendor"] = (byte)reader["Gendor"];
                    dataRow["Address"] = (string)reader["Address"];
                    dataRow["Phone"] = (string)reader["Phone"];


                    // Handling Email null
                    if (reader["Email"] != DBNull.Value)
                    {
                        dataRow["Email"] = (string)reader["Email"];
                    }
                    else
                    {
                        dataRow[""] = "";
                    }



                    dataRow["NationalityCountryID"] = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        dataRow["ImagePath"] = (string)reader["ImagePath"];
                    }
                    else
                    {
                        dataRow["ThirdName"] = "";
                    }
                    Counter++;
                    dtPeople.Rows.Add(dataRow);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return dtPeople;
        }

        static public bool DeletePerson(int PersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE People WHERE @PersonID = PersonID";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        static public bool IsPersonExist(string nationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " SELECT Found = 1 FROM People WHERE NationalNo = @nationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                connection.Open();
                int AffectedRows = (int)command.ExecuteScalar();

                if (AffectedRows > 0)
                {
                    isFound = true;

                }
                else
                {
                    isFound = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        static public bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " SELECT Found = 1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                int AffectedRows = (int)command.ExecuteScalar();

                if (AffectedRows > 0)
                {
                    isFound = true;

                }
                else
                {
                    isFound = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    }
}




