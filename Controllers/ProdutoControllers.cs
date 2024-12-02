using desafioBoiSaude.Models;
using desafioBoiSaude.Service.ProdutoService;
using Microsoft.AspNetCore.Mvc;

namespace desafioBoiSaude.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoInterface PrInterface;
        public ProdutoController(ProdutoInterface _interface)
        {
            PrInterface = _interface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Produto>>>> PegarProdutos()
        {
            return Ok(await PrInterface.PegarProdutos());
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<ServiceResponse<Produto>>> PegarProdutoId(int id)
        {
            return Ok(await PrInterface.PegarProdutoId(id));
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<ServiceResponse<Produto>>> PegarProdutoNome(string nome)
        {
            return Ok(await PrInterface.PegarProdutoNome(nome));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Produto>>> AdicionarProduto(Produto produtoNovo)
        {
            return Ok(await PrInterface.AdicionarProduto(produtoNovo));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Produto>>> AtualizarProduto(Produto produtoAtualizado)
        {
            var response = await PrInterface.AtualizarProduto(produtoAtualizado);

            if (!response.Sucesso)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("idDeletar/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Produto>>>> DeletarProduto(int id)
        {
            var response = await PrInterface.DeletarProduto(id);

            if (!response.Sucesso)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("nomeDeletar/{nome}")]
        public async Task<ActionResult<ServiceResponse<List<Produto>>>> DeletarProdutoNome(string nome)
        {
            var response = await PrInterface.DeletarProdutoNome(nome);

            if (!response.Sucesso)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
