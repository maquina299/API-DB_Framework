using System.ComponentModel.DataAnnotations;

namespace API_DB.Models
{
    public class CreateItemRequest
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Категория не найдена!")]
        public string Section { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public string Params { get; set; }
    }
}
