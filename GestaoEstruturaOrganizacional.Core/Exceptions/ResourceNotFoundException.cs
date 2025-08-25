namespace GestaoEstruturaOrganizacional.Core.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }
        
        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
        public ResourceNotFoundException(string resourceName, string fieldName, object fieldValue)
            : base($"{resourceName} not found with {fieldName}: '{fieldValue}'")
        {
        }
    }
}
