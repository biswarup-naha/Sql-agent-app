namespace SqlAgent.Base
{
    public class ApiExceptionBase : Exception
    {
        //
        // Summary:
        //     Error number
        public int Number { get; set; }

        //
        // Summary:
        //     Suggestion to resolve the issue
        public string Suggestion { get; set; }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   message:
        //     Error message
        public ApiExceptionBase(string message)
            : base(message)
        {
        }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   number:
        //     Error number/code
        //
        //   message:
        //     Error message
        public ApiExceptionBase(int number, string message)
            : base(message)
        {
            Number = number;
        }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   message:
        //     Error message
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public ApiExceptionBase(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   number:
        //     Error number/code
        //
        //   message:
        //     Error message
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public ApiExceptionBase(int number, string message, Exception innerException)
            : base(message, innerException)
        {
            Number = number;
        }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   message:
        //     Error message
        //
        //   suggestion:
        //     Suggestion to resolve this error
        public ApiExceptionBase(string message, string suggestion)
            : this(message, null, suggestion)
        {
        }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   number:
        //     Error number/code
        //
        //   message:
        //     Error message
        //
        //   suggestion:
        //     Suggestion to resolve this error
        public ApiExceptionBase(int number, string message, string suggestion)
            : this(number, message, null, suggestion)
        {
        }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   message:
        //     Error message
        //
        //   suggestion:
        //     Suggestion to resolve this error
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public ApiExceptionBase(string message, Exception innerException, string suggestion)
            : base(message, innerException)
        {
            Suggestion = suggestion;
        }

        //
        // Summary:
        //     Create new instance of exception object
        //
        // Parameters:
        //   number:
        //     Error number/code
        //
        //   message:
        //     Error message
        //
        //   suggestion:
        //     Suggestion to resolve this error
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public ApiExceptionBase(int number, string message, Exception innerException, string suggestion)
            : base(message, innerException)
        {
            Number = number;
            Suggestion = suggestion;
        }
    }
}
