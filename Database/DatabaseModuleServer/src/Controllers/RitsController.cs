using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class RitsController : Controller<RIT>
{
    public RitsController(IMongoCollection<RIT> collection) : base(collection) {}

    public List<int> ReadDistinct(string key)
    {
        var filter = Builders<RIT>.Filter.Empty;
        var anos = _collection.Distinct(r => r.Ano, filter);

        return anos.ToList();
    }
}