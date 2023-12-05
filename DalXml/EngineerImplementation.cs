namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    const string FILEENGINEER = "engineers";

    /// <summary>
    /// create engineer Element
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(Engineer item)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        list.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(list, FILEENGINEER);
        return item.Id;
    }
    /// <summary>
    /// Delete engineer by id.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER) ?? throw new Exception($"Task with such an ID={id} does not exist");
        list.RemoveAll(l => l!.Id == id);
        XMLTools.SaveListToXMLSerializer<Engineer>(list, FILEENGINEER);
    }

    /// <summary>
    /// Read engineer by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Engineer? Read(int id)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        return list.FirstOrDefault(Engineer => Engineer!.Id == id);
    }

    /// <summary>
    /// Read engineer by Func<Dependency, bool> filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        return list.FirstOrDefault(filter!);
    }

    /// <summary>
    /// Read all engineers.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        if (filter == null)
            return list.Select(item => item);
        else
            return list.Where(filter!);
    }

    /// <summary>
    /// update engineer.
    /// </summary>
    /// <param name="item"></param>
    public void Update(Engineer item)
    {
        Delete(item.Id);
        List<Engineer?> list = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        list.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(list, FILEENGINEER);
    }
}
