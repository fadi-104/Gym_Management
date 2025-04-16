namespace ShopApp.Core.Exceptions
{
    public class ServerErrorException : BaseException
    {
        public ServerErrorException(string message) : base(message)
        {
        }
    }
}
