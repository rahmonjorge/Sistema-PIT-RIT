namespace DatabaseModule.Entities;

using Database.Auth;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Account
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("user_id")]
    public string UserId { get; set; }

    [BsonElement("provider_type")]
    public ProtoProviderType ProviderType { get; set; }

    [BsonElement("provider")]
    public string Provider { get; set; }

    [BsonElement("provider_id")]
    public string ProviderAccountId { get; set; }

    [BsonElement("refresh_token")]
    public string? RefreshToken { get; set; }

    [BsonElement("access_token")]
    public string? AccessToken { get; set; }

    [BsonElement("expires_in")]
    public Int32 ExpiresIn { get; set; }

    [BsonElement("token_type")]
    public string? TokenType { get; set; }

    [BsonElement("scope")]
    public string? Scope { get; set; }

    [BsonElement("token_id")]
    public string? TokenId { get; set; }
    
    [BsonElement("session_state")]
    public string? SessionState { get; set; }

    public Account(string userId, string provider, string providerAccountId, ProtoProviderType type)
    {
        this.UserId = userId;
        this.ProviderType = type;
        this.Provider = provider;
        this.ProviderAccountId = providerAccountId;
    }

    public override string ToString()
    {
        return $"user_id: {this.UserId}, provider_type: {this.ProviderType}, provider_id: {this.ProviderAccountId}";
    }
}