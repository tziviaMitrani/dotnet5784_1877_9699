namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        Dependency newItem = item with { Id = DataSource.Config.NextDependencyId };
        DataSource.Dependencys.Add(newItem);
        return newItem.Id;
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
    
}
