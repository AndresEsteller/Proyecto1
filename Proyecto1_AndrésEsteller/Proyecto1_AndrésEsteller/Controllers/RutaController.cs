using Microsoft.AspNetCore.Mvc;
using Proyecto1_AndrésEsteller.Models;
using System.Data;

namespace Proyecto1_AndrésEsteller.Controllers
{
    public class RutaController : Controller
    {
        private readonly DataBaseSQL _database;

        public RutaController(DataBaseSQL dbConnection)
        {
            _database = dbConnection;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Buscar(string origen, string destino, DateTime fecha)
        {
            string query = $"SELECT * FROM Rutas WHERE Origen = '{origen}' AND Destino = '{destino}' AND CAST(Horario AS DATE) = '{fecha:yyyy-MM-dd}'";
            DataTable rutasTable = _database.EjecutarConsulta(query);

            var rutas = new List<Ruta>();
            foreach (DataRow row in rutasTable.Rows)
            {
                rutas.Add(new Ruta
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Origen = row["Origen"].ToString(),
                    Destino = row["Destino"].ToString(),
                    Horario = Convert.ToDateTime(row["Horario"]),
                    Precio = Convert.ToDecimal(row["Precio"])
                });
            }

            return View("Resultados", rutas);
        }

        public ActionResult Detalles(int id)
        {
            string query = $"SELECT * FROM Rutas WHERE Id = {id}";
            DataTable rutaTable = _database.EjecutarConsulta(query);

            if (rutaTable.Rows.Count == 0)
                return NotFound();

            var ruta = new Ruta
            {
                Id = id,
                Origen = rutaTable.Rows[0]["Origen"].ToString(),
                Destino = rutaTable.Rows[0]["Destino"].ToString(),
                Horario = Convert.ToDateTime(rutaTable.Rows[0]["Horario"]),
                Precio = Convert.ToDecimal(rutaTable.Rows[0]["Precio"])
            };

            return View(ruta);
        }
    }
}
