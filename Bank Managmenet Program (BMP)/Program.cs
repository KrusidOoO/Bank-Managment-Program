using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Bank_Managmenet_Program__BMP_
{
    class Program
    {
        //TO DO LIST
        //1. Create a simple login system and "main menu" - DONE
        //2. Create classes for each "function" of main program - PARTLY DONE (Not currently knowing how many functions I want in the program)
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
            LoginClass loginClass = new LoginClass();

            //Program variables
            string user = "";
            //SQL commands and variables
            string connectionString;
            SqlConnection connect;
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            connect = new SqlConnection(connectionString);


            //Actual program

            loginClass.LoginHere();
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
