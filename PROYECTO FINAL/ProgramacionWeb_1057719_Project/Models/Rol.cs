using System;
using System.Collections.Generic;

namespace ProgramacionWeb_1057719_Project.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
