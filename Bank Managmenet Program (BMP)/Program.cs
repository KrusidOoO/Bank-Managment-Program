using System;
using System.Data.SqlClient;
using System.Linq;


namespace Bank_Managmenet_Program__BMP_
{
    class Program
    {
        static void Main(string[] args)
        {
            //Program variables
            int userNum = 0;
            bool usercheck = false;
            bool PassCheck = false;
            string user = "";
            string passWord;
            string[] users = { "Andreas", "Daniel", "Emil", "admin" };
            string[] userPass = { "1234", "4321", "54321", "admin" };

            //SQL commands and variables
            string connectionString;
            SqlConnection connect;
            SqlCommand command;
            SqlDataReader reader;
            String sql, Output = "";
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            connect = new SqlConnection(connectionString);
            connect.Open();

            //Actual program

            WelcomeInterface();

            SelectionInterface();


            void WelcomeInterface()
            {
                Console.WriteLine("Welcome to Bank of Pride. \n\nPlease enter your login name");
                while (!usercheck)
                {
                    user = Console.ReadLine();
                    userNum = Array.IndexOf(users, user);
                    if (user == users.ElementAt(userNum))
                    {
                        usercheck = true;
                    }

                }
                Console.WriteLine("\nLogin name is correct\n\nPlease enter your password");
                while (!PassCheck)
                {
                    passWord = Console.ReadLine();
                    if (passWord == userPass[userNum])
                    {
                        PassCheck = true;
                    }
                }
                Console.WriteLine("Pasword is correct, press any key to continue");
                Console.ReadKey();
            }

            void SelectionInterface()
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                ConsoleKeyInfo selection;
                do
                {
                    Console.Clear();
                    Console.WriteLine("\nWelcome " + user + "\n\nPlease enter the number of the action you would like to take\n" +
                                                               "------------------------------------------------------------");
                    Console.WriteLine("1. Deposit cash \n2. Withdraw cash \n3. Check your balance \n4. Take a loan \n5. Check the status of your loan (if you got a loan)\nEsc. Close the application\n" +
                        "------------------------------------------------------------");
                    selection = Console.ReadKey();
                    Console.Clear();
                    if (selection.Key == ConsoleKey.D1)
                    {
                        //Deposit cash
                        Console.Clear();
                        Console.WriteLine($"{user} please enter the amount you want to deposit: ");
                        int CashInput = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("------------------------------------------------------------\nHere is your new balance:\n");
                        sql = $"UPDATE Balance SET Balance=Balance+{CashInput} WHERE CustomerName LIKE '{user}%'";

                        command = new SqlCommand(sql, connect);
                        adapter.UpdateCommand = new SqlCommand(sql, connect);
                        adapter.UpdateCommand.ExecuteNonQuery();

                        sql = $"SELECT * FROM Balance WHERE CustomerName LIKE '{user}%'";
                        command = new SqlCommand(sql, connect);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Output = reader.GetValue(4) + " - " + reader.GetValue(1) + " " + reader.GetValue(2) + "\n";
                        }


                        Console.WriteLine(Output);
                        reader.Close();
                        command.Dispose();
                        Console.ReadKey();
                    }
                    else if (selection.Key == ConsoleKey.D2)
                    {
                        //Withdraw cash
                        Console.Clear();
                        Console.WriteLine($"{user} please enter the amount you want to withdraw: ");
                        int CashWithdraw = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("------------------------------------------------------------\nHere is your new balance:\n");
                        sql = $"UPDATE Balance SET Balance=Balance-{CashWithdraw} WHERE CustomerName LIKE '{user}%'";

                        command = new SqlCommand(sql, connect);
                        adapter.UpdateCommand = new SqlCommand(sql, connect);
                        adapter.UpdateCommand.ExecuteNonQuery();

                        sql = $"SELECT * FROM Balance WHERE CustomerName LIKE '{user}%'";
                        command = new SqlCommand(sql, connect);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Output = reader.GetValue(4) + " - " + reader.GetValue(1) + " " + reader.GetValue(2) + "\n";
                        }

                        
                        Console.WriteLine(Output);
                        reader.Close();
                        command.Dispose();
                        Console.ReadKey();
                    }
                    else if (selection.Key == ConsoleKey.D3)
                    {
                        //Balance check
                        Console.Clear();
                        sql = $"Select * FROM Balance WHERE CustomerName LIKE '{user}%'";
                        command = new SqlCommand(sql, connect);
                        reader = command.ExecuteReader();
                        Console.WriteLine("------------------------------------------------------------\nHere is your balance:\n");
                        while (reader.Read())
                        {
                            Output = reader.GetValue(4) + " - " + reader.GetValue(1) + " " + reader.GetValue(2) + "\n";
                        }

                        Console.WriteLine(Output+ "\n------------------------------------------------------------");
                        reader.Close();
                        command.Dispose();
                        Console.ReadKey();
                    }
                    else if (selection.Key == ConsoleKey.D4)
                    {
                        //Create a loan
                    }
                    else if (selection.Key == ConsoleKey.D5)
                    {
                        int trueOrNot;
                        //Loan Status
                        sql = $"SELECT LoanStatus FROM Balance WHERE CustomerName LIKE '{user}%'";
                        command = new SqlCommand(sql, connect);
                        reader = command.ExecuteReader();
                        trueOrNot =Convert.ToInt32(reader.GetValue(5));
                        if (trueOrNot==0)
                        {
                            Console.WriteLine("There is a loan associated with your account.");
                            Console.ReadKey();
                        }
                        else if(trueOrNot==1)
                        {
                            Console.WriteLine("There is no loan associated with your account.");
                            Console.ReadKey();
                        }
                    }
                    else
                    {

                    }
                } while (selection.Key != ConsoleKey.Escape);
                connect.Close();
            }
        }
    }
}
