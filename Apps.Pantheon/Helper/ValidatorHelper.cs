using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Pantheon.Helper;

public static class ValidatorHelper
{
    public static void ValidateDateRange(DateTime? before, DateTime? after)
    {
        if (before is null || after is null) return;
        if (before > after)
            throw new PluginMisconfigurationException("Invalid date range. Date after should be later than date before");
    }
}
