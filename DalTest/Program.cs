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

        public static void DeleteDependency()
        {
            Console.WriteLine("Enter the ID number of the dependency you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dalDependency!.Delete(_ID);
        }
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
       
        public static void ReadAllDependency()
        {
            List<Dependency> dependencies = s_dalDependency!.ReadAll();
            foreach (var dependency in dependencies)
            {
                s_dalDependency!.Show(dependency);
            }
        }
        public static void ReadDependency()
        {
            Console.WriteLine("Enter the ID number of the dependency you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            Dependency dependency = s_dalDependency!.Read(_ID)!;
            s_dalDependency!.Show(dependency);
        }
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

        public static void DeleteEngineer()
        {
            Console.WriteLine("Enter the ID number of the engineer you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dalEngineer!.Delete(_ID);
        }
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

        public static void ReadAllEngineer()
        {
            List<Engineer> engineers = s_dalEngineer!.ReadAll();
            foreach(var engineer in engineers)
            {
                s_dalEngineer!.Show(engineer);
            }
        }
        public static void ReadEngineer() 
        {
            Console.WriteLine("Enter the ID number of the engineer you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            Engineer engineer= s_dalEngineer!.Read(_ID)!;
            s_dalEngineer!.Show(engineer);
        }
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


        public static void DeleteTask()
        {
            Console.WriteLine("Enter the ID number of the task you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dalTask!.Delete(_ID);
        }

        public static void UpdateTask()
        {

        }

        /*
         public static void UpdateTask()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Task's id to update");
                int.TryParse(Console.ReadLine()!, out id);
                DO.Task? previousTask= s_dal!.Task.Read(id);
                Console.WriteLine("the task tou want to update: "+previousTask);
                Console.WriteLine("Enter Engineer Id, Description, Alias, Deliverables, Remarks, createAt date,ForecastDate date, Deadline date, CompmlexityLevel");
                int EngineerId;
                if (!int.TryParse(Console.ReadLine(), out EngineerId))
                    EngineerId = previousTask!.EngineerId;
                string? Description, Alias, Deliverables, Remarks;
                Description = stringIsNullOrEmpty(previousTask!.Description);
                Alias = stringIsNullOrEmpty(previousTask!.Alias);
                Deliverables = stringIsNullOrEmpty(previousTask!.Deliverables);
                Remarks = stringIsNullOrEmpty(previousTask!.Remarks);
                DateTime? createAt = TryParseNullableDateTime(previousTask!.DeadlineDate);
                DateTime? ScheduleDate = TryParseNullableDateTime(previousTask!.ScheduleDate);
                DateTime? ForecastDate = TryParseNullableDateTime(previousTask!.ForecastDate);
                DateTime? Deadline = TryParseNullableDateTime(previousTask!.DeadlineDate);
                EngineerExperience? CompmlexityLevel = TryParseNullableEngineerExperience(previousTask!.CompmlexityLevel);
                DO.Task newTask = new(0, EngineerId, Description, Alias, false, true, Deliverables, Remarks, createAt, ScheduleDate, ForecastDate, Deadline, null, CompmlexityLevel);
                s_dal!.Task.Update(newTask);
                task();
            }
            catch(Exception ex)
            { Console.WriteLine(ex); };
        }

         */

        public static void ReadAllTask()
        {
            List<DO.Task> tasks = s_dalTask!.ReadAll();
            foreach (var task in tasks)
            {
                s_dalTask!.Show(task);
            }
        }
        public static void ReadTask()
        {
            Console.WriteLine("Enter the ID number of the task you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            DO.Task task = s_dalTask!.Read(_ID)!;
            s_dalTask!.Show(task);
        }
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

         

        //--------------------------------------------------------------
        //Initialize the date in the input entered by the user or in the
        //value received by the function if necessary
        //--------------------------------------------------------------
        public static DateTime? TryParseNullableDateTime(DateTime? previous)
        {
            DateTime value;
            return DateTime.TryParse(Console.ReadLine(), out value) ? value : previous;
        }
        //--------------------------------------------------------------
        //Initialize the level in the input entered by the user or in the
        //value received by the function if necessary
        //--------------------------------------------------------------
        public static EngineerExperience? TryParseNullableEngineerExperience(EngineerExperience? previous)
        {
            EngineerExperience value;
            return EngineerExperience.TryParse(Console.ReadLine(), out value) ? value : previous;
        }  
            
        }
    }