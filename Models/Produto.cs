using System.ComponentModel.DataAnnotations;

public class Produto
{
    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Disponível { get; set; }
}
