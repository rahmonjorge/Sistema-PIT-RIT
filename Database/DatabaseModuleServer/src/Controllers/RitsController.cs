using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

// TODO: CHANGE TO SINGLETON!!
// TODO: ADD INTERFACE WITH DYNAMIC TYPES FOR CRUD
public class RitsController
{
    private readonly string collectionName = "rits";
    private IMongoCollection<RIT> _collection;

    public RitsController()
    {
        _collection = DatabaseModuleMain._database.GetCollection<RIT>(collectionName);  
    }

    public RIT Read(string key, string value)
    {
        FilterDefinition<RIT> filter;
        if (key == "_id")
            filter = Builders<RIT>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<RIT>.Filter.Eq(key, value);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public List<int> ReadDistinct(string key)
    {
        var filter = Builders<RIT>.Filter.Empty;
        var anos = _collection.Distinct(r => r.Ano, filter);

        return anos.ToList();
    }
}