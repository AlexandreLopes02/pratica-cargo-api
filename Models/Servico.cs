namespace PraticaCargo.Api.Models
{
    public class Servico
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }

        public int MotoristaId { get; set; }
        public Motorista? Motorista { get; set; }

        public string Carga { get; set; } = string.Empty;
        public double Peso { get; set; }
        public string QuemRetira { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Tipo { get; set; } = "carga"; // carga/descarga
    }
}
