using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;

namespace Bank_Managmenet_Program__BMP_
{
    class LoginClass
    {
        public void LoginHere()
        {
            string user = "";
            string connectionString;
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            SqlConnection connect;
            connect = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT LoginName, LoginPassword FROM Customers", connect);
            List<string> users = new List<string> { };
            List<int> passwords = new List<int> { };

            connect.Open();

            int loginAttempts = 0;

            //Simple iteration upto three times
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
                    Console.WriteLine("We can see you do not have a user associated with this bank, would you like to create one?" +
                        "\n(press \"Y\" for yes,\"N\" to enter your login information again or \"Esc\" to exit the application)");
                    ConsoleKeyInfo keyInfo;
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        UserCreation();
                        break;
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

            //Display the result
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
            string connectionString;
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            SqlConnection connect;
            connect = new SqlConnection(connectionString);
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(CustomerID) FROM Customers", connect);
            connect.Open();
            int NewCustomerID = (Int32)cmd1.ExecuteScalar();
            Console.WriteLine("Please enter your first name");
            string Firstname = Console.ReadLine();
            Console.WriteLine("Please enter your last name");
            string LastName = Console.ReadLine();
            Console.WriteLine("Please enter the name you wish to login as");
            string LoginName = Console.ReadLine();
            Console.WriteLine("Please enter your desired login password");
            int LoginPassword = int.Parse(Console.ReadLine());
            SqlCommand cmd2 = new SqlCommand("SELECT MAX(BalanceID) FROM Customers", connect);
            int NewBalanceID = (Int32)cmd2.ExecuteScalar();
            SqlCommand cmd3 = new SqlCommand($"INSERT INTO Customers(CustomerID,FirstName,LastName,LoginName,LoginPassword,BalanceID) VALUES({NewCustomerID},{Firstname},{LastName},{LoginName},{LoginPassword},{NewBalanceID})", connect);
            SqlCommand cmd4 = new SqlCommand($"INSERT INTO Balance(BalanceID,Balance,Currency,Stocks,CustomerName,LoanStatus,LoanAmount) VALUES({NewBalanceID},0,'DKK','NONE','{Firstname}" + " " + $"{LastName},0,0')", connect);
            cmd3.ExecuteScalar();
            cmd4.ExecuteScalar();
            Console.Clear();
            Console.WriteLine("User succesfully created");
        }
    }
}
