using System;
using System.Data.SqlClient;
using System.Threading;

namespace Bank_Managmenet_Program__BMP_.Classes
{
    class UserCreation
    {
        public void userCreation()
        {
            Console.Clear();
            string connectionString;
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            SqlConnection connect;
            connect = new SqlConnection(connectionString);
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(CustomerID) FROM Customers", connect);
            connect.Open();
            int NewCustomerID = (Int32)cmd1.ExecuteScalar() + 1;
            Console.WriteLine("\nPlease enter your first name");
            string Firstname = Console.ReadLine();
            Console.WriteLine("\nPlease enter your last name");
            string LastName = Console.ReadLine();
            Console.WriteLine("\nPlease enter the name you wish to login as");
            string LoginName = Console.ReadLine();
            Console.WriteLine("\nPlease enter your desired login password");
            int LoginPassword = int.Parse(Console.ReadLine());
            SqlCommand cmd2 = new SqlCommand("SELECT MAX(BalanceID) FROM Customers", connect);
            int NewBalanceID = (Int32)cmd2.ExecuteScalar() + 1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String CustomerQuery = "INSERT INTO Customers(CustomerID,FirstName,LastName,LoginName,LoginPassword,BalanceID) " +
                    "VALUES (@CustomerID,@FirstName,@LastName, @LoginName,@LoginPassword,@BalanceID)";
                String BalanceQuery = "INSERT INTO Balance(BalanceID,Balance,Currency,Stocks,CustomerName,LoanStatus,LoanAmount) " +
                    "VALUES(@BalanceID,@Balance,@Currency,@Stocks,@CustomerName,@LoanStatus,@LoanAmount)";
                using (SqlCommand command = new SqlCommand(CustomerQuery, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", NewCustomerID);
                    command.Parameters.AddWithValue("@FirstName", Firstname);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@LoginName", LoginName);
                    command.Parameters.AddWithValue("@LoginPassword", LoginPassword);
                    command.Parameters.AddWithValue("@BalanceID", NewBalanceID);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                    command.Dispose();
                }
                using (SqlCommand cmd = new SqlCommand(BalanceQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@BalanceID", NewBalanceID);
                    cmd.Parameters.AddWithValue("@Balance", 0);
                    cmd.Parameters.AddWithValue("@Currency", "DKK");
                    cmd.Parameters.AddWithValue("@Stocks", "NONE");
                    cmd.Parameters.AddWithValue("@CustomerName", Firstname + " " + LastName);
                    cmd.Parameters.AddWithValue("@LoanStatus", 0);
                    cmd.Parameters.AddWithValue("@LoanAmount", 0);
                    int secondresult = cmd.ExecuteNonQuery();
                    if (secondresult < 0)
                        Console.WriteLine("Error inserting data into Database!");
                    cmd.Dispose();
                }

            }
            cmd1.Dispose();
            cmd2.Dispose();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------\n" +
                              "User succesfully created");
            Thread.Sleep(2000);
            Console.Clear();



        }
    }
}
