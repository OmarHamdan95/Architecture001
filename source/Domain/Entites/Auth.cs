using DotNetCore.Domain;

namespace Architecture.Domain;

public class Auth : EntityBase
{
    public Auth
    (
        string login,
        string password
        // Roles roles
    )
    {
        Login = login;
        Password = password;
        //Roles = roles;
        Salt = Guid.NewGuid().ToString();
    }

    public virtual string Login { get; private set; }

    public virtual string Password { get; private set; }

    public virtual string Salt { get; private set; }

    private readonly List<Role> _roles = new List<Role>();

    public virtual IReadOnlyCollection<Role> Role => _roles.AsReadOnly();

    public void UpdatePassword(string password)
    {
        Password = password;
    }
}
