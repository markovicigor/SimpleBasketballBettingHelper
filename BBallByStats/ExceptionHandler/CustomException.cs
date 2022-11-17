namespace BBallByStats.ExceptionHandler
{
    public class CustomException : Exception 
    {
        public CustomException() : base()
        {

        }
        public CustomException(string? message, Exception exception) : base(message, exception)
        {

        }
        
    }
}
