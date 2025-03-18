using RestSharp;
using System.ComponentModel.DataAnnotations;
using API_DB.Models;

namespace API_DB.API
{
    public class ApiClient  // ⚡️ Renamed from APIClient → ApiClient
    {
        private readonly RestClient _client;

        public ApiClient()
        {
            _client = new RestClient();
        }

        public async Task<RestResponse> PostAsync(string url, object body)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            try
            {
                var response = await _client.ExecuteAsync(request);

                Log.Information("API Response: {StatusCode} {Content}", response.StatusCode, response.Content);

                // ⚡️ Handle error responses gracefully
                if (!response.IsSuccessful)
                {
                    Log.Error("API Request Failed! Status: {StatusCode}, Error: {ErrorMessage}",
                               response.StatusCode, response.ErrorMessage ?? "No Error Message");

                    throw new Exception($"API Request Failed: {response.StatusCode} - {response.ErrorMessage}");
                }

                return response;
            }
            catch (Exception ex)
            {
                Log.Error("Exception in ApiClient: {ExceptionMessage}", ex.Message);
                throw;  // ⚡️ Re-throw the exception so the test fails with useful info
            }
        }


    }
}

