
namespace BlImplementation;
using BlApi;
using System;
using System.Collections.Generic;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Engineer?, bool>? filter)
    {
        throw new NotImplementedException();
    }



    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}
