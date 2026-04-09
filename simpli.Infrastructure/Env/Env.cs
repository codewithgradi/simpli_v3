public class JwtSettings
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? SigningKey { get; set; }
}

public class ConnnectionStrings
{
    public string? DbDB { get; set; }
    public string? ProdDB { get; set; }
}
public class OtherSettings
{
    public string? Env { get; set; }
    public string? FrontEndurl { get; set; }
}