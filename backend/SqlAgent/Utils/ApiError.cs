using Newtonsoft.Json;

namespace SqlAgent.Utils
{
    public class ApiError
    {
        //
        // Summary:
        //     Error number/code
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Number { get; set; }

        //
        // Summary:
        //     Error message
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        //
        // Summary:
        //     Error suggestion
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Suggestion { get; set; }

        //
        // Summary:
        //     Detailed exception (for developer use only)
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Exception Exception { get; set; }

        //
        // Summary:
        //     Create new instance for API error class
        public ApiError()
        {
        }

        //
        // Summary:
        //     Create new instance for API error class
        public ApiError(Exception exception)
        {
            Message = exception.Message;
            Exception = exception;
        }

        //
        // Summary:
        //     Create new instance for API error class
        public ApiError(string message)
        {
            Message = message;
        }

        //
        // Summary:
        //     Create new instance for API error class
        //
        // Parameters:
        //   message:
        //     Error message to be returned
        //
        //   suggestion:
        public ApiError(string message, string suggestion)
        {
            Message = message;
            Suggestion = suggestion;
        }

        //
        // Summary:
        //     Create new instance for API error class
        //
        // Parameters:
        //   exception:
        //     Exception object
        //
        //   message:
        //     Error message to be returned
        public ApiError(Exception exception, string message)
        {
            Exception = exception;
            Message = message;
        }
    }
}
