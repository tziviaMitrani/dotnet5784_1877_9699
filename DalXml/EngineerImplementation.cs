namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class EngineerImplementation : IEngineer
{
    const string FILEENGINEER = @"..\..\xml\engineers.xml";
    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
    public int Create(Engineer item)
    {
        List<Engineer?> l = XMLTools.LoadListFromXMLSerializer<Engineer>(FILEENGINEER);
        l.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(l, FILEENGINEER);
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
