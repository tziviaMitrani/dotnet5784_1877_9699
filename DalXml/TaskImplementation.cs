namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{

    const string FILETASK = @"..\..\xml\tasks.xml";
    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
    public int Create(Task item)
    {
        List<Task?> l = XMLTools.LoadListFromXMLSerializer<Task>(FILETASK);
        l.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(l,FILETASK);
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
