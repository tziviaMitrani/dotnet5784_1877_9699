namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    const string FILEDEPENDENCY = "dependencies";
    static DO.Dependency? getDependency(XElement d)
    {
        return d.ToIntNullable("Id") is null ? null : new DO.Dependency()
        {
            Id = (int)d.Element("Id")!,
            IdDependTask = (int?)d.Element("IdDependTask")!,
            IdPreviousDependTask = (int?)d.Element("IdPreviousDependTask")!
        };
    }
    static IEnumerable<XElement> createDependencyElement(Dependency dependency)
    {
        yield return new XElement("Id", dependency.Id);
        if (dependency.IdDependTask is not null)
            yield return new XElement("IdDependTask", dependency.IdDependTask);
        if (dependency.IdPreviousDependTask is not null)
            yield return new XElement("IdPreviousDependTask", dependency.IdPreviousDependTask);
    }
    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
    public int Create(Dependency item)
    {
        XElement? list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        int id = Config.NextDependencyId;
        Dependency newItem = item with { Id = id };
        list.Add(new XElement("Dependency", createDependencyElement(newItem)));
        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
        return newItem.Id;
    }

    public void Delete(int id)
    {
        XElement? list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        (list.Elements()
            .FirstOrDefault(st => (int?)st.Element("Id") == id) ?? throw new DalDoesNotExistException($"Dependency with such an ID={id} does not exist"))
            .Remove();

        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
    }

    

    public Dependency? Read(int id)
    {
        return (Dependency)getDependency(XMLTools.LoadListFromXMLElement(FILEDEPENDENCY)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("Id") == id) ?? throw new DalDoesNotExistException($"Dependency with such an ID={id} does not exist"))!;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return (Dependency)XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s)).FirstOrDefault(filter!)!;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        return filter is null
        ? XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s))
        : XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s)).Where(filter!);

    }

    public void Update(Dependency item)
    {
        Delete(item.Id);
        XElement? list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        list.Add(new XElement("Dependency", createDependencyElement(item)));
        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
    }
}
