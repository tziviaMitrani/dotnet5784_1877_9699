﻿namespace BlApi;
public interface ITaskInList
{
    public BO.TaskInList? Read(int id);//Reads entity object by its ID 
    public IEnumerable<BO.TaskInList> ReadAll(Func<DO.Task?, bool>? filter = null);//Reads all entity objects
   
}
