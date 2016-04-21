namespace DAL.Interfaces
{
    public interface IUserNameResolver
    {
        string CurrentUserName { get; }
    }
}