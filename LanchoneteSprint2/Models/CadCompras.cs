namespace LanchoneteSprint2.Models
{
    public class CadCompras
    {
        public Guid CadComprasId { get; set; }
        public string Nota { get; set; }
        public DateTime? DataHora { get; set; }
        public IEnumerable<Produto>? Produto { get; set; }
        public string Movimentacao { get; set; }
    }
}
