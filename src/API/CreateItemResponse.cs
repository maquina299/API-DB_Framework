using Newtonsoft.Json;

namespace API_DB.Models
{
    public class CreateItemResponse
    {
        public string Method { get; set; }
        public string Status { get; set; }
        public CreateItemResult Result { get; set; }
    }

    public class CreateItemResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public string Params { get; set; }
    }
}
