namespace OutageApi.Application.Interfaces;

public interface IUserService
{
    Task<bool> AuthenticateUserAsync(string username, string password);
}