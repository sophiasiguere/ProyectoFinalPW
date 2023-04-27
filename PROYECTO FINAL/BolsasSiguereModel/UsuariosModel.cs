using Microsoft.AspNetCore.Mvc.Rendering;
using ProgramacionWeb_1057719_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BolsasSiguereModel
{
    public class UsuariosModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Usuario1 { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public int IdRol { get; set; }

        public string NombreRol { get; set; }

        public List<SelectListItem>? Roles { get; set; }

    }
}
