using API_DB.Config;
using System.ComponentModel.DataAnnotations;

public class ConditionalMessageAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string message)
        {
            return new ValidationResult("Invalid value type.");
        }
        else
        {

            // Get the instance of the model being validated
            var model = validationContext.ObjectInstance as FailedCreateItemResult;

            if (model != null)
            {
                var errorMessages = AppConfig.ErrorMessages;
                // Check the error field and validate the message accordingly
                Log.Information(errorMessages.Name);
                switch (model.ErrorField)
                {
                    case "name":
                        if (message != errorMessages.Name)
                        {
                            return new ValidationResult(errorMessages.Default);
                        }
                        break;

                    case "section":
                        if (message != errorMessages.Section)
                        {
                            return new ValidationResult(errorMessages.Default);
                        }
                        break;
                    case "description":
                        if (message != errorMessages.Description)
                        {
                            return new ValidationResult(errorMessages.Default);
                        }
                        break;

                    default:
                        return new ValidationResult("Invalid error field.");
                }
            }
        }

        // If the message matches the expected value, return success
        return ValidationResult.Success!;
    }
}