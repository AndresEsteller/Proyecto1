namespace Proyecto1_AndrésEsteller.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int RutaId { get; set; }
        public Ruta Ruta { get; set; }
        public string Asiento { get; set; }
        public string EstadoPago { get; set; }

        public Reserva()
        {
            Id = -1;
            UsuarioId = -1;
            Usuario = new Usuario();
            RutaId = -1;
            Ruta = new Ruta();
            Asiento = "";
            EstadoPago = "";
        }
    }
}
