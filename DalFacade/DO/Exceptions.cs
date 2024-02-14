namespace DO;

/// <summary>
/// Throwing an error according to the type of exception.
/// </summary>

[Serializable]
//exception for thomthing that does not exist
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}



[Serializable]
//exception for thomthing that already exist
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}


[Serializable]
//exception for thomthing that already deleted
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}


