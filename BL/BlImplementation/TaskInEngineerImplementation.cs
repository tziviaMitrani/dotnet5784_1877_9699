namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
internal class TaskInEngineerImplementation : ITaskInEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.TaskInEngineer? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        return doTask == null
            ? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist")
            : new BO.TaskInEngineer(id, doTask.Alias!);
    }

    public IEnumerable<BO.TaskInEngineer> ReadAll(Func<BO.TaskInEngineer?, bool>? filter = null)
    {
        try
        {
            Func<DO.Task, bool> filter2 = (Func<DO.Task, bool>)filter!;

            IEnumerable<DO.Task> doAllTasks = _dal.Task.ReadAll(filter2)!;
            return from doTask in doAllTasks
                   select new BO.TaskInEngineer(doTask.Id, doTask.Alias);
        }
        catch
        {
            throw new BO.BlDoesNotExistException("There are no tasks who meet the requirements.");
        }
        
    }
}
