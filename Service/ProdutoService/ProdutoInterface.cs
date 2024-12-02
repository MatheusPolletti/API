using desafioBoiSaude.Models;

namespace desafioBoiSaude.Service.ProdutoService
{
    public interface ProdutoInterface
    {
        Task<ServiceResponse<List<Produto>>> PegarProdutos();
        Task<ServiceResponse<Produto>> PegarProdutoId(int Id);
        Task<ServiceResponse<Produto>> PegarProdutoNome(string nome);

        Task<ServiceResponse<Produto>> AdicionarProduto(Produto novoProduto);
        Task<ServiceResponse<Produto>> AtualizarProduto(Produto produtoAtualizado);

        Task<ServiceResponse<List<Produto>>> DeletarProduto(int id);
        Task<ServiceResponse<List<Produto>>> DeletarProdutoNome(string nome);
    }
}
