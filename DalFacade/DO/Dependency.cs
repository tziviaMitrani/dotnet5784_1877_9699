namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">Dependency ID number</param>
/// <param name="DependentTask">ID number of pending task</param>
/// <param name="DependsOnTask">ID number of a previous assignment</param>



public record Dependency
(
    int Id,
    int? DependentTask,//DependentTask<-IdDependTask
    int? DependsOnTask//DependsOnTask<-IdPreviousDependTask

)
{
    public Dependency() : this(0, 0, 0) { } //empty ctor for stage 3
}




