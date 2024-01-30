namespace BlImplementation;
using BlApi;
using System.Collections.Generic;
using System.Reflection.Emit;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new 
            (boEngineer.Id, boEngineer.Name!, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);

        }
    }


    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        DateTime now = DateTime.Now;
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
            DO.Task task = (from DO.Task doTask1 in _dal.Task.ReadAll()
                        where doTask1.Engineerid == id
                        select doTask1).FirstOrDefault()!;
        if (task.StartDate > now)
        {
            Delete(id);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} can not remove");
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        DO.Task? doTask = _dal.Task.Read(id);
        BO.TaskInEngineer? boTaskInEngineer = (doTask != null) ? new BO.TaskInEngineer { Id = id, Alias = doTask.Alias! } : null;
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = boTaskInEngineer!
        };
    }


    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer?, bool>? filter=null)
    {
        try
        {
            IEnumerable<DO.Engineer> doEngineers = _dal.Engineer.ReadAll(filter)!;
            if (doEngineers == null)
                throw new BO.BlDoesNotExistException("There are no engineer who meet the requirements.");
            IEnumerable<BO.Engineer> boEngineers = from doEngineer  in doEngineers
                                                  select Read(doEngineer.Id);
            return boEngineers;
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException("There are no engineer who meet the requirements.", ex);
        }


    }

    public void Update(BO.Engineer item)
    {
        Delete(item.Id);
        Update(item);
    }
}


/*      DO.Task? doTask = _dal.Task.Read(Id)
BO.TaskInEngineer? boTaskInEngineer = (doTask != null) ? new BO.TaskInEngineer { Id = id, Alias = doTask.Alias! } : null;

IEnumerable<BO.Engineer> doEngineers = from doEngineer in _dal.Engineer.ReadAll()
                                       select new BO.Engineer()
                                       {
                                           Id = doEngineer.Id,
                                           Name = doEngineer.Name,
                                           Email = doEngineer.Email,
                                           Level = (BlTest.BO.EngineerExperience)doEngineer.Level,
                                           Cost = doEngineer.Cost,

                                       };*/