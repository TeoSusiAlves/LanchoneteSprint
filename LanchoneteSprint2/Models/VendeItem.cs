namespace LanchoneteSprint2.Models
{
    public class VendeItem
    {
        public Guid VendeItemId { get; set; } 
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public Guid CadVendasId { get; set; }
        public CadVendas? CadVendas { get; set; }
        public int Qtd { get; set; }
        public decimal Preco { get; set; }
    }
}
