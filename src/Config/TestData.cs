namespace API_DB.Config
{
    public static class TestData
    {
        public static readonly CreateItemRequest CreateNewItem = new CreateItemRequest
        {
            Name = "Jeans",
            Section = "Test",
            Description = "Levis!",
            Color = "blue",
            Size = 42,
            Price = 30,
            Params = "slim"
        };
        public static readonly CreateItemRequest CreateNewItemNoName = new CreateItemRequest
        {
            Section = "Test",
            Description = "Levis!",
            Color = "blue",
            Size = 42,
            Price = 30,
            Params = "slim"
        };
        public static readonly Item expectedDbRequestItem = new Item
            {
                ID = 34,
                Title = "Polo Shirt",
                Price = 65.00m,
                Size = 50,
                Category_Id = 5,
                Description = "Casual polo T-shirt",
                Image = "/images/shirt3.jpg"
            };
}
}
