
namespace BlImplementation;
using BlApi;
using System;
using System.Collections.Generic;

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

        DO.Engineer? doStudent = _dal.Engineer.Read(id);
        if (doStudent == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doStudent.Name,
            Email = doStudent.Email,
            Level = (DO.EngineerExperience)doStudent.Level,
            BirthDate = doStudent.BirthDate,
            RegistrationDate = doStudent.RegistrationDate,
            CurrentYear = (BO.Year)(DateTime.Now.Year - doStudent.RegistrationDate.Year)
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
