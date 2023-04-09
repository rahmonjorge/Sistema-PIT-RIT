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
}