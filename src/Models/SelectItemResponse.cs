using API_DB.Models;
using Newtonsoft.Json;

namespace API_DB.Models
{
    public class SelectItemResponse
    {
        public string Status { get; set; }
        [JsonProperty("result")]
        public List<SelectItemResult> Result { get; set; }
    }
}
