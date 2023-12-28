
namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
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
        throw new NotImplementedException();
    }

    public BO.Engineer? Read(int id)
    {

        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BlTest.BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = (from t in _dal.Task
                    let t= _dal.Task.ReadAll()
                    where t.Id=id
                    select t)

                    //(from DO.Student doStudent in _dal.Student.ReadAll()
                    // select new BO.StudentInList
                    // {
                    //     Id = doStudent.Id,
                    //     Name = doStudent.Name,
                    //     CurrentYear = (BO.Year)(DateTime.Now.Year - doStudent.RegistrationDate.Year)
                    // });

        /*(from t
                 let t=_dal.Task.ReadAll() 
                 where t.Id == id
                 select t)*/
    };
    }


public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool>? filter)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }
}
