using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class PitsController : Controller<PIT>
{
    public PitsController(IMongoCollection<PIT> collection) : base(collection) {}

    public List<int> ReadDistinct(string key)
    {
        var filter = Builders<PIT>.Filter.Empty;
        var anos = _collection.Distinct(r => r.Ano, filter);

        return anos.ToList();
    }
    
}