namespace DatabaseModule.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Google.Protobuf.WellKnownTypes;

public class Sheet 
{
    [BsonElement("ch_grad")]
    public float CHGrad { get; set; } = 0;

    [BsonElement("ch_pos")]
    public float CHPos { get; set; } = 0;

    [BsonElement("ch_ensino")]
    public float CHEnsino { get; set; } = 0;

    [BsonElement("ch_pesquisa")]
    public float CHPesquisa { get; set; } = 0;

    [BsonElement("ch_extensao")]
    public float CHExtensao { get; set; } = 0;

    [BsonElement("ch_adm")]
    public float CHAdm { get; set; } = 0;

    [BsonElement("ensino")]
    public bool[] Ensino { get; set; }

    [BsonElement("pesquisa")]
    public bool[] Pesquisa { get; set; }

    [BsonElement("extensao")]
    public bool[] Extensao { get; set; }

    [BsonElement("adm")]
    public bool[] Adm { get; set; }

    public Sheet()
    {
        this.Ensino = new bool[16];
        this.Pesquisa = new bool[29];
        this.Extensao = new bool[18];
        this.Adm = new bool[11];
    }

    public Sheet(bool[] ensino, bool[] pesquisa, bool[] extensao, bool[] adm)
    {
        this.Ensino = ensino;
        this.Pesquisa = pesquisa;
        this.Extensao = extensao;
        this.Adm = adm;
    }
}