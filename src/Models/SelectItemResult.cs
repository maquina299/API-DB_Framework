using Newtonsoft.Json;

public class SelectItemResult
{
    [JsonProperty("last_id")]
    public int Id { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string Params { get; set; }
}
