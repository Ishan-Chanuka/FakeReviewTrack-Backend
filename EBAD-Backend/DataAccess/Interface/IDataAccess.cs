using MongoDB.Driver;

namespace EBAD_Backend.DataAccess.Interface
{
    public interface IDataAccess
    {
        IMongoCollection<T> ConnectToMongo<T>(in string collection);
    }
}
