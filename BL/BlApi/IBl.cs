using BlImplementation;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ITaskInEngineer TaskInEngineer { get; }

    //public IMilestone milestone { get; }
}
