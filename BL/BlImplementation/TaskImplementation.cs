namespace BlImplementation;
using BlApi;
using System.Collections.Generic;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        if (boTask.Id <= 0 || boTask.Alias == "")
            throw new Exception("task details are not valid");
        try
        {
            DO.Task doTask = ConversionToDo(boTask);
        }
        catch
        {
            throw new BO.BlAlreadyExistsException($"Task number {boTask.Id} exists");
        }
        return boTask.Id;
    }


    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter = null)
    {
        Func<DO.Task, bool> filter1 = (Func<DO.Task, bool>)filter!;
        IEnumerable<DO.Task> doAllTasks = _dal.Task.ReadAll(filter1)!;
        return from doTask in doAllTasks
               select ConversionToBo(doTask);
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }
        catch
        {
            throw new BO.BlDoesNotExistException($"Task number {id} dos't exist");
        }
    }

    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task? myTask = _dal.Task.Read(id);
            return ConversionToBo(myTask!);
        }
        catch
        {
            throw new BO.BlDoesNotExistException($"Task number {id} dos't exist");
        }
    }

    public void Update(BO.Task boTask)
    {
        try
        {
            _dal.Task.Update(ConversionToDo(boTask));
        }
        catch
        {
            throw new BO.BlDoesNotExistException($"Task number {boTask.Id} dos't exist");
        }
    }

/*    private BO.EngineerInTask? CreateEngineerInTask(int Engineerid)
    {
        return new BO.EngineerInTask() { Id = Engineerid, Name = _dal.Engineer.Read(Engineerid)!.Name };
    }*/

    private BO.Status SetStatus(DO.Task doTask)
    {
        return (BO.Status)(doTask.CreatedAtDate is null ? 0
                            : doTask.StartDate is null ? 1
                            : doTask.CompleteDate is null ? 2
                            : 3);
    }

    private BO.Task ConversionToBo(DO.Task doTask)
    {
        return new BO.Task()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias!,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = SetStatus(doTask),
            Dependencies = new List<BO.TaskInList>() { },
            StartDate = doTask.StartDate,
            ScheduledDate = doTask.ScheduledDate,
            ForecastDate = doTask.ForecastDate,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverables = doTask.Deliverables!,
            Remarks = doTask.Remarks!,
            Engineer = null,
            Copmlexity = (BO.EngineerExperience)doTask.Difficulty
        };
    }
    private DO.Task ConversionToDo(BO.Task boTask)
    {
        return new DO.Task(
               boTask.Id,
               boTask.Description,
               boTask.Alias,
               //boTask.Milestone != null ? true : false,
               boTask.CreatedAtDate,
               null,
               boTask.StartDate,
               boTask.ScheduledDate,
               boTask.ForecastDate,
               boTask.DeadlineDate,
               boTask.CompleteDate,
               boTask.Deliverables,
               boTask.Remarks,
               boTask.Engineer?.Id,
               (DO.EngineerExperience)boTask.Copmlexity);
    }
}





