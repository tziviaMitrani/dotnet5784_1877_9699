namespace BlImplementation;
using BlApi;

using DalApi;
using System.Reflection.Emit;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Create(BO.Task boTask)
    {
        if (boTask.Id <= 0 || boTask.Alias == "")
            throw new Exception("task details are not valid");
         try{
         DO.Task doTask = new (
         boTask.Id,
         boTask.Description,
         boTask.Alias,
         boTask.Milestone != null ? true : false,
         boTask.CreatedAtDate,
         null,
         boTask.StartDate,
         boTask.ScheduledDate,
         boTask.DeadlineDate,
         boTask.CompleteDate,
         boTask.Deliverables,
         boTask.Remarks,
         boTask.Engineer != null ? boTask.Engineer.Id : null,
         (DO.EngineerExperience)boTask.Copmlexity
         );
           
        }
        catch
        {
            ///צריך לתפוס חריגה ולעטוף בחריגה עצמית
        }
    }


    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        Func<DO.Task, bool> filter2 = (Func<DO.Task, bool>)filter!;

        IEnumerable<DO.Task> doAllTasks = _dal.Task.ReadAll(filter2)!;
        return from doTask in doAllTasks
               select new BO.Task()
               { 
                   Id = doTask.Id,
                   Description = doTask.Description,
                   Alias = doTask.Alias,
                   CreatedAtDate = doTask.CreatedAtDate,
                   status = setStatus(doTask),
                   Milestone = doTask.Milestone? new BO.MilestoneInTask() { }
                   DependentTasks = doTask.DependentTasks,
                   EstimatedBeginningDate = doTask.EstimatedBeginningDate,
                   BeginningDate = doTask.BeginningDate,
                   EstimatedFinishDate = doTask.EstimatedBeginningDate != null ?
                            doTask.EstimatedBeginningDate.Value.Add(doTask.TaskPerformanceDays) : null,
                   DeadlineDate = doTask.DeadlineDate,
                   CompleteDate = doTask.CompleteDate,
                   Deliverables = doTask.Deliverables,
                   Remarks = doTask.Remarks,
                   Engineer = createEngineerInTask(doTask.Engineer),
                   Copmlexity = (BO.EngineerExperience)doTask.Copmlexity
               };
    }

    //public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    //{
    //    Func<DO.Task, bool> filter2 = (Func<DO.Task, bool>)filter!;

    //    IEnumerable<DO.Task> doAllTasks = _dal.Task.ReadAll(filter2)!;
    //    return from doTask in doAllTasks
    //           select new BO.Task()
    //           {
    //               Id = boTask.Id,//למה צריך אי די?
    //               Description = boTask.Description,
    //               Alias = boTask.Alias,
    //               CreatedAtDate = boTask.CreatedAtDate,
    //               status = boTask.status,
    //               Dependencies = boTask.Dependencies,
    //               Milestone = boTask.Milestone == null ? false : true,
    //               StartDate = boTask.StartDate,
    //               ScheduledDate = boTask.ScheduledDate,
    //               ForecastDate = boTask.ForecastDate,
    //               DeadlineDate = boTask.DeadlineDate,
    //               CompleteDate = (BO.EngineerExperience)doTask.CompleteDate,
    //               Deliverables = boTask.Deliverables,
    //               Remarks = boTask.Remarks,
    //               ForecastDate = doTask.ForecastDate != null ?
    //                        doTask.ForecastDate.Value.Add(doTask.ForecastDate) : null,
    //               Status = setStatus(doTask),
    //               Milestone = doTask.Milestone ? new BO.MilestoneInTask() { }
    //               DependentTasks = doTask.DependentTasks,
    //               Engineer = createEngineerInTask(doTask.Engineer),
    //           };
    //}
    public BO.EngineerInTask? createEngineerInTask(DO.Engineer engineer)
    {
        //לא ככה זה אמור להיות
        return new BO.EngineerInTask() { Id = engineer.Id, Name = engineer.Name };
    }
    //public void Delete(int id)
    //{
    //}

    //public BO.Task? Read(int id)
    //{

    //}
    //public void Create(BO.Task boTask)
    //{
    //    try
    //    {
    //        DO.Task doTask = BOToDO(boTask);
    //        _dal.Task.Create(doTask);
    //    }
    //    catch (BO.BlNullPropertyException ex)
    //    {
    //        throw ex;
    //    }
    //    catch
    //    {
    //        throw new BO.BlAlreadyExistsException($"Task number {boTask.Id} exists");
    //    }
    //}
    //public void Delete(int id)
    //{
    //    try
    //    {
    //        if (_dal.Dependency.ReadAll().Any(dependence => dependence!.DependsOnTask == id))
    //        {
    //            throw new BO.BlDependesOnIt("There are tasks that rely on it");
    //        }
    //        _dal.Task.Delete(id);
    //    }
    //    catch (BO.BlDependesOnIt ex)
    //    {
    //        throw ex;
    //    }
    //    catch
    //    {
    //        throw new BlDoesNotExistException($"Task number {id} dos't exist");
    //    }
    //}

    //public BO.Task? Read(int id)
    //{
    //    try
    //    {
    //        DO.Task? myTask = _dal.Task.Read(id);
    //        return DOToBO(myTask!);
    //    }
    //    catch
    //    {
    //        throw new BlDoesNotExistException($"Task number {id} dos't exist");
    //    }
    //}

    //public IEnumerable<BO.Task> ReadAll(BO.Status status = Status.All)
    //{
    //    if (status == Status.All)
    //    {
    //        return _dal.Task.ReadAll()
    //           .Select(doTask => DOToBO(doTask!));
    //    }
    //    return from DO.Task doTask in _dal.Task.ReadAll()
    //           let boTask = DOToBO(doTask)
    //           where (BO.Status)boTask.Status == status
    //           select boTask;
    //}

    //public void Update(BO.Task boTask)
    //{
    //    try
    //    {
    //        _dal.Task.Update(BOToDO(boTask));
    //    }
    //    catch (BO.BlNullPropertyException ex)
    //    {
    //        throw ex;
    //    }
    //    catch
    //    {
    //        throw new BlDoesNotExistException($"Task number {boTask.ID} dos't exist");
    //    }
    //}

    //private BO.Task DOToBO(DO.Task doTask)
    //{
    //    return new BO.Task
    //    {
    //        Id = doTask.Id!,
    //        Description = doTask.Description,
    //        Alias = doTask.Alias!,
    //        CreatedAtDate = doTask.CreatedAtDate,
    //        status =  FindStatus(doTask),
    //        Dependencies = doTask.Dependencies,
    //        TaskList = FindTaskList(doTask.Id),
    //        RelatedMilestone = findMilestone(doTask.ID),

    //        EstimatedStartDate = doTask.EstimatedStartDate,
    //        EstimatedStartDate = doTask.EstimatedStartDate,

    //        AcualStartNate = doTask.AcualStartNate,
    //        EstimatedEndDate = doTask.EstimatedEndDate,
    //        deadline = doTask.deadline,
    //        AcualEndNate = doTask.AcualEndNate,
    //        Product = doTask.Product,
    //        Remarks = doTask.Remarks,
    //        Copmlexity = (BO.EngineerExperience)doTask.Copmlexity,
    //        EngineerInTask = findEngineer(doTask.IDEngineer)
    //    };
    //     Engineer  Deliverables CompleteDate DeadlineDate ForecastDate ScheduledDate StartDate Milestone Dependencies 
    //}

    //private DO.Task BOToDO(BO.Task boTask)
    //{
    //    if (boTask.Id <= 0 || string.IsNullOrEmpty(boTask.Alias))
    //    {
    //        throw new BlIncorrectDetails("Id and Alias must have valid values");
    //    }
    //    return new DO.Task
    //    {
    //        Id = boTask.Id,
    //        Description = boTask.Description,
    //        Alias = boTask.Alias,
    //        Milestone = false,
    //        Production = boTask.Production,
    //        EstimatedStartDate = boTask.EstimatedStartDate,
    //        AcualStartNate = boTask.AcualStartNate,
    //        EstimatedEndDate = boTask.EstimatedEndDate,
    //        deadline = boTask.deadline,
    //        AcualEndNate = boTask.AcualEndNate,
    //        Product = boTask.Product,
    //        Remaeks = boTask.Remaeks,
    //        IDEngineer = boTask.EngineerIdName!.ID,
    //        Difficulty = (DO.EngineerExperience)boTask.Copmlexity,
    //    };
    //}

    //private TasksEngineer findEngineer(int id)
    //{
    //    try
    //    {
    //        return new BO.TaskInEngineer{ ID = id, Name = _dal.Engineer.Read(id)!.Name };
    //    }
    //    catch
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //private List<int> FindIdList(int TaskId)
    //{
    //    return (from doDependence in _dal.Dependency.ReadAll()
    //            where TaskId == doDependence.IDTask
    //            select doDependence.IDPreviousTask)
    //    .ToList();
    //}
    //private List<TaskInList> FindTaskList(int TaskId)
    //{

    //    return FindIdList(TaskId)
    //        .Select(id => _dal.Task.Read(id)!)
    //        .Select(task => new TaskInList
    //        {
    //            Id = task.Id,
    //            Alias = task.Alias!,
    //            Description = task.Description!,
    //            status = FindStatus(task)
    //        })
    //        .ToList();
    //}

    //private BO.Status FindStatus(DO.Task doTask)
    //{
    //    return (BO.Status)(doTask.EstimatedStartDate is null ? 0
    //                        : doTask.StartDate is null ? 1
    //                        : doTask.CompleteDate is null ? 2
    //                        : 3);
    //}

    /*    private BO.MilestoneIdNickname findMilestone(int TaskId)
        {
            BO.MilestoneIdNickname? milestone = (from id in FindIdList(TaskId)
                                                 let task = _dal.Task.Read(id)!
                                                 where task.Milestone == true
                                                 select new BO.MilestoneIdNickname
                                                 {
                                                     ID = id!,
                                                     NickName = task.Nickname!
                                                 }).FirstOrDefault();
            return milestone;
        }*/

    //int ITask.Create(Task item)
    //{
    //    throw new NotImplementedException();
    //}

    //public IEnumerable<Task> ReadAll(Func<Engineer?, bool>? filter)
    //{
    //    throw new NotImplementedException();
    //}
    //}
    //public int Id { get; init; }
    //public required string Description { get; set; }
    //public required string Alias { get; set; }
    //public DateTime? CreatedAtDate { get; set; }
    //public Status status { get; set; }
    //public required List<TaskInList> Dependencies { get; set; }
    //public required MilestoneInTask Milestone { get; set; }
    //public required DateTime? StartDate { get; set; }
    //public DateTime? ScheduledDate { get; set; }
    //public DateTime? ForecastDate { get; set; }
    //public DateTime? DeadlineDate { get; set; }
    //public DateTime? CompleteDate { get; set; }
    //public required string Deliverables { get; set; }
    //public required string Remarks { get; set; }
    //public required EngineerInTask Engineer { get; set; }
    //public EngineerExperience Copmlexity { get; set; }

}

