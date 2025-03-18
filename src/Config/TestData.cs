using API_DB.Models;

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
    }
}
