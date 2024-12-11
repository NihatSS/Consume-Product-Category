namespace Api_intro.Helpers.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message) { }
    }
}
