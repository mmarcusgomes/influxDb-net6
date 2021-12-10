using InfluxDb.Model;

namespace InfluxDb.Service
{
    public interface IVeiculoService
    {
        Task GerarVeiculos(int quantidadeVeiculoColecao = 10, int quantidadeColecao = 5);

        Task<List<Veiculo>> ObterVeiculos();
    }
}
