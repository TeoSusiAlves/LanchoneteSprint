namespace LanchoneteSprint2.Models
{
    public class Fornecedor
    {
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public IEnumerable<CadCompras>? CadCompras { get; set; }
    }
}
