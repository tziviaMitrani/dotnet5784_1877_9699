namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{

    const string FILETASK = "tasks";
    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
    public int Create(Task item)
    {
        int id= Config.NextTaskId;
        Task newTask= item with { Id = id };    
        List<Task?> list = XMLTools.LoadListFromXMLSerializer<Task>(FILETASK);
        list.Add(newTask);
        XMLTools.SaveListToXMLSerializer<Task>(list,FILETASK);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Task?> list = XMLTools.LoadListFromXMLSerializer<Task>(FILETASK) ?? throw new Exception($"Task with such an ID={id} does not exist");
        list.RemoveAll(l => l!.Id == id);
        XMLTools.SaveListToXMLSerializer<Task>(list, FILETASK);

    }

    public Task? Read(int id)
    {
        List<Task?> list = XMLTools.LoadListFromXMLSerializer<Task>(FILETASK);
        return list.FirstOrDefault(Task => Task!.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task?> list = XMLTools.LoadListFromXMLSerializer<Task>(FILETASK);
        return list.FirstOrDefault(filter!);
        
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task?> list = XMLTools.LoadListFromXMLSerializer<Task>(FILETASK);
        if (filter == null)
            return list.Select(item => item);
        else
            return list.Where(filter!);
    }

    public void Update(Task item)
    {
        Delete(item.Id);
        List<Task?> list = XMLTools.LoadListFromXMLSerializer<Task>(FILETASK);
        list.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(list, FILETASK);
    }
}
