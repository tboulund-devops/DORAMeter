namespace Models;

public class Repository
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Repository() : this(0, string.Empty)
    { }

    public Repository(int id, string name)
    {
        Id = id;
        Name = name;
    }
}