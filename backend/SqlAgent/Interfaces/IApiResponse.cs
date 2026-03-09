using SqlAgent.Utils;

namespace SqlAgent.Interfaces
{
    public interface IApiResponse
    {
        //
        // Summary:
        //     Errors (if response have error)
        List<ApiError> Errors { get; set; }

        //
        // Summary:
        //     Add exception to response
        //
        // Parameters:
        //   ex:
        void AddError(Exception ex);

        //
        // Summary:
        //     Add error message to response
        //
        // Parameters:
        //   errorMessage:
        void AddError(string errorMessage);
    }
}
