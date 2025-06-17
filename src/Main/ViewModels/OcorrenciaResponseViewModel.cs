namespace Fase4Cap7WebserviceASPNET.Main.ViewModels
{
    public class OcorrenciaResponseViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataOcorrencia { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int TipoImpactoId { get; set; }
        public string TipoImpactoNome { get; set; } = string.Empty;
    }
}
