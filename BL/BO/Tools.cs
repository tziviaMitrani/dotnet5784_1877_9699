﻿using System.Reflection;
namespace BO;
using System.Reflection.Metadata.Ecma335;
//Tools- converts the data to print form on the screen
static class Tools
{
    public static string ToStringProperty<T>(T item)
    {
        string rst = "";
        foreach (PropertyInfo prop in item!.GetType().GetProperties())
        {
            rst += prop.Name;
            rst += " ";
            rst += item.GetType().GetProperty(prop.Name)?.GetValue(item);
            rst += "\n";
        }
        return rst;
    }
  
}
