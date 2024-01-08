using BlTest.BO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID number</param>
/// <param name="Description">Description of the task</param>
/// <param name="Alias">nickname of the task</param>
/// <param name="Status">calculated</param>
/// <param name="Milestone">calculated when building schedule, populated if there is milestone in dependency, relevant only after schedule is built</param>
/// <param name="Dependencies">Dependency of task in lisl</param>
/// <param name="CreatedAtDate">Date when the task was added to the system</param>
/// <param name="StartDate">Start Date of the task</param>
/// <param name="ScheduledDate">he planned start date</param>
/// <param name="ForecastDate"> calcualed planned completion date</param>
/// <param name="DeadlineDate">the latest complete date</param>
/// <param name="CompleteDate">real completion date</param>
/// <param name="Deliverables">description of deliverables for MS copmletion</param> 
/// <param name="product">Product(a string describing the product)</param>
/// <param name="Remarks">free remarks from project meetings</param>
/// <param name="Copmlexity">minimum expirience for engineer to assign</param>
/// <param name="Engineer">The engineer ID assigned to the task</param>

public class Task
{
    public int Id { get; init; }
    public required string  Description { get; set; }
    public required string Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public Status status { get; set; }
    public required List<TaskInList> Dependencies { get; set; }
    public required MilestoneInTask Milestone { get; set; }
    public required DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public required string Deliverables { get; set; }
    public required string Remarks { get; set; }
    public required EngineerInTask Engineer { get; set;}
    public EngineerExperience Copmlexity { get; set; }


   

}
