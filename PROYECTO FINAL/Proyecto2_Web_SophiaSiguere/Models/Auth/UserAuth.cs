using System.ComponentModel;

namespace Proyecto2_Web_SophiaSiguere.Models.Auth;

public class UserAuth
{
    [DisplayName("Usuario")]
    public string User { get; set; } = null!;

    [DisplayName("Contraseña")]
    public string Password { get; set; } = null!;
}
