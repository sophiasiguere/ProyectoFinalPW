using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProgramacionWeb_1057719_Project.Models;
using System.Diagnostics;
using System.Security.Claims;
using BolsasSiguereModel.Auth;

namespace ProgramacionWeb_1057719_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cotizar(string correo, string telefono, string nombre, int altura, int horizonte, int extension, string pantone, string material, string calibre, string logo)
        {

            Models.Cotizacion cotizacion = new Models.Cotizacion
            {
                CorreoCliente = correo,
                TelefonoCliente = telefono,
                FechaCotizacion = DateTime.Now
            };
            var result =  Functions.APIServiceCotizaciones.PostCotizacion(cotizacion);

            IEnumerable<Models.Cotizacion> cotizaciones = Functions.APIServiceCotizaciones.GetCotizaciones().Result;
            int NoPedido = cotizaciones.Max(c => c.Id) + 1;
            Models.Cotizacion cotizacions = cotizaciones.FirstOrDefault(c => c.Id == NoPedido);

            Models.Pedido pedidos = new Models.Pedido
            {
                TamañoHorizonte = horizonte,
                TamañoExtension = extension,
                TamañoAltura = altura,
                Calibre = calibre,
                ColorPantone = pantone,
                Material = material,
                NoPedido = NoPedido,
                Logo = logo
            };
            var pedido = Functions.APIServicePedidos.PostPedido(pedidos);

            return RedirectToAction(nameof(Index));
        }

        [Route("/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserAuth credentials)
        {
            var user = await ApiService.Login(credentials);
            if (user == null)
            {
                return View(credentials);
            }
            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("TokenAPI", user.Token)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            ApiService.token = user.Token;
            return RedirectToAction("Index", "Cotizacion");
        }

        [Route("/register")]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuarionuevo)
        {
            usuarionuevo.IdRol = 2;
            var user = await ApiService.Register(usuarionuevo);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("username", user.Username),
                    new Claim("TokenAPI", user.Token)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                ApiService.token = user.Token;
                return RedirectToAction("Index", "Cotizacion");
            }
            return View(usuarionuevo);
        }

        [Route("/logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}