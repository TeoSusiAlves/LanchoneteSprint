namespace LanchoneteSprint2.Models
{
 
        public class RelatorioProduto
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }
            public int Quanti { get; set; }
            public Guid? CadProdutoId { get; set; }
            public CadProduto? CadProduto { get; set; }
            public decimal PrecoUn { get; set; }

        }
    
}