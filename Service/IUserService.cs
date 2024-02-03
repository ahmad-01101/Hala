namespace Hala.Service
{
    public interface IUserService
    {
        bool IsAuthenticated();
        string GetUserId();
    }
}