//using BlApi;
//using BO;
//using System.Reflection.Emit;

//internal class TaskImplementation : ITask
//{
//    private DalApi.IDal _dal = DalApi.Factory.Get;

//    public void Create(BO.Task boTask)
//    {
//        try
//        {
//            DO.Task doTask = BOToDO(boTask);
//            _dal.Task.Create(doTask);
//        }
//        catch (BO.BlNullPropertyException ex)
//        {
//            throw ex;
//        }
//        catch
//        {
//            throw new BO.BlAlreadyExistsException($"Task number {boTask.ID} exists");
//        }
//    }
//    public void Delete(int id)
//    {
//        try
//        {
//            if (_dal.Dependence.ReadAll().Any(dependence => dependence!.IDPreviousTask == id))
//            {
//                throw new BO.BlDependesOnIt("There are tasks that rely on it");
//            }
//            _dal.Task.Delete(id);
//        }
//        catch (BO.BlDependesOnIt ex)
//        {
//            throw ex;
//        }
//        catch
//        {
//            throw new BlDoesNotExistException($"Task number {id} dos't exist");
//        }
//    }

//    public BO.Task? Read(int id)
//    {
//        try
//        {
//            DO.Task? myTask = _dal.Task.Read(id);
//            return DOToBO(myTask);
//        }
//        catch
//        {
//            throw new BlDoesNotExistException($"Task number {id} dos't exist");
//        }
//    }

