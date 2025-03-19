namespace API_DB.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int Category_Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
