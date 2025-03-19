using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Section { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
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
        [Required]
        [ConditionalMessage]
        //[RegularExpression(@"^Название товара не заполнено!$", ErrorMessage = "Message value is invalid.")] // ⚡️ Exact value check
        public string Message { get; set; }
    }
}
