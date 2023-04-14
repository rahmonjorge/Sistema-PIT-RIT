using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class SessionController : Controller<Session>
{
    public SessionController(IMongoCollection<Session> collection) : base(collection) {}
}