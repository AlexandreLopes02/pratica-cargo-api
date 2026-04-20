namespace PraticaCargo.Api.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Por simplicidade (estagiário), vamos usar senha "hash" depois.
        // Hoje: vamos guardar como texto para aprender fluxo.
        public string Password { get; set; } = string.Empty;
    }
}
