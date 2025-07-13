namespace Users.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("A successful result cannot have an error.");

            if (!isSuccess && string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("A failed result must have an error.");

            IsSuccess = isSuccess;
            Error = error;
        }
        public static Result<T> Success<T>(T value) => new(value, true, string.Empty);

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error) => new(false, error);
    }

    public class Result<T> : Result
    {
        internal Result(T? value, bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = value;
        }
        public T? Value { get; }
        public static Result<T> Failure<T>(string error) => new(default, false, error);


        public static implicit operator Result<T>(T value) => Success(value);
    }
}
