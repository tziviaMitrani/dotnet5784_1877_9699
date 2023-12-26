
using BlTest.BO;

namespace BO;
/// <summary>
/// Logical helper entity of a task milestone
/// </summary>
/// /// <param name="Id">Unique ID number</param>
/// <param name="Alias">nickname of the task</param>
/// <param name="CreatedAtDate">Date when the task was added to the system</param>
/// <param name="Status">calculated</param>
/// <param name="CompletionPercentage">percentage of completed tasks - calculated</param>

public class MilestoneInTask
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public Status status { get; set; }
    public double CompletionPercentage { get; set; }

}
