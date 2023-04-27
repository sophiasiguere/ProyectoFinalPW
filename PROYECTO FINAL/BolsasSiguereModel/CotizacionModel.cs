using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsasSiguereModel
{
    public class CotizacionModel
    {
        public int Id { get; set; }

        public string CorreoCliente { get; set; } = null!;

        public string TelefonoCliente { get; set; } = null!;

        public DateTime FechaCotizacion { get; set; }
    }
}
