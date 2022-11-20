using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Carrinho Compra Itens")]
    public class CarrinhoCompraItem
    {
        [Key]
        public int CarrinhoCompraItemId { get; set; }

        public Lanche Lanche { get; set; }

        public int Quantidade { get; set; }
        [StringLength(200)]
        public string CarrinhoDeCompraId { get; set; }
    }
}
