using EBAD_Backend.DataAccess.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EBAD_Backend.DataAccess.Concrete
{
    public class DataAccess : IDataAccess
    {
        private readonly MongoSettings _settings;
        public DataAccess(IOptions<MongoSettings> options)
        {
            _settings = options.Value;
        }

        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            return database.GetCollection<T>(collection);
        }
    }
}
