
namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public IEngineer Engineer =>  new EngineerImplementation();

    //public ITask Task => new MilestoneImplementation();

    //public IMilestone milestone =>  new TaskImplementation();
}
