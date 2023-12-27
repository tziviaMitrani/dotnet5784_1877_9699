namespace BlImplementation;
using BlApi;


internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;

    public BO.Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Milestone Update(BO.Milestone item)
    {
        throw new NotImplementedException();
    }
}
