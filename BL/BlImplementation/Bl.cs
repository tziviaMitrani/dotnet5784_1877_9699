
namespace BlImplementation;
using BlApi;
using BO;

internal class Bl : IBl
{
    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    public ITaskInEngineer TaskInEngineer => new TaskInEngineerImplementation();
    public IEngineerInList EngineerInList => new EngineerInListImplementation();
    public ITaskInList TaskInList => new TaskInListImplementation();

    //public IMilestone milestone =>  new MilestoneImplementation();
}
