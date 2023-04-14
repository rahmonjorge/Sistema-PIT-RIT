namespace DatabaseModule.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class VerificationToken
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    [BsonElement("expires")]
    public DateTime Expires { get; set; }
    [BsonElement("token")]
    public string Token { get; set; }

    public VerificationToken(string token)
    {
        this.Token = token;
    }

    public override string ToString()
    {
        return $"expires: {this.Expires}, token: {this.Token}";
    }
}