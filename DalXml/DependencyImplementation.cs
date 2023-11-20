using System.Linq;

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency
{

    const string FILEDEPENDENCY ="dependencies";
    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
    public int Create(Dependency item)
    {
        XElement list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        list.Add(item);
        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
        return item.Id;
    }

    public void Delete(int id)
    {
        XElement list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        (list.Elements()
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new DalDoesNotExistException($"Dependency with such an ID={id} does not exist"))
            .Remove();

        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
    }

    static DO.Dependency? getDependency(XElement d)
    {
        return d.ToIntNullable("Id") is null ? null : new DO.Dependency()
        {
            Id = (int)d.Element("Id")!,
            IdDependTask = (int)d.Element("IdDependTask")!,
            IdPreviousDependTask = (int)d.Element("IdPreviousDependTask")!
        };
    }

    public Dependency? Read(int id)
    {
        return (DO.Dependency)getDependency(XMLTools.LoadListFromXMLElement(FILEDEPENDENCY)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id) ?? throw new DalDoesNotExistException($"Dependency with such an ID={id} does not exist"))!;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return (DO.Dependency)XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s)).FirstOrDefault(filter)!;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        return filter is null
        ? XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s))
        : XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s)).Where(filter);

    }

    public void Update(Dependency item)
    {
        XElement list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        Delete(item.Id);
        list.Add(item);
    }
}
