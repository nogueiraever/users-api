using System.Text.Json.Serialization;

namespace Users.Application.Common
{
    public class Result
    {
        [JsonIgnore]
        public bool IsSuccess { get; }
        [JsonIgnore]
        public bool IsFailure => !IsSuccess;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Error { get; } = null;

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("A successful result cannot have an error.");

            if (!isSuccess && string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("A failed result must have an error.");

            IsSuccess = isSuccess;

            Error = string.IsNullOrWhiteSpace(error) ? null : error;
        }
        public static Result<T> Success<T>(T value) => new(value, true, string.Empty);
        public static Result<T> Failure<T>(string error) => new(default, false, error);

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error) => new(false, error);
    }

    public class Result<T> : Result
    {
        internal Result(T? value, bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = value;
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Value { get; }


        public static implicit operator Result<T>(T value) => Success(value);
    }
}
