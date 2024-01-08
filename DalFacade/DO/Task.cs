namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID number</param>
/// <param name="Description">Description of the task</param>
/// <param name="Alias">nickname of the task</param>
/// <param name="Milestone">Milestone (Boolean) - of the assignment</param>
/// <param name="CreatedAtDate">Production date of the task</param>
/// <param name="StartDate">Start Date of the task</param>
/// <param name="ScheduledDate">Estimated completion date of the task</param>
/// <param name="DeadlineDate">Final date for completion of the task</param>
/// <param name="CompleteDate">Actual end date of the task</param>
/// <param name="Deliverables">Deliverables(a string describing the product)</param>
/// <param name="Remarks">Comments on the task</param>
/// <param name="Engineerid">The engineer ID assigned to the task</param>
/// <param name="Difficulty">Difficulty level of the task</param>
/// 

public record Task
(
        int Id,
        string Description,
        string? Alias,
        bool Milestone,
        DateTime? CreatedAtDate,//CreatedAtDate<-ProductionDate
        TimeSpan? RequiredEffortTime,
        DateTime? StartDate,
        DateTime? ScheduledDate,//ScheduledDate<-EstimatedCompletionDate
        DateTime? DeadlineDate,//DeadlineDate<-FinalDateCompletion
        DateTime? CompleteDate,//CompleteDate<-ActualEndDate
        string? Deliverables,//product,
        string? Remarks,//Notes,
        int? Engineerid,
        EngineerExperience Difficulty
)

{
    public Task() : this(0, "", "", false, null,null, null, null, null, null, "", "", 0, 0) { } //empty ctor for stage 3
}





