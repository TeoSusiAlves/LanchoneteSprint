namespace LanchoneteSprint2.Models
{
    public class CompraItem
    {
        public Guid CompraItemId { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public Guid CadComprasId { get; set; }
        public CadCompras? CadCompras { get; set; }
        public int Qtd { get; set; }
        public decimal Preco { get; set; }
    }
}
