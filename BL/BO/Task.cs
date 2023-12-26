using BlTest.BO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Task
{
    public int Id { get; init; }
    public required string  Description { get; set; }
    public required string Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public Status status { get; set; }
    public List<TaskInList> Dependencies { get; set; }
    public MilestoneInTask Milestone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? Deliverables { get; set; }
    public string Remarks { get; set; }
    public Engineer Engineer { get; set;}

    /*
    
       
        DateTime? ScheduledDate,
        DateTime? DeadlineDate,
        DateTime? CompleteDate,
        string? product,
        string? Notes,
        int? Engineerid,
        int Difficulty*/

}
