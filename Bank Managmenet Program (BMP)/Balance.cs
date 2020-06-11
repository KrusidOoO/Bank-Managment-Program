using System;
using System.Data.SqlClient;

namespace Bank_Managmenet_Program__BMP_
{
    class Balance
    {
        public void BalanceCheck(string user)
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
            Console.Clear();
            sql = $"Select * FROM Balance WHERE CustomerName LIKE '{user}%'";
            command = new SqlCommand(sql, connect);
            connect.Open();
            reader = command.ExecuteReader();
            Console.WriteLine($"------------------------------------------------------------\n{user}, here is your balance:\n");
            while (reader.Read())
            {
                Output = reader.GetValue(4) + " - " + reader.GetValue(1) + " " + reader.GetValue(2) + "\n";
            }

            Console.WriteLine(Output + "\n------------------------------------------------------------\nPress any key to return to the main menu");
            reader.Close();
            command.Dispose();
            Console.ReadKey();
        }
    }
}
