namespace BlApi;

public interface ITaskInEngineer
{
    public BO.TaskInEngineer? Read(int id);//Reads entity object by its ID 
    public IEnumerable<BO.TaskInEngineer> ReadAll(Func<BO.TaskInEngineer?, bool>? filter = null);//Reads all entity objects
}