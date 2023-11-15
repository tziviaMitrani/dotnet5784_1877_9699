namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency
{

    const string FILEDEPENDENCY = @"..\..\xml\dependencies.xml";
    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
    public int Create(Dependency item)
    {
        List<Dependency?> l = XMLTools.LoadListFromXMLSerializer<Dependency>(FILEDEPENDENCY);
        l.Add(item);
        XMLTools.SaveListToXMLSerializer<Dependency>(l, FILEDEPENDENCY);
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
