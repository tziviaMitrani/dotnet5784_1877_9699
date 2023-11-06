using Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DalTest
{
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        /// <summary>
        /// the main program.
        /// The user can choose any entity he wants and it must perform actions according to his choice. 
        /// </summary>
        static void Main()
        {
            try
            {
                Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
                main2();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            } 
        }


        /// <summary>
        /// This function directs the user to the desired entity.
        /// </summary>
        public static void  main2()
        {
            Console.WriteLine("Select the desired entity,\n 0 to exit\n 1 to engineer, \n 2 to task, \n 3 to dependency\n");
            int choice=0;
            int.TryParse(Console.ReadLine()!, out choice);
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    EnigieerChoice();
                    break;
                case 2:
                    TaskChoice();
                    break;
                case 3:
                    DependencyChoice();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The program write the menu to the user.
        /// </summary>
        public static void WriteMenu() 
        {
            Console.WriteLine("Choose the action you want to perform,\n" +
                " 1 exit from the main menu,\n" +
                " 2 add a new object to the list,\n" +
                " 3 display an object by ID,\n" +
                " 4 display a list of all objects from this entity, \n" +
                "5 update existing object data, \n" +
                "6 delete an existing object from the list,");
        }

        /// <summary>
        /// The function refers to the desired action on a dependency.
        /// </summary>
        public static void DependencyChoice()
        {
            try
            {
                WriteMenu();
                int choice = 1;
                int.TryParse(Console.ReadLine()!, out choice);
                switch (choice)
                {
                    case 1:
                        return;
                    case 2:
                        CreatDependency();
                        break;
                    case 3:
                        ReadDependency();
                        break;
                    case 4:
                        ReadAllDependency();
                        break;
                    case 5:
                        UpdateDependency();
                        break;
                    case 6:
                        DeleteDependency();
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// The program delets the desirable dependency.
        /// </summary>
        public static void DeleteDependency()
        {
            Console.WriteLine("Enter the ID number of the dependency you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dalDependency!.Delete(_ID);
        }
        // <summary>
        /// The program updates the desirable depenency.
        /// </summary>
        public static void UpdateDependency()
        {
            Console.WriteLine("Enter the ID number of the depevdency you want to update.\n");
            int _ID = 0, _IDDependTask = 0, _IDPreviousDependTask = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            if (s_dalDependency!.Read(_ID) is null)
                throw new Exception($"Dependency with such an ID={_ID} does not exists");
            Dependency dependency = s_dalDependency!.Read(_ID)!;
            s_dalDependency!.Show(dependency);
            Console.WriteLine("Enter the ID of the depend task or the ID of the pervious depend task.\n");
            if (!int.TryParse(Console.ReadLine(), out _IDDependTask))
                _IDDependTask = dependency.IdDependTask;
            if (int.TryParse(Console.ReadLine(), out _IDPreviousDependTask))
                _IDPreviousDependTask = dependency.IdPreviousDependTask;
            Dependency dependency1 = new(_ID, _IDDependTask, _IDPreviousDependTask);
            s_dalDependency!.Update(dependency1);
        }

        // <summary>
        /// The program read all the depenencies.
        /// </summary>
        public static void ReadAllDependency()
        {
            List<Dependency> dependencies = s_dalDependency!.ReadAll();
            foreach (var dependency in dependencies)
            {
                s_dalDependency!.Show(dependency);
            }
        }

        // <summary>
        /// The program read the desirable depenency.
        /// </summary>
        public static void ReadDependency()
        {
            Console.WriteLine("Enter the ID number of the dependency you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            Dependency dependency = s_dalDependency!.Read(_ID)!;
            s_dalDependency!.Show(dependency);
        }

        // <summary>
        /// The program creat the desirable depenency.
        /// </summary>
        public static void CreatDependency()
        {
            Console.WriteLine("Enter the ID, ID of depend task and the ID of the previous depend task.\n");
            int _ID = 0, _DependTask=0,_PreviousTask=0 ;
            int.TryParse(Console.ReadLine(), out _ID);
            int.TryParse(Console.ReadLine(), out _DependTask);
            int.TryParse(Console.ReadLine(), out _PreviousTask);
            Dependency dependency = new(_ID, _DependTask, _PreviousTask);
            int IDShow = s_dalDependency!.Create(dependency);
            Console.WriteLine(IDShow);
        }
        /// <summary>
        /// The function refers to the desired action on an engineer.
        /// </summary>
        public static void EnigieerChoice()
        {
            try
            {
                WriteMenu();
                int choice = 1;
                int.TryParse(Console.ReadLine()!, out choice);
                switch (choice)
                {
                    case 1:
                        return;
                    case 2:
                        CreatEngineer();
                        break;
                    case 3:
                        ReadEngineer();
                        break;
                    case 4:
                        ReadAllEngineer();
                        break;
                    case 5:
                        UpdateEngineer();
                        break;
                    case 6:
                        DeleteEngineer();
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            
        }

        /// <summary>
        /// The program delet the desirable engineer.
        /// </summary>
        public static void DeleteEngineer()
        {
            Console.WriteLine("Enter the ID number of the engineer you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dalEngineer!.Delete(_ID);
        }

        // <summary>
        /// The program update the desirable engineer.
        /// </summary>
        public static void UpdateEngineer()
        {
            Console.WriteLine("Enter the ID number of the engineer you want to update.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            if (s_dalEngineer!.Read(_ID) is null)
                throw new Exception($"Engineer with such an ID={_ID} does not exists");
            Engineer engineer = s_dalEngineer!.Read(_ID)!;
            s_dalEngineer!.Show(engineer);
            Console.WriteLine("Enter the Name, email, level: 0 for Expert, 1 for Junior or 2 for Novice , and payment.\n");
            int _l = 0;
            double _cost = 0;
            string? _name = Console.ReadLine() ?? engineer.Name;
            string? _email = Console.ReadLine()?? engineer.Email;
            int.TryParse(Console.ReadLine(), out _l);
            EngineerExperience _level = engineer.Level;
            if (_l == 0 || _l == 1 || _l == 2)
                _level = (EngineerExperience)_l;
            double.TryParse(Console.ReadLine(), out _cost);
            Engineer engineer1 = new(_ID, _name, _email, _level, _cost);
            s_dalEngineer!.Update(engineer);
        }

        // <summary>
        /// The program read all the engineers.
        /// </summary>
        public static void ReadAllEngineer()
        {
            List<Engineer> engineers = s_dalEngineer!.ReadAll();
            foreach(var engineer in engineers)
            {
                s_dalEngineer!.Show(engineer);
            }
        }

        // <summary>
        /// The program read the desirable engineer.
        /// </summary>
        public static void ReadEngineer() 
        {
            Console.WriteLine("Enter the ID number of the engineer you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            Engineer engineer= s_dalEngineer!.Read(_ID)!;
            s_dalEngineer!.Show(engineer);
        }

        // <summary>
        /// The program creat the desirable engineer.
        /// </summary>
        public static void CreatEngineer()
        {
            Console.WriteLine("Enter the ID, Name, email, level: 0 for Expert, 1 for Junior or 2 for Novice , and payment.\n");
            int _ID = 0, _l=0;
            double _cost = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            string _name = Console.ReadLine()!;
            string _email = Console.ReadLine()!;
            int.TryParse(Console.ReadLine(), out _l);
            EngineerExperience _level = (EngineerExperience)_l;
            double.TryParse(Console.ReadLine(), out _cost);
            Engineer engineer= new(_ID,_name, _email, _level, _cost);
            int IDShow = s_dalEngineer!.Create(engineer);
            Console.WriteLine(IDShow);
        }

        /// <summary>
        /// The function refers to the desired action on a task.
        /// </summary>
        public static void TaskChoice() 
        {
            try
            {
                WriteMenu();
                int choice = 1;
                int.TryParse(Console.ReadLine()!, out choice);
                switch (choice)
                {
                    case 1:
                        return;
                    case 2:
                        CreatTask();
                        break;
                    case 3:
                        ReadTask();
                        break;
                    case 4:
                        ReadAllTask();
                        break;
                    case 5:
                        UpdateTask();
                        break;
                    case 6:
                        DeleteTask();
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// The program delet the desirable task.
        /// </summary>

        public static void DeleteTask()
        {
            Console.WriteLine("Enter the ID number of the task you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dalTask!.Delete(_ID);
        }

        // <summary>
        /// The program update the desirable task.
        /// </summary>
        public static void UpdateTask()
        {
            Console.WriteLine("Enter the ID number of the task you want to update.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            if (s_dalEngineer!.Read(_ID) is null)
                throw new Exception($"Engineer with such an ID={_ID} does not exists");
            DO.Task task = s_dalTask!.Read(_ID)!;
            s_dalTask!.Show(task);
            Console.WriteLine("Enter the description ,an alias o,a production date,a start date,an estimated completion date,a final date completion, an actual end date,a product,notes, an engineer Id and difficulty.\n.\n");
            int EngineerId;
            if (!int.TryParse(Console.ReadLine(), out EngineerId))
                EngineerId = task!.Engineerid;
            string? _Description=Console.ReadLine()!, _Alias=Console.ReadLine(), _Product=Console.ReadLine(), _Notes=Console.ReadLine();
            DateTime _ProductionDate, _StartDate = DateTime.Now, _EstimatedCompletionDate, _FinalDateCompletion;
            while (!DateTime.TryParse(Console.ReadLine(), out _ProductionDate))
                Console.WriteLine("Enter product date");
            while (!DateTime.TryParse(Console.ReadLine(), out _EstimatedCompletionDate))
                Console.WriteLine("Enter _estimated completion date");
            while (!DateTime.TryParse(Console.ReadLine(), out _FinalDateCompletion))
                Console.WriteLine("Enter final date completion");
            int _IDEngineer = 0;
            int.TryParse(Console.ReadLine(), out _IDEngineer);
            EngineerExperience _Difficulty;
            EngineerExperience.TryParse(Console.ReadLine(), out _Difficulty);
            DO.Task newTask = new(_ID, _Description, _Alias, false, _ProductionDate, _StartDate, _EstimatedCompletionDate, _FinalDateCompletion,null, _Product,_Notes, _IDEngineer, _Difficulty);
            s_dalTask!.Update(newTask);
        }

        // <summary>
        /// The program read all the tasks.
        /// </summary>

        public static void ReadAllTask()
        {
            List<DO.Task> tasks = s_dalTask!.ReadAll();
            foreach (var task in tasks)
            {
                s_dalTask!.Show(task);
            }
        }
        // <summary>
        /// The program read the desirable task.
        /// </summary>
        public static void ReadTask()
        {
            Console.WriteLine("Enter the ID number of the task you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            DO.Task task = s_dalTask!.Read(_ID)!;
            s_dalTask!.Show(task);
        }
        // <summary>
        /// The program creat the desirable task.
        /// </summary>
        public static void CreatTask()
        {
            Console.WriteLine("Enter the Id, a description,an alias, a production date, a start date, an estimated completion date,a final date completion, an actual end date,a product,notes, an engineer Id and difficulty.\n");
            int _ID = 0 , _Engineerid=0;
            int.TryParse(Console.ReadLine(), out _ID);
            string _Description = Console.ReadLine()!;
            string? _Alias = Console.ReadLine()!;
            DateTime _ProductionDate, _StartDate=DateTime.Now, _EstimatedCompletionDate, _FinalDateCompletion;
            while(!DateTime.TryParse(Console.ReadLine(), out _ProductionDate))
                Console.WriteLine("Enter product date");
            while (!DateTime.TryParse(Console.ReadLine(), out _EstimatedCompletionDate))
                Console.WriteLine("Enter _estimated completion date");
            while (!DateTime.TryParse(Console.ReadLine(), out _FinalDateCompletion))
                Console.WriteLine("Enter final date completion");
            string? _product=Console.ReadLine();
            string? _Notes= Console.ReadLine();
            int.TryParse(Console.ReadLine(), out _Engineerid);
            EngineerExperience _Difficulty;
            EngineerExperience.TryParse(Console.ReadLine(), out _Difficulty);
            DO.Task task = new(_ID, _Description, _Alias,false, _ProductionDate, _StartDate, _EstimatedCompletionDate , _FinalDateCompletion,null, _product, _Notes, _Engineerid, _Difficulty);
            int IDShow = s_dalTask!.Create(task);
            Console.WriteLine(IDShow);
        }

         

        
            
        }
    }