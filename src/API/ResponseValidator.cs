using System.ComponentModel.DataAnnotations;
using RestSharp;

namespace API_DB.src.API
{
    public static class ResponseValidator
    {
        // ⚡️ CHANGE: Generic method to validate response against a model
        public static void ValidateResponse<T>(RestResponse response) where T : class
        {
            Log.Information("Starting validating the response against {T}");

            if (response.Content == null)
            {
                throw new Exception("Response content is null.");
            }

            // Deserialize the response into the specified model
            T responseModel;
            try
            {
                responseModel = JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize response: {ex.Message}");
            }

            if (responseModel == null)
            {
                throw new Exception("Deserialized response model is null.");
            }

            // Validate the model using data annotations
            var validationContext = new ValidationContext(responseModel);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(responseModel, validationContext, validationResults, true);

            if (!isValid)
            {
                var errors = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new Exception($"Response validation failed: {errors}");
            }
        }
    }
}