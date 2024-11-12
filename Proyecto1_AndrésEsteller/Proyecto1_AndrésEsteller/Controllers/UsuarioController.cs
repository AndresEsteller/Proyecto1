using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto1_AndrésEsteller.Models;
using System.Data;

namespace Proyecto1_AndrésEsteller.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DataBaseSQL _database;

        public UsuarioController(DataBaseSQL database)
        {
            _database = database;
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.PasswordHash = HashPassword(usuario.PasswordHash);

                string query = "INSERT INTO Usuarios (Nombre, Email, PasswordHash) VALUES (@Nombre, @Email, @PasswordHash)";
                var command = new SqlCommand(query);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);

                int resultado = _database.EjecutarComandoConParametros(command);

                if (resultado > 0)
                    return RedirectToAction("Login");
                else
                    ModelState.AddModelError("", "Error al registrar el usuario.");
            }

            return View(usuario);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            string query = "SELECT * FROM Usuarios WHERE Email = @Email AND PasswordHash = @PasswordHash";
            var command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@PasswordHash", HashPassword(password));

            DataTable usuarioTable = _database.EjecutarConsultaConParametros(command);

            if (usuarioTable.Rows.Count > 0)
            {
                HttpContext.Session.SetString("UserEmail", email);

                return RedirectToAction("Perfil");
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View();
        }

        [Authorize]
        public ActionResult Perfil()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            string query = "SELECT * FROM Usuarios WHERE Email = @Email";
            var command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Email", email);

            DataTable usuarioTable = _database.EjecutarConsultaConParametros(command);

            if (usuarioTable.Rows.Count == 0)
                return NotFound();

            var usuario = new Usuario
            {
                Id = Convert.ToInt32(usuarioTable.Rows[0]["Id"]),
                Nombre = usuarioTable.Rows[0]["Nombre"].ToString(),
                Email = usuarioTable.Rows[0]["Email"].ToString()
            };

            return View(usuario);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
