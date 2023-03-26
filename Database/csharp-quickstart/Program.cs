using MongoDB.Driver;
using MongoDB.Bson;

var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
if (connectionString == null)
{
    Console.WriteLine("'MONGODB_URI' environmental variable not found.");
    Environment.Exit(0);
}

var client = new MongoClient(connectionString);

var collection = client.GetDatabase("pit-rit").GetCollection<BsonDocument>("users");

// TODO: USAR TIPOS DE OBJETO AO INVÉS DE <BSONDOCUMENT>

// READ 
var filter = Builders<BsonDocument>.Filter.Eq("_user_id", "0000000001");
var document = collection.Find(filter).First();
Console.WriteLine(document.GetElement("name"));

// UPDATE
var update = Builders<BsonDocument>.Update.Set("name", "rahpom");
var result = collection.UpdateOne(filter, update);

// CREATE
var user = new Dictionary<String, Object>();


user.Add("name","gianzinho");
user.Add("idade","79");


var newdocument = new BsonDocument(user);
collection.InsertOne(newdocument);

// READ
filter = Builders<BsonDocument>.Filter.Eq("name", "johnrah");
document = collection.Find(filter).First();
Console.WriteLine(document);

// DELETE
filter = Builders<BsonDocument>.Filter.Eq("name", "johnrah");
var deleteResult = collection.DeleteOne(filter);
Console.WriteLine(deleteResult.IsAcknowledged);

// REPLACE
// Ó BÓT AQI