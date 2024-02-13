namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer item);//Creates new entity object.
    public BO.Engineer? Read(int id);//Reads entity object by its ID 
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer? , bool>? filter=null);//Reads all entity objects
    public void Update(BO.Engineer item);//Updates entity object
    public void Delete(int id);//Deletes an object by its Id
}
