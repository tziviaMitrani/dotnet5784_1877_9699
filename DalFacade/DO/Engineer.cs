namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID number</param>
/// <param name="Name">The name of the engineer</param>
/// <param name="Email">Email address of the engineer</param>
/// <param name="Level">Engineer level</param>
/// <param name="Cost">Hourly cost of the engineer</param>
/// 

public record Engineer
(
    int Id,
    string Name,
    string? Email=null,
    EngineerExperience Level = EngineerExperience.AdvancedBeginner,
    double Cost=0

)

{
    public Engineer() : this(0,"") { } //empty ctor for stage 3
}

