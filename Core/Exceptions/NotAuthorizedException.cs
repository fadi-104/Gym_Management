namespace ShopApp.Core.Exceptions
{
    public class NotAuthorizedException : BaseException
    {
        public NotAuthorizedException(string message) : base(message)
        {
        }
    }
}
