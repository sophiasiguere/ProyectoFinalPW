using System;
using System.Collections.Generic;

namespace Proyecto2_Web_SophiaSiguere.Models;

public partial class Cotizacion
{
    public int Id { get; set; }

    public string CorreoCliente { get; set; } = null!;

    public string TelefonoCliente { get; set; } = null!;

    public DateTime FechaCotizacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; } = new List<Pedido>();
}
