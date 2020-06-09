using System;
using System.Data.SqlClient;


namespace Bank_Managmenet_Program__BMP_
{
    class LoanStatusOrTakeALoan
    {
        public void LoanStatus(string user)
        {
            Program Main = new Program();
            string connectionString;
            SqlConnection connect;
            SqlCommand command;
            SqlDataReader reader;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql, LoanOut = "";
            connectionString = @"Data Source=ANDREAS-KRUSE-G;Initial Catalog=BankManagement;Integrated Security=True;Pooling=False";
            connect = new SqlConnection(connectionString);
            connect.Open();
            Console.Clear();
            int trueOrNot = 1;
            sql = $"SELECT LoanStatus FROM Balance WHERE CustomerName LIKE '{user}%'";
            command = new SqlCommand(sql, connect);
            reader = command.ExecuteReader();
            Object[] values = new Object[reader.FieldCount];
            int fieldCount = reader.GetValues(values);
            if (trueOrNot == 0)
            {
                Console.WriteLine("There is a loan associated with your account.");
                Console.ReadKey();
            }
            else if (trueOrNot == 1)
            {
                Console.WriteLine("There is no loan associated with your account.\nWould you like to take a loan? (pressy \"Y\" for yes or \"N\" for no)");
                ConsoleKeyInfo LoanSelection;
                LoanSelection = Console.ReadKey();

                if (LoanSelection.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("Here is a list of loan options\n------------------------------------------------------------");
                    for(int i=0;i<fieldCount;i++)
                    {
                        Console.WriteLine(values[i]);
                    }
                    Console.WriteLine("\n------------------------------------------------------------");

                }
                else if (LoanSelection.Key == ConsoleKey.N)
                {

                }
            }
        }
    }
}
