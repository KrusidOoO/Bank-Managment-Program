using System;
using System.Data.SqlClient;

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
