namespace ZeissProducts.Business.Models
{
    public record ZeissError(string ErrorMessage)
    {
        public static ZeissError None => new(string.Empty);
    }
}
