namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        Dependency newItem = item with { Id = DataSource.Config.NextDependencyId };
        DataSource.Dependencys.Add(newItem);
        return newItem.Id;
    }

    public void Delete(int id)
    {
        Dependency? Dependency = Read(id) ?? throw new DalDoesNotExistException($"Dependency with such an ID={id} does not exist");
        DataSource.Dependencys.RemoveAll(dep=>dep.Id== id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? Dependency = DataSource.Dependencys?.FirstOrDefault(filter);
        return Dependency;
    }

    public Dependency? Read(int id)
    {
        Dependency? result = DataSource.Dependencys.FirstOrDefault(Dependency => Dependency.Id == id);
        return result;
    }

  
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependencys.Select(item => item);
        else
            return DataSource.Dependencys.Where(filter);
    }

        public void Update(Dependency item)
    {
        Delete(item.Id);
        DataSource.Dependencys.Add(item);
    }
    
}
