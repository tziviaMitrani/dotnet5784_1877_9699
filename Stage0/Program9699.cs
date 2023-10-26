using System;

namespace Stage0
{
    class Program
    {
        static void Main(string[] args)
        {
            Welcome9699();
            Console.ReadKey();
        }

        private static void Welcome9699()
        {
            Console.WriteLine("Enter your nam: ");
            string user = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first consol application", user);
        }
    }
}