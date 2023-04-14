namespace DatabaseModule.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Google.Protobuf.WellKnownTypes;

public class PIT 
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("user_id")]
    public string UserId { get; set; }

    [BsonElement("ano")]
    public int Ano { get;set; }
    
    [BsonElement("planilha")]
    public Sheet Planilha { get; set; }

    public PIT(string userId, int ano, Sheet planilha)
    {
        this.UserId = userId;
        this.Ano = ano;
        this.Planilha = planilha;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not PIT) return false;

        PIT pit = (PIT) obj;

        return (pit.UserId == this.UserId && pit.Ano == this.Ano);
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