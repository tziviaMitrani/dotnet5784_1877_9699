namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID number</param>
/// <param name="Description">Description of the task</param>
/// <param name="Alias">nickname of the task</param>
/// <param name="Milestone">Milestone (Boolean) - of the assignment</param>
/// <param name="ProductionDate">Production date of the task</param>
/// <param name="StartDate">Start Date of the task</param>
/// <param name="EstimatedCompletionDate">Estimated completion date of the task</param>
/// <param name="FinalDateCompletion">Final date for completion of the task</param>
/// <param name="ActualEndDate">Actual end date of the task</param>
/// <param name="product">Product(a string describing the product)</param>
/// <param name="Notes">Comments on the task</param>
/// <param name="Engineerid">The engineer ID assigned to the task</param>
/// <param name="Difficulty">Difficulty level of the task</param>
/// 

public record Task
(
        int Id,
        string Description,
        string? Alias,
        bool Milestone,
        DateTime ProductionDate,
        DateTime StartDate,
        DateTime EstimatedCompletionDate,
        DateTime FinalDateCompletion,
        DateTime? ActualEndDate,
        string? product,
        string? Notes,
        int Engineerid,
        EngineerExperience Difficulty
);

 

