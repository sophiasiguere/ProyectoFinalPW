using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ClasificacionPeliculasModel;

public class UserRegister
{
    [Required]
    [DisplayName("Nombre de usuario")]
    public string Username { get; set; } = null!;

    [Required]
    [DisplayName("Contraseña")]
    public string Password { get; set; } = null!;

    [Required]
    [DisplayName("Confirmar Contraseña")]
    public string ConfirmPassword { get; set; } = null!;
}
