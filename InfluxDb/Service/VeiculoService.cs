using InfluxDb.Model;
using InfluxDb.Repositories;
using InfluxDB.Client.Writes;

namespace InfluxDb.Service
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IInfluxDBRepository _influxDBRepository;
        private readonly List<PointData> Points = new();
        private readonly RandomData Rand = new();

        public VeiculoService(IInfluxDBRepository influxDBRepository)
        {
            _influxDBRepository = influxDBRepository;
        }

        public async Task GerarVeiculos(int quantidadeVeiculoColecao = 10, int quantidadeColecao = 5)
        {
            for (int i = 0; i < quantidadeColecao; i++)
            {
                for (int j = 0; j < quantidadeVeiculoColecao; j++)
                {
                    var veiculo = Rand.ObterCarroAleatorio();
                    Points.Add(PointData.Measurement("veiculos" + i)
                   //Tag são indexadas 
                   .Tag("Modelo", veiculo.Modelo)
                   .Tag("Chassi", veiculo.Chassi)
                   .Tag("Preco", veiculo.Preco.ToString())
                   .Tag("Marca", veiculo.Marca)
                   //Field não é indexado, qualquer consulta q execute sobre ele sera em todos os registros de acordo com o range selecionado 
                   .Field("AnoCarro", veiculo.AnoCarro)
                   .Field("value", veiculo.AnoCarro)
                   .Timestamp(DateTime.UtcNow, InfluxDB.Client.Api.Domain.WritePrecision.Ns));
                }
            }

            await _influxDBRepository.Write(Points);
        }

        public async Task<List<Veiculo>> ObterVeiculos()
        {
            var query = "|> range(start: 0)" +
                        "|> filter(fn: (r) => r[\"_measurement\"] == \"veiculos0\")";

            return await _influxDBRepository.QueryList<Veiculo>(query);
        }

        public async Task AtualizarVeiculo()
        {

        }
    }
}
