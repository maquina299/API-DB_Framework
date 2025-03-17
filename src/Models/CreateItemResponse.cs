using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API_DB.src.Models
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
    public class FailedCreateItemResult
    {
        [JsonProperty("method")]
        [Required]
        public string Method { get; set; }
        [JsonProperty("status")]
        [Required]
        public string Status { get; set; }
        [Required]
        [JsonProperty("field_error")]
        public string ErrorField { get; set; }
        [Required]
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("message")]
        [Required(ErrorMessage = "Название товара не з1аполнено!")]
        public string Message { get; set; }
    }
}
