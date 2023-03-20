namespace LegoService.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? SetCollectionName { get; set; }
    public string? ThemeCollectionName { get; set; }
}