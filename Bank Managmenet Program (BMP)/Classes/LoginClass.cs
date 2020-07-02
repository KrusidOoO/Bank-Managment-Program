using Bank_Managmenet_Program__BMP_.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;

namespace Bank_Managmenet_Program__BMP_
{
    class LoginClass
    {
        UserCreation UC = new UserCreation();
        public string user = "";
        public int password;
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
            Console.WriteLine("Do you wish to login to an existing account, or create a new one? \n(Press 1 to login to an exisiting account or 2 to create a new one)");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.D1)
            {
                Console.Clear();
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
                    password = Convert.ToInt32(Console.ReadLine());
                    if (users.Contains(user) || passwords.Contains(password))
                        break;

                }


                if (loginAttempts > 2)
                {
                    Console.WriteLine("Login failure, exiting application");
                    Thread.Sleep(500);
                    Environment.Exit(0);
                }
                else
                    Console.WriteLine("Login successful");
            }
            else if (keyInfo.Key == ConsoleKey.D2)
            {
                UC.userCreation();
            }
        }
    }
}
