namespace Dal;
using DalApi;
using DO;


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
            string[] Aliases = { "a", "b", "c", "d", "e" };
            string[] Remarks = { "a", "b", "c", "d", "e" };
            string[] Deliverables = { "r", "a", "c", "e", "l" };

        for (int i = 0; i < 100; i++)
            {
                int indexA = s_rand.Next(0, 4);
                int indexD = s_rand.Next(0, 4);
                string _Description = ((Urgency)indexD).ToString();
                string _Alias = Aliases[indexA];
                bool _Milestone = false;
                DateTime _CreatedAt = RandomDate(new DateTime(2000, 1, 1), DateTime.Today);
                DateTime _Start = RandomDate(_CreatedAt, DateTime.Today);
                DateTime _ForecasDate = _Start.AddDays(((indexD * 365).Days);
                DateTime _Deadline = ForecasDate.AddDays((365 / 2).Days);
                DateTime _Complete = RandomDate(_ForecasDate, _Deadline);
                int indexEI = s_rand.Next(0, 40);
                //int _EngineerId = DataList.DataSource.Engineers[indexEI].Id;
                EngineerExperience _level = (EngineerExperience)s_rand.Next(0, 2);
                int indexR = s_rand.Next(0, 4);
                int indexDe = s_rand.Next(0, 4);
                string _Remarks = Remarks[indexR];
                string _Deliverables = Deliverables[indexDe];
                Task newTask = new(_Description, _Alias, _Milestone, _CreatedAt, _Start, _ForecasDate, _Deadline, _Complete, _Deliverables, _Remarks, _EngineerId, _level);
                s_dalTask!.Create(newTask);

            }
        }
}
