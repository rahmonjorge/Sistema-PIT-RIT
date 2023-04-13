using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class VerificationTokenController : Controller<VerificationToken>
{
    public VerificationTokenController(IMongoCollection<VerificationToken> collection) : base(collection) {}
}