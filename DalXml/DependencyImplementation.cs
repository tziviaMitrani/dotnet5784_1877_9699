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
    /// <summary>
    /// converter XElement to Dependency.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    static Dependency? getDependency(XElement d)
    {
        return d.ToIntNullable("Id") is null ? null : new Dependency()
        {
            Id = (int)d.Element("Id")!,
            DependentTask = (int?)d.Element("DependentTask")!,
            DependsOnTask = (int?)d.Element("DependsOnTask")!
        };
    }
    /// <summary>
    /// converter Dependency to IEnumerable<XElement>.
    /// </summary>
    /// <param name="dependency"></param>
    /// <returns></returns>
    static IEnumerable<XElement> createDependencyElement(Dependency dependency)
    {
        yield return new XElement("Id", dependency.Id);
        if (dependency.DependentTask is not null)
            yield return new XElement("DependentTask", dependency.DependentTask);
        if (dependency.DependsOnTask is not null)
            yield return new XElement("DependsOnTask", dependency.DependsOnTask);
    }

    /// <summary>
    /// create Dependency Element
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(Dependency item)
    {
        XElement? list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        int id = Config.NextDependencyId;
        Dependency newItem = item with { Id = id };
        list.Add(new XElement("Dependency", createDependencyElement(newItem)));
        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
        return newItem.Id;
    }

    /// <summary>
    /// Delete dependency by id.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        XElement? list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        (list.Elements()
            .FirstOrDefault(st => (int?)st.Element("Id") == id) ?? throw new DalDoesNotExistException($"Dependency with such an ID={id} does not exist"))
            .Remove();

        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
    }

    /// <summary>
    /// Read dependency by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Dependency? Read(int id)
    {
        return (Dependency)getDependency(XMLTools.LoadListFromXMLElement(FILEDEPENDENCY)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("Id") == id) ?? throw new DalDoesNotExistException($"Dependency with such an ID={id} does not exist"))!;
    }

    /// <summary>
    /// Read dependency by Func<Dependency, bool> filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return (Dependency)XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s)).FirstOrDefault(filter!)!;
    }

    /// <summary>
    /// Read all dependencies.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        return filter is null
        ? XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s))
        : XMLTools.LoadListFromXMLElement(FILEDEPENDENCY).Elements().Select(s => getDependency(s)).Where(filter!);

    }

    /// <summary>
    /// update dependency.
    /// </summary>
    /// <param name="item"></param>
    public void Update(Dependency item)
    {
        Delete(item.Id);
        XElement? list = XMLTools.LoadListFromXMLElement(FILEDEPENDENCY);
        list.Add(new XElement("Dependency", createDependencyElement(item)));
        XMLTools.SaveListToXMLElement(list, FILEDEPENDENCY);
    }
}
