using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

// TODO: CHANGE TO SINGLETON!!
// TODO: ADD INTERFACE WITH DYNAMIC TYPES FOR CRUD
public class PitsController
{
    private readonly string collectionName = "pits";
    private IMongoCollection<PIT> _collection;

    public PitsController()
    {
        _collection = DatabaseModuleMain._database.GetCollection<PIT>(collectionName);  
    }

    public PIT Read(string key, string value)
    {
        FilterDefinition<PIT> filter;
        if (key == "_id")
            filter = Builders<PIT>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<PIT>.Filter.Eq(key, value);
        
        return _collection.Find(filter).FirstOrDefault();
    }
    
    public async Task<PIT> Read(string key1, string value1, string key2, int value2)
    {
        FilterDefinition<PIT> filter = 
            Builders<PIT>.Filter.Eq(key1, value1) &
            Builders<PIT>.Filter.Eq(key2, value2);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public List<int> ReadDistinct(string key)
    {
        var filter = Builders<PIT>.Filter.Empty;
        var anos = _collection.Distinct(r => r.Ano, filter);

        return anos.ToList();
    }

    public bool Update(string key, string value, PIT document)
    {
        FilterDefinition<PIT> filter;
        if (key == "_id")
            filter = Builders<PIT>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<PIT>.Filter.Eq(key, value);
        return _collection.ReplaceOne(filter, document).IsAcknowledged;
    }
    
}