namespace Dal;
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
        "Shira Israelof",
        "Toiby Braish",
        "Maly Kibelevitz",
        "Ruti Salomon",
        "Dvory Mimran",
        "Sari Brodi",
        "Roizy Lefkovit",
        "Chani Rozinberg",
        "Ayala Shraber",
        "Chaya Klain",
        "Esty Shvartz",
        "Pnini Cohen",
        "Giti Leder",
        "Feigy Haker",
        "Kaila Avramovitz",
        "Rachely Vainberg",
        "Gili Reker",
        "Zehava Simcha",
        "Nahama Levi",
        "Hindi Nachumi",
        "Leah Segal",
        "Chaya Toyal",
        "Debbi Pety",
        "Anna Coheni",
        "Efrat Kati",
        "Devora Tal",
        "Tova Eliimelech",
        "Yeudit Avramov",
        "Sury Shvartz",
        "Malki Gotfrid",
        "Sari Brodi",
        "Roizy Safrin",
        "Eti Deblinger",
        "Racheli Bekerman",
        "Miri Kaner",
        "Suly Eler"
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
        for (int i = 1; i <= 101; i++)
            {
            int index = s_rand.Next(0, 4);
            string _Description = "";
            bool _Milestone = false;
            Random rnd = new Random();
            DateTime _ProductionDate =new DateTime(2023, 1, 1);
            DateTime _EstimatedCompletionDate = _ProductionDate.AddDays(30 * index);
            DateTime _FinalDateCompletion = _EstimatedCompletionDate.AddDays(20);
            int _IdRandom = rnd.Next(0, 40);
            int _difficulty = s_rand.Next(0, 2);
            DateTime _startDate = DateTime.Now;
            DateTime _ActualEndDate = DateTime.MinValue;
            Task newTask = new(0,_Description, null, _Milestone, _ProductionDate, _startDate, _EstimatedCompletionDate, _FinalDateCompletion, _ActualEndDate, null, null, 1, _difficulty);
            s_dal!.Task.Create(newTask);
        }
     }

    private static void createDependency()
    {
        for (int i = 1; i < 251; i++)
        {
            Random rnd = new Random();
            int _IdDependTask = rnd.Next(1, 101);
            int _IdPreviousDependTask = rnd.Next(1, _IdDependTask);
            Dependency newDependency = new(0, _IdDependTask, _IdPreviousDependTask);
            s_dal!.Dependency.Create(newDependency);
        }
    }

    public static void Do(IDal dal)
    {
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        createEngineer();
        createTask();
        createDependency();
    }
}
