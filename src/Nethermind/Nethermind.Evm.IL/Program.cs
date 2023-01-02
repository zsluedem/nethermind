// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{

    class Program
    {
        private delegate void HelloDelegate();
        public static void Main(string[] args)

        {
            Console.WriteLine("Running Example 1");
            Example1.call();
            Console.WriteLine("Running Example 1 done");

            Console.WriteLine("Running Example 2");
            Example2.call();
            Console.WriteLine("Running Example 2 done");
            
            Console.WriteLine("Running Example 3");
            Example3.call();
            Console.WriteLine("Running Example 3 done");

            Console.WriteLine("Running Example 4");
            Example4.call();
            Console.WriteLine("Running Example 4 done");

            Console.WriteLine("Running Example 5");
            Example5.call();
            Console.WriteLine("Running Example 5 done");

            Console.WriteLine("Running Example 6");
            Example6.call();
            Console.WriteLine("Running Example 6 done");

            Console.WriteLine("Running Example 7");
            Example7.call();
            Console.WriteLine("Running Example 7 done");
        }
    }

}

