namespace BO;
internal class EngineerInList
{
    private DO.EngineerExperience level;

    public EngineerInList(int id, string name, string? email, DO.EngineerExperience level)
    {
        Id = id;
        Name = name;
        Email = email;
        this.level = level;
    }

    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerExperience Level { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
