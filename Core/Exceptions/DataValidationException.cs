namespace ShopApp.Core.Exceptions
{
    public class DataValidationException : BaseException
    {
        public DataValidationException(string message) : base(message)
        {
        }
    }
}
