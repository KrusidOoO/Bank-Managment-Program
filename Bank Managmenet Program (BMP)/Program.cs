namespace Bank_Managmenet_Program__BMP_
{
    class Program
    {
        //TO DO LIST
        //1. Create a simple login system and "main menu" - DONE
        //2. Create classes for each "function" of main program - PARTLY DONE (Not currently knowing how many functions I want in the program)
        //3. Establish SQL connection, and understand it better - DONE
        //4. Use SQL to store and retrieve data - DONE
        //5. Quality of life improvements - NOT STARTED
        //6. Have a finished product (sort of) - IN PROGRESS
        static void Main(string[] args)
        {
            //Class references
            LoginClass loginClass = new LoginClass();
            SelectionInterface selection = new SelectionInterface();
            //Actual program
            loginClass.LoginHere();
            selection.Selection(loginClass.user);
        }
    }
}