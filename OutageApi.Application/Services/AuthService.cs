using OutageApi.Application.Interfaces;

public class AuthService : IUserService
{
    private readonly IUserRepository _repository;
    public AuthService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> AuthenticateUserAsync(string username, string password)
    {
        //Authentication
        return await _repository.AuthenticateUserAsync(username, password);
    }
}