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
}