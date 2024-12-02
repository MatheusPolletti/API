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
            ServiceResponse<Produto> serviceResponse = new ServiceResponse<Produto>();

            try
            {
                List<Produto> produtosAtuais = context.Produtos.ToList();
                bool jaCadastrado = produtosAtuais.Any(prod => prod.Nome == novoProduto.Nome);

                if (jaCadastrado)
                {
                    serviceResponse.DadosProdutos = null;
                    serviceResponse.Mensagem = "Produto já cadastrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                if (novoProduto == null)
                {
                    serviceResponse.DadosProdutos = null;
                    serviceResponse.Mensagem = "Produto não colocado corretamente.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                context.Produtos.Add(novoProduto);
                await context.SaveChangesAsync();

                serviceResponse.Mensagem = "Produto adicionado.";
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
            ServiceResponse<Produto> serviceResponse = new ServiceResponse<Produto>();

            try
            {
                if (produtoAtualizado == null)
                {
                    serviceResponse.DadosProdutos = null;
                    serviceResponse.Mensagem = "Produto não colocado corretamente.";
                    serviceResponse.Sucesso = false;
                }

                Produto? produto = context.Produtos.AsNoTracking().FirstOrDefault(pro => pro.Id == produtoAtualizado.Id);

                context.Produtos.Update(produtoAtualizado);
                await context.SaveChangesAsync();

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
            ServiceResponse<List<Produto>> serviceResponse = new ServiceResponse<List<Produto>>();

            try
            {
                Produto? produto = context.Produtos.FirstOrDefault(pro => pro.Id == id);

                if (produto == null)
                {
                    serviceResponse.DadosProdutos = null;
                    serviceResponse.Mensagem = "Produto não cadastrado.";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();

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
            ServiceResponse<List<Produto>> serviceResponse = new ServiceResponse<List<Produto>>();

            try
            {
                serviceResponse.DadosProdutos = context.Produtos.ToList();
            }
            catch (Exception erro)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = $"Deu errado {erro}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Produto>> PegarProdutoId(int id)
        {
            ServiceResponse<Produto> serviceResponse = new ServiceResponse<Produto>();

            try
            {
                Produto? produtoAtual = context.Produtos.FirstOrDefault(prod => prod.Id == id);

                if (produtoAtual == null)
                {
                    serviceResponse.DadosProdutos = null;
                    serviceResponse.Mensagem = "Esse produto não está cadastrado";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.DadosProdutos = produtoAtual;
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
            ServiceResponse<Produto> serviceResponse = new ServiceResponse<Produto>();

            try
            {
                nome = nome.Trim();
                nome = char.ToUpper(nome[0]) + nome.Substring(1).ToLower();

                Produto? produtoAtual = await context.Produtos.FirstOrDefaultAsync(prod => prod.Nome == nome);

                if (produtoAtual == null)
                {
                    serviceResponse.Mensagem = "Esse produto não está cadastrado ou o nome não foi digitado corretamente.";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.DadosProdutos = produtoAtual;
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
            ServiceResponse<List<Produto>> serviceResponse = new ServiceResponse<List<Produto>>();

            try
            {
                nome = nome.Trim();
                nome = char.ToUpper(nome[0]) + nome.Substring(1).ToLower();

                Produto? produtoAtual = await context.Produtos.FirstOrDefaultAsync(prod => prod.Nome == nome);

                if (produtoAtual == null)
                {
                    serviceResponse.Mensagem = "Esse produto não está cadastrado ou o nome não foi digitado corretamente.";
                    serviceResponse.Sucesso = false;
                }

                context.Produtos.Remove(produtoAtual);
                await context.SaveChangesAsync();

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
