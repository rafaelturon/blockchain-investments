namespace Blockchain.Investments.Core.Infrastructure
{
    public static class Constants
    {
        public const string ApplicationName = "Blockchain Investments";
        public const string DatabaseName = "expense-point";
        public const string EventStoreCollectionName = "EventStore";
        public const string AuthorizationPolicy = "BitcoinUser";
        public const string ClaimType = "BitId";
        public const string ClaimValue = "PublicAddress";
        public const int SpanTimeInSeconds = 600;
    }
}