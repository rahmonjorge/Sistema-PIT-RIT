namespace DatabaseModule.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Google.Protobuf.WellKnownTypes;

public class RIT 
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("user_id")]
    public string UserId { get; set; }

    [BsonElement("ano")]
    public int Ano { get;set; }
    
    [BsonElement("planilha")]
    public Sheet Planilha { get; set; }

    public RIT(string userId, int ano, Sheet planilha)
    {
        this.UserId = userId;
        this.Ano = ano;
        this.Planilha = planilha;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not RIT) return false;

        RIT rit = (RIT) obj;

        return (rit.UserId == this.UserId && rit.Ano == this.Ano);
    }

    public override string ToString()
    {
        return $"_id: {Id}, user_id: {UserId}, ano: {Ano}";
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}