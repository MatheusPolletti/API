using System.ComponentModel.DataAnnotations;

public class Produto
{
    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
    public decimal PrecoCusto { get; set; }
    public decimal PrecoVenda { get; set; }
    public int Quantidade { get; set; }
}
