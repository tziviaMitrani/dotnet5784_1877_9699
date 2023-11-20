namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    const string FILEENGINEER = "engineers";

    public int Create(Engineer item)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        list.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(list, FILEENGINEER);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER) ?? throw new Exception($"Task with such an ID={id} does not exist");
        list.RemoveAll(l => l.Id == id);
        XMLTools.SaveListToXMLSerializer<Engineer>(list, FILEENGINEER);
    }

    public Engineer? Read(int id)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        return list.FirstOrDefault(Engineer => Engineer.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        return list.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        if (filter == null)
            return list.Select(item => item);
        else
            return list.Where(filter);
    }



    public void Update(Engineer item)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        Delete(item.Id);
        list.Add(item);
    }
}
