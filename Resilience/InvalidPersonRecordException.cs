public class InvalidPersonRecordException : Exception
{
    public InvalidPersonRecordException() { }
    public InvalidPersonRecordException(string message) : base(message) { }
    public InvalidPersonRecordException(string message, Exception inner) : base(message, inner) { }
}
