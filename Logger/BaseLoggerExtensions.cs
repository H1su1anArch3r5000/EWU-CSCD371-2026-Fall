using System;

namespace Logger;

public static class BaseLoggerExtensions
{
    private static void NullVerifier(BaseLogger logger)
    {
        if (logger is null)
            ArgumentNullException.ThrowIfNull(logger);

    }
    public static void Error(this BaseLogger logger, string message, params object[] args)
    {
        NullVerifier(logger);
        logger!.Log(LogLevel.Error, string.Format(message, args));
    }
    public static void Warning(this BaseLogger logging, string notes, params object[] args)
    {
        NullVerifier(logging);
        logging!.Log(LogLevel.Warning, string.Format(notes, args));
    }
    public static void Information(this BaseLogger logger, string message, params object[] args)
    {
        NullVerifier(logger);
        logger!.Log(LogLevel.Information, string.Format(message, args));
    }
    public static void Debug(this BaseLogger logger, string message, params object[] args)
    {
        NullVerifier(logger);
        logger!.Log(LogLevel.Debug, string.Format(message, args));
    }
}
