using Immediate.Handlers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnedRepro;

public record NoHandlerEvent; // no handler
public record OneHandlerEvent; // one handler
public record TwoHandlerEvent; // two handlers

[Handler]
public static partial class OneHandler
{
    private static ValueTask HandleAsync(OneHandlerEvent ev, ILogger<OneHandler.Handler> logger, CancellationToken _)
    {
        logger.LogInformation("OneHandler called");
        return ValueTask.CompletedTask;
    }
}

[Handler]
public static partial class FirstTwoHandler
{
    private static ValueTask HandleAsync(TwoHandlerEvent ev, ILogger<FirstTwoHandler.Handler> logger, CancellationToken _)
    {
        logger.LogInformation("FirstTwoHandler called");
        return ValueTask.CompletedTask;
    }
}

[Handler]
public static partial class SecondTwoHandler
{
    private static ValueTask HandleAsync(TwoHandlerEvent ev, ILogger<SecondTwoHandler.Handler> logger, CancellationToken _)
    {
        logger.LogInformation("SecondTwoHandler called");
        return ValueTask.CompletedTask;
    }
}
