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
    public DateTime? ProductionDate { get; set; }
    public Status status { get; set; }
    public List<TaskInList> Dependencies { get; set; }
    public MilestoneInTask Milestone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EstimatedCompletionDate { get; set; }


    /*
    ,
       
        DateTime? EstimatedCompletionDate,
        DateTime? FinalDateCompletion,
        DateTime? ActualEndDate,
        string? product,
        string? Notes,
        int? Engineerid,
        int Difficulty*/

}
