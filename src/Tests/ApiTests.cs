using FluentAssertions;
using API_DB.Models;
using API_DB.API;
using Newtonsoft.Json;
using API_DB.Config;
using API_DB.Utils;
using System.Net;


namespace API_DB.Tests
{
    public class ApiTests
    {
        private ApiClient _apiClient;

        [SetUp]
        public void Setup()
        {
            LoggerSetup.ConfigureLogging(); // ✅ Ensure logging is configured
            _apiClient = new ApiClient();
            Log.Information("Test Setup Initialized");
        }

        [Test]
        public async Task Create_Select_Delete_Item_Test()
        {
            Log.Information("Starting Create_Select_Delete_Item_Test");

            try
            {
                var createResponse = await _apiClient.PostAsync(ApiEndpoints.CreateItem, TestData.CreateNewItem);  // ⚡️ Updated APIEndpoints → ApiEndpoint
                createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                Log.Information("Create Item Response: {ResponseContent}", createResponse.Content);
                var createdItem = JsonConvert.DeserializeObject<CreateItemResponse>(createResponse.Content);
                createdItem.Status.Should().Be("ok");
                int itemId = createdItem.Result.Id;
                Log.Debug("Created Item ID: {ItemId}", itemId);

                var selectRequest = new SelectItemRequest { Sql_Query = $"SELECT * FROM items WHERE last_id = {itemId};" };
                string requestJson = JsonConvert.SerializeObject(selectRequest);
                var selectResponse = await _apiClient.PostAsync(ApiEndpoints.SelectItem, requestJson);  // ⚡️ Updated APIEndpoints → ApiEndpoint
                selectResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                Log.Information("Select Item Response: {ResponseContent}", selectResponse.Content);
                var selectedItem = JsonConvert.DeserializeObject<SelectItemResponse>(selectResponse.Content);
                selectedItem.Status.Should().Be("ok");
                selectedItem.Result.Should().ContainSingle(item => item.Id == itemId);

                var deleteRequest = new DeleteItemRequest { Id = itemId };
                var deleteResponse = await _apiClient.PostAsync(ApiEndpoints.DeleteItem, deleteRequest);  // ⚡️ Updated APIEndpoints → ApiEndpoint
                deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                Log.Information("Delete Item Response: {ResponseContent}", deleteResponse.Content);
                var deletedItem = JsonConvert.DeserializeObject<DeleteItemResponse>(deleteResponse.Content);
                deletedItem.Status.Should().Be("ok");
            }
            catch (Exception ex) // ⚡️ Handle API errors gracefully
            {
                Log.Error("Test failed due to an exception: {ExceptionMessage}", ex.Message);
                Assert.Fail($"Test failed: {ex.Message}");
            }

            Log.Information("Test Create_Select_Delete_Item_Test completed successfully!");
        }
    }
}
