namespace API_DB.API
{
    public static class ApiEndpoints
    {
        public const string BaseUrl = "http://shop.bugred.ru/api/items";
        public const string CreateItem = $"{BaseUrl}/create/";
        public const string SelectItem = $"{BaseUrl}/select/";
        public const string DeleteItem = $"{BaseUrl}/delete/";
    }
}
