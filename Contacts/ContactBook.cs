namespace Contacts;

public sealed record class FullName(string First, string Last, string? Middle) 
{
    public override string ToString()
        => Middle is null
            ? $"{First} {Last}"
            : $"{First} {Middle} {Last}";
}
public sealed record class Student(FullName name, string Major, string grad_year) : IContact
{
    private static int _nextID = 1;
    public int ID { get; init; } = _nextID++;

}
public sealed record class Employee(FullName name, string Department, string Company_Name) : IContact{ 
    private static int _nextID = 1;
    public int ID { get; init; } = _nextID++;
}
public sealed record class ContactBase(FullName name) {
    private static int next_ID = 1;
    public int ID { get; init; } = next_ID++;
    public IReadOnlyList<string> Emails { get; init; } = new List<string>();
}
public class ContactBook
{
    private HashSet<IContact> Entities { get; } = [];

    public bool Add(IContact item)
    {
        return Entities.Add(item);
    }

    public bool Remove(IContact item)
    {
        return Entities.Remove(item);
    }

    public bool Contains(IContact item)
    {
        
        return Entities.Contains(item);
    }

    public IContact[] GetAll() => Entities.ToArray();
}
