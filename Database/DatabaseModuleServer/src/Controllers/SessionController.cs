using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

// TODO: CHANGE TO SINGLETON!!
// TODO: ADD INTERFACE WITH DYNAMIC TYPES FOR CRUD
public class SessionController
{
    private readonly string collectionName = "sessions";
    private IMongoCollection<Session> _collection;

    public SessionController()
    {
        _collection = DatabaseModuleMain._database.GetCollection<Session>(collectionName);
        
        // testing connection
        var filter = Builders<Session>.Filter.Eq("token", "tjsaoiaokenn");
        var document = _collection.Find(filter).First();
        if (document != null) Console.WriteLine("Connection test was successful.");
        else Console.WriteLine("Connection test failed.");
        
    }

    public void Create(Session document)
    {
        _collection.InsertOne(document);
    }

    public Session Read(string key, string value)
    {
        FilterDefinition<Session> filter;
        if (key == "_id")
            filter = Builders<Session>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<Session>.Filter.Eq(key, value);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public bool Update(string key, string value, Session document)
    {
        FilterDefinition<Session> filter;
        if (key == "_id")
            filter = Builders<Session>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<Session>.Filter.Eq(key, value);
        return _collection.ReplaceOne(filter, document).IsAcknowledged;
    }

    public bool Delete(string key, string value)
    {
        FilterDefinition<Session> filter;
        if (key == "_id")
            filter = Builders<Session>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<Session>.Filter.Eq(key, value);
        return _collection.DeleteOne(filter).IsAcknowledged;
    }
}