namespace desafioBoiSaude.Models
{
    public class ServiceResponse<Dados>
    {
        public Dados? DadosProdutos { get; set; }
        public bool Sucesso { get; set; } = true;
        public string Mensagem { get; set; } = "Deu certo";
    }
}
