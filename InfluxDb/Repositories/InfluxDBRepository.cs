using InfluxDb.Configuration;
using InfluxDb.Model;
using InfluxDB.Client;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Options;

namespace InfluxDb.Repositories
{
    public class InfluxDBRepository : IInfluxDBRepository
    {
        private readonly InfluxDbOptions _appSettings;
        private readonly InfluxDBClient _client;

        public InfluxDBRepository(IOptions<InfluxDbOptions> appSettings)
        {
            _appSettings = appSettings.Value;
            var options = new InfluxDBClientOptions.Builder()
                .Url(_appSettings.Host)
                .AuthenticateToken(_appSettings.Token.ToCharArray())
                .Org(_appSettings.Organization)
                .Bucket(_appSettings.Bucket)
                .Build();

            _client = InfluxDBClientFactory.Create(options);
        }

        public async Task Write(List<PointData> points)
        {
            var writeApiAsync = _client.GetWriteApiAsync();
            await writeApiAsync.WritePointsAsync(points);
        }

        public async Task<List<T>> QueryList<T>(string query, string bucket = "")
        {
            if (string.IsNullOrWhiteSpace(bucket))
            {
                bucket = _appSettings.Bucket;
            }
            query = $"from(bucket:\"{bucket}\") " + query;
            query += "|> pivot(rowKey:[\"_time\"], columnKey: [\"_field\"], valueColumn: \"_value\")";

            return await _client.GetQueryApi().QueryAsync<T>(query);
        }

        //public async Task<T> Query<T>(Func<QueryApi, Task<T>> action)
        //{
        //    using (_client)
        //    {
        //        var query = _client.GetQueryApi();
        //        return await action(query);
        //    }
        //}

        public async Task Delete(DateTime start, DateTime stop, string predicate)
        {
            await _client.GetDeleteApi().Delete(start, stop, predicate, _appSettings.Bucket, _appSettings.Organization);
        }
    }
}
