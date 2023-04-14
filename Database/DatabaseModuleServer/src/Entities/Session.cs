namespace DatabaseModule.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Session
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    [BsonElement("token")]
    public string Token { get; set; }
    [BsonElement("user_id")]
    public ObjectId UserId { get; set; }
    [BsonElement("expires")]
    public DateTime Expires { get; set; }

    public Session(string token, string userId, DateTime expires)
    {
        this.Token = token;
        this.UserId = new ObjectId(userId);
        this.Expires = expires;
    }

    public override string ToString()
    {
        return $"token: {this.Token}, user_id: {this.UserId}, expires: {this.Expires}";
    }
}