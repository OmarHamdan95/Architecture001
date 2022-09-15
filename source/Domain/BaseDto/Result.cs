using Architecture.Domain.Extension;

namespace Architecture.Domain.BaseDto;

public class Result<T> : Result
{
    public T Data { get; set; }

    public Result()
    {
    }

    public Result(T data) => this.Data = data;

    public Result(params string[] errorMessages) => this.ErrorMessages =
        ((IEnumerable<string>)errorMessages).IsNotNullOrEmpty<string>()
            ? (IList<string>)((IEnumerable<string>)errorMessages).ToList<string>()
            : (IList<string>)new List<string>();

    public Result(IEnumerable<string> errorMessages) => this.ErrorMessages = errorMessages.IsNotNullOrEmpty<string>()
        ? (IList<string>)errorMessages.ToList<string>()
        : (IList<string>)new List<string>();

    public Result(T data, params string[] errorMessages)
    {
        this.Data = data;
        this.ErrorMessages = ((IEnumerable<string>)errorMessages).IsNotNullOrEmpty<string>()
            ? (IList<string>)((IEnumerable<string>)errorMessages).ToList<string>()
            : (IList<string>)new List<string>();
    }

    public Result(T data, IEnumerable<string> errorMessages)
    {
        this.Data = data;
        this.ErrorMessages = errorMessages.IsNotNullOrEmpty<string>()
            ? (IList<string>)errorMessages.ToList<string>()
            : (IList<string>)new List<string>();
    }

    public Result(string errorMessage)
    {
        if (errorMessage.IsNotNullOrEmpty())
        {
            List<string> message = new List<string>();
            message.Add(errorMessage);
            this.ErrorMessages = (IList<string>)message;
        }
        else
        {
            this.ErrorMessages = (IList<string>)new List<string>();
        }
    }
}

public class Result
{
    public IList<string> ErrorMessages { get; set; }
    public IList<string> WarningMessages { get; set; }
    public IList<string> InfoMessages { get; set; }

    public bool HasErrors => ErrorMessages.IsNotNullOrEmpty();
    public bool HasWarnings => WarningMessages.IsNotNullOrEmpty();
    public bool HasInfos => InfoMessages.IsNotNullOrEmpty();

    public Result()
    {
    }

    public Result(string errorMessage)
    {
        List<string> message = new List<string>();
        message.Add(errorMessage);
        ErrorMessages = message;
    }

    public Result(params string[] errorMessages) => this.ErrorMessages =
        ((IEnumerable<string>)errorMessages).IsNotNullOrEmpty<string>()
            ? (IList<string>)((IEnumerable<string>)errorMessages).ToList<string>()
            : (IList<string>)new List<string>();

    public Result(IEnumerable<string> errorMessages) => this.ErrorMessages = errorMessages.IsNotNullOrEmpty<string>()
        ? (IList<string>)errorMessages.ToList<string>()
        : (IList<string>)new List<string>();

    public static Result Return() => new Result();

    public static Result Error(params string[] errorMessages) => new Result(errorMessages);

    public static Result Error(IEnumerable<string> errorMessages) => new Result(errorMessages);

    public static Result<T> Return<T>(T data, params string[] errorMessages) => new Result<T>(data, errorMessages);

    public static Result<T> Return<T>(T data, IEnumerable<string> errorMessages) => new Result<T>(data, errorMessages);

    public static Result<T> Error<T>(params string[] errorMessages) => new Result<T>(errorMessages);

    public static Result<T> Error<T>(IEnumerable<string> errorMessages) => new Result<T>(errorMessages);
}
