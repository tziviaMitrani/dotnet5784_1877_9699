namespace DalTest;
using DalApi;
using DO;
using Dal;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Transactions;
using System.Xml.Linq;
using System.Collections;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;


internal class Program
{
    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    //static readonly IDal s_dal = new DalList(); //stage 2
    //static readonly IDal s_dal = new DalXml(); //stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4
    /// <summary>
    /// the main program.
    /// The user can choose any entity he wants and it must perform actions according to his choice. 
    /// </summary>
    static void Main()
    {
        try
        {
            Initialization.Do(s_dal); //stage 2
            main2();
        }
        catch (Exception ex)
        {
            Console.WriteLine( ex);
        }


        
    }


    /// <summary>
    /// This function directs the user to the desired entity.
    /// </summary>
    public static void main2()
    {
        Console.WriteLine("Select the desired entity,\n 0 to exit\n 1 to engineer, \n 2 to task, \n 3 to dependency\n");
        int choice = 0;
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
            "1 exit from the main menu,\n" +
            "2 add a new object to the list,\n" +
            "3 display an object by ID,\n" +
            "4 display a list of all objects from this entity, \n" +
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
            main2();
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
        try
        {
            Console.WriteLine("Enter the ID number of the dependency you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dal!.Dependency.Delete(_ID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    // <summary>
    /// The program updates the desirable depenency.
    /// </summary>
    public static void UpdateDependency()
    {

        try
        {
            Console.WriteLine("Enter the ID number of the depevdency you want to update.\n");
            int _ID = 0, _IDDependTask = 0, _IDPreviousDependTask = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            if (s_dal!.Dependency.Read(_ID) is null)
                throw new DalDoesNotExistException($"Dependency with such an ID={_ID} does not exists");
            Dependency dependency = s_dal!.Dependency.Read(_ID)!;
            Console.WriteLine("The dependency you want to update is:\n");
            ShowDependency(dependency);
            Console.WriteLine("Enter the ID of the depend task or the ID of the pervious depend task.\n");
            if (!int.TryParse(Console.ReadLine(), out _IDDependTask))
                _IDDependTask = (int)dependency.IdDependTask!;
            if (!int.TryParse(Console.ReadLine(), out _IDPreviousDependTask))
                _IDPreviousDependTask = (int)dependency.IdPreviousDependTask!;
            Dependency dependency1 = new(_ID, _IDDependTask, _IDPreviousDependTask);
            s_dal!.Dependency.Update(dependency1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }

    public static void ShowDependency(Dependency item)
    {
        Console.WriteLine("ID: {0}, ID of depend task: {1}, ID of previous task: {2}\n", item.Id, item.IdDependTask, item.IdPreviousDependTask);
    }

    // <summary>
    /// The program read all the depenencies.
    /// </summary>
    public static void ReadAllDependency()
    {
        IEnumerable<Dependency> dependencies = s_dal!.Dependency.ReadAll()!;
        Console.WriteLine("The whole dependencies details:\n");
        foreach (var dependency in dependencies)
        {
            ShowDependency(dependency);
        }
    }

    // <summary>
    /// The program read the desirable depenency.
    /// </summary>
    public static void ReadDependency()
    {
        try
        {
            Console.WriteLine("Enter the ID number of the dependency you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            Dependency dependency = s_dal!.Dependency.Read(_ID)! ?? throw new DalDoesNotExistException($"Dependency with such an ID={_ID} does not exist");
            Console.WriteLine("The dependency details is:\n");
            ShowDependency(dependency);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }

    // <summary>
    /// The program creat the desirable depenency.
    /// </summary>
    public static void CreatDependency()
    {
        try
        {
            Console.WriteLine("Enter ID of depend task and the ID of the previous depend task.\n");
            int _DependTask = 0, _PreviousTask = 0;
            int.TryParse(Console.ReadLine(), out _DependTask);
            int.TryParse(Console.ReadLine(), out _PreviousTask);
            Dependency dependency = new(0, _DependTask, _PreviousTask);
            int IDShow = s_dal!.Dependency.Create(dependency);
            Console.WriteLine("A dependency with this ID={0} is created.", IDShow);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

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
            main2();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
        }

    }

    /// <summary>
    /// show the details of the Engineer.
    /// </summary>
    public static void ShowEngineer(Engineer item)
    {
        Console.WriteLine("ID: {0}, name: {1}, email: {2}, level: {3}, cost: {4}\n", item.Id, item.Name, item.Email, item.Level, item.Cost);
    }

    /// <summary>
    /// The program delet the desirable engineer.
    /// </summary>
    public static void DeleteEngineer()
    {
        try
        {
            Console.WriteLine("Enter the ID number of the engineer you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dal!.Engineer.Delete(_ID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }

    // <summary>
    /// The program update the desirable engineer.
    /// </summary>
    public static void UpdateEngineer()
    {
        try
        {
            Console.WriteLine("Enter the ID number of the engineer you want to update.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            if (s_dal!.Engineer.Read(_ID) is null)
                throw new DalDoesNotExistException($"Engineer with such an ID={_ID} does not exists");
            Engineer engineer = s_dal!.Engineer.Read(_ID)!;
            Console.WriteLine("The Engineer that you want to update is:\n");
            ShowEngineer(engineer);
            Console.WriteLine("Enter the Name, email, level: 1 for Expert, 2 for Junior or 3 for Novice , and payment.\n");
            int _l = 1;
            double _cost = engineer.Cost;
            string? _name = Console.ReadLine() ?? engineer.Name;
            string? _email = Console.ReadLine() ?? engineer.Email;
            EngineerExperience _level = (EngineerExperience)_l;
            if (!int.TryParse(Console.ReadLine(), out _l))
                _level = engineer.Level;
            else
                _level = (EngineerExperience)_l;
            if (!double.TryParse(Console.ReadLine(), out _cost))
                _cost = engineer.Cost;
            Engineer engineer1 = new(_ID, _name, _email, _level, _cost);
            s_dal!.Engineer.Update(engineer1);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
        }


    }

    // <summary>
    /// The program read all the engineers.
    /// </summary>
    public static void ReadAllEngineer()
    {
        IEnumerable<Engineer> engineers = s_dal!.Engineer.ReadAll()!;
        Console.WriteLine("The whole engineers:\n");
        foreach (var engineer in engineers)
        {
            ShowEngineer(engineer);
        }
    }

    // <summary>
    /// The program read the desirable engineer.
    /// </summary>
    public static void ReadEngineer()
    {
        try
        {
            Console.WriteLine("Enter the ID number of the engineer you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            Engineer engineer = s_dal!.Engineer.Read(_ID)! ?? throw new DalDoesNotExistException($"Engineer with such an ID={_ID} does not exist");
            Console.WriteLine("The engineer detailes:\n");
            ShowEngineer(engineer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


    }

    // <summary>
    /// The program creat the desirable engineer.
    /// </summary>
    public static void CreatEngineer()
    {
        try
        {
            Console.WriteLine("Enter the ID, Name, email, level: 1 for Expert, 2 for Junior or 3 for Novice , and payment.\n");
            int _ID = 0, _l = 0;
            double _cost = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            if ((s_dal!.Engineer.Read(_ID) is not null))
                throw new DalAlreadyExistsException($"Engineer with such an ID={_ID} already exists");
            string _name = Console.ReadLine()!;
            string _email = Console.ReadLine()!;
            int.TryParse(Console.ReadLine(), out _l);
            EngineerExperience _level = (EngineerExperience)_l;
            double.TryParse(Console.ReadLine(), out _cost);
            Engineer engineer = new(_ID, _name, _email, _level, _cost);
            int IDShow = s_dal!.Engineer.Create(engineer);
            Console.WriteLine("A engineer with this id={0} created.", IDShow);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


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
            main2();
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
        try
        {
            Console.WriteLine("Enter the ID number of the task you want to delete.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            s_dal!.Task.Delete(_ID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


    }

    // <summary>
    /// The program update the desirable task.
    /// </summary>
    public static void UpdateTask()
    {
        try
        {
            Console.WriteLine("Enter the ID number of the task you want to update.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            if ((s_dal!.Task.Read(_ID) is null))
                throw new DalDoesNotExistException($"Task with such an ID={_ID} does not exists");
            Task task = s_dal!.Task.Read(_ID)!;
            Console.WriteLine("The task you want to update is:\n");
            ShowTask(task);            
            int _IDEngineer = 0, _Difficulty = 1;
            DateTime _ProductionDate = (DateTime)task.ProductionDate!, _StartDate, _EstimatedCompletionDate, _FinalDateCompletion, _ActualEndDate;
            Console.WriteLine("Enter the description\n");
            string? _Description = Console.ReadLine();
            if (_Description is null)
                _Description = task.Description;
            Console.WriteLine("an alias\n");
            string? _Alias = Console.ReadLine();
            if (_Alias is null)
                _Alias = task.Alias;
            Console.WriteLine("\a start date\n");
            DateTime.TryParse(Console.ReadLine(), out _StartDate);
            if (_StartDate == DateTime.MinValue)
                _StartDate = (DateTime)task.StartDate!;
            Console.WriteLine("an estimated completion date\n");
            DateTime.TryParse(Console.ReadLine(), out _EstimatedCompletionDate);
            if (_EstimatedCompletionDate == DateTime.MinValue)
                _EstimatedCompletionDate = (DateTime)task.EstimatedCompletionDate!;
            Console.WriteLine("a final date completion\n");
            DateTime.TryParse(Console.ReadLine(), out _FinalDateCompletion);
            if (_FinalDateCompletion == DateTime.MinValue)
                _FinalDateCompletion = (DateTime)task.FinalDateCompletion!;
            Console.WriteLine("an actual end date\n");
            DateTime.TryParse(Console.ReadLine(), out _ActualEndDate);
            if (_ActualEndDate == DateTime.MinValue)
                _ActualEndDate = (DateTime)task.ActualEndDate!;
            Console.WriteLine("a product\n");
            string? _Product = Console.ReadLine();
            if (_Product is null)
                _Product = task.product;
            Console.WriteLine("notes\n");
            string? _Notes = Console.ReadLine();
            if (_Notes is null)
                _Notes = task.Notes;
            Console.WriteLine("_ID Engineer\n");
            if (!int.TryParse(Console.ReadLine(), out _IDEngineer))
                _IDEngineer = (int)task.Engineerid!;
            Console.WriteLine("an engineer Id and difficulty 1-10\n");
            if (!int.TryParse(Console.ReadLine(), out _Difficulty))
                _Difficulty = task.Difficulty;
            Task newTask = new(_ID, _Description, _Alias, false, _ProductionDate, _StartDate, _EstimatedCompletionDate, _FinalDateCompletion, _ActualEndDate, _Product, _Notes, _IDEngineer, _Difficulty);
            s_dal!.Task.Update(newTask);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
        }


    }

    // <summary>
    /// The program read all the tasks.
    /// </summary>

    public static void ReadAllTask()
    {
        IEnumerable<DO.Task> tasks = s_dal!.Task.ReadAll()!;
        Console.WriteLine("The whole tasks details:\n");
        foreach (var task in tasks)
        {
            ShowTask(task);
        }
    }
    // <summary>
    /// The program read the desirable task.
    /// </summary>
    public static void ReadTask()
    {
        try
        {
            Console.WriteLine("Enter the ID number of the task you want to display.\n");
            int _ID = 0;
            int.TryParse(Console.ReadLine(), out _ID);
            DO.Task task = s_dal!.Task.Read(_ID)! ?? throw new DalDoesNotExistException($"Task with such an ID={_ID} does not exist");
            Console.WriteLine("The task details:\n");
            ShowTask(task);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    // <summary>
    /// The program creat the desirable task.
    /// </summary>
    public static void CreatTask()
    {
        try
        {
            Console.WriteLine("Enter the description,\nan alias,\na production date,\na start date,\n" +
                "an estimated completion date,\na final date completion,\nan actual end date,\na product,\n" +
                "notes,\nan engineer Id and difficulty 1-10.\n");
            int _ID = 0, _Engineerid = 1, _Difficulty = 1;
            string _Description = Console.ReadLine()!;
            string? _Alias = Console.ReadLine();
            DateTime _ProductionDate, _EstimatedCompletionDate, _FinalDateCompletion;
            DateTime _StartDate = DateTime.Now;   
            DateTime.TryParse(Console.ReadLine(), out _ProductionDate);
            DateTime.TryParse(Console.ReadLine(), out _EstimatedCompletionDate);
            DateTime.TryParse(Console.ReadLine(), out _FinalDateCompletion);
            DateTime _ActualEndDate = DateTime.MinValue;
            string? _product = Console.ReadLine();
            string? _Notes = Console.ReadLine();
            int.TryParse(Console.ReadLine(), out _Engineerid);
            int.TryParse(Console.ReadLine(), out _Difficulty);
            Task task = new(_ID, _Description, _Alias, false, _ProductionDate, _StartDate, _EstimatedCompletionDate, _FinalDateCompletion, _ActualEndDate, _product, _Notes, _Engineerid, _Difficulty);
            int IDShow = s_dal!.Task.Create(task);
            Console.WriteLine("A task with this ID={0} created.", IDShow);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
        }

    }
    /// <summary>
    /// shows the details of the Task.
    /// </summary>

    public static void ShowTask(DO.Task item)
    {
        Console.WriteLine("ID: {0},\n description: {1},\n alias: {2},\n mileston: {3},\n production date: {4},\n start date: {5},\n" +
            " estimated completion date: {6},\n final date completion: {7},\n actual end date {8},\n" +
            " product: {9},\n notes: {10},\n engineer ID: {11},\n difficulty: {12} \n", item.Id, item.Description, item.Alias, item.Milestone, item.ProductionDate, item.StartDate,
            item.EstimatedCompletionDate, item.FinalDateCompletion, item.ActualEndDate, item.product, item.Notes, item.Engineerid, item.Difficulty);
    }
}
