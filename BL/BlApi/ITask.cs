﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);//Creates new entity object.
    public BO.Task? Read(int id);//Reads entity object by its ID 
    public IEnumerable<BO.Task> ReadAll(Func<BO.Engineer?, bool>? filter);//Reads all entity objects
    public void Update(BO.Task item);//Updates entity object
    public void Delete(int id);//Deletes an object by its Id
}