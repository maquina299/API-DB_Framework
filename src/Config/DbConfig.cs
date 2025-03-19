public class DatabaseConfig
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string Name { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

    public string ConnectionString =>
        $"Server={Host};Port={Port};Database={Name};User ID={User};Password={Password};";
}
