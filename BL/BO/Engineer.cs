using BlTest.BO;

namespace BO;
/// <summary>
/// Engineer main logical entity for Engineer List and Engineer Details screens.
/// </summary>
/// <param name="Id">Unique ID number</param>
/// <param name="Name">The name of the engineer</param>
/// <param name="Email">Email address of the engineer</param>
/// <param name="Level">Engineer level</param>
/// <param name="Cost">daily cost of the engineer, including salary, workplace, tools</param>
/// <param name="Task">Task-in-list helper entity for the list of task dependencies/milestones and for the task list screen</param>

public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public required TaskInEngineer Task { get; init; }

}
