namespace BlImplementation;
using BlApi;

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

internal class EngineerInListImplementation : IEngineerInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;


    public BO.EngineerInList? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        return doEngineer == null
            ? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist")
        : new BO.EngineerInList(id, doEngineer.Name, doEngineer.Email, (BO.EngineerExperience)doEngineer.Level);
    }

    public IEnumerable<BO.EngineerInList> ReadAll(Func<DO.Engineer?, bool>? filter)
    {



            IEnumerable<DO.Engineer> doAllEngineers = _dal.Engineer.ReadAll(filter)!;
            IEnumerable<BO.EngineerInList?> engineerInLists = from doEngineer in doAllEngineers
                                                              select Read(doEngineer.Id);
            return engineerInLists!;



    }
}

