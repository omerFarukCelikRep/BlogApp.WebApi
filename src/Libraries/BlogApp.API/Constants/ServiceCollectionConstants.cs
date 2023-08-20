namespace BlogApp.API.Constants;

public struct ServiceCollectionConstants
{
    internal struct RateLimitConstans
    {
        internal const string PolicyName = "Fixed";
        internal const int PermitLimit = 10;
        internal const int TimeWindow = 10;
        internal const int QueueLimit = 2;
    }
}