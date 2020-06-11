using System;
using System.Data.SqlClient;


namespace Bank_Managmenet_Program__BMP_
{
    class LoanStatusOrTakeALoan
    {
        protected int trueOrNot;
        protected int LoanAmount;
        protected string LoanStatusOutput;
        public void LoanStatus(string user)
        {
            ConsoleKeyInfo FinalLoanSelection;
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
            using (connect)
            {
                sql = $"SELECT LoanStatus FROM Balance WHERE CustomerName LIKE '{user}%'";
                command = new SqlCommand(sql, connect);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trueOrNot = reader.GetInt32(0);
                    }
                }
            }
            if (trueOrNot == 1)
            {
                Console.WriteLine("There is a loan associated with your account.\nHere is some information about your loan");
                using (SqlConnection con1 = new SqlConnection(connectionString))
                {
                    string LoanStatusString = $"SELECT * FROM Balance WHERE CustomerName LIKE {user}%";
                    SqlCommand cmd1 = new SqlCommand(LoanStatusString, con1);
                    con1.Open();
                    using (SqlDataReader LoanReader = cmd1.ExecuteReader())
                    {
                        while (LoanReader.Read())
                        {
                            LoanStatusOutput = LoanReader.GetValue(5).ToString() + " - " + LoanReader.GetValue(6).ToString() + " " + LoanReader.GetValue(2);
                        }
                    }
                }
                Console.WriteLine(LoanStatusOutput);
                Console.ReadKey();
            }
            else if (trueOrNot == 0)
            {
                Console.WriteLine("There is no loan associated with your account.\nWould you like to take a loan? (pressy \"Y\" for yes, \"N\" for no or \"Esc\" to cancel)");
                ConsoleKeyInfo LoanSelection;
                LoanSelection = Console.ReadKey();
                connect.Close();
                if (LoanSelection.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    Console.WriteLine("Here is a list of loan options\n------------------------------------------------------------");
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string selectString = $"SELECT * FROM Loan_Options";
                        SqlCommand cmd = new SqlCommand(selectString, con);
                        con.Open();
                        using (SqlDataReader DatReader = cmd.ExecuteReader())
                        {
                            while (DatReader.Read())
                            {
                                LoanOut = LoanOut + DatReader.GetValue(2) + " - " + DatReader.GetValue(0) + " " + DatReader.GetValue(4) + " - " + DatReader.GetValue(1) + " per month\n";
                            }
                        }
                    }
                    Console.WriteLine(LoanOut);
                    Console.WriteLine("\n------------------------------------------------------------\nPress any of the following keys to select a loan amount and plan\n1. 5000 DKK\n2. 10000 DKK\n3. 15000 DKK");
                    FinalLoanSelection = Console.ReadKey();
                    if (FinalLoanSelection.Key == ConsoleKey.D1)
                    {
                        LoanAmount = 5000;
                    }
                    else if (FinalLoanSelection.Key == ConsoleKey.D2)
                    {
                        LoanAmount = 10000;
                    }
                    else if (FinalLoanSelection.Key == ConsoleKey.D3)
                    {
                        LoanAmount = 15000;
                    }
                    string UpdateString = $"UPDATE Balance SET LoanStatus='1', LoanAmount={LoanAmount} WHERE CustomerName LIKE '{user}%'";
                    adapter.UpdateCommand = new SqlCommand(UpdateString, connect);
                    adapter.UpdateCommand.ExecuteNonQuery();
                }
                else if (LoanSelection.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                }
            }
        }
    }
}
