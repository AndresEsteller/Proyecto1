using Microsoft.AspNetCore.Mvc;
using Proyecto1_AndrésEsteller.Models;
using System.Data;

namespace Proyecto1_AndrésEsteller.Controllers
{
    public class ReservaController : Controller
    {
        private readonly DataBaseSQL _database;

        public ReservaController(DataBaseSQL dbConnection)
        {
            _database = dbConnection;
        }

        public ActionResult Formulario(int rutaId)
        {
            string queryRuta = $"SELECT * FROM Rutas WHERE Id = {rutaId}";
            DataTable rutaTable = _database.EjecutarConsulta(queryRuta);

            if (rutaTable.Rows.Count == 0)
                return NotFound();

            var ruta = new Ruta
            {
                Id = rutaId,
                Origen = rutaTable.Rows[0]["Origen"].ToString(),
                Destino = rutaTable.Rows[0]["Destino"].ToString(),
                Horario = Convert.ToDateTime(rutaTable.Rows[0]["Horario"]),
                Precio = Convert.ToDecimal(rutaTable.Rows[0]["Precio"])
            };

            var reserva = new Reserva
            {
                RutaId = rutaId,
                Ruta = ruta
            };

            return View(reserva);
        }

        [HttpPost]
        public ActionResult CrearReserva(int rutaId, string asiento)
        {
            int usuarioId = GetLoggedUserId();

            string queryInsertReserva = $"INSERT INTO Reservas (UsuarioId, RutaId, Asiento, EstadoPago) " +
                                        $"VALUES ({usuarioId}, {rutaId}, '{asiento}', 'Pendiente')";
            int resultado = _database.EjecutarComando(queryInsertReserva);

            if (resultado <= 0)
                return View("Error"); 

            return RedirectToAction("Formulario", new { rutaId = rutaId });
        }

        [HttpPost]
        public ActionResult ConfirmarPago(int reservaId)
        {
            string queryReserva = $"SELECT * FROM Reservas WHERE Id = {reservaId}";
            DataTable reservaTable = _database.EjecutarConsulta(queryReserva);

            if (reservaTable.Rows.Count == 0)
                return NotFound();

            string queryUpdatePago = $"UPDATE Reservas SET EstadoPago = 'Confirmado' WHERE Id = {reservaId}";
            int resultado = _database.EjecutarComando(queryUpdatePago);

            if (resultado <= 0)
                return View("Error"); 

            return RedirectToAction("Confirmacion", new { id = reservaId });
        }

        private int GetLoggedUserId()
        {
            return 1;
        }
    }
}
