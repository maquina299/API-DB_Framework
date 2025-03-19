using System.ComponentModel.DataAnnotations;
using RestSharp;


namespace API_DB.API
{
    public static class ResponseValidator
    {
        // ⚡️ CHANGE: Generic method to validate response against a model
        public static void ValidateResponse<T>(RestResponse response) where T : class
        {
            // ⚡️ ADD: Log the start of validation
            Log.Information("Starting validating the response against {ModelType}", typeof(T).Name);

            if (response.Content == null)
            {
                Log.Error("Response content is null."); // ⚡️ ADD: Log error
                throw new Exception("Response content is null.");
            }

            // Deserialize the response into the specified model
            T responseModel;

            try
            {
                responseModel = JsonConvert.DeserializeObject<T>(response.Content)
                ?? throw new Exception("Deserialization returned null. Response content might be invalid.");
            }
            catch (JsonException ex)
            {
                Log.Error(ex, "Failed to deserialize response: {ErrorMessage}", ex.Message); // ⚡️ ADD: Log deserialization error
                throw new Exception($"Failed to deserialize response: {ex.Message}");
            }

            if (responseModel == null)
            {
                Log.Error("Deserialized response model is null."); // ⚡️ ADD: Log error
                throw new Exception("Deserialized response model is null.");
            }

            // Validate the model using data annotations
            var validationContext = new ValidationContext(responseModel);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(responseModel, validationContext, validationResults, true);

            if (!isValid)
            {
                var errors = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                Log.Error("Response validation failed: {ValidationErrors}", errors); // ⚡️ ADD: Log validation errors
                throw new Exception($"Response validation failed: {errors}");
            }

            // ⚡️ ADD: Log successful validation
            Log.Information("Response validation succeeded for {ModelType}", typeof(T).Name);
        }
    }
}