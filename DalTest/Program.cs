using Dal;
using DalApi;
using System.Xml.XPath;

namespace DalTest
{
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        static void Main()
        {
            try
            {
                Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            } 
        }
        
        static void  main2()
        {
            Console.WriteLine("Select the desired entity,\n 0 to exit\n 1 to engineer, \n 2 to task, \n 3 to dependency\n");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":

                default:
                    break;
            }
        }

        
    }
}