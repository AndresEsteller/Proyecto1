namespace Proyecto1_AndrésEsteller.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Historial { get; set; }

        public Usuario()
        {
            Id = -1;
            Nombre = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            Historial = string.Empty;
        }
    }
}
