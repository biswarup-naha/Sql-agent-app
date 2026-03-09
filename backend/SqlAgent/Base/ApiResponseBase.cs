using System.ComponentModel.DataAnnotations.Schema;
using SqlAgent.Interfaces;
using SqlAgent.Utils;

namespace SqlAgent.Base
{
    public class ApiResponseBase : IApiResponse
    {
        //
        // Summary:
        //     Errors occures while processing request
        [NotMapped]
        public List<ApiError> Errors { get; set; }

        //
        // Summary:
        //     Add error to the response
        //
        // Parameters:
        //   ex:
        //     Exception
        public void AddError(Exception ex)
        {
            if (Errors == null)
            {
                List<ApiError> list = (Errors = new List<ApiError>());
            }

            if (ex is ApiExceptionBase)
            {
                ApiExceptionBase? ex2 = ex as ApiExceptionBase;
                Errors.Add(new ApiError(ex2.Message)
                {
                    Number = ex2.Number,
                    Suggestion = ex2.Suggestion
                });
            }
            else
            {
                Errors.Add(new ApiError(ex));
            }
        }

        //
        // Summary:
        //     Add error to the response
        //
        // Parameters:
        //   message:
        //     Error message
        public void AddError(string message)
        {
            if (Errors == null)
            {
                List<ApiError> list = (Errors = new List<ApiError>());
            }

            Errors.Add(new ApiError(message));
        }

        //
        // Summary:
        //     Add error to the response
        //
        // Parameters:
        //   number:
        //     Error number/code
        //
        //   message:
        //     Error message
        public void AddError(int number, string message)
        {
            if (Errors == null)
            {
                List<ApiError> list = (Errors = new List<ApiError>());
            }

            Errors.Add(new ApiError(message)
            {
                Number = number
            });
        }

        //
        // Summary:
        //     Add error to the response
        //
        // Parameters:
        //   message:
        //     Error message
        //
        //   suggestion:
        //     Error suggestion
        public void AddError(string message, string suggestion)
        {
            if (Errors == null)
            {
                List<ApiError> list = (Errors = new List<ApiError>());
            }

            Errors.Add(new ApiError(message, suggestion));
        }

        //
        // Summary:
        //     Add error to the response
        //
        // Parameters:
        //   number:
        //     Error number/code
        //
        //   message:
        //     Error message
        //
        //   suggestion:
        //     Error suggestion
        public void AddError(int number, string message, string suggestion)
        {
            if (Errors == null)
            {
                List<ApiError> list = (Errors = new List<ApiError>());
            }

            Errors.Add(new ApiError(message, suggestion)
            {
                Number = number
            });
        }
    }
}
