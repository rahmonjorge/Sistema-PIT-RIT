using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class UserController
{
    private readonly string collectionName = "users";
    private IMongoCollection<User> _collection;

    public UserController()
    {
        /*
        /// WARNING: Check if IP address is added at cloud.mongodb.com.

        // setting up mongodb connection
        
        string? connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        if (connectionString == null) Console.WriteLine("'MONGODB_URI' environmental variable not found.");
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("pit-rit");
        */

        _collection = DatabaseModuleMain._database.GetCollection<User>(collectionName);

        // testing connection
        var filter = Builders<User>.Filter.Eq("name", "rahpom");
        var document = _collection.Find(filter).First();
        if (document != null) Console.WriteLine("Connection test was successful.");
        else Console.WriteLine("Connection test failed.");
        
    }

    public void Create(User document)
    {
        _collection.InsertOne(document);
    }

    public User Read(string key, string value)
    {
        FilterDefinition<User> filter;
        if (key == "_id")
            filter = Builders<User>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<User>.Filter.Eq(key, value);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public bool Update(string key, string value, User document)
    {
        FilterDefinition<User> filter;
        if (key == "_id")
            filter = Builders<User>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<User>.Filter.Eq(key, value);
        return _collection.ReplaceOne(filter, document).IsAcknowledged;
    }

    public bool Delete(string key, string value)
    {
        FilterDefinition<User> filter;
        if (key == "_id")
            filter = Builders<User>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<User>.Filter.Eq(key, value);
        return _collection.DeleteOne(filter).IsAcknowledged;
    }
}
