namespace Proyecto1_AndrésEsteller.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime Horario { get; set; }
        public decimal Precio { get; set; }

        public Ruta()
        {
            Id = -1;
            Origen = "";
            Destino = "";
            Horario = DateTime.Now;
            Precio = 0;
        }
    }
}
