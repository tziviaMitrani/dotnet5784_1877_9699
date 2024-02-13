namespace BO;
/// <summary>
/// Task-in-list helper entity for the list of task dependencies/milestones and for the task list screen
/// </summary>
/// <param name="Id">Unique ID numberof the engineer</param>
/// <param name="Description">Description of the task</param>
/// <param name="Status">calculated</param>
/// <param name="Alias">The alias of the engineer in the task</param>
public class TaskInList
{


    public TaskInList(int id,  string description, string alias, BO.Status status)
    {
        Id = id;
        Description = description;
        Alias = alias;
        Status = status;
    }
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public BO.Status Status { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
