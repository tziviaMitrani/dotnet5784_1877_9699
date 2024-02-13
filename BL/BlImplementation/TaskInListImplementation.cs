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
        DO.Task? doTask = _dal.Task.Read(id);
        return doTask == null
            ? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist")
            : new BO.TaskInList(id, doTask.Description, doTask.Alias!,SetStatus(doTask));
    }

    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList?, bool>? filter = null)
    {
        try
        {
            Func<DO.Task, bool> filter2 = (Func<DO.Task, bool>)filter!;

            IEnumerable<DO.Task> doAllTasks = _dal.Task.ReadAll(filter2)!;
            return from doTask in doAllTasks
                   select Read(doTask.Id);
            //new BO.TaskInList(doTask.Id, doTask.Description, doTask.Alias!, SetStatus(doTask));
        }
        catch
        {
            throw new BO.BlDoesNotExistException("There are no tasks who meet the requirements.");
        }

    }
}
