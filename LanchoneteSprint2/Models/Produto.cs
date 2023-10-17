namespace LanchoneteSprint2.Models
{
    public class Produto
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Estoque { get; set; }
        public decimal PrecoUn { get; set; }
        public Guid CadProdutoId { get; set; }
        public CadProduto? CadProduto { get; set; }
        public Guid FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }
}
