﻿namespace Dal;
using DalApi;
using DO;
using System;
using System.Data.Common;

public static class Initialization
{
    private static IEngineer? s_dalEngineer; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IDependency? s_dalDependency; //stage 1

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
            while (s_dalEngineer!.Read(_id) != null);
            string _ename = _name;
            string _email = _ename + "@gmail.com";
            EngineerExperience _level = (EngineerExperience)s_rand.Next(0, 2);
            int _cost = s_rand.Next(MIN_C, MAX_C);
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);
            s_dalEngineer!.Create(newEngineer);
        }
    }


     private static void createTask()
        {
        List<Engineer> Engineers = s_dalEngineer!.ReadAll();
        for (int i = 0; i < 100; i++)
            {
            int index = s_rand.Next(0, 4);
            string _Description = ((Urgency)index).ToString();
            bool _Milestone = false;
            Random rnd = new Random();
            int day = rnd.Next(1, 31);
            int month = rnd.Next(1, 13);
            int year = rnd.Next(2000,2024);
            DateTime _ProductionDate =new DateTime(year, month, day);
            int p = _ProductionDate.Day;
            int now = DateTime.Now.Day;
            int startInDay = rnd.Next(p, now+1);
            DateTime _StartDate = DateTime.Parse(startInDay.ToString());
            DateTime _EstimatedCompletionDate = DateTime.Parse((now + 30 * index).ToString());
            DateTime _FinalDateCompletion = DateTime.Parse((_EstimatedCompletionDate.Day+20).ToString());
            int _IdRandom = rnd.Next(0, 40);
            int _EngineerId = Engineers[_IdRandom].Id;
            EngineerExperience _level = (EngineerExperience)s_rand.Next(0, 2);
            Task newTask = new(0,_Description, null, _Milestone, _ProductionDate, _StartDate, _EstimatedCompletionDate, _FinalDateCompletion,null, null, null, _EngineerId, _level);
            s_dalTask!.Create(newTask);
        }
     }

    private static void createDependency()
    {
        for (int i = 0; i < 250; i++)
        {
            Random rnd = new Random();
            int _IdDependTask = rnd.Next(1, 101);
            int _IdPreviousDependTask = rnd.Next(1, _IdDependTask);
            Dependency newDependency = new(0, _IdDependTask, _IdPreviousDependTask);
            s_dalDependency!.Create(newDependency);
        }
    }

    public static void Do(IEngineer? dalEngineer, ITask? dalTask, IDependency? dalDependency)
    {
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        createDependency();
        createEngineer();
        createTask();
    }
}