//    public IEnumerable<BO.Task> ReadAll(BO.Status status = Status.All)
//    {
//        if (status == Status.All)
//        {
//            return _dal.Task.ReadAll()
//               .Select(doTask => DOToBO(doTask!));
//        }
//        return from DO.Task doTask in _dal.Task.ReadAll()
//               let boTask = DOToBO(doTask)
//               where (BO.Status)boTask.TaskStatus == status
//               select boTask;
//    }

//    public void Update(BO.Task boTask)
//    {
//        try
//        {
//            _dal.Task.Update(BOToDO(boTask));
//        }
//        catch (BO.BlNullPropertyException ex)
//        {
//            throw ex;
//        }
//        catch
//        {
//            throw new BlDoesNotExistException($"Task number {boTask.ID} dos't exist");
//        }
//    }

//    private BO.Task DOToBO(DO.Task doTask)
//    {
//        return new BO.Task
//        {
//            ID = doTask.ID!,
//            Nickname = doTask.Nickname!,
//            Description = doTask.Description,
//            Production = doTask.Production,
//            TaskStatus = FindStatus(doTask),
//            TaskList = FindTaskList(doTask.ID),
//            RelatedMilestone = findMilestone(doTask.ID),
//            EstimatedStartDate = doTask.EstimatedStartDate,
//            AcualStartNate = doTask.AcualStartNate,
//            EstimatedEndDate = doTask.EstimatedEndDate,
//            deadline = doTask.deadline,
//            AcualEndNate = doTask.AcualEndNate,
//            Product = doTask.Product,
//            Remaeks = doTask.Remaeks,
//            Difficulty = (BO.EngineerLevelEnum)doTask.Difficulty,
//            EngineerIdName = findEngineer(doTask.IDEngineer)
//        };
//    }

