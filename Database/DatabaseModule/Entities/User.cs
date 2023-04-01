namespace DatabaseModule.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Google.Protobuf.WellKnownTypes;

// TODO: Para não usar campos anuláveis, adicionar um construtor sem parâmetros para inicializá-los
public class User 
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    [BsonElement("email")]
    public string? Email { get; set; }
    [BsonElement("registration_complete")]
    public bool CadastroCompleto { get; set; }
    [BsonElement("email_verified")]
    public DateTime EmailVerified { get; set; }
    [BsonElement("name")]
    public string? Nome { get; set; }
    [BsonElement("departamento")]
    public string? Departamento { get; set; }
    [BsonElement("carga_horaria")]
    public string? CargaHoraria { get; set; }
    [BsonElement("regime")]
    public string? Regime { get; set; }
    [BsonElement("siape")]
    public string? Siape { get; set; }
    [BsonElement("vinculo")]
    public string? Vinculo { get; set; }
    [BsonElement("image")]
    public string? Image { get; set; }
}