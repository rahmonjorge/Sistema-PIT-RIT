using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

// TODO: CHANGE TO SINGLETON!!
// TODO: ADD INTERFACE WITH DYNAMIC TYPES FOR CRUD
public class AccountController
{
    private readonly string collectionName = "accounts";
    private IMongoCollection<Account> _collection;

    public AccountController()
    {
        _collection = DatabaseModuleMain._database.GetCollection<Account>(collectionName);
    }

    public void Create(Account document)
    {
        _collection.InsertOne(document);
    }

    public Account Read(string key, string value)
    {
        FilterDefinition<Account> filter;
        if (key == "_id")
            filter = Builders<Account>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<Account>.Filter.Eq(key, value);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public async Task<Account> Read(string key1, string value1, string key2, string value2)
    {
        FilterDefinition<Account> filter = 
            Builders<Account>.Filter.Eq(key1, value1) &
            Builders<Account>.Filter.Eq(key2, value2);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public DeleteResult Delete(string key1, string value1, string key2, string value2)
    {
        FilterDefinition<Account> filter = 
            Builders<Account>.Filter.Eq(key1, value1) &
            Builders<Account>.Filter.Eq(key2, value2);
        return _collection.DeleteOne(filter);
    }
}