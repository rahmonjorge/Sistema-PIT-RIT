using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

// TODO: CHANGE TO SINGLETON!!
// TODO: ADD INTERFACE WITH DYNAMIC TYPES FOR CRUD
public class VerificationTokenController
{
    private readonly string collectionName = "tokens";
    private IMongoCollection<VerificationToken> _collection;

    public VerificationTokenController()
    {
        _collection = DatabaseModuleMain._database.GetCollection<VerificationToken>(collectionName);  
    }

    public void Create(VerificationToken token)
    {
        _collection.InsertOne(token);
    }
}