using InfluxDb.Model;
using InfluxDb.Repositories;
using InfluxDb.ViewModel;
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
                    Points.Add(BindToPointData(veiculo.Modelo, veiculo.Marca, veiculo.Chassi, veiculo.Preco, veiculo.AnoCarro,DateTime.UtcNow));
                }
            }

            await _influxDBRepository.Write(Points);
        }

        public async Task<List<Veiculo>> ObterVeiculos()
        {
            var query = "|> range(start: 0)";
            //+ "|> filter(fn: (r) => r[\"_measurement\"] == \"veiculos\")";

            return await _influxDBRepository.QueryList<Veiculo>(query);
        }

        public async Task AtualizarVeiculo(VeiculoAtualizarViewModel veiculo)
        {
            //Case sensitive as tags no influx
            var query = $"Chassi=\"{veiculo.Chassi}\"";           

            await _influxDBRepository.Delete(veiculo.Time.AddMilliseconds(-1), veiculo.Time, query);

            //DataMedição deve ser enviado como um UTC, com Z no final ou convertido, mas isso adiciona horas 
            var point = BindToPointData(veiculo.Modelo, veiculo.Marca, veiculo.Chassi, veiculo.Preco, veiculo.AnoCarro, veiculo.DataMedicao);
            await _influxDBRepository.Update(point);
        }

        private PointData BindToPointData(string modelo, string marca, string chassi, decimal preco, string anoCarro, DateTime time )
        {  
            return PointData.Measurement("veiculos")
            //Tag são indexadas 
            .Tag("Modelo", modelo)
            .Tag("Marca", marca)
            .Tag("Chassi", chassi)
            //Field não é indexado, qualquer consulta q execute sobre ele sera em todos os registros de acordo com o range selecionado 
            .Field("Preco", preco.ToString())
            .Field("AnoCarro", anoCarro)
            .Timestamp(time, InfluxDB.Client.Api.Domain.WritePrecision.Ns);
        }
    }
}
