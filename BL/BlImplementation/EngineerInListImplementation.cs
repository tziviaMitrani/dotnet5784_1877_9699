namespace BlImplementation;

using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
internal class EngineerInListImplementation : IEngineerInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

   /* public BO.EngineerInList? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        return doEngineer == null
            ? throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist")
            : new EngineerInList(id, doEngineer.Name, doEngineer.Email, (DO.EngineerExperience)doEngineer.Level);

    }*/
    /*  public IEnumerable<BO.EngineerInList> ReadAll(Func<BO.EngineerInList?, bool>? filter = null)
      {
          try
          {
              IEnumerable<DO.EngineerInList> doEngineers = _dal.Engineer.ReadAll(filter)!;
              if (doEngineers == null)
                  throw new BO.BlDoesNotExistException("There are no engineer who meet the requirements.");
              IEnumerable<BO.EngineerInList> boEngineers = from doEngineer in doEngineers
                                                     select Read(doEngineer.Id);
              return boEngineers;
          }
          catch (Exception ex)
          {
              throw new BO.BlDoesNotExistException("There are no engineer who meet the requirements.", ex);
          }

          throw new NotImplementedException();
      }*/
    public BO.EngineerInList? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        return doEngineer == null
            ? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist")
        : new EngineerInList(id, doEngineer.Name, doEngineer.Email, (DO.EngineerExperience)doEngineer.Level);
    }
    public IEnumerable<BO.EngineerInList> ReadAll(Func<BO.EngineerInList?, bool>? filter = null)
    {
        try
        {
            Func<DO.Engineer, bool> filter2 = (Func<DO.Engineer, bool>)filter!;

            IEnumerable<DO.Engineer> doAllEngineers = _dal.Engineer.ReadAll(filter2)!;
            return from doEngineer in doAllEngineers
                   select Read(doEngineer.Id);
            //new BO.EngineerInList(doTask.Id, doTask.Description, doTask.Alias!, SetStatus(doTask));
        }
        catch
        {
            throw new BO.BlDoesNotExistException("There are no engineer who meet the requirements.");
        }

    }
}
