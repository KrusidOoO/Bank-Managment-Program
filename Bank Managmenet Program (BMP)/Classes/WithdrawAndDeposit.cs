using System;
using System.Data.SqlClient;

namespace Bank_Managmenet_Program__BMP_
{
    class WithdrawAndDeposit
    {

        public void Deposit(string user)
        {
            bool didDeposit = false;
            do
            {
                Program Main = new Program();
                string connectionString;
                SqlConnection connect;
                SqlCommand command;
                SqlDataReader reader;
                SqlDataAdapter adapter = new SqlDataAdapter();
                String sql, Output = "";
                connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
                connect = new SqlConnection(connectionString);
                connect.Open();
                Console.Clear();
                Console.WriteLine($"{user}, please enter the amount you want to deposit: ");
                int CashInput = Convert.ToInt32(Console.ReadLine());
                if (CashInput > 2500)
                {
                    Console.WriteLine("\nError! You can only deposit 2000 of your currency");
                    didDeposit = false;
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("------------------------------------------------------------\nHere is your new balance:\n");
                    sql = $"UPDATE Balance SET Balance=Balance+{CashInput} WHERE CustomerName LIKE '{user}%'";
                    adapter.UpdateCommand = new SqlCommand(sql, connect);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    sql = $"SELECT * FROM Balance WHERE CustomerName LIKE '{user}%'";
                    command = new SqlCommand(sql, connect);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Output = reader.GetValue(4) + " - " + reader.GetValue(1) + " " + reader.GetValue(2) + "\n";
                    }
                    didDeposit = true;
                    Console.WriteLine(Output + "\nPress any key to return to the main menu");
                    reader.Close();
                    command.Dispose();
                    Console.ReadKey();
                }
            } while (!didDeposit);
        }
        public void Withdraw(string user)
        {
            bool didWithdraw = false;
            do
            {
                Program Main = new Program();
                string connectionString;
                SqlConnection connect;
                SqlCommand command;
                SqlDataReader reader;
                SqlDataAdapter adapter = new SqlDataAdapter();
                String sql, Output = "";
                connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
                connect = new SqlConnection(connectionString);
                connect.Open();
                int CurrentBalance = 0;
                sql = $"SELECT Balance FROM Balance WHERE CustomerName LIKE '{user}%'";
                command = new SqlCommand(sql, connect);
                CurrentBalance = (Int32)command.ExecuteScalar();
                command.Dispose();
                Console.Clear();
                Console.WriteLine($"{user}, please enter the amount you want to withdraw: ");
                int CashWithdraw = Convert.ToInt32(Console.ReadLine());
                if (CurrentBalance - CashWithdraw < 0)
                {
                    Console.WriteLine("\n Error! Your balance cannot go below 0");
                    didWithdraw = false;
                    Console.ReadKey();
                }
                else
                {
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
                    didWithdraw = true;
                    Console.WriteLine(Output + "\nPress any key to return to the main menu");
                    reader.Close();
                    command.Dispose();
                    Console.ReadKey();
                }
            } while (!didWithdraw);
        }

    }
}
