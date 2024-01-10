namespace DalTest;
using DalApi;
using DO;
using System;
using System.Data.Common;

public static class Initialization
{
    //private static IEngineer? s_dalEngineer; //stage 1
    //private static ITask? s_dalTask; //stage 1
    //private static IDependency? s_dalDependency; //stage 1
    private static IDal? s_dal; //stage 2



    private static readonly Random s_rand = new();

    private static void createEngineer()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        const int MIN_C = 27;
        const int MAX_C = 300;

        string[] engineerNames =
             {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
    };

        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer.Read(_id) != null);
            string _ename = _name;
            string _email = _ename + "@gmail.com";
            EngineerExperience _level = (EngineerExperience)s_rand.Next(1, 4);
            int _cost = s_rand.Next(MIN_C, MAX_C);
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);
            s_dal!.Engineer.Create(newEngineer);
        }
    }


     private static void createTask()
        {
        for (int i = 1; i <= 21; i++)
            {
            int index = s_rand.Next(0, 4);
            string _Description = "";
            bool _Milestone = false;
            Random rnd = new Random();
            DateTime _CreatedAtDate =new DateTime(2023, 1, 1);
            DateTime _ScheduledDate = _CreatedAtDate.AddDays(30 * index);
            TimeSpan _RequiredEffortTime = new(rnd.Next(300), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            DateTime _DeadlineDate = _ScheduledDate.AddDays(20);
            //int _IdRandom = rnd.Next(0, 40);
            EngineerExperience _difficulty = (EngineerExperience)s_rand.Next(1, 5);
            DateTime _startDate = DateTime.Now;
            DateTime _CompleteDate = DateTime.MinValue;
            Task newTask = new(0,_Description, null, _Milestone, _CreatedAtDate, _RequiredEffortTime, _startDate, _ScheduledDate, _DeadlineDate, _CompleteDate, null, null, 1, _difficulty);
            s_dal!.Task.Create(newTask);
        }
     }

    private static void createDependency()
    {
        for (int i = 1; i < 41; i++)
        {
            Random rnd = new Random();
            int _DependentTask = rnd.Next(1, 101);
            int _DependsOnTask = rnd.Next(1, _DependentTask);
            Dependency newDependency = new(0, _DependentTask, _DependsOnTask);
            s_dal!.Dependency.Create(newDependency);
        }
    }

    public static void Do()
    {
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dal = Factory.Get; //?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        createEngineer();
        createTask();
        createDependency();
    }
}
