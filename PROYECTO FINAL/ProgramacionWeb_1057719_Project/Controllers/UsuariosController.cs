using BolsasSiguereModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using ProgramacionWeb_1057719_Project.Models;
using ProgramacionWeb_1057719_Project.Utils;

namespace ProgramacionWeb_1057719_Project.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DbBolsassiguereContext _dbbolsassiguere = new DbBolsassiguereContext();

        public async Task<IActionResult> Index() 
        {
            IEnumerable<Models.Usuario> usuarios = Functions.APIServicesUsuarios.GetUsuarios().Result;
            IEnumerable<Models.RolModel> roles = Functions.APIServiceRols.GetRols().Result;
             var result = from u in usuarios
                          join rol in roles on u.IdRol equals rol.Id
                          select new BolsasSiguereModel.UsuariosModel
                         {
                             Id = u.Id,
                             Nombre = u.Nombre,
                             Usuario1 = u.Usuario1,
                             Correo = u.Correo,
                             Telefono = u.Telefono,
                             Contrasena = u.Contrasena,
                             IdRol = u.IdRol,
                             NombreRol = rol.Nombre
                         };
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var u = Functions.APIServicesUsuarios.GetUsuario(id).Result;

            IEnumerable<Models.RolModel> roles = Functions.APIServiceRols.GetRols().Result;
            var rol = roles.Where(r => r.Id == u.IdRol).FirstOrDefault();
            
            var result = new BolsasSiguereModel.UsuariosModel
                         {
                             Id = u.Id,
                             Nombre = u.Nombre,
                             Usuario1 = u.Usuario1,
                             Correo = u.Correo,
                             Telefono = u.Telefono,
                             Contrasena = u.Contrasena,
                             IdRol = u.IdRol,
                             NombreRol = rol.Nombre
                         };
            
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

       public ActionResult Create()
        {
            IEnumerable<Models.RolModel> roles = Functions.APIServiceRols.GetRols().Result;
            List<SelectListItem> Variable = roles.Select(info => new SelectListItem
            {
                Value = info.Id.ToString(),
                Text = info.Nombre
            }).ToList();
            ViewBag.Roles = Variable;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string nombre, string usuario1, string correo, string telefono, string contrasena, int idRol)
        {
            Models.Usuario usuario = new Models.Usuario
            {
                Nombre = nombre,
                Usuario1 = usuario1,
                Correo = correo,
                Telefono = telefono,
                Contrasena = contrasena,
                IdRol = idRol
            };
            var result = Functions.APIServicesUsuarios.PostUsuario(usuario);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _dbbolsassiguere.Usuarios == null)
            {
                return NotFound();
            }
            var u = Functions.APIServicesUsuarios.GetUsuario(id).Result;

            IEnumerable<Models.RolModel> roles = Functions.APIServiceRols.GetRols().Result;
            var rol = roles.Where(r => r.Id == u.IdRol).FirstOrDefault();

            var result = new BolsasSiguereModel.UsuariosModel
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Usuario1 = u.Usuario1,
                Correo = u.Correo,
                Telefono = u.Telefono,
                Contrasena = u.Contrasena,
                IdRol = u.IdRol,
            };
            List<SelectListItem> Variable = roles.Select(info => new SelectListItem
            {
                Value = info.Id.ToString(),
                Text = info.Nombre
            }).ToList();
            ViewBag.Roles = Variable;


            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string nombre, string usuario1, string correo, string telefono, string contrasena, int idRol)
        {
            IEnumerable<Models.RolModel> roles = Functions.APIServiceRols.GetRols().Result;
            IEnumerable<Models.Usuario> usuarios = Functions.APIServicesUsuarios.GetUsuarios().Result;
            var usuariopass = usuarios.Where(r => r.Id == id).FirstOrDefault();
            string pass = "";
            if (usuariopass.Contrasena != contrasena)
            {
               contrasena = Encryption.EncryptPassword(contrasena);
            }
            Models.Usuario usuario = new Models.Usuario
            {
                Id = id,
                Nombre = nombre,
                Usuario1 = usuario1,
                Correo = correo,
                Telefono = telefono,
                Contrasena = contrasena,
                IdRol = idRol
            };
            Functions.APIServicesUsuarios.PutUsuario(usuario, id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var u = Functions.APIServicesUsuarios.GetUsuario(id).Result;

            IEnumerable<Models.RolModel> roles = Functions.APIServiceRols.GetRols().Result;
            var rol = roles.Where(r => r.Id == u.IdRol).FirstOrDefault();

            var result = new BolsasSiguereModel.UsuariosModel
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Usuario1 = u.Usuario1,
                Correo = u.Correo,
                Telefono = u.Telefono,
                Contrasena = u.Contrasena,
                IdRol = u.IdRol,
                NombreRol = rol.Nombre
            };


            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Functions.APIServicesUsuarios.DeleteUsuario(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
