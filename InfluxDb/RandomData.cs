using InfluxDb.Model;
using System.Security.Cryptography;
using System.Text;

namespace InfluxDb
{
    public class RandomData
    {

        private static Random _random = new();      

        public Veiculo ObterCarroAleatorio()
        {
            var carros = new List<Veiculo>
            {
                new Veiculo() { Modelo = "Corsa", Marca = "Chevrolet", Preco = 18000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Punto", Marca = "Fiat", Preco = 12000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Gol", Marca = "Volkswagen", Preco = 14000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Saveiro", Marca = "Volkswagen", Preco = 20000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Uno", Marca = "Fiat", Preco = 12000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Onix", Marca = "Chevrolet", Preco = 25000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Palio", Marca = "Fiat", Preco = 13000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Prisma", Marca = "Chevrolet", Preco = 15000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
                new Veiculo() { Modelo = "Logan", Marca = "Renault", Preco = 15000, AnoCarro = _random.Next(1970, 2025).ToString(), Chassi = RandomString(10).ToString()},
            };
            int index = _random.Next(carros.Count);
            return carros.GetRange(index, 1).First();
        }

        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private string RandomString(int qtd)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, qtd)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
