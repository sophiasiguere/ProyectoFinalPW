﻿using System;
using System.Collections.Generic;

namespace Proyecto2_Web_SophiaSiguere.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
