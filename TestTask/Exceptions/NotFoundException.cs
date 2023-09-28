namespace TestTask.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity)
            : base($"Not found: {entity}") { }

    }
}
