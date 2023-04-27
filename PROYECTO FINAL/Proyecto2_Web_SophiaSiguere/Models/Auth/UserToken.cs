namespace Proyecto2_Web_SophiaSiguere.Models.Auth;

public class UserToken
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
}
