using System;
using System.Data.SqlClient;
using System.Transactions;

namespace TransactionScopeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string connectionString = "YourConnectionString";

                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();

                        string updatPersonPhoneQuery = "Update [AdventureWorks2012].[Person].[PersonPhone] SET PhoneNumber = '697-555-0145' WHERE BusinessEntityID = 1";
                        SqlCommand updatePersonPhoneSqlCommand = new SqlCommand(updatPersonPhoneQuery, sqlConnection);

                        updatePersonPhoneSqlCommand.ExecuteNonQuery();

                        //throw new TransactionAbortedException("error");

                        string updatePersonQuery = $"UPDATE [AdventureWorks2012].[Person].[Person] SET ModifiedDate = '{DateTime.UtcNow}' WHERE BusinessEntityID = 1";
                        SqlCommand updatePersonSqlCommand = new SqlCommand(updatePersonQuery, sqlConnection);

                        updatePersonSqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }

                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
