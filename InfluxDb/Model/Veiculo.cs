using InfluxDB.Client.Core;

namespace InfluxDb.Model
{
    [Measurement("veiculos")]
    public class Veiculo
    {
        //public string Time { get; init; }
        [Column("modelo", IsTag = true)]
        public string Modelo { get; init; }

        public string AnoCarro { get; init; }
        [Column("marca", IsTag = true)]
        public string Marca { get; init; }

        [Column("chassi", IsTag = true)]
        public string Chassi { get; init; }
        [Column("preco", IsTag = true)]
        public decimal Preco { get; init; }

        [Column(IsTimestamp = true)]
        public DateTime Time { get; set; }
    }
}
