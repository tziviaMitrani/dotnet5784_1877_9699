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
            throw new Exception($"Engineer with such an ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? engineer = Read(id) ?? throw new Exception($"Engineer with such an ID={id} does not exist");
        DataSource.Engineers.Remove(engineer);
    }

    public Engineer? Read(int id)
    {
        Engineer? result = DataSource.Engineers.Find(Engineer => Engineer.Id == id);
        return result;
    }

    public List<Engineer> ReadAll()
    {
       return new List<Engineer>( DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Delete(item.Id);
        DataSource.Engineers.Add(item);
    }

    
}
