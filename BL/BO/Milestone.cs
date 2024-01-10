using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// Milestone main logical entity for Milestone Details screen.
/// </summary>
/// <param name="Id">Unique ID number</param>
/// <param name="Description">Description of the task</param>
/// <param name="Alias">nickname of the task</param>
/// <param name="CreatedAtDate">Date when the task was added to the system</param>
/// <param name="Status">calculated</param>
/// <param name="StartDate">Start Date of the task</param>
/// <param name="DeadlineDate">the latest complete date</param>
/// <param name="CompleteDate">real completion date</param>
/// <param name="CompletionPercentage">percentage of completed tasks - calculated</param>
/// <param name="ForecastDate">a revised scheduled completion date</param>
/// <param name="Remarks">free remarks from project meetings</param>
/// <param name="Dependencies">Dependency of task in list</param>
public class Milestone
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public Status status { get; set; }
    public required DateTime? StartDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double CompletionPercentage { get; set; }
    public required string Remarks { get; set; }
    public required List<TaskInList> Dependencies { get; set; }


}
