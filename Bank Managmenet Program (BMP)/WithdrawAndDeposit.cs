using System;
using System.Data.SqlClient;
using System.Linq;


namespace Bank_Managmenet_Program__BMP_
{
    class WithdrawAndDeposit
    {

        public void Deposit(string user)
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
            Console.WriteLine($"{user} please enter the amount you want to deposit: ");
            int CashInput = Convert.ToInt32(Console.ReadLine());
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


            Console.WriteLine(Output);
            reader.Close();
            command.Dispose();
            Console.ReadKey();
        }
        public void Withdraw(string user)
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

    }
}
