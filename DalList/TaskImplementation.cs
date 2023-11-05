namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {

        if (Read(item.Id) is not null)
            throw new Exception($"Task with such an ID={item.Id} already exists");
        DataSource.Tasks.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Task? Task = Read(id) ?? throw new Exception($"Task with such an ID={id} does not exist");
        DataSource.Tasks.Remove(Task);
    }

    public Task? Read(int id)
    {
        Task? result = DataSource.Tasks.Find(Task => Task.Id == id);
        return result;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Delete(item.Id);
        DataSource.Tasks.Add(item);
    }
}
