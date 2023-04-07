using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class SessionController
{
    private readonly string collectionName = "sessions";
    private IMongoCollection<Session> _collection;

    public SessionController()
    {
        /*
        /// WARNING: Check if IP address is added at cloud.mongodb.com.

        // setting up mongodb connection
        
        string? connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        if (connectionString == null) Console.WriteLine("'MONGODB_URI' environmental variable not found.");
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("pit-rit");
        */

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