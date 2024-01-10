namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with such an ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? engineer = Read(id) ?? throw new DalDoesNotExistException($"Engineer with such an ID={id} does not exist");
        DataSource.Engineers.RemoveAll(eng => eng.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        Engineer? engineer = DataSource.Engineers?.FirstOrDefault(filter);
        return engineer;
    }

    public Engineer? Read(int id)
    {
        Engineer? result = DataSource.Engineers.FirstOrDefault(Engineer => Engineer.Id == id);
        return result;
    }


    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    public void Update(Engineer item)
    {
        Delete(item.Id);
        DataSource.Engineers.Add(item);
    }

    
}
