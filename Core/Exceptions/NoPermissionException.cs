namespace ShopApp.Core.Exceptions
{
    public class NoPermissionException : BaseException
    {
        public NoPermissionException(string message) : base(message)
        {
        }
    }
}
