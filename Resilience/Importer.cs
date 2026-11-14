namespace Resilience;



public sealed class Importer : IDisposable
{
    private readonly TextReader _textReader;
    private bool _disposed = false;

    public Importer(string fileName)
        : this(new StreamReader(fileName))
    { }

    /// <summary>
    /// Make tests easier to implement and setup
    /// </summary>
    /// <param name="textReader"></param>
    internal Importer(TextReader textReader)
    {
        _textReader = textReader;
    }

    private static Person ParsePerson(string line)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<Person> ImportCsvRows(ImportMode importMode)
    {
        int rowCount = 0;
        string? line;
        _ = _textReader.ReadLine(); //skips first line
        while ((line = _textReader.ReadLine()) != null)
        {
            Person person;
            ++rowCount;
            try
            {
                person = ParsePerson(line);
            }
            catch (IOException ex)
            {
                if (importMode == ImportMode.FailFast)
                {
                    throw new InvalidPersonRecordException($"Error parsing row {rowCount}: [{line}]", ex);
                }
                else if (importMode == ImportMode.SkipInvalid)
                {
                    continue;
                }

                throw;
            }

            yield return person;
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _textReader.Dispose();
            _disposed = true;
        }
    }

}