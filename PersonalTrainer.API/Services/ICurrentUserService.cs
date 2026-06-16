namespace PersonalTrainer.API.Services;

public interface ICurrentUserService
{
    bool IsLoggedIn { get; }
    string? UserId { get; }
}
