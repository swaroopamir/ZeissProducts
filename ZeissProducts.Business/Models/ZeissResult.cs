namespace ZeissProducts.Business.Models
{
    public class ZeissResult<TValue> where TValue : class, new()
    {
        private readonly TValue? _value;
        
        
        public bool IsSuccess { get; }
        private ZeissError Error { get; }

        private bool _isEmptyResult;

        private ZeissResult() 
        {
            IsSuccess = true;
            Error = ZeissError.None;
            _value = new TValue();
            _isEmptyResult = true;
        }

        private ZeissResult(TValue value)
        {
            IsSuccess = true;
            _value = value;
            Error = ZeissError.None;
            _isEmptyResult = false;
        }

        private ZeissResult(ZeissError error) 
        {
            IsSuccess = false;
            Error = error;
            _value = default;
        }

        public static ZeissResult<TValue> Success() => new();
        public static ZeissResult<TValue> Success(TValue value) => new(value);
        public static ZeissResult<TValue> Failure(ZeissError error) => new(error);

        public static implicit operator ZeissResult<TValue>(TValue value) => new(value);
        public static implicit operator ZeissResult<TValue>(ZeissError error) => new(error);

        public TResult? Match<TResult>(Func<TValue, TResult> success, Func<ZeissError, TResult> failure) 
        {
            if (IsSuccess)
            {
                if (_isEmptyResult)
                {
                    return success(null);
                }

                return success(_value!);
            }
            else 
            {
                return failure(Error);
            }
        }
    }
}
