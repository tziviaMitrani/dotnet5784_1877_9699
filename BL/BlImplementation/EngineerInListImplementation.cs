namespace BlImplementation;

using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
internal class EngineerInListImplementation : IEngineerInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.EngineerInList? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        return doEngineer == null
            ? throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist")
            : new EngineerInList(id, doEngineer.Name, doEngineer.Email, (DO.EngineerExperience)doEngineer.Level);

    }
    public IEnumerable<BO.EngineerInList> ReadAll(Func<DO.Engineer?, bool>? filter = null)
    {
        try
        {
            IEnumerable<DO.Engineer> doEngineers = _dal.Engineer.ReadAll(filter)!;
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
    }
}
