namespace DatabaseModule.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Google.Protobuf.WellKnownTypes;

public class Sheet 
{
    [BsonElement("ch_grad")]
    public float CHGrad { get; set; }

    [BsonElement("ch_pos")]
    public float CHPos { get; set; }

    [BsonElement("ch_ensino")]
    public float CHEnsino { get; set; }

    [BsonElement("ch_pesquisa")]
    public float CHPesquisa { get; set; }

    [BsonElement("ch_extensao")]
    public float CHExtensao { get; set; }

    [BsonElement("ch_adm")]
    public float CHAdm { get; set; }

    [BsonElement("ensino")]
    public bool[] Ensino { get; set; }

    [BsonElement("pesquisa")]
    public bool[] Pesquisa { get; set; }

    [BsonElement("extensao")]
    public bool[] Extensao { get; set; }

    [BsonElement("adm")]
    public bool[] Adm { get; set; }

    public Sheet(bool[] ensino, bool[] pesquisa, bool[] extensao, bool[] adm)
    {
        this.Ensino = ensino;
        this.Pesquisa = pesquisa;
        this.Extensao = extensao;
        this.Adm = adm;
    }
}