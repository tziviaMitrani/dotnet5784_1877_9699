namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Dependency with such an ID={item.Id} already exists");
        DataSource.Dependencys.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Dependency? Dependency = Read(id) ?? throw new Exception($"Dependency with such an ID={id} does not exist");
        DataSource.Dependencys.Remove(Dependency);
    }

    public Dependency? Read(int id)
    {
        Dependency? result = DataSource.Dependencys.Find(Dependency => Dependency.Id == id);
        return result;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    public void Update(Dependency item)
    {
        Delete(item.Id);
        DataSource.Dependencys.Add(item);
    }
    public void Show(Dependency item)
    {
        Console.WriteLine($"ID: {0}, ID of depend task: {1}, ID of previous task: {2}\n", item.Id, item.IdDependTask,item.IdPreviousDependTask);
    }
}
