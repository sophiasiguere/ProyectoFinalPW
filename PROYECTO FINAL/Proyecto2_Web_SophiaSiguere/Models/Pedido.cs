using System;
using System.Collections.Generic;

namespace Proyecto2_Web_SophiaSiguere.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int NoPedido { get; set; }

    public int TamañoAltura { get; set; }

    public int TamañoHorizonte { get; set; }

    public int TamañoExtension { get; set; }

    public string ColorPantone { get; set; } = null!;

    public string Material { get; set; } = null!;

    public string Calibre { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public virtual Cotizacion? NoPedidoNavigation { get; set; } = null!;
}
