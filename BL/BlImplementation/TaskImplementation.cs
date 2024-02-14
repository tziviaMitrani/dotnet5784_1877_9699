namespace BlImplementation;
using BlApi;
using System.Collections.Generic;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        DO.Task doTask = ConversionToDo(boTask);

        try
        {
            if (_dal.Engineer.Read(boTask.Engineer.Id) == null)
            {
                throw new BO.BlDoesNotExistException("Engineer was not found");
            }
            _dal.Task.Create(doTask);

        }
        catch(DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task number {boTask.Id} exists",ex);
        }
        return boTask.Id;
    }



    public IEnumerable<BO.Task> ReadAll(Func<DO.Task?, bool>? filter = null)
    {

        IEnumerable<DO.Task> doAllTasks = _dal.Task.ReadAll(filter)!;
        IEnumerable<BO.Task> tasks = from task in doAllTasks
                                     select Read(task.Id);
        return tasks;
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
            if (_dal.Engineer.Read(boTask.Engineer.Id) == null)
            {
                throw new BO.BlDoesNotExistException("Engineer was not found");
            }
            _dal.Task.Update(ConversionToDo(boTask));
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task number {boTask.Id} dos't exist",ex);
        }
    }


    private BO.Status SetStatus(DO.Task doTask)
    {

            return (BO.Status)(doTask.CompleteDate is not null ? 4
            : doTask.DeadlineDate < DateTime.Now ? 3
            : doTask.StartDate < DateTime.Now ? 2
            : 1);
       
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
            Engineer = new BO.EngineerInTask(),
            Complexity = (BO.EngineerExperience)doTask.Difficulty
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
               (DO.EngineerExperience)boTask.Complexity);
    }
}