//    private DO.Task BOToDO(BO.Task boTask)
//    {
//        if (boTask.ID <= 0 || string.IsNullOrEmpty(boTask.Nickname))
//        {
//            throw new BlIncorrectDetails("ID and Nickname must have valid values");
//        }
//        return new DO.Task
//        {
//            ID = boTask.ID,
//            Description = boTask.Description,
//            Nickname = boTask.Nickname,
//            Milestone = false,
//            Production = boTask.Production,
//            EstimatedStartDate = boTask.EstimatedStartDate,
//            AcualStartNate = boTask.AcualStartNate,
//            EstimatedEndDate = boTask.EstimatedEndDate,
//            deadline = boTask.deadline,
//            AcualEndNate = boTask.AcualEndNate,
//            Product = boTask.Product,
//            Remaeks = boTask.Remaeks,
//            IDEngineer = boTask.EngineerIdName!.ID,
//            Difficulty = (DO.EngineerLevelEnum)boTask.Difficulty,
//        };
//    }

//    private TasksEngineer findEngineer(int id)
//    {
//        try
//        {
//            return new BO.TasksEngineer { ID = id, Name = _dal.Engineer.Read(id)!.Name };
//        }
//        catch
//        {
//            throw new NotImplementedException();
//        }
//    }

//    private List<int> FindIdList(int TaskId)
//    {
//        return (from doDependence in _dal.Dependence.ReadAll()
//                where TaskId == doDependence.IDTask
//                select doDependence.IDPreviousTask)
//        .ToList();
//    }
//    private List<TaskInList> FindTaskList(int TaskId)
//    {

//        return FindIdList(TaskId)
//            .Select(id => _dal.Task.Read(id)!)
//            .Select(task => new TaskInList
//            {
//                ID = task.ID,
//                Nickname = task.Nickname!,
//                Description = task.Description!,
//                Status = FindStatus(task)
//            })
//            .ToList();
//    }

//    private BO.Status FindStatus(DO.Task doTask)
//    {
//        return (BO.Status)(doTask.EstimatedStartDate is null ? 0
//                            : doTask.AcualStartNate is null ? 1
//                            : doTask.AcualEndNate is null ? 2
//                            : 3);
//    }

//    private BO.MilestoneIdNickname findMilestone(int TaskId)
//    {
//        BO.MilestoneIdNickname? milestone = (from id in FindIdList(TaskId)
//                                             let task = _dal.Task.Read(id)!
//                                             where task.Milestone == true
//                                             select new BO.MilestoneIdNickname
//                                             {
//                                                 ID = id!,
//                                                 NickName = task.Nickname!
//                                             }).FirstOrDefault();
//        return milestone;
//    }
//}

