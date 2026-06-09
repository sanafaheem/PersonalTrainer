using PersonalTrainer.API.Models;

namespace PersonalTrainer.API.Services;

public interface ITokenService
{
    string GenerateAccessToken(AppUser user);
    RefreshToken GenerateRefreshToken();
}
