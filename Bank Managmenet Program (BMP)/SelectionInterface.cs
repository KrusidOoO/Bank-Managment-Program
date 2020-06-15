using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;

namespace Bank_Managmenet_Program__BMP_
{
    class SelectionInterface
    {
        public void Selection(string user)
        {
            WithdrawAndDeposit WAD = new WithdrawAndDeposit();
            Balance Bal = new Balance();
            LoanStatusOrTakeALoan LSOTAL = new LoanStatusOrTakeALoan();
            LoginClass loginClass = new LoginClass();
            string connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            SqlConnection connect = new SqlConnection(connectionString);
            ConsoleKeyInfo selection;
            do
            {
                Console.Clear();
                Console.WriteLine("\nWelcome " + user + "\n\n" +
                                  "Please enter the number of the action you would like to take\n" +
                                  "------------------------------------------------------------");
                Console.WriteLine("1. Deposit cash \n" +
                                  "2. Withdraw cash \n" +
                                  "3. Check your balance \n" +
                                  "4. Check the status of your loan (if you got a loan)\n" +
                                  "Esc. Close the application\n" +
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
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------\n\n" +
                              "We hope to see you again!\n\n" +
                              "------------------------------------------------------------");
            Thread.Sleep(1000);
            connect.Close();
        }
    }
}
