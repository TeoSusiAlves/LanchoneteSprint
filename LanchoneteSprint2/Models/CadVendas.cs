using System.ComponentModel;

namespace LanchoneteSprint2.Models
{
    public class CadVendas
    {

        public Guid CadVendasId { get; set; }
        public string Nota { get; set; }
        public DateTime? DataHora { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public  IEnumerable<Produto>? Produto { get; set; }
        public string Movimentacao { get; set; }
    }
}