using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsasSiguereModel
{
    public class PedidoModel
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

        public string Cliente { get; set;} = null!;
    }
}
