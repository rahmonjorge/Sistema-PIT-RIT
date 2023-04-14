using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseModule.Controllers;

public class AccountController : Controller<Account>
{
    public AccountController(IMongoCollection<Account> collection) : base(collection) {}
}