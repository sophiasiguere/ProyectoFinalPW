using System;
using System.Collections.Generic;

namespace Proyecto2_Web_SophiaSiguere.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
