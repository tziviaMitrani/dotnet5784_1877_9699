namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">Dependency ID number</param>
/// <param name="IdDependTask">ID number of pending task</param>
/// <param name="IdPreviousDependTask">ID number of a previous assignment</param>



public record Dependency
(
    int Id,
    int IdDependTask,
    int IdPreviousDependTask

)
{
    public Dependency() : this(0, 0, 0) { } //empty ctor for stage 3
}




