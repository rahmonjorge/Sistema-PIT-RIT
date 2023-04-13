using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class UserController : Controller<User>
{
    public UserController(IMongoCollection<User> collection) : base(collection) {}
}
