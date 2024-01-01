namespace Api.Services.ThirdParty;
using HashidsNet;

public class HashIdsService
{
    private readonly Hashids _hashids = new Hashids("be3p bop b00p g0 h0k13$");

    public HashIdsService() { }
    public string Encode(int id)
    {
        return _hashids.Encode(id);
    }
    public int Decode(string input)
    {
        return _hashids.DecodeSingle(input);
    }
}
