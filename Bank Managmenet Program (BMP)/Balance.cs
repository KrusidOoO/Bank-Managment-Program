using System;
using System.Data.SqlClient;
using System.Linq;

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
            Console.WriteLine("------------------------------------------------------------\nHere is your balance:\n");
            while (reader.Read())
            {
                Output = reader.GetValue(4) + " - " + reader.GetValue(1) + " " + reader.GetValue(2) + "\n";
            }

            Console.WriteLine(Output + "\n------------------------------------------------------------");
            reader.Close();
            command.Dispose();
            Console.ReadKey();
        }
    }
}
