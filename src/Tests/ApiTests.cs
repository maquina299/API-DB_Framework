using FluentAssertions;
using API_DB.Config;

namespace API_DB.Tests
{
    public class ApiTests
    {
        private ApiClient _apiClient;

        [SetUp]
        public void Setup()
        {
            LoggerSetup.ConfigureLogging();
            _apiClient = new ApiClient();
            Log.Information("Test Setup Initialized");
        }

        [Test]
        public async Task Create_Select_Delete_Item_Test()
        {
            Log.Information("Starting Create_Select_Delete_Item_Test");

            try
            {
                var createResponse = await _apiClient.PostAsync(ApiEndpoints.CreateItem, TestData.CreateNewItem); 
                createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                Log.Information("Create Item Response: {ResponseContent}", createResponse.Content);
                var createdItem = JsonConvert.DeserializeObject<CreateItemResponse>(createResponse.Content);
                createdItem.Status.Should().Be("ok");
                int itemId = createdItem.Result.Id;
                Log.Debug("Created Item ID: {ItemId}", itemId);

                var selectRequest = new SelectItemRequest { Sql_Query = $"SELECT * FROM items WHERE last_id = {itemId};" };
                string requestJson = JsonConvert.SerializeObject(selectRequest);
                var selectResponse = await _apiClient.PostAsync(ApiEndpoints.SelectItem, requestJson);
                selectResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                Log.Information("Select Item Response: {ResponseContent}", selectResponse.Content);
                var selectedItem = JsonConvert.DeserializeObject<SelectItemResponse>(selectResponse.Content);
                selectedItem.Status.Should().Be("ok");
                selectedItem.Should().BeEquivalentTo(createdItem, options => options.ExcludingMissingMembers());


                var deleteRequest = new DeleteItemRequest { Id = itemId };
                var deleteResponse = await _apiClient.PostAsync(ApiEndpoints.DeleteItem, deleteRequest); 
                deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);


                Log.Information("Delete Item Response: {ResponseContent}", deleteResponse.Content);
                var deletedItem = JsonConvert.DeserializeObject<DeleteItemResponse>(deleteResponse.Content);
                deletedItem.Status.Should().Be("ok");
            }
            catch (Exception ex)
            {
                Log.Error("Test failed due to an exception: {ExceptionMessage}", ex.Message);
                Assert.Fail($"Test failed: {ex.Message}");
            }

            Log.Information("Test Create_Select_Delete_Item_Test completed successfully!");
        }

        [Test]
        public async Task Failed_Item_Creation_Test()
        {
            Log.Information("Starting Failed_Item_Creation_Test");

            try
            {
                var createResponse = await _apiClient.PostAsync(ApiEndpoints.CreateItem, TestData.CreateNewItemNoName);
                createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                Log.Information("Create Item Response: {ResponseContent}", createResponse.Content);
                var failedItemCreation = JsonConvert.DeserializeObject<CreateItemResponse>(createResponse.Content);

                failedItemCreation.Status.Should().Be("error");
                ResponseValidator.ValidateResponse<FailedCreateItemResult>(createResponse);

            }
            catch (Exception ex)
            {
                Log.Error("Test failed due to an exception: {ExceptionMessage}", ex.Message);
                Assert.Fail($"Test failed: {ex.Message}");
            }

            Log.Information("Test Failed_Item_Creation_Test completed successfully!");
        }

    }
}
