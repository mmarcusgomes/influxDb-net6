using InfluxDB.Client.Writes;

namespace InfluxDb.Repositories
{
    public interface IInfluxDBRepository
    {
        Task Write(List<PointData> points);
        Task<List<T>> QueryList<T>(string query, string bucket = "");
        Task Delete(DateTime start, DateTime stop, string predicate);
        Task Update(PointData point);
    }
}
