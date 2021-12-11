namespace InfluxDb.ViewModel
{
    public class VeiculoAtualizarViewModel
    {
        public string Modelo { get; set; }
        public string AnoCarro { get; set; }
        public string Marca { get; set; }
        public string Chassi { get; set; }
        public decimal Preco { get; set; }
        public DateTime Time { get; set; }
        public DateTime DataMedicao { get; set; }
    }
}
