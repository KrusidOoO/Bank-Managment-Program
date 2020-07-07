using System;
using System.Threading;

namespace Bank_Managmenet_Program__BMP_
{

    class Program
    {

        //TO DO LIST
        //1. Create a simple login system and "main menu" - DONE
        //2. Create classes for each "function" of main program - PARTLY DONE (Not currently knowing how many functions I want in the program)
        //3. Establish SQL connection, and understand it better - DONE
        //4. Use SQL to store and retrieve data - DONE
        //5. Quality of life improvements - DONE
        //6. Making use of race conditions - IN PROGRESS
        //7. Have a finished product (sort of) - IN PROGRESS
        AutoResetEvent event1 = new AutoResetEvent(false);
        AutoResetEvent event2 = new AutoResetEvent(false);
        AutoResetEvent event3 = new AutoResetEvent(false);
        int test = 0;
        void Work1()
        {
            WaitHandle.WaitAll(new WaitHandle[] { event2, event3 });
            test = 1;
            event1.Set();
        }
        void Work2() { test = 2;event2.Set(); }
        void Work3() { test = 3;event3.Set(); }
        static void Main(string[] args)
        {

            //Class references

            LoginClass loginClass = new LoginClass();

            SelectionInterface selection = new SelectionInterface();

            Program program = new Program();
            //Actual program
            Thread worker1 = new Thread(program.Work1);
            Thread worker2 = new Thread(program.Work2);
            Thread worker3 = new Thread(program.Work3);
            WaitHandle[] waitHandles = new WaitHandle[] { program.event2, program.event3 };

            worker1.Start();
            worker2.Start();
            worker3.Start();

            WaitHandle.WaitAny(new WaitHandle[] { program.event1 });

            Console.WriteLine(program.test);

            Console.ReadKey();

            loginClass.LoginHere();

            selection.Selection(loginClass.user);

        }

    }

}