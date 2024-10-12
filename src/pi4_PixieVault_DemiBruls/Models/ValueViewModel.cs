namespace pi4_PixieVault_DemiBruls.Models
{
    public class ValueViewModel
    {
        public string Email { get; set; }
        public Dictionary<string, decimal> itemValues { get; set; } = [];
        public decimal? TotalValue { get; set; }
    }
}
