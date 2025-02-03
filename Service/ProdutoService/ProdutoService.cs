using desafioBoiSaude.DataContext;
using desafioBoiSaude.Models;
using Microsoft.EntityFrameworkCore;

namespace desafioBoiSaude.Service.ProdutoService
{
    public class ProdutoService : ProdutoInterface
    {
        private readonly ApplicationDbContext context;

        public ProdutoService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<ServiceResponse<Produto>> AdicionarProduto(Produto novoProduto)
        {
            ServiceResponse<Produto> serviceResponse = new();

            try
            {
                if (novoProduto == null || string.IsNullOrWhiteSpace(novoProduto.Nome))
                {
                    serviceResponse.Mensagem = "Produto inválido.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                bool jaCadastrado = await context.Produtos.AnyAsync(prod => prod.Nome == novoProduto.Nome);

                if (jaCadastrado)
                {
                    serviceResponse.Mensagem = "Produto já cadastrado.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                context.Produtos.Add(novoProduto);
                await context.SaveChangesAsync();

                serviceResponse.DadosProdutos = novoProduto;
                serviceResponse.Mensagem = "Produto adicionado com sucesso.";
            }
            catch (Exception erro)
            {
                serviceResponse.Mensagem = erro.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Produto>> AtualizarProduto(Produto produtoAtualizado)
        {
            ServiceResponse<Produto> serviceResponse = new();

            try
            {
                if (produtoAtualizado == null || produtoAtualizado.Id == 0)
                {
                    serviceResponse.Mensagem = "Produto inválido.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                var produto = await context.Produtos.FindAsync(produtoAtualizado.Id);

                if (produto == null)
                {
                    serviceResponse.Mensagem = "Produto não encontrado.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Atualiza os campos relevantes
                produto.Nome = produtoAtualizado.Nome;
                produto.Descricao = produtoAtualizado.Descricao;
                produto.Preco = produtoAtualizado.Preco;
                produto.Disponível = produtoAtualizado.Disponível;

                context.Produtos.Update(produto);
                await context.SaveChangesAsync();

                serviceResponse.DadosProdutos = produto;
                serviceResponse.Mensagem = "Produto atualizado com sucesso.";
            }
            catch (Exception erro)
            {
                serviceResponse.Mensagem = erro.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Produto>>> DeletarProduto(int id)
        {
            ServiceResponse<List<Produto>> serviceResponse = new();

            try
            {
                var produto = await context.Produtos.FindAsync(id);

                if (produto == null)
                {
                    serviceResponse.Mensagem = "Produto não encontrado.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();

                serviceResponse.DadosProdutos = await context.Produtos.ToListAsync();
                serviceResponse.Mensagem = "Produto deletado com sucesso.";
            }
            catch (Exception erro)
            {
                serviceResponse.Mensagem = erro.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Produto>>> PegarProdutos()
        {
            ServiceResponse<List<Produto>> serviceResponse = new();

            try
            {
                serviceResponse.DadosProdutos = await context.Produtos.ToListAsync();
            }
            catch (Exception erro)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = $"Erro ao buscar produtos: {erro.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Produto>> PegarProdutoId(int id)
        {
            ServiceResponse<Produto> serviceResponse = new();

            try
            {
                var produto = await context.Produtos.FindAsync(id);

                if (produto == null)
                {
                    serviceResponse.Mensagem = "Produto não encontrado.";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.DadosProdutos = produto;
                }
            }
            catch (Exception erro)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = erro.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Produto>> PegarProdutoNome(string nome)
        {
            ServiceResponse<Produto> serviceResponse = new();

            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    serviceResponse.Mensagem = "Nome do produto inválido.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                nome = char.ToUpper(nome[0]) + nome.Substring(1).ToLower();

                var produto = await context.Produtos.FirstOrDefaultAsync(prod => prod.Nome == nome);

                if (produto == null)
                {
                    serviceResponse.Mensagem = "Produto não encontrado.";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.DadosProdutos = produto;
                }
            }
            catch (Exception erro)
            {
                serviceResponse.Mensagem = erro.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Produto>>> DeletarProdutoNome(string nome)
        {
            ServiceResponse<List<Produto>> serviceResponse = new();

            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    serviceResponse.Mensagem = "Nome do produto inválido.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                nome = char.ToUpper(nome[0]) + nome.Substring(1).ToLower();

                var produto = await context.Produtos.FirstOrDefaultAsync(prod => prod.Nome == nome);

                if (produto == null)
                {
                    serviceResponse.Mensagem = "Produto não encontrado.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();

                serviceResponse.DadosProdutos = await context.Produtos.ToListAsync();
                serviceResponse.Mensagem = "Produto deletado com sucesso.";
            }
            catch (Exception erro)
            {
                serviceResponse.Mensagem = erro.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
