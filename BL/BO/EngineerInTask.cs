namespace BO;

/// <summary>
/// Auxiliary entity engineer-on-task, receiving partial information about an engineer on task.
/// </summary>
/// <param name="Id">Unique ID numberof the engineer</param>
/// <param name="Name">The name of the engineer in the task</param>
public class EngineerInTask
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);


}
