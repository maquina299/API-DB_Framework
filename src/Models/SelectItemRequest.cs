namespace API_DB.Models

{
    public class SelectItemRequest
    {
        [JsonProperty("sql_query")]
        public string Sql_Query { get; set; }
    }
}