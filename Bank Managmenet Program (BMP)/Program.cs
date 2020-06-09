using System;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;


namespace Bank_Managmenet_Program__BMP_
{
    class Program
    {
        //TO DO LIST
        //1. Create a simple login system and "main menu" - DONE
        //2. Create classes for each "function" of main program - PARTLY DONE
        //3. Establish SQL connection, and understand it better - DONE
        //4. Use SQL to store and retrieve data - IN PROGRESS
        //5. Quality of life improvements - NOT STARTED
        //6. Have a finished product (sort of)

        static void Main(string[] args)
        {
            //Class references
            WithdrawAndDeposit WAD = new WithdrawAndDeposit();
            Balance Bal = new Balance();
            LoanStatusOrTakeALoan LSOTAL = new LoanStatusOrTakeALoan();

            //Program variables
            string user = "";
            List<string> users = new List<string> { "Andreas", "Daniel", "Emil", "admin" };
            List<string> passwords = new List<string> { "1234", "4321", "54321", "admin" };

            //SQL commands and variables
            string connectionString;
            SqlConnection connect;
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            connect = new SqlConnection(connectionString);


            //Actual program

            int loginAttempts = 0;

            //Simple iteration upto three times
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Enter username");
                user = Console.ReadLine();
                Console.WriteLine("Enter password");
                string password = Console.ReadLine();

                if (!users.Contains(user) || !passwords.Contains(password))
                    loginAttempts++;
                else
                    break;
            }

            //Display the result
            if (loginAttempts > 2)
            {
                Console.WriteLine("Login failure");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Login successful");
            }
                Console.ReadKey();

            SelectionInterface();


            void SelectionInterface()
            {
                ConsoleKeyInfo selection;
                do
                {
                    Console.Clear();
                    Console.WriteLine("\nWelcome " + user + "\n\nPlease enter the number of the action you would like to take\n" +
                                                               "------------------------------------------------------------");
                    Console.WriteLine("1. Deposit cash \n2. Withdraw cash \n3. Check your balance \n4. Check the status of your loan (if you got a loan)\nEsc. Close the application\n" +
                        "------------------------------------------------------------");
                    selection = Console.ReadKey();
                    Console.Clear();
                    if (selection.Key == ConsoleKey.D1)
                    {
                        //Deposit cash
                        WAD.Deposit(user);
                    }
                    else if (selection.Key == ConsoleKey.D2)
                    {
                        //Withdraw cash
                        WAD.Withdraw(user);
                    }
                    else if (selection.Key == ConsoleKey.D3)
                    {
                        //Balance check
                        Bal.BalanceCheck(user);
                    }
                    else if (selection.Key == ConsoleKey.D4)
                    {
                        //Loan Status
                        LSOTAL.LoanStatus(user);
                    }
                    else
                    {
                        Console.Clear();
                    }
                } while (selection.Key != ConsoleKey.Escape);
                connect.Close();
            }
        }
    }
}
