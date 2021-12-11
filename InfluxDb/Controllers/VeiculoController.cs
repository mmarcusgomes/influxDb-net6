using InfluxDb.Service;
using InfluxDb.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InfluxDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : Controller
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        [HttpPost("gerar-veiculos")]
        public async Task<IActionResult> NovosVeiculos()
        {
            try
            {
                await _veiculoService.GerarVeiculos();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("obter-veiculos")]
        public async Task<IActionResult> ObterVeiculos()
        {
            try
            {
                var veiculos = await _veiculoService.ObterVeiculos();
                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("atualizar-veiculos")]
        public async Task<IActionResult> AtualizarVeiculos([FromBody] VeiculoAtualizarViewModel viewModel)
        {
            try
            {
                await _veiculoService.AtualizarVeiculo(viewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
