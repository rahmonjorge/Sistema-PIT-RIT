using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

/// Classe responsável por controlar a base de dados de tokens de verificação.
public class VerificationTokenController
{
    private IMongoCollection<VerificationToken> _collection;

    public VerificationTokenController()
    {
        /// WARNING: Check if IP address is added at cloud.mongodb.com.

        // setting up mongodb connection
        string? connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        if (connectionString == null) Console.WriteLine("'MONGODB_URI' environmental variable not found.");
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("pit-rit");
        _collection = database.GetCollection<VerificationToken>("verification_tokens");
        
    }
}