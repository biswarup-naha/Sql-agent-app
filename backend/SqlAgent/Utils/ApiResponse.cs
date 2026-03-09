using Newtonsoft.Json;
using SqlAgent.Base;

namespace SqlAgent.Utils
{
    public class ApiResponse<T> : ApiResponseBase
    {
        private readonly DateTime? time;

        private bool? status;

        private string message;

        //
        // Summary:
        //     Total execution time (read-only)
        public double ExecutionTime => time.HasValue ? DateTime.Now.Subtract(time.Value).TotalSeconds : 0.0;

        //
        // Summary:
        //     Result for this API call
        public T Result { get; set; }

        //
        // Summary:
        //     API processing status
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Status
        {
            get
            {
                bool? flag = status;
                int result;
                if (!flag.HasValue)
                {
                    List<ApiError> errors = base.Errors;
                    result = ((errors == null || errors.Count <= 0) ? 1 : 0);
                }
                else
                {
                    result = ((flag == true) ? 1 : 0);
                }

                return (byte)result != 0;
            }
            set
            {
                status = value;
            }
        }

        //
        // Summary:
        //     Message to display after processing
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public string Message
        {
            get
            {
                object obj = message;
                if (obj == null)
                {
                    List<ApiError> errors = base.Errors;
                    obj = ((errors != null && errors.Count > 0) ? base.Errors[0].Message : null);
                }

                return (string)obj;
            }
            set
            {
                message = value;
            }
        }

        //
        // Summary:
        //     Create new instance for ApiResponse
        public ApiResponse()
        {
            time = DateTime.Now;
        }
    }
}
