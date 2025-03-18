public class ErrorMessages
{
    //[JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("section")]
    public string Section { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("default")]
    public string Default { get; set; }
}