namespace BO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID numberof the engineer</param>
/// <param name="Alias">The alias of the engineer in the task</param>
public class TaskInEngineer
{
    public TaskInEngineer(int id, string? alias)
    {
        Id = id;
        Alias = alias!;
    }

    public int Id { get; init; }
    public string Alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
