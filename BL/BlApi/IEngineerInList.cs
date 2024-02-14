namespace BlApi;
public interface IEngineerInList
{
    public BO.EngineerInList? Read(int id);//Reads entity object by its ID 
    public IEnumerable<BO.EngineerInList> ReadAll(Func<DO.Engineer?, bool>? filter = null);//Reads all entity objects
//  public IEnumerable<BO.EngineerInList?> ReadAll(Func<DO.Engineer?, bool>? filter = null);//Reads all entity objects
}