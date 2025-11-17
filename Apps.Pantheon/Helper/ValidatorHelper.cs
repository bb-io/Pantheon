using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Pantheon.Helper;

public static class ValidatorHelper
{
    public static void ValidateDateRange(DateTime? start, DateTime? end)
    {
        if (start is null || end is null) return;
        if (start < end)
            throw new PluginMisconfigurationException("Invalid date range. Date after should be later than date before");
    }
}
