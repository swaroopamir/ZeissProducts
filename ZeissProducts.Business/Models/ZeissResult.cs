namespace ZeissProducts.Business.Models
{
    /// <summary>
    /// Implemented result pattern to handle errors and provide generic result.
    /// </summary>
    /// <typeparam name="TValue">Generic return type.</typeparam>
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

        //The below statement used to implicitly convert the custom object type to Zeiss Result Type for both success and failure cases.
        public static implicit operator ZeissResult<TValue>(TValue value) => new(value);
        public static implicit operator ZeissResult<TValue>(ZeissError error) => new(error);

        /// <summary>
        /// The below method is used to verify whether the response is success or failure based on that it will send the response.
        /// </summary>
        /// <typeparam name="TResult">Custom Type Object</typeparam>
        /// <param name="success">Function delegate for success</param>
        /// <param name="failure">Function delegate for failure</param>
        /// <returns>Custom Type Object</returns>
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
