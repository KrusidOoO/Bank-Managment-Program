using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;

namespace Bank_Managmenet_Program__BMP_
{
    class LoginClass
    {
        public string user = "";
        public void LoginHere()
        {
            string connectionString;
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            SqlConnection connect;
            connect = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT LoginName, LoginPassword FROM Customers", connect);
            List<string> users = new List<string> { };
            List<int> passwords = new List<int> { };
            connect.Open();
            int loginAttempts = 0;
            for (int i = 0; i < 3; i++)
            {
                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        users.Add(reader.GetValue(0).ToString());
                        passwords.Add(Convert.ToInt32(reader.GetValue(1)));
                    }
                }
                Console.WriteLine("Enter username");
                user = Console.ReadLine();
                Console.WriteLine("Enter password");
                int password = Convert.ToInt32(Console.ReadLine());
                if (!users.Contains(user) || !passwords.Contains(password))
                {
                    Console.WriteLine("\n\nWe can see you do not have a user associated with this bank, would you like to create one?" +
                        "\n(press \"Y\" for yes,\"N\" to enter your login information again or \"Esc\" to exit the application)");
                    ConsoleKeyInfo keyInfo;
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        UserCreation();
                    }
                    else if (keyInfo.Key == ConsoleKey.N)
                    {
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        Console.WriteLine("We hope to see you again!");
                        Thread.Sleep(500);
                        Environment.Exit(0);
                    }
                }
                else if (users.Contains(user) || passwords.Contains(password))
                    break;
            }
            if (loginAttempts > 2)
            {
                Console.WriteLine("Login failure, exiting application");
                Thread.Sleep(500);
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Login successful");
            }
        }
        protected void UserCreation()
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
                }
            }
            Console.Clear();
            Console.WriteLine("User succesfully created");
            Thread.Sleep(500);
        }
    }
}
