namespace BO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID numberof the engineer</param>
/// <param name="Alias">The alias of the engineer in the task</param>
public class TaskInEngineer
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
