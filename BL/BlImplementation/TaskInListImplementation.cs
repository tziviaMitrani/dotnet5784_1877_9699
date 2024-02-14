namespace BlImplementation;
using BlApi;

using System;
using System.Collections.Generic;

internal class TaskInListImplementation: ITaskInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    private BO.Status SetStatus(DO.Task doTask)
    {
        return (BO.Status)(doTask.CreatedAtDate is null ? 1
                            : doTask.StartDate is null ? 2
                            : doTask.CompleteDate is null ? 3
                            : 4);
    }

    public BO.TaskInList? Read(int id)
    {
        try
        {
        DO.Task? doTask = _dal.Task.Read(id);
        return doTask == null
            ? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist")
            : new BO.TaskInList(id, doTask.Description, doTask.Alias!,SetStatus(doTask), (BO.EngineerExperience)doTask.Difficulty);
        }
        catch (BO.BlDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task  does Not exist", ex);
        }


    }

    public IEnumerable<BO.TaskInList> ReadAll(Func<DO.Task?, bool>? filter = null)
    {

        IEnumerable<DO.Task> doAllTasks = _dal.Task.ReadAll(filter)!;
        IEnumerable<BO.TaskInList> alltasks = from task in doAllTasks
                                     select Read(task.Id);
        return alltasks;
    }

   
}
