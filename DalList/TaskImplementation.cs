namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        DO.Task newItem = item with { Id = DataSource.Config.NextTaskId };
        DataSource.Tasks.Add(newItem);
        return newItem.Id;
    }

    public void Delete(int id)
    {
        DO.Task? Task = Read(id) ?? throw new DalDoesNotExistException($"Task with such an ID={id} does not exist");
        DataSource.Tasks.RemoveAll(task => task.Id == id);
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        DO.Task? task = DataSource.Tasks?.FirstOrDefault(filter);
        return task;
    }

    public DO.Task? Read(int id)
    {
        DO.Task? result = DataSource.Tasks.FirstOrDefault(Task => Task.Id == id);
        return result;
    }

    public IEnumerable<DO.Task> ReadAll(Func<DO.Task, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }

    public void Update(DO.Task item)
    {
        Delete(item.Id);
        DataSource.Tasks.Add(item);
    }

}
