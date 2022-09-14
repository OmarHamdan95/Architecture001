using DotNetCore.Domain;

namespace Architecture.Domain;

public class User : EntityBase
{
    public User
    (
        Name name,
        string email,
        Auth auth
    )
    {
        Name = name;
        Email = email;
        Auth = auth;
        Activate();
    }

    public User(long id) => Id = id;

    public Name Name { get; private set; }

    public string Email { get; private set; }

    public Status Status { get; private set; }
    public long? StatusId { get; private set; }

    public Auth Auth { get; private set; }
    public long? AuthId { get; private set; }

    public void Activate()
    {
        Status = Status;
    }

    public void Inactivate()
    {
        Status = Status;
    }

    public void Update(string firstName, string lastName, string email)
    {
        Name = new Name(firstName, lastName);
        Email = email;
    }
}
