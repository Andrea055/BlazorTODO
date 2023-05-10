using Microsoft.AspNetCore.Components.Authorization;

public class Auth
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public Auth(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    async public Task<string> GetLoggedUserName()
    {
        var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = auth.User;
        return user.Identity.Name ?? throw new Exception("User not found");
    }
}