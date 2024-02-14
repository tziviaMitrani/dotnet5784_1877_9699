using DalApi;

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
            if (boEngineer.Task is not null && boEngineer.Task.Id != 0)
                try
                {
                    _dal.Task.Update(_dal.Task.Read(boEngineer.Task.Id)! with { Engineerid = boEngineer.Id });
                }
                catch (DO.DalDoesNotExistException ex)
                { throw new BO.BlDoesNotExistException($"Task with ID={boEngineer.Id} does not exist", ex); }
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
        try
        {
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);
            DO.Task task = (from DO.Task doTask1 in _dal.Task.ReadAll()
                            where doTask1.Engineerid == id
                            select doTask1).FirstOrDefault()!;
            if (task is null)
            {
                _dal.Engineer.Delete(id);
            }
            else
            {
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} can not remove");
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", ex);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        DO.Task? doTask = _dal.Task.Read(id);
        BO.TaskInEngineer? boTaskInEngineer = (doTask != null) ? new(doTask.Id, doTask.Alias) : null;
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
        DO.Task? engineersTask = (from task in allTasks
                                  where task.Engineerid == id
                                  select task).FirstOrDefault();
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            /*Task = new BO.TaskInEngineer(0,"")*/
            Task = engineersTask is not null ? new BO.TaskInEngineer(engineersTask.Id, engineersTask.Alias) : new BO.TaskInEngineer(0, ""),
        };
    }


    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer?, bool>? filter = null)
    {
        try
        {
            IEnumerable<DO.Engineer> doEngineers = _dal.Engineer.ReadAll(filter)!;
            if (doEngineers == null)
                throw new BO.BlDoesNotExistException("There are no engineer who meet the requirements.");
            IEnumerable<BO.Engineer> boEngineers = from doEngineer in doEngineers
                                                   select Read(doEngineer.Id);
            return boEngineers;
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException("There are no engineer who meet the requirements.", ex);
        }


    }

    public void Update(BO.Engineer boEngineer)
    {

        if (boEngineer.Task is not null&& boEngineer.Task.Id != 0)
            try
            {
                DO.Task? task = _dal.Task.Read(boEngineer.Task.Id);
                if (task is null)
                {
                    throw new BO.BlDoesNotExistException($"Task  dos't exist");
                }
                
                _dal.Task.Update(task! with { Engineerid = boEngineer.Id });
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Engineer number {boEngineer.Id} dos't exist",ex);
            }
        DO.Engineer doEngineer = new
           (boEngineer.Id, boEngineer.Name!, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        _dal.Engineer.Update(doEngineer);
    }
